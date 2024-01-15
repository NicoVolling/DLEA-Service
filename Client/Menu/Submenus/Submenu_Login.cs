using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Menu.Submenus
{
    internal class Submenu_Login : MenuBase
    {
        public Submenu_Login(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Anmelden";

        protected override void InitializeMenu(UIMenu Menu)
        {
            string Username = string.Empty;
            string Password = string.Empty;

            string Vorname = string.Empty;
            string Nachname = string.Empty;

            UIMenuItem Item_Username = AddMenuTextItem(Menu, "Nutzernamen eingeben", "Gib den Nutzernamen ein", (text) =>
            {
                Username = text;
            });
            UIMenuItem Item_Password = AddMenuTextItem(Menu, "Passwort eingeben", "Gib den Passwort ein", (text) =>
            {
                Password = text;
            });
            UIMenuItem Item_Login = AddMenuItem(Menu, "Anmelden", "Anmelden", (menu) =>
            {
                if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
                {
                    StoredUser User = new StoredUser() { Username = Username, Password = Password };
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_RequestPlayerData, ClientObject.ServerID, User.GetUserRAW(), false);
                }
                else
                {
                    Screen.ShowNotification("Bitte geben füllen Sie alle Felder aus!");
                }
            });

            UIMenu MenuLogin_Create = AddSubMenu(Menu, "Erstellen", "Einen neuen Nutzer anlegen");

            UIMenuItem Item_Username2 = AddMenuTextItem(MenuLogin_Create, "Nutzernamen eingeben", "Gib den Nutzernamen ein", (text) =>
            {
                Username = text;
            });
            UIMenuItem Item_Password2 = AddMenuTextItem(MenuLogin_Create, "Passwort eingeben", "Gib den Passwort ein", (text) =>
            {
                Password = text;
            });
            UIMenuItem Item_Vorname = AddMenuTextItem(MenuLogin_Create, "Vorname eingeben", "Gib den Vorname ein", (text) =>
            {
                Vorname = text;
            });

            UIMenuItem Item_Nachname = AddMenuTextItem(MenuLogin_Create, "Nachname eingeben", "Gib den Nachname ein", (text) =>
            {
                Nachname = text;
            });
            UIMenuItem Item_Create = AddMenuItem(MenuLogin_Create, "Registrieren", "Registrieren", (menu) =>
            {
                if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Vorname) && !string.IsNullOrWhiteSpace(Nachname))
                {
                    StoredUser User = new StoredUser() { Username = Username, Password = Password, Vorname = Vorname, Nachname = Nachname };
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_RequestPlayerData, API.GetPlayerServerId(API.PlayerId()), User.GetUserRAW(), true);
                }
                else
                {
                    Screen.ShowNotification("Bitte geben füllen Sie alle Felder aus!");
                }
            });
        }

        protected override void OnTick()
        {
        }
    }
}
