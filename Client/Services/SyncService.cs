using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.ClientHelper;
using DLEA_Lib;
using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Game;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class SyncService : Service
    {
        public override string Name => nameof(SyncService);

        private Dictionary<int, int> WaypointBlips  = new Dictionary<int, int>();
        private Dictionary<int, int> PlayerBlips    = new Dictionary<int, int>();

        #region Events
        public Action<string> EventOnGetPlayerList { get; }
        public Action<int> EventOnChangeWeather { get; }
        public DateTime TimerBlipChange { get; set; } = DateTime.Now;
        public bool PlayerBlipChangeState { get; set; }
        public int PlayerBlipChangeMilliseconds { get; set; } = 700;
        public override string UserFriendlyName => "Synchronisationseinstellungen";
        #endregion

        public SyncService(ClientObject ClientObject) : base( ClientObject) 
        {
            EventOnGetPlayerList = OnGetPlayerList;
            EventOnChangeWeather = OnChangeWeather;
        }

        protected override void InitializeSettings()
        {
            Settings.Add(new ServiceSetting(Name, "Positionen", "Positionen von Spielern", true));
            Settings.Add(new ServiceSetting(Name, "Wegpunkte", "Wegpunkte von Spielern", true));
            Settings.Add(new ServiceSetting(Name, "Unsichtbar", "Für andere Spieler sichtbar", false));
            base.InitializeSettings();
        }

        private int GetPlayerBlipColor(ExtendedUser Player) 
        {
            int PlayerBlipColor = 57;
            if (Player.IsSirenOn)
            {
                PlayerBlipColor = PlayerBlipChangeState ? 59 : 38;
                if (Player.IsSirenSoundOn)
                {
                    PlayerBlipChangeMilliseconds = 300;
                }
                else
                {
                    PlayerBlipChangeMilliseconds = 700;
                }
            }
            return PlayerBlipColor;
        }

        public void OnGetPlayerList(string UserListRAW) 
        {
            try
            {
                if (!string.IsNullOrEmpty(UserListRAW))
                {
                    if (Users.Deserialize(UserListRAW, false) is List<ExtendedUser> UserList)
                    {
                        DateTime Now = DateTime.Now;
                        UserList = UserList.OrderBy(o => o.Name).ToList();

                        if (ClientObject.MainMenu.RefreshUserList != null)
                        {
                            ClientObject.MainMenu.RefreshUserList.Invoke(UserList);
                        }
                        foreach (ExtendedUser CurrentUser in UserList)
                        {
                            if (CurrentUser.ServerID != ServerID || ClientObject.CurrentUser.GetSetting("DataService", "Debugmode"))
                            {
                                int WaypointBlip = -1;
                                int PlayerBlip = -1;

                                if (WaypointBlips.Any(o => o.Key == CurrentUser.ServerID) && WaypointBlips.FirstOrDefault(o => o.Key == CurrentUser.ServerID) is KeyValuePair<int, int> WpKvpL)
                                {
                                    WaypointBlip = WpKvpL.Value;
                                }

                                if (PlayerBlips.Any(o => o.Key == CurrentUser.ServerID) && PlayerBlips.FirstOrDefault(o => o.Key == CurrentUser.ServerID) is KeyValuePair<int, int> PyKvpL)
                                {
                                    PlayerBlip = PyKvpL.Value;
                                }

                                int PlayerBlipColor = GetPlayerBlipColor(CurrentUser);

                                //Wegpunkte
                                {
                                    if ((!CurrentUser.IsWaypointActive || !GetSettingValue("Wegpunkte")) && WaypointBlip != -1)
                                    {
                                        API.RemoveBlip(ref WaypointBlip);
                                        WaypointBlips.Remove(CurrentUser.ServerID);
                                        WaypointBlip = -1;
                                    }

                                    if (CurrentUser.IsWaypointActive && GetSettingValue("Wegpunkte") && WaypointBlip == -1)
                                    {
                                        WaypointBlip = CommonFunctions.AddBlipForCoord(CurrentUser.Waypoint, 364, 2, 3, $"Wegpunkt von { new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name }");
                                        WaypointBlips.Add(CurrentUser.ServerID, WaypointBlip);
                                    }
                                }

                                //Postitionen
                                {
                                    if ((!CurrentUser.Visible || !GetSettingValue("Positionen") || CurrentUser.PlayerSprite == -1) && PlayerBlip != -1)
                                    {
                                        API.RemoveBlip(ref PlayerBlip);
                                        PlayerBlips.Remove(CurrentUser.ServerID);
                                        PlayerBlip = -1;
                                    }

                                    if (CurrentUser.Visible && GetSettingValue("Positionen") && CurrentUser.PlayerSprite != -1)
                                    {
                                        if (PlayerBlip == -1)
                                        {
                                            PlayerBlip = CommonFunctions.AddBlipForCoord(CurrentUser.Position, CurrentUser.PlayerSprite, PlayerBlipColor, 2, $"{ new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name }", (float)CurrentUser.Heading);
                                            PlayerBlips.Add(CurrentUser.ServerID, PlayerBlip);
                                        }
                                        CommonFunctions.RefreshBlip(PlayerBlip, CurrentUser.Position, CurrentUser.PlayerSprite, PlayerBlipColor, 2, $"{ new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name }", (float)CurrentUser.Heading);
                                    }
                                }
                            }
                        }

                        //Entferne Nutzer, die nicht mehr auf dem Server sind oder deren Ping zu lange ist.
                        {
                            IEnumerable<KeyValuePair<int, int>> UsersRemoveList = PlayerBlips.Where(o => !UserList.Any(p => p.ServerID == o.Key));
                            foreach (KeyValuePair<int, int> User in UsersRemoveList)
                            {
                                try
                                {
                                    int PlayerBlip = PlayerBlips.Where(o => o.Key == User.Key).First().Value;
                                    API.RemoveBlip(ref PlayerBlip);
                                    PlayerBlips.Remove(CurrentUser.ServerID);
                                    PlayerBlip = -1;
                                }
                                catch { }

                                try
                                {
                                    int WaypointBlip = WaypointBlips.Where(o => o.Key == User.Key).First().Value;
                                    API.RemoveBlip(ref WaypointBlip);
                                    WaypointBlips.Remove(CurrentUser.ServerID);
                                    WaypointBlip = -1;
                                }
                                catch { }
                            }
                        }

                        Textdisplay.RefreshUserList(ClientObject, UserList); 
                    }
                }
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
        }

        DateTime LastTick = DateTime.MinValue;

        public override void OnTick()
        {
            DateTime now = DateTime.Now;
            if ((now - LastTick).TotalMilliseconds > 50)
            {
                LastTick = now;
                try
                {
                    SendPlayerData();
                    if (now.Subtract(TimerBlipChange).TotalMilliseconds >= PlayerBlipChangeMilliseconds)
                    {
                        TimerBlipChange = DateTime.Now;
                        PlayerBlipChangeState = !PlayerBlipChangeState;
                    }
                    base.OnTick();
                }
                catch (Exception ex) 
                { 
                    Tracing.Trace(ex); 
                }
            }
        }

        long TimerSiren = 0;
        bool CountingSiren = false;

        long TimerShooting = 0;
        bool CountingShooting = false;

        bool EinsatzSirene = false;

        private void SendPlayerData() 
        {
            int playerSprite = 1;
            bool siren = false;
            bool sirenSound = false;
            float heading = Game.PlayerPed.Heading;
            string oldStatus = CurrentUser.Status;

            if(CurrentUser.Status != "Im Einsatz") 
            {
                EinsatzSirene = false;
            }
            if (Game.PlayerPed.IsInVehicle())
            {
                Vehicle CurrentVehicle = Game.PlayerPed.CurrentVehicle;
                playerSprite = 225;

                if (!CountingSiren && CurrentVehicle.HasSiren && CurrentVehicle.IsSirenActive)
                {
                    CountingSiren = true;
                    TimerSiren = DateTime.Now.Ticks;
                }
                else if (CountingSiren && CurrentVehicle.HasSiren && !CurrentVehicle.IsSirenActive)
                {
                    CountingSiren = false;
                }

                if (Game.PlayerPed.IsInFlyingVehicle)
                {
                    playerSprite = 589;
                    if (CurrentVehicle.Model.IsHelicopter)
                    {
                        playerSprite = 64;
                    }
                    else if (CurrentVehicle.Model.IsPlane) 
                    {
                        playerSprite = 423;
                    }
                }
                else if (CurrentVehicle.HasSiren && CurrentVehicle.IsSirenActive)
                {
                    playerSprite = 56;
                    siren = true;
                    sirenSound = API.IsVehicleSirenAudioOn(CurrentVehicle.Handle);
                    if (CurrentUser != null && CountingSiren && DateTime.Now.Subtract(new DateTime(TimerSiren)).TotalSeconds > 1)
                    {
                        if (CurrentUser.Status == "Verfügbar")
                        {
                            EinsatzSirene = true;
                            CurrentUser.Status = "Im Einsatz";
                        }
                        CountingSiren = false;
                    }
                } 
                else if(CurrentVehicle.HasSiren && EinsatzSirene) 
                {
                    CurrentUser.Status = "Verfügbar";
                }
                else if(CurrentVehicle.HasSiren && CurrentUser.Status == "nicht im Dienst") 
                {
                    CurrentUser.Status = "Verfügbar";
                }
                else if (CurrentVehicle.Model.IsBike) 
                {
                    playerSprite = 559;
                    heading = heading + 90;
                }
                else if (CurrentVehicle.Model.IsBoat)
                {
                    playerSprite = 427;
                }
                else if (CurrentVehicle.Model.IsQuadbike)
                {
                    playerSprite = 512;
                }
                else if (CurrentVehicle.Model.IsBicycle)
                {
                    //Todo: Bicycle-Sprite
                    playerSprite = 559;
                    heading = heading + 90;
                }
                if(CurrentVehicle.IsSeatFree(VehicleSeat.Driver)) 
                {
                    playerSprite = 1;
                }
                else if(CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver).Handle != Game.PlayerPed.Handle) 
                {
                    playerSprite = -1;
                }

                CurrentUser.VehicleName = CurrentVehicle.LocalizedName;
                CurrentUser.VehicleHealth = Math.Round((CurrentVehicle.BodyHealth + CurrentVehicle.EngineHealth) / 20, 2);
            }
            
            Vector3 Waypoint = API.GetBlipInfoIdCoord(API.GetFirstBlipInfoId(8));

            CurrentUser.ServerID = ServerID;
            CurrentUser.Position = new DVector3(Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z);
            CurrentUser.Waypoint = new DVector3(Waypoint.X, Waypoint.Y, Waypoint.Z);
            CurrentUser.IsWaypointActive = API.IsWaypointActive();
            CurrentUser.PlayerSprite = playerSprite;
            CurrentUser.IsSirenOn = siren;
            CurrentUser.IsSirenSoundOn = sirenSound;
            CurrentUser.Heading = heading;
            CurrentUser.Velocity = API.GetEntitySpeed(Game.PlayerPed.Handle) * 3.6;
            CurrentUser.Visible = !GetSettingValue("Unsichtbar");
            CurrentUser.Status = CurrentUser?.Status;
            CurrentUser.TimeStamp = DateTime.Now.Ticks;
            CurrentUser.IsAiming = Game.PlayerPed.IsAiming;
            CurrentUser.IsAutoaimActive = ClientObject.GetService<DataService>().GetSettingValue("Zielhilfe");
            CurrentUser.IsInVehicle = Game.PlayerPed.IsInVehicle();

            if (Game.PlayerPed.IsShooting && !CountingShooting)
            {
                TimerShooting = DateTime.Now.Ticks;
                CountingShooting = true;
                CurrentUser.IsShooting = true;
            }
            if (!Game.PlayerPed.IsShooting && CountingShooting && DateTime.Now.Subtract(new DateTime(TimerShooting)).TotalMilliseconds > 200) 
            {
                CountingShooting = false;
                CurrentUser.IsShooting = false;
            }
            string UserRAW = CurrentUser.GetUserRAW();
            ClientObject.TriggerServerEvent(ServerEvents.SyncService_SendData, ServerID, UserRAW);

            if(oldStatus == "Im Einsatz" && CurrentUser.Status == "Verfügbar") 
            {
                if(!API.IsWaypointActive() && CurrentUser.DepartmentCoords.HasValue) 
                {
                    API.SetNewWaypoint(CurrentUser.DepartmentCoords.Value.X, CurrentUser.DepartmentCoords.Value.Y);
                }
            }
        }

        public void ChangeWeather(EnumWeather Weather) 
        {
            ClientObject.TriggerServerEvent(ServerEvents.SyncService_ChangeWeather, ServerID, (int)Weather);
        }

        private void OnChangeWeather(int Weather)
        {
            CommonFunctions.SetWeather((EnumWeather)Weather);
            ClientObject.SendMessage($"~g~Neues Wetter: ~y~{EnumWeatherHelper.GetUserFriendlyNames().FirstOrDefault(o => o.Value == (EnumWeather)Weather).Key}");
        }
    }
}
