using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.Menu.Submenus;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Menu
{
    public partial class MainMenu : MainMenuBase
    {
        public MainMenu(ClientObject ClientObject, out Action Tick) : base(ClientObject, out Tick, ApplicationSettings.Name, ApplicationSettings.Name_Long)
        {
        }

        protected void AddProtectedSubmenu(string Menuname, List<MenuBase> List, MenuBase Menu)
        {
            bool permission = CheckPermission(Menuname);
            if (permission)
            {
                List.Add(Menu);
            }
        }

        protected override List<MenuBase> InitializeSubmenus()
        {
            List<MenuBase> list = new List<MenuBase>();

            AddProtectedSubmenu("Menu.Tools", list, new Submenu_Tools(ClientObject, MenuPool, this));
            AddProtectedSubmenu("Menu.Outfits", list, new Submenu_Outfits(ClientObject, MenuPool, this));
            AddProtectedSubmenu("Menu.VehicleSpawner", list, new Submenu_VehicleSpawner(ClientObject, MenuPool, this));
            AddProtectedSubmenu("Menu.Animations", list, new Submenu_Animations(ClientObject, MenuPool, this));
            AddProtectedSubmenu("Menu.Navigation", list, new Submenu_Navigation(ClientObject, MenuPool, this));
            AddProtectedSubmenu("Menu.ServiceSettings", list, new Submenu_ServiceSettings(ClientObject, MenuPool, this));
            AddProtectedSubmenu("Menu.PlayerList", list, new Submenu_PlayerList(ClientObject, MenuPool, this));

            if(CurrentUser != null && CurrentUser.Admin)
            {
                list.Add(new Submenu_Admin(ClientObject, MenuPool, this));
            }

            list.Add(new Submenu_About(ClientObject, MenuPool, this));

            return list;
        }

        public void ApplyRoutes(List<DLEA_Lib.Shared.Navigation.Route> RouteList)
        {
            try
            {
                Submenus.Where(o => o is Submenu_Navigation).Select(o => o as Submenu_Navigation).FirstOrDefault()?.ApplyRoutes(RouteList);
            }
            catch (Exception ex) { ClientObject.Trace(ex.ToString()); }
        }

        public void RefreshUserList(List<ExtendedUser> UserList)
        {
            try
            {
                (Submenus.Where(o => o is Submenu_PlayerList).FirstOrDefault() as Submenu_PlayerList)?.RefreshUserList?.Invoke(UserList);
                (Submenus.Where(o => o is Submenu_Admin).FirstOrDefault() as Submenu_Admin)?.RefreshUserList?.Invoke(UserList);
            } 
            catch(Exception ex) { ClientObject.Trace(ex.ToString()); }
        }

        protected bool CheckPermission(string Menuname, bool ExplicitOption = false)
        {
            bool hasPermission = false;
            if (CurrentUser != null && CurrentUser.Admin)
            {
                return true;
            }
            if (CurrentUser != null && CurrentUser.Permissions != null && CurrentUser.Permissions.Menus is List<string> Menus)
            {
                if (Menus.Contains("*") && !ExplicitOption)
                {
                    hasPermission = true;
                }
                if (!ExplicitOption && Menuname.Contains(".") && Menuname.Split('.') is string[] Menuparts && Menuparts.Length > 1 && Menuparts[0] != "*")
                {
                    if (CheckPermission($"{Menuparts[Menuparts.Length - 1]}.*"))
                    {
                        hasPermission = true;
                    }
                }
                if (Menus.Contains(Menuname))
                {
                    hasPermission = true;
                }
                if (Menus.Contains($"-{Menuname}"))
                {
                    hasPermission = false;
                }
            }
            return hasPermission;
        }

        protected override void OnTick()
        {
            if ((Game.IsControlPressed(0, Control.FrontendRight) && Game.IsControlPressed(0, Control.FrontendAccept) && !MenuPool.IsAnyMenuOpen()) || Game.IsControlJustReleased(0, Control.VehicleCinematicDownOnly))
            {
                Visible = !Visible;
            }
            base.OnTick();
        }
    }
}