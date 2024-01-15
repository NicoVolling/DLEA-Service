using CitizenFX.Core.Native;
using Client.Menu.Submenus;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;

namespace Client.Menu
{
    public abstract class MainMenuBase : UIMenu
    {
        public StoredUser CurrentUser = null;

        public bool IsAnyMenuOpen { get => MenuPool.IsAnyMenuOpen(); }

        protected List<MenuBase> Submenus { get; set; } = new List<MenuBase>();

        public MainMenuBase(ClientObject ClientObject, out Action Tick, string Title, string Subtitle) : base(Title, Subtitle, false)
        {
            this.ClientObject = ClientObject;
            if (MenuPool == null)
            {
                MenuPool = new MenuPool();
            }
            MenuPool.Add(this);

            MenuPool.ControlDisablingEnabled = false;
            MenuPool.MouseEdgeEnabled = false;
            ResetCursorOnOpen = false;

            InitializeMenu();

            ClientObject.Trace($"Initialized Menu: {Title}");

            Tick = OnTick;
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
                Submenus = InitializeSubmenus();
                InitializeMenu();
            }
            else
            {
                this.Clear();
                MenuPool.CloseAllMenus();
                new Submenu_Login(ClientObject, MenuPool, this).AttachToParent(this);
            }
        }

        public void Login(StoredUser User)
        {
            this.CurrentUser = User;
            Login();
        }

        protected ClientObject ClientObject { get; }

        protected abstract List<MenuBase> InitializeSubmenus();

        protected virtual void InitializeMenu()
        {
            foreach(MenuBase Menu in Submenus)
            {
                Menu.AttachToParent(this);
            }
        }

        protected virtual void OnTick()
        {
            if (!(API.UpdateOnscreenKeyboard() == 0))
            {
                API.Wait(500);
                MenuPool.ProcessMenus();
            }

            foreach (MenuBase menu in Submenus)
            {
                try
                {
                    menu.Tick();
                }
                catch (Exception ex) { ClientObject.Trace(ex.ToString()); }
            }
        }

        #region Static Properties

        protected static MenuPool MenuPool;

        #endregion Static Properties
    }
}