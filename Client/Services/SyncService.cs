﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Game;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Services
{
    public class SyncService : Service
    {
        private bool CountingSiren = false;
        private bool EinsatzSirene = false;
        private DateTime LastTick = DateTime.MinValue;
        private ExtendedUser OldUser;
        private Dictionary<int, int> PlayerBlips = new Dictionary<int, int>();
        private long TimerSiren = 0;
        private Dictionary<int, int> WaypointBlips = new Dictionary<int, int>();

        public SyncService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetPlayerList = OnGetPlayerList;
            EventOnChangeWeather = OnChangeWeather;
        }

        public override string Name => nameof(SyncService);

        #region Events

        public Action<int> EventOnChangeWeather { get; }
        public Action<string> EventOnGetPlayerList { get; }
        public int PlayerBlipChangeMilliseconds { get; set; } = 700;
        public bool PlayerBlipChangeState { get; set; }
        public DateTime TimerBlipChange { get; set; } = DateTime.Now;
        public override string UserFriendlyName => "Synchronisationseinstellungen";

        #endregion Events

        public void ChangeWeather(EnumWeather Weather)
        {
            ClientObject.TriggerServerEvent(ServerEvents.SyncService_ChangeWeather, ServerID, (int)Weather);
        }

        public void OnGetPlayerList(string UserListRAW)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserListRAW))
                {
                    Users.Deserialize(UserListRAW);
                }
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
        }

        public override void OnTick()
        {
            DateTime now = DateTime.Now;
            if ((now - LastTick).TotalMilliseconds > 0) //50
            {
                LastTick = now;
                //try
                //{
                SendPlayerData();
                if (now.Subtract(TimerBlipChange).TotalMilliseconds >= PlayerBlipChangeMilliseconds)
                {
                    TimerBlipChange = DateTime.Now;
                    PlayerBlipChangeState = !PlayerBlipChangeState;
                }
                RefreshUsers();
                base.OnTick();
                //}
                //catch (Exception ex)
                //{
                //    Tracing.Trace(ex);
                //}
            }
        }

        public void RefreshUsers()
        {
            if (Users.List == null)
            {
                Users.List = new List<ExtendedUser>();
            }
            Users.List.RemoveAll(o => !new PlayerList().Any(p => p.ServerId == o.ServerID));
            DateTime Now = DateTime.Now;
            Users.List = Users.List.OrderBy(o => o.Name).ToList();

            if (ClientObject.MainMenu.RefreshUserList != null)
            {
                ClientObject.MainMenu.RefreshUserList.Invoke(Users.List);
            }
            foreach (ExtendedUser CurrentUser in Users.List)
            {
                if (true)
                {
                    int WaypointBlip = -1;
                    int PlayerBlip = -1;
                    if (new PlayerList().FirstOrDefault(o => o.ServerId == CurrentUser.ServerID).Character is Ped Ped)
                    {
                        int PlayerSprite = 1;
                        bool Siren = false;
                        bool SirenSound = false;
                        float heading = Game.PlayerPed.Heading;

                        if (Ped.IsInVehicle())
                        {
                            Vehicle CurrentVehicle = Ped.CurrentVehicle;
                            PlayerSprite = 225;

                            if (Game.PlayerPed.IsInFlyingVehicle)
                            {
                                PlayerSprite = 589;
                                if (CurrentVehicle.Model.IsHelicopter)
                                {
                                    PlayerSprite = 64;
                                }
                                else if (CurrentVehicle.Model.IsPlane)
                                {
                                    PlayerSprite = 423;
                                }
                            }
                            else if (CurrentVehicle.HasSiren)
                            {
                                PlayerSprite = 56;
                                if (CurrentVehicle.IsSirenActive)
                                {
                                    Siren = true;
                                }
                            }
                            else if (CurrentVehicle.Model.IsBike)
                            {
                                PlayerSprite = 559;
                                heading = heading + 90;
                            }
                            else if (CurrentVehicle.Model.IsBoat)
                            {
                                PlayerSprite = 427;
                            }
                            else if (CurrentVehicle.Model.IsQuadbike)
                            {
                                PlayerSprite = 512;
                            }
                            else if (CurrentVehicle.Model.IsBicycle)
                            {
                                //Todo: Bicycle-Sprite
                                PlayerSprite = 559;
                                heading = heading + 90;
                            }
                            if (CurrentVehicle.IsSeatFree(VehicleSeat.Driver))
                            {
                                PlayerSprite = 1;
                            }
                            else if (CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver).Handle != Ped.Handle)
                            {
                                PlayerSprite = -1;
                            }
                        }

                        if (WaypointBlips.Any(o => o.Key == CurrentUser.ServerID) && WaypointBlips.FirstOrDefault(o => o.Key == CurrentUser.ServerID) is KeyValuePair<int, int> WpKvpL)
                        {
                            WaypointBlip = WpKvpL.Value;
                        }

                        if (PlayerBlips.Any(o => o.Key == CurrentUser.ServerID) && PlayerBlips.FirstOrDefault(o => o.Key == CurrentUser.ServerID) is KeyValuePair<int, int> PyKvpL)
                        {
                            PlayerBlip = PyKvpL.Value;
                        }

                        int PlayerBlipColor = GetPlayerBlipColor(Ped);

                        //Wegpunkte
                        {
                            if (API.DoesBlipExist(WaypointBlip) && ((!CurrentUser.IsWaypointActive || !GetSettingValue("Wegpunkte")) || (CurrentUser.ServerID == ServerID && !ClientObject.CurrentUser.GetSetting("DataService", "Debugmode"))))
                            {
                                API.RemoveBlip(ref WaypointBlip);
                                WaypointBlips.Remove(CurrentUser.ServerID);
                                WaypointBlip = -1;
                            }

                            if (!API.DoesBlipExist(WaypointBlip) && CurrentUser.IsWaypointActive && GetSettingValue("Wegpunkte") && (CurrentUser.ServerID != ServerID || ClientObject.CurrentUser.GetSetting("DataService", "Debugmode")))
                            {
                                if (WaypointBlips.ContainsKey(CurrentUser.ServerID))
                                {
                                    WaypointBlip = CommonFunctions.AddBlipForCoord(CurrentUser.Waypoint, 364, 2, 3, $"Wegpunkt von {new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name}");
                                    WaypointBlips.Add(CurrentUser.ServerID, WaypointBlip);
                                }
                            }
                        }

                        //Postitionen
                        {
                            if (API.DoesBlipExist(PlayerBlip) && ((!CurrentUser.Visible || !GetSettingValue("Positionen") || PlayerSprite == -1) || (CurrentUser.ServerID == ServerID && !ClientObject.CurrentUser.GetSetting("DataService", "Debugmode"))))
                            {
                                API.RemoveBlip(ref PlayerBlip);
                                PlayerBlips.Remove(CurrentUser.ServerID);
                                PlayerBlip = -1;
                            }

                            if (PlayerSprite != -1 && CurrentUser.Visible && GetSettingValue("Positionen") && (CurrentUser.ServerID != ServerID || ClientObject.CurrentUser.GetSetting("DataService", "Debugmode")))
                            {
                                if (!API.DoesBlipExist(PlayerBlip))
                                {
                                    if (!PlayerBlips.ContainsKey(CurrentUser.ServerID))
                                    {
                                        PlayerBlip = CommonFunctions.AddBlipForEntity(Ped.Handle, PlayerSprite, PlayerBlipColor, 2, $"{new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name}");
                                        PlayerBlips.Add(CurrentUser.ServerID, PlayerBlip);
                                    }
                                }
                                else
                                {
                                    CommonFunctions.RefreshBlip(PlayerBlip, PlayerSprite, PlayerBlipColor, 2, $"{new PlayerList().Where(o => o.ServerId == CurrentUser.ServerID)?.First().Name}");
                                }
                            }
                        }
                    }
                }

                //Entferne Nutzer, die nicht mehr auf dem Server sind oder deren Ping zu lange ist.
                {
                    IEnumerable<KeyValuePair<int, int>> UsersRemoveList = PlayerBlips.Where(o => !Users.List.Any(p => p.ServerID == o.Key));
                    foreach (KeyValuePair<int, int> User in UsersRemoveList)
                    {
                        try
                        {
                            KeyValuePair<int, int> PlayerBlipKvp = PlayerBlips.Where(o => o.Key == User.Key).First();
                            int ServerId = PlayerBlipKvp.Key;
                            int BlipId = PlayerBlipKvp.Value;
                            API.RemoveBlip(ref BlipId);
                            PlayerBlips.Remove(ServerID);
                            BlipId = -1;
                        }
                        catch { }

                        try
                        {
                            KeyValuePair<int, int> WaypointBlipKvp = WaypointBlips.Where(o => o.Key == User.Key).First();
                            int ServerId = WaypointBlipKvp.Key;
                            int BlipId = WaypointBlipKvp.Key;
                            API.RemoveBlip(ref BlipId);
                            WaypointBlips.Remove(ServerID);
                            BlipId = -1;
                        }
                        catch { }
                    }
                }
                Textdisplay.RefreshUserList(ClientObject, Users.List);
            }
        }

        protected override void InitializeSettings()
        {
            Settings.Add(new ServiceSetting(Name, "Positionen", "Positionen von Spielern", true));
            Settings.Add(new ServiceSetting(Name, "Wegpunkte", "Wegpunkte von Spielern", true));
            Settings.Add(new ServiceSetting(Name, "Unsichtbar", "Für andere Spieler sichtbar", false));
            base.InitializeSettings();
        }

        private int GetPlayerBlipColor(Ped Player)
        {
            int PlayerBlipColor = 57;
            if (Player.IsInVehicle() && Player.CurrentVehicle.HasSiren)
            {
                if (Player.CurrentVehicle.IsSirenActive)
                {
                    PlayerBlipColor = PlayerBlipChangeState ? 59 : 38;
                    if (API.IsVehicleSirenAudioOn(Player.CurrentVehicle.Handle))
                    {
                        PlayerBlipChangeMilliseconds = 300;
                    }
                    else
                    {
                        PlayerBlipChangeMilliseconds = 700;
                    }
                }
            }
            return PlayerBlipColor;
        }

        private void OnChangeWeather(int Weather)
        {
            CommonFunctions.SetWeather((EnumWeather)Weather);
            ClientObject.SendMessage($"~g~Neues Wetter: ~y~{EnumWeatherHelper.GetUserFriendlyNames().FirstOrDefault(o => o.Value == (EnumWeather)Weather).Key}");
        }

        private void SendPlayerData()
        {
            if (OldUser is null)
            {
                OldUser = new ExtendedUser();
            }
            string oldStatus = CurrentUser.Status;

            if (CurrentUser.Status != "Im Einsatz")
            {
                EinsatzSirene = false;
            }

            if (Game.PlayerPed.IsInVehicle())
            {
                Vehicle CurrentVehicle = Game.PlayerPed.CurrentVehicle;

                if (!CountingSiren && CurrentVehicle.HasSiren && CurrentVehicle.IsSirenActive)
                {
                    CountingSiren = true;
                    TimerSiren = DateTime.Now.Ticks;
                }
                else if (CountingSiren && CurrentVehicle.HasSiren && !CurrentVehicle.IsSirenActive)
                {
                    CountingSiren = false;
                }

                if (CurrentVehicle.HasSiren && CurrentVehicle.IsSirenActive)
                {
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
                else if (CurrentVehicle.HasSiren && EinsatzSirene)
                {
                    CurrentUser.Status = "Verfügbar";
                }
                else if (CurrentVehicle.HasSiren && CurrentUser.Status == "nicht im Dienst")
                {
                    CurrentUser.Status = "Verfügbar";
                }
            }

            Vector3 Waypoint = API.GetBlipInfoIdCoord(API.GetFirstBlipInfoId(8));

            CurrentUser.ServerID = ServerID;
            CurrentUser.Waypoint = new DVector3(Waypoint.X, Waypoint.Y, Waypoint.Z);
            CurrentUser.IsWaypointActive = API.IsWaypointActive();
            CurrentUser.Visible = !GetSettingValue("Unsichtbar");
            CurrentUser.Status = CurrentUser?.Status;
            CurrentUser.IsAutoaimActive = ClientObject.GetService<DataService>().GetSettingValue("Zielhilfe");

            if (!ObjectCompare.Equals(OldUser, CurrentUser))
            {
                string UserRAW = CurrentUser.GetUserRAW();
                ClientObject.TriggerServerEvent(ServerEvents.SyncService_SendData, ServerID, UserRAW);
                OldUser = ExtendedUser.GetData(CurrentUser.GetUserRAW());
            }

            if (oldStatus == "Im Einsatz" && CurrentUser.Status == "Verfügbar")
            {
                if (!API.IsWaypointActive() && CurrentUser.DepartmentCoords.HasValue)
                {
                    Tracing.Trace("Difference");
                    API.SetNewWaypoint(CurrentUser.DepartmentCoords.Value.X, CurrentUser.DepartmentCoords.Value.Y);
                }
            }
        }
    }
}