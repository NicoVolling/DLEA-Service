using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Services
{
    public class TDataService : TService
    {
        public TDataService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnGetPlayerData = OnGetPlayerData;
            EventOnRequestPlayerData = OnRequestPlayerData;
            EventOnChangePlayerData = OnChangePlayerData;
            EventOnDeletePlayer = OnDeletePlayer;
            EventOnLogin = OnLogin;
            EventOnSendAutoLoginData = OnSendAutoLogin;
            EventOnGetPermissions = OnGetPermissions;

            API.RegisterCommand("dlea-refresh", new Action<int, dynamic, string>((source, args, raw) =>
            {
                foreach (Player player in new PlayerList())
                {
                    OnSendAutoLogin(player);
                    ((TLocationService)ServerObject.Services.First(o => o.Name == nameof(TLocationService))).OnSendLocations(player);
                }
            }), true);
        }

        public override string Name => nameof(TDataService);

        #region Events

        public Action<int, string, string> EventOnChangePlayerData { get; }

        public Action<int, string> EventOnDeletePlayer { get; }

        public Action<int, string> EventOnGetPermissions { get; }

        public Action<int, string, bool> EventOnGetPlayerData { get; }

        public Action<int, string, bool> EventOnLogin { get; }

        public Action<int, string, bool> EventOnRequestPlayerData { get; }

        public Action<int> EventOnSendAutoLoginData { get; }

        #endregion Events

        public void OnGetPlayerData(int PlayerID, string UserRaw, bool Silent)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                Users.Deserialize(RAW);
                StoredUser CurrentUser = StoredUser.GetData(UserRaw);

                if (Users.List != null)
                {
                    int index = Users.List.FindIndex(o => o.Username == CurrentUser.Username && o.Password == CurrentUser.Password);
                    if (index != -1)
                    {
                        Users.List[index] = CurrentUser.ToExtendedUser();

                        CurrentUser.JustCreated = false;
                        CurrentUser.Exists = true;
                        CurrentUser.LoggedIn = true;
                    }
                }
                RAW = Users.SerializeStoredUsers();
                API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json", RAW, -1);
                TServerObject.Trace($"Saved Data:{CurrentUser.Username}");
                if (!Silent)
                {
                    TMessageService.SendMessage(PlayerID, "~g~Einstellungen gespeichert");
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        public void OnRequestPlayerData(int PlayerID, string UserRaw, bool Create)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                Users.Deserialize(RAW);
                StoredUser CurrentUser = StoredUser.GetData(UserRaw);
                if (CurrentUser != null)
                {
                    CurrentUser.Exists = false;
                    CurrentUser.LoggedIn = false;
                    CurrentUser.JustCreated = true;

                    if (Users.List == null)
                    {
                        Users.List = new List<ExtendedUser>();
                    }
                    IEnumerable<StoredUser> SelectedUser = Users.List.Where(o => o.Username == CurrentUser.Username);
                    IEnumerable<StoredUser> LoggedUser = SelectedUser.Where(o => o.Password == CurrentUser.Password);
                    string Message = "~r~Anmeldung erfolglos";

                    if (!Create)    //Login
                    {
                        if (SelectedUser.Any() && LoggedUser.Any())
                        {
                            LoggedUser.First().LoggedIn = true;
                            LoggedUser.First().JustCreated = false;
                            LoggedUser.First().Exists = true;
                            Message = "~g~Erfolgreich angemeldet";
                            CurrentUser = LoggedUser.First();
                        }
                        else if (SelectedUser.Any())
                        {
                            SelectedUser.First().LoggedIn = false;
                            SelectedUser.First().JustCreated = false;
                            SelectedUser.First().Exists = true;
                            Message = "~r~Passwort ist falsch";
                            CurrentUser = SelectedUser.First();
                        }
                        else
                        {
                            CurrentUser.LoggedIn = false;
                            CurrentUser.JustCreated = false;
                            CurrentUser.Exists = true;
                            Message = "~r~Nutzer existiert nicht";
                        }
                    }
                    else            //Register
                    {
                        if (SelectedUser.Any())
                        {
                            SelectedUser.First().LoggedIn = false;
                            SelectedUser.First().JustCreated = false;
                            SelectedUser.First().Exists = true;
                            Message = "~r~Nutzername bereits vergeben";
                            CurrentUser = SelectedUser.First();
                        }
                        else
                        {
                            Users.List.Add(CurrentUser.ToExtendedUser());
                            CurrentUser.LoggedIn = true;
                            CurrentUser.JustCreated = true;
                            CurrentUser.Exists = true;
                            Message = "~g~Erfolgreich registriert";
                            RAW = Users.SerializeStoredUsers();
                            API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json", RAW, -1);
                        }
                    }
                    new PlayerList()[PlayerID].TriggerEvent(ClientEvents.DataService_SendPlayerData, CurrentUser.GetUserRAW());
                    TServerObject.Trace($"Got Data: {CurrentUser.Username}");
                    TMessageService.SendMessage(PlayerID, Message);
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnChangePlayerData(int PlayerID, string Username, string UserRaw)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                Users.Deserialize(RAW);
                StoredUser CurrentUser = StoredUser.GetData(UserRaw);
                if (CurrentUser != null)
                {
                    if (Users.List != null)
                    {
                        Users.List.RemoveAll(o => o.Username == CurrentUser.Username);
                        Users.List.Add(CurrentUser.ToExtendedUser());
                        RAW = Users.SerializeStoredUsers();
                        API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json", RAW, -1);
                        if (Username != CurrentUser.Username)
                        {
                            try
                            {
                                string RAW2 = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Login.json");
                                Dictionary<string, string> LoggedInUsers = new Dictionary<string, string>();
                                if (!string.IsNullOrEmpty(RAW2))
                                {
                                    LoggedInUsers = Users.DeserializeLoggedInList(RAW2);
                                }

                                if (LoggedInUsers.Any(o => o.Value == Username))
                                {
                                    string ident = LoggedInUsers.Where(o => o.Value == Username).First().Key;
                                    LoggedInUsers.Remove(ident);
                                    LoggedInUsers.Add(ident, CurrentUser.Username);
                                }

                                API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Login.json", Users.SerializeLoggedInList(LoggedInUsers), -1);
                            }
                            catch (Exception ex)
                            {
                                TServerObject.Trace(ex);
                            }
                        }
                        TServerObject.Trace($"Changed Data: {CurrentUser.Username}");
                        TMessageService.SendMessage(PlayerID, "~g~Nutzerdaten gespeichert");
                    }
                    else
                    {
                        TMessageService.SendMessage(PlayerID, "~r~Nutzerdaten nicht gespeichert");
                    }
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnDeletePlayer(int PlayerID, string UserRaw)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                Users.Deserialize(RAW);
                StoredUser CurrentUser = StoredUser.GetData(UserRaw);
                if (CurrentUser != null)
                {
                    Users.List.RemoveAll(o => o.Username == CurrentUser.Username);
                    RAW = Users.SerializeStoredUsers();
                    API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json", RAW, -1);
                    TServerObject.Trace($"Deleted Data: {CurrentUser.Username}");
                    TMessageService.SendMessage(PlayerID, "~g~Nutzer gelöscht");
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnGetPermissions(int PlayerId, string UserRaw)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                Users.Deserialize(RAW);
                StoredUser CurrentUser = StoredUser.GetData(UserRaw);
                if (CurrentUser != null)
                {
                    if (Users.List != null)
                    {
                        int index = Users.List.FindIndex(o => o.Username == CurrentUser.Username && o.Password == CurrentUser.Password);
                        if (index != -1)
                        {
                            Users.List[index].Permissions = CurrentUser.Permissions;
                        }
                        RAW = Users.SerializeStoredUsers();
                        API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json", RAW, -1);
                        TServerObject.Trace($"Changed Permissions: {CurrentUser.Username}");
                        TMessageService.SendMessage(PlayerId, "~g~Berechtigungen gespeichert");
                        TServerObject.TriggerClientEvent(ClientEvents.DataService_PermissiosChanged, UserRaw);
                    }
                    else
                    {
                        TMessageService.SendMessage(PlayerId, "~r~Berechtigungen nicht gespeichert");
                    }
                }
                else
                {
                    TMessageService.SendMessage(PlayerId, "~r~Berechtigungen nicht gespeichert");
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnLogin(int PlayerID, string Username, bool Login)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Login.json");
                Dictionary<string, string> LoggedInUsers = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(RAW))
                {
                    LoggedInUsers = Users.DeserializeLoggedInList(RAW);
                }

                string Identifier = new PlayerList()[PlayerID].Identifiers.ToList()[0].Replace("license:", "");
                string log = Login ? "in" : "out";
                Tracing.TraceString($"{Identifier} logged {log} with Username: {Username}");

                if (LoggedInUsers.ContainsKey(Identifier))
                {
                    if (Login)
                    {
                        LoggedInUsers[Identifier] = Username;
                    }
                    else
                    {
                        LoggedInUsers.Remove(LoggedInUsers.Where(o => o.Value == Username).First().Key);
                    }
                }
                else
                {
                    if (Login)
                    {
                        LoggedInUsers.Add(Identifier, Username);
                    }
                }

                API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Login.json", Users.SerializeLoggedInList(LoggedInUsers), -1);
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnSendAutoLogin(int PlayerID)
        {
            OnSendAutoLogin(new PlayerList()[PlayerID]);
        }

        private void OnSendAutoLogin(Player Player)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Login.json");
                string UsersRAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/UserData/Users.json");
                if (!string.IsNullOrEmpty(RAW) && !string.IsNullOrEmpty(UsersRAW))
                {
                    Users.Deserialize(UsersRAW);

                    Dictionary<string, string> LoggedInUsers = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(RAW))
                    {
                        LoggedInUsers = Users.DeserializeLoggedInList(RAW);
                    }
                    if (Player.Identifiers.Count() > 0)
                    {
                        string Identifier = Player.Identifiers.ToList()[0].Replace("license:", "");
                        IEnumerable<StoredUser> SearchResult = Users.List.Where(o => o.Username == LoggedInUsers[Identifier]);

                        if (LoggedInUsers.ContainsKey(Identifier) && SearchResult.FirstOrDefault() is StoredUser CurrentUser)
                        {
                            Player.TriggerEvent(ClientEvents.DataService_AutoLogin, CurrentUser.GetUserRAW());
                        }
                        TMessageService.SendMessage(Player, "~g~Automatisch Angemeldet");
                    }
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }
    }
}