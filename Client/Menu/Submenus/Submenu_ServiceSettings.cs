using CitizenFX.Core.UI;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Menu.Submenus
{
    internal class Submenu_ServiceSettings : MenuBase
    {

        public Submenu_ServiceSettings(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Einstellungen";

        protected override void InitializeMenu(UIMenu Menu)
        {
            UIMenuItem Save = AddMenuItem(Menu, "Einstellungen speichern", "Einstellungen speichern", (item) =>
            {
                CurrentUser.SaveSettings(ClientObject.Services);
                ClientObject.TriggerServerEvent(ServerEvents.DataService_SendPlayerData, ClientObject.ServerID, CurrentUser.GetUserRAW(), false);
            });
            Save.TextColor = Colors.DarkGreen;
            Save.HighlightColor = Colors.LightGreen;

            UIMenu DataServiceMenu = null;
            UIMenu DisplayServiceMenu = null;
            foreach (Service Service in ClientObject.Services)
            {
                if (Service.Settings.Count > 0)
                {
                    UIMenu ServiceMenu = AddSubMenu(Menu, Service.UserFriendlyName, Service.UserFriendlyName);
                    foreach (ServiceSetting Setting in Service.Settings)
                    {
                        AddMenuCheckboxItem(ServiceMenu, Setting.SettingName, Setting.UserFriendlyName, Setting.Value, new Action<bool>((checkState) =>
                        {
                            Setting.Value = checkState;
                            CurrentUser.SaveSettings(ClientObject.Services);
                            ClientObject.SendMessage("~g~Einstellung geändert");
                            if (ClientObject.GetService<DataService>().GetSettingValue("Automatisches Speichern"))
                            {
                                CurrentUser.SaveSettings(ClientObject.Services);
                                ClientObject.TriggerServerEvent(ServerEvents.DataService_SendPlayerData, ClientObject.ServerID, CurrentUser.GetUserRAW(), false);
                            }
                        }));
                    }
                    if (Service is DataService)
                    {
                        DataServiceMenu = ServiceMenu;
                    }
                    if (Service is DisplayService)
                    {
                        DisplayServiceMenu = ServiceMenu;
                    }
                }
            }
            DataService DataService = ClientObject.GetService<DataService>();
            if (DataServiceMenu == null)
            {
                DataServiceMenu = AddSubMenu(Menu, DataService.UserFriendlyName, DataService.UserFriendlyName);
            }
            if (DataServiceMenu != null)
            {
                UIMenu ChangeSettingsMenu = AddSubMenu(DataServiceMenu, "Anmeldedaten ändern", "Anmeldedaten ändern");

                string Password = CurrentUser.Password;
                string Vorname = CurrentUser.Vorname;
                string Nachname = CurrentUser.Nachname;
                string Username = CurrentUser.Username;
                string OldUsername = CurrentUser.Username;

                UIMenuItem Item_Password2 = AddMenuTextItem(ChangeSettingsMenu, "Passwort eingeben", "Gib den Passwort ein", (text) =>
                {
                    Password = text;
                }, "Password eingeben", Password);

                UIMenuItem Item_Vorname = AddMenuTextItem(ChangeSettingsMenu, "Vorname eingeben", "Gib den Vorname ein", (text) =>
                {
                    Vorname = text;
                }, "Vorname eingeben", Vorname);

                UIMenuItem Item_Nachname = AddMenuTextItem(ChangeSettingsMenu, "Nachname eingeben", "Gib den Nachname ein", (text) =>
                {
                    Nachname = text;
                }, "Nachname eingeben", Nachname);

                UIMenuItem Item_Login = AddMenuItem(ChangeSettingsMenu, "Nutzerdaten ändern", "Nutzerdaten ändern", (menu) =>
                {
                    if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Vorname) && !string.IsNullOrWhiteSpace(Nachname))
                    {
                        CurrentUser.Password = Password;
                        CurrentUser.Vorname = Vorname;
                        CurrentUser.Nachname = Nachname;
                        CurrentUser.Username = Username;
                        ClientObject.TriggerServerEvent(ServerEvents.DataService_ChangePlayerData, ClientObject.ServerID, OldUsername, CurrentUser.GetUserRAW());
                    }
                    else
                    {
                        Screen.ShowNotification("Bitte füllen Sie alle Felder aus!");
                    }
                });

                UIMenuItem Delete = AddMenuItem(DataServiceMenu, "Nutzer löschen", "Nutzer löschen", (item) =>
                {
                    CurrentUser.SaveSettings(ClientObject.Services);
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_DeletePlayerData, ClientObject.ServerID, CurrentUser.Username);
                    ClientObject.InitializeUser();
                    MainMenu.Login(null);
                });
                Delete.TextColor = Colors.DarkRed;
                Delete.HighlightColor = Colors.LightCoral;

                AddMenuItem(DataServiceMenu, "Alles neuladen", "Alles neuladen", (item) =>
                {
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_RequestPlayerData, ClientObject.ServerID, CurrentUser.GetUserRAW());
                });

                UIMenuItem Logout = AddMenuItem(DataServiceMenu, "Abmelden", "Abmelden", (item) =>
                {
                    CurrentUser.SaveSettings(ClientObject.Services);
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_Login, ClientObject.ServerID, CurrentUser.Username, false);
                    ClientObject.InitializeUser();
                    MainMenu.Login(null);
                });
            }
        }

        protected override void OnTick()
        {
        }
    }
}
