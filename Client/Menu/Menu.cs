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
    public partial class MainMenu : BaseMenu
    {
        public MainMenu(ClientObject ClientObject, out Action Tick) : base(ClientObject, out Tick, ApplicationSettings.Name, ApplicationSettings.Name_Long)
        {
        }

        protected override void InitializeMenu()
        {
            Login(null);
        }

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
            try
            {
                AddProtectedSubmenu("Menu.Tools", AddSubmenu_Tools);
                AddProtectedSubmenu("Menu.Aussehen", AddSubmenu_Aussehen);
                AddProtectedSubmenu("Menu.Dienst", AddSubmenu_Dienst);
                AddProtectedSubmenu("Menu.Vehicle", AddSubmenu_Vehicle);
                AddProtectedSubmenu("Menu.Einsatz", AddSubmenu_Einsatz);
                AddProtectedSubmenu("Menu.Telefon", AddSubmenu_Telefon);
                AddProtectedSubmenu("Menu.Objects", AddSubmenu_Objects);
                AddProtectedSubmenu("Menu.Animationen", AddSubmenu_Animationen);
                AddProtectedSubmenu("Menu.Interaktion", AddSubmenu_Interaktion);
                AddProtectedSubmenu("Menu.Navigation", AddSubmenu_Navigation);
                AddProtectedSubmenu("Menu.Einstellungen", AddSubmenu_Einstellungen);
                AddProtectedSubmenu("Menu.Spiel", AddSubmenu_Spiel);
                AddProtectedSubmenu("Menu.Spieler", AddSubmenu_Spieler);
                AddSubmenu_Admin();
                AddSubmenu_About();
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        protected override void OnTick()
        {
            if ((Game.IsControlPressed(0, Control.FrontendRight) && Game.IsControlPressed(0, Control.FrontendAccept) && !MenuPool.IsAnyMenuOpen()) || Game.IsControlPressed(0, Control.VehicleCinematicDownOnly))
            {
                Visible = !Visible;
            }
            base.OnTick();

            OnTick_Submenu_Objects();
            OnTick_Submenu_Interkation();
            OnTick_Submenu_Spiel();
        }

        public bool IsAnyMenuOpen { get => MenuPool.IsAnyMenuOpen(); }
    }
}