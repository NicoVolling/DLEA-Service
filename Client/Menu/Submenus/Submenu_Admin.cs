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
    internal class Submenu_Admin : MenuBase
    {
        public Action<IEnumerable<ExtendedUser>> RefreshUserList { get; private set; }

        public Submenu_Admin(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Administration";

        protected override void InitializeMenu(UIMenu Menu)
        {
            UIMenu Menu_Admin_Players = AddSubMenu(Menu, "Spieler", "Liste aller Spieler");
            IEnumerable<ExtendedUser> UserList = null;
            RefreshUserList = (Users) =>
            {
                bool equal = true;
                if (Users != null)
                {
                    foreach (ExtendedUser user in Users)
                    {
                        if (UserList == null || !UserList.Any(o => o.Username == user.Username))
                        {
                            equal = false;
                        }
                    }
                }
                else { Users = new List<ExtendedUser>(); }
                UserList = Users;
                if (!equal)
                {
                    Menu_Admin_Players?.Clear();
                    if (UserList != null)
                    {
                        foreach (ExtendedUser User in Users)
                        {
                            UIMenu Usermenu = AddSubMenu(Menu_Admin_Players, $"{User.Name}", $"{User.Name} ({User.Username})");
                            UIMenuItem MenuStrChangeTb = AddMenuTextItem(
                            Usermenu, "Berechtigungen", "Menüliste", o =>
                            {
                                User.Permissions.MenusStr = o;
                                ClientObject.TriggerServerEvent(ServerEvents.DataService_SetPermissions, ClientObject.CurrentUser.ServerID, User.GetStoredUserRaw());
                            },
                            "Berechtigungen ändern", User.Permissions.MenusStr);
                        }
                    }
                }
            };
        }

        protected override void OnTick()
        {
        }
    }
}
