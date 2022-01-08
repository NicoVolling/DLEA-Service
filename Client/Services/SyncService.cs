using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
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

        public void OnGetPlayerList(string UserListRAW) 
        {
            try
            {
                if (string.IsNullOrEmpty(UserListRAW)) { return; }
                List<ExtendedUser> UserList = Users.Deserialize(UserListRAW, false);
                if(UserList == null) { return; }
                DateTime Now = DateTime.Now;
                UserList = UserList.OrderBy(o => o.Name).ToList();
                if (ClientObject.MainMenu.RefreshUserList != null)
                {
                    ClientObject.MainMenu.RefreshUserList.Invoke(UserList.Where(o => Now.Subtract(new DateTime(o.TimeStamp)).TotalSeconds < 10));
                }
                foreach (ExtendedUser CurrentUser in UserList) 
                {
                    if (CurrentUser.ServerID != ServerID || ClientObject.CurrentUser.GetSetting("DataService", "Debugmode"))
                    {
                        int WaypointBlip = -1;
                        int PlayerBlip = -1;

                        IEnumerable<KeyValuePair<int, int>> WpKvpL = WaypointBlips.Where(o => o.Key == CurrentUser.ServerID);

                        if (WpKvpL.Any())
                        {
                            WaypointBlip = WpKvpL.First().Value;
                        }

                        IEnumerable<KeyValuePair<int, int>> PyKvpL = PlayerBlips.Where(o => o.Key == CurrentUser.ServerID);

                        if (PyKvpL.Any())
                        {
                            PlayerBlip = PyKvpL.First().Value;
                        }

                        int PlayerBlipColor = 57;
                        if (CurrentUser.IsSirenOn) 
                        {
                            PlayerBlipColor = PlayerBlipChangeState ? 59 : 38;
                            if (CurrentUser.IsSirenSoundOn)
                            {
                                PlayerBlipChangeMilliseconds = 300;
                            }
                            else
                            {
                                PlayerBlipChangeMilliseconds = 700;
                            }
                        }

                        if (Now.Subtract(new DateTime(CurrentUser.TimeStamp)).TotalSeconds < 10)
                        {
                            if ((!CurrentUser.Visible || !GetSettingValue("Positionen")) && PlayerBlip != -1)
                            {
                                API.RemoveBlip(ref PlayerBlip);
                                PlayerBlips.Remove(CurrentUser.ServerID);
                                PlayerBlip = -1;
                            }
                            if ((!CurrentUser.IsWaypointActive || !GetSettingValue("Wegpunkte")) && WaypointBlip != -1)
                            {
                                API.RemoveBlip(ref WaypointBlip);
                                WaypointBlips.Remove(CurrentUser.ServerID);
                                WaypointBlip = -1;
                            }

                            if (CurrentUser.Visible && PlayerBlip != -1 && GetSettingValue("Positionen"))
                            {
                                API.SetBlipCoords(PlayerBlip, CurrentUser.Position.X, CurrentUser.Position.Y, CurrentUser.Position.Z);
                                API.SetBlipSprite(PlayerBlip, CurrentUser.PlayerSprite);
                                API.SetBlipColour(PlayerBlip, PlayerBlipColor);
                                API.SetBlipSquaredRotation(PlayerBlip, (float)CurrentUser.Heading);
                            }
                            else if (CurrentUser.Visible && GetSettingValue("Positionen"))
                            {
                                PlayerBlips.Add(CurrentUser.ServerID, API.AddBlipForCoord((float)CurrentUser.Position.X, (float)CurrentUser.Position.X, (float)CurrentUser.Position.X));
                                PlayerBlip = PlayerBlips.Where(o => o.Key == CurrentUser.ServerID).First().Value;
                                API.SetBlipSprite(PlayerBlip, CurrentUser.PlayerSprite);
                                API.SetBlipColour(PlayerBlip, PlayerBlipColor);
                                API.SetBlipDisplay(PlayerBlip, 2);
                                API.SetBlipCategory(PlayerBlip, 7);
                                API.SetBlipSquaredRotation(PlayerBlip, (float)CurrentUser.Heading);
                            }


                            if (CurrentUser.IsWaypointActive && GetSettingValue("Wegpunkte") && WaypointBlip == -1)
                            {
                                WaypointBlips.Add(CurrentUser.ServerID, API.AddBlipForCoord((float)CurrentUser.Waypoint.X, (float)CurrentUser.Waypoint.Y, (float)CurrentUser.Waypoint.Z));
                                WaypointBlip = WaypointBlips.Where(o => o.Key == CurrentUser.ServerID).First().Value;

                                API.SetBlipSprite(WaypointBlip, 364);
                                API.SetBlipColour(WaypointBlip, 2);
                                API.SetBlipDisplay(WaypointBlip, 3);
                                API.BeginTextCommandSetBlipName("STRING");
                                API.AddTextComponentString($"Wegpunkt von { new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name }");
                                API.EndTextCommandSetBlipName(WaypointBlip);
                                API.SetBlipCategory(WaypointBlip, 2);
                            }
                        }
                    }
                    if (Now.Subtract(new DateTime(CurrentUser.TimeStamp)).TotalSeconds > 10 || (CurrentUser.ServerID == ServerID && !ClientObject.CurrentUser.GetSetting("DataService", "Debugmode")))
                    {
                        try { 
                            int PlayerBlip = PlayerBlips.Where(o => o.Key == CurrentUser.ServerID).First().Value;
                            API.RemoveBlip(ref PlayerBlip);
                            PlayerBlips.Remove(CurrentUser.ServerID);
                            PlayerBlip = -1;
                        }
                        catch { }


                        try
                        {
                            int WaypointBlip = WaypointBlips.Where(o => o.Key == CurrentUser.ServerID).First().Value;
                            API.RemoveBlip(ref WaypointBlip);
                            WaypointBlips.Remove(CurrentUser.ServerID);
                            WaypointBlip = -1;
                        }
                        catch { }
                    }
                }
                Textdisplay.RefreshUserList(ClientObject, UserList);
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

        private void SendPlayerData() 
        {
            int playerSprite = 1;
            bool siren = false;
            bool sirenSound = false;
            float heading = Game.PlayerPed.Heading;
            if (Game.PlayerPed.IsInVehicle())
            {
                Vehicle CurrentVehicle = Game.PlayerPed.CurrentVehicle;
                playerSprite = 326;

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
                            CurrentUser.Status = "Im Einsatz";
                        }
                        CountingSiren = false;
                    }
                    else if (CurrentVehicle.HasSiren)
                    {
                        playerSprite = 56;
                    }
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
        }

        public void ChangeWeather(EnumWeather Weather) 
        {
            ClientObject.TriggerServerEvent(ServerEvents.SyncService_ChangeWeather, ServerID, (int)Weather);
        }

        private void OnChangeWeather(int Weather)
        {
            ClientHelper.SetWeather((EnumWeather)Weather);
            ClientObject.SendMessage($"~g~Neues Wetter: ~y~{EnumWeatherHelper.GetUserFriendlyNames().FirstOrDefault(o => o.Value == (EnumWeather)Weather).Key}");
        }
    }
}
