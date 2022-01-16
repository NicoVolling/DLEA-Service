using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLEA_Lib.Shared;
using CitizenFX.Core.UI;
using DLEA_Lib;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.User;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Application;

namespace Client.Services
{
    class DataService : Service
    {
        public override string Name => nameof(DataService);

        #region Events
        public Action<string> EventOnGetResult { get; }
        public Action<string> EventOnPermissionsChanged { get; }
        public Action<string> EventOnGetAutoLoginData { get; }
        #endregion

        public override string UserFriendlyName => "Benutzereinstellungen";

        public DataService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetResult = OnGetResult;
            EventOnPermissionsChanged = OnPermissionsChanged;
            EventOnGetAutoLoginData = OnGetAutoLoginData;
        }

        private void OnPermissionsChanged(string UserRaw)
        {
            StoredUser CurrentUser = StoredUser.GetData(UserRaw);
            if (ClientObject.CurrentUser.Username == CurrentUser.Username)
            {
                ClientObject.CurrentUser.Permissions = CurrentUser.Permissions;
                ClientObject.MainMenu.Login();
            }
        }

        public override void Start() 
        {
            ClientObject.TriggerServerEvent(ServerEvents.DataService_GetLogin, Game.Player.ServerId);
        }

        protected override void InitializeSettings()
        {
            Settings.Add(new ServiceSetting(Name, "Zielhilfe", "Automatisches Zielen", false));
            Settings.Add(new ServiceSetting(Name, "Debugmode", "Debugmode", false));
            Settings.Add(new ServiceSetting(Name, "Automatisches Speichern", "Einstellungen werden automatisch gespeichert.", false));
            base.InitializeSettings();
        }

        private void OnGetAutoLoginData(string UserRaw)
        {
            Tracing.Trace("202201161956");
            Tracing.Trace(UserRaw);
            OnGetResult(UserRaw);
        }

        private void OnGetResult(string UserRaw) 
        {
            try 
            {
                StoredUser User = StoredUser.GetData(UserRaw);
                bool success = false;
                if (User != null)
                {
                    if (User.Exists == true)
                    {
                        if (User.LoggedIn == true)
                        {
                            CurrentUser = User.ToExtendedUser();
                            CurrentUser.LoadSettings(ClientObject.Services);
                            ClientObject.TriggerServerEvent(ServerEvents.DataService_SendPlayerData, ClientObject.ServerID, CurrentUser.GetStoredUserRaw(), true);
                            ClientObject.MainMenu.Login(CurrentUser);
                            success = true;
                        }
                    }
                }
                else 
                {
                    Screen.ShowNotification("Unbekannter Fehler bei der Anmeldung");
                }
                if (success)
                {
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_Login, Game.Player.ServerId, CurrentUser.Username, true);
                }
                else
                {
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_Login, Game.Player.ServerId, string.Empty, false);
                }
            }
            catch (Exception ex) 
            {
                Tracing.Trace(ex);
            }
        }

        public override void OnTick()
        {
            API.SetPlayerTargetingMode(GetSettingValue("Zielhilfe") ? 0 : 3);
            base.OnTick();
        }

    }
}
