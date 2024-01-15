using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Menu.Submenus
{
    internal class Submenu_PlayerList : MenuBase
    {
        public Action<IEnumerable<ExtendedUser>> RefreshUserList { get; private set; }

        public Submenu_PlayerList(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        { 
        }

        protected override string Title => "Spieler";

        protected override void InitializeMenu(UIMenu Menu)
        {
            ClientObject.Trace("Init...");
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
                else
                {
                    Users = new List<ExtendedUser>();
                }
                UserList = Users;
                if (!equal)
                {
                    Menu.Clear();

                    if (UserList != null)
                    {
                        foreach (ExtendedUser User in Users)
                        {
                            UIMenu Usermenu = AddSubMenu(Menu, $"{User.Name}", $"{User.Name} ({User.Username})");
                        }
                    }
                }
            };
            ClientObject.Trace((RefreshUserList == null).ToString() + "RUL");
        }

        protected override void OnTick()
        {
        }
    }
}
