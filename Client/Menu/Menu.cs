using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;

namespace Client.Menu
{
    public partial class MainMenu : BaseMenu
    {
        public MainMenu(ClientObject ClientObject, out Action Tick) : base(ClientObject, out Tick, ApplicationSettings.Name, ApplicationSettings.Name_Long)
        {
        }

        public bool IsAnyMenuOpen { get => MenuPool.IsAnyMenuOpen(); }

        public void Login(StoredUser User)
        {
            this.CurrentUser = User;
            Login();
        }

        public void Login()
        {
            this.Clear();
            if (CurrentUser != null)
            {
                if (CurrentUser.LastPlayedVersion != ApplicationSettings.Version)
                {
                    ClientObject.SendMessage($"~y~\nUpdate erhalten: ~r~{CurrentUser.LastPlayedVersion} ~y~> ~g~{ApplicationSettings.Version}");
                    CurrentUser.LastPlayedVersion = ApplicationSettings.Version;
                }

                CurrentUser.SaveSettings(ClientObject.Services);
                ClientObject.TriggerServerEvent(ServerEvents.DataService_SendPlayerData, ClientObject.ServerID, CurrentUser.GetUserRAW(), false);

                this.Clear();
                MenuPool.CloseAllMenus();
                AddSubmenus();
            }
            else
            {
                this.Clear();
                MenuPool.CloseAllMenus();
                AddSubmenu_Login();
            }
        }

        protected void AddProtectedSubmenu(string Menuname, Action AddMenu)
        {
            bool permission = CheckPermission(Menuname);
            if (permission)
            {
                AddMenu();
            }
        }

        protected override void AddSubmenus()
        {
            AddProtectedSubmenu("Menu.Tools", AddSubmenu_Tools);
            AddProtectedSubmenu("Menu.Aussehen", AddSubmenu_Aussehen);
            AddProtectedSubmenu("Menu.Vehicle", AddSubmenu_Vehicle);
            AddProtectedSubmenu("Menu.Objects", AddSubmenu_Objects);
            AddProtectedSubmenu("Menu.Animationen", AddSubmenu_Animationen);
            AddProtectedSubmenu("Menu.Navigation", AddSubmenu_Navigation);
            //AddProtectedSubmenu("Menu.Kollegen", AddSubmenu_Kollegen); //nicht ausgereift
            AddProtectedSubmenu("Menu.Einstellungen", AddSubmenu_Einstellungen);
            //AddProtectedSubmenu("Menu.Spiel", AddSubmenu_Spiel); //vMenu
            AddProtectedSubmenu("Menu.Spieler", AddSubmenu_Spieler);
            AddProtectedSubmenu("Menu.Dienst", AddSubmenu_Dienst);
            AddSubmenu_Admin();
            AddSubmenu_About();
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

        protected override void InitializeMenu()
        {
            Login(null);
        }

        protected override void OnTick()
        {
            if ((Game.IsControlPressed(0, Control.FrontendRight) && Game.IsControlPressed(0, Control.FrontendAccept) && !MenuPool.IsAnyMenuOpen()) || Game.IsControlJustReleased(0, Control.VehicleCinematicDownOnly))
            {
                Visible = !Visible;
            }
            base.OnTick();

            OnTick_Submenu_Objects();
            OnTick_Submenu_Spiel();
            OnTick_Submenu_Tools();
            OnTick_Submenu_Vehicle();
            OnTick_Submenu_Kollegen();
            OnTick_Submenu_Navigation();
        }
    }
}