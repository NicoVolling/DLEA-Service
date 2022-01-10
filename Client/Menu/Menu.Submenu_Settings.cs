using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using DLEA_Lib;
using DLEA_Lib.Shared;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Services;
using DLEA_Lib.Shared.Wardrobe;
using DLEA_Lib.Shared.User;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Application;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private Action<IEnumerable<StoredUser>> RefreshPlayerDisplayList;

        private void AddSubmenu_Einstellungen()
        {
            UIMenu MenuServiceSettings = MenuPool.AddSubMenu(this, "Einstellungen", "Einstellungen");

            UIMenuItem Save = AddMenuItem(MenuServiceSettings, "Einstellungen speichern", "Einstellungen speichern", (item) =>
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
                    UIMenu ServiceMenu = MenuPool.AddSubMenu(MenuServiceSettings, Service.UserFriendlyName, Service.UserFriendlyName);
                    foreach (ServiceSetting Setting in Service.Settings)
                    {
                        AddMenuCheckboxItem(ServiceMenu, Setting.SettingName, Setting.UserFriendlyName, Setting.Value, new Action<bool>((checkState) =>
                        {
                            Setting.Value = checkState;
                            CurrentUser.SaveSettings(ClientObject.Services);
                            ClientObject.SendMessage("~g~Einstellung geändert");
                            if(ClientObject.GetService<DataService>().GetSettingValue("Automatisches Speichern")) 
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
                    if(Service is DisplayService) 
                    {
                        DisplayServiceMenu = ServiceMenu;
                    }
                }
            }
            DataService DataService = ClientObject.GetService<DataService>();
            if (DataServiceMenu == null)
            {
                DataServiceMenu = MenuPool.AddSubMenu(MenuServiceSettings, DataService.UserFriendlyName, DataService.UserFriendlyName);
            }
            if (DataServiceMenu != null)
            {
                UIMenu ChangeSettingsMenu = MenuPool.AddSubMenu(DataServiceMenu, "Anmeldedaten ändern", "Anmeldedaten ändern");

                string Password = CurrentUser.Password;
                string Vorname = CurrentUser.Vorname;
                string Nachname = CurrentUser.Nachname;
                string Username = CurrentUser.Username;
                string OldUsername = CurrentUser.Username;

                //UIMenuItem Item_Username = AddMenuTextItem(ChangeSettingsMenu, "Nutzernamen eingeben", "Gib den Nutzernamen ein", (text) =>
                //{
                //    Username = text;
                //});

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
                        Screen.ShowNotification("Bitte geben füllen Sie alle Felder aus!");
                    }
                });

                UIMenuItem Delete = AddMenuItem(DataServiceMenu, "Nutzer löschen", "Nutzer löschen", (item) =>
                {
                    CurrentUser.SaveSettings(ClientObject.Services);
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_DeletePlayerData, ClientObject.ServerID, CurrentUser.Username);
                    ClientObject.InitializeUser();
                    Login(null);
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
                    Login(null);
                });
            }

            DisplayService DisplayService = ClientObject.GetService<DisplayService>();
            if(DisplayServiceMenu != null) 
            {
                UIMenu Spieler = MenuPool.AddSubMenu(DisplayServiceMenu, "Spieler wählen");
                RefreshPlayerDisplayList = List => 
                {
                    Spieler.Clear();

                    foreach(StoredUser User in List) 
                    {
                        UIMenuItem item = AddMenuCheckboxItem(Spieler, $"{User.Vorname} {User.Nachname}", true, value => 
                        {
                            if(ClientObject.GetService<DisplayService>().Users.Contains(User.Username) && !value) 
                            {
                                ClientObject.GetService<DisplayService>().Users.Remove(User.Username);
                            } 
                            else if(!ClientObject.GetService<DisplayService>().Users.Contains(User.Username) && value) 
                            {
                                ClientObject.GetService<DisplayService>().Users.Add(User.Username);
                            }
                        });
                        ClientObject.GetService<DisplayService>().Users.Add(User.Username);
                    }
                };
            }
        }
    }
}
