using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Menu
{
    public partial class MainMenu
    {
        public Action<IEnumerable<ExtendedUser>> RefreshUserList { get; private set; }

        private void AddSubmenu_Admin()
        {
            UIMenu Menu_Admin = null;
            UIMenu Menu_Admin_Players = null;
            if (CurrentUser.Admin)
            {
                Menu_Admin = MenuPool.AddSubMenu(this, "Administration", "Administration");
                Menu_Admin_Players = MenuPool.AddSubMenu(Menu_Admin, "Spieler", "Liste aller Spieler");
            }
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
                    if (getPlayerMenu != null)
                    {
                        getPlayerMenu.Invoke().Clear();
                    }
                    if (UserList != null)
                    {
                        RefreshPlayerDisplayList?.Invoke(UserList);
                        foreach (ExtendedUser User in Users)
                        {
                            if (Menu_Admin != null)
                            {
                                UIMenu Usermenu = MenuPool.AddSubMenu(Menu_Admin_Players, $"{User.Name}", $"{User.Name} ({User.Username})");
                                UIMenuItem MenuStrChangeTb = AddMenuTextItem(
                                    Usermenu, "Berechtigungen", "Menüliste", o =>
                                    {
                                        User.Permissions.MenusStr = o;
                                        ClientObject.TriggerServerEvent(ServerEvents.DataService_SetPermissions, ClientObject.CurrentUser.ServerID, User.GetStoredUserRaw());
                                    },
                                    "Berechtigungen ändern", User.Permissions.MenusStr);
                            }
                            if (getPlayerMenu != null)
                            {
                                UIMenu Usermenu = MenuPool.AddSubMenu(getPlayerMenu(), $"{User.Name}", $"{User.Name} ({User.Username})");
                                if (CheckPermission("Menu.Spieler.Kick", true))
                                {
                                    //Auskommentiert, da buggy
                                    //UIMenuItem MenuKick = AddMenuItem(Usermenu, "Kicken", "Nutzer wird vom Server gekickt", menuitem =>
                                    //{
                                    //    if (new PlayerList().FirstOrDefault(o => o.ServerId == User.ServerID) is Player Player)
                                    //    {
                                    //        API.NetworkSessionKickPlayer(Player.Handle);
                                    //    }
                                    //});
                                }
                                UIMenuItem MenuNavigateStart = AddMenuItem(Usermenu, "Navigation starten", $"Startet die Navigation zu {User.Name} ({User.Username})", o =>
                                {
                                    ClientObject.SendMessage("~r~Noch nicht implementiert");
                                });
                                UIMenuItem MenuNavigateStop = AddMenuItem(Usermenu, "Navigation starten", $"Startet die Navigation zu {User.Name} ({User.Username})", o =>
                                {
                                    ClientObject.SendMessage("~r~Noch nicht implementiert");
                                });
                            }
                        }
                    }
                }
            };
        }
    }
}