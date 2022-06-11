using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using NativeUI;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private UIMenu Submenu_Login;

        private void AddSubmenu_Login()
        {
            string Username = string.Empty;
            string Password = string.Empty;

            string Vorname = string.Empty;
            string Nachname = string.Empty;

            Submenu_Login = MenuPool.AddSubMenu(this, "Anmelden", "Anmelden");

            UIMenuItem Item_Username = AddMenuTextItem(Submenu_Login, "Nutzernamen eingeben", "Gib den Nutzernamen ein", (text) =>
            {
                Username = text;
            });
            UIMenuItem Item_Password = AddMenuTextItem(Submenu_Login, "Passwort eingeben", "Gib den Passwort ein", (text) =>
            {
                Password = text;
            });
            UIMenuItem Item_Login = AddMenuItem(Submenu_Login, "Anmelden", "Anmelden", (menu) =>
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

            UIMenu MenuLogin_Create = MenuPool.AddSubMenu(Submenu_Login, "Erstellen", "Einen neuen Nutzer anlegen");

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
                    ClientObject.TriggerServerEvent(ServerEvents.DataService_RequestPlayerData, API.PlayerId(), User.GetUserRAW(), true);
                }
                else
                {
                    Screen.ShowNotification("Bitte geben füllen Sie alle Felder aus!");
                }
            });

            //AddSubmenus();
        }
    }
}