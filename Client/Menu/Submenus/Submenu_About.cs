using DLEA_Lib.Shared.Application;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Menu.Submenus
{
    internal class Submenu_About : MenuBase
    {
        public Submenu_About(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Infos";

        protected override void InitializeMenu(UIMenu Menu)
        {
            AddMenuItem(Menu, $"{ApplicationSettings.Name}");
            AddMenuItem(Menu, $"{ApplicationSettings.Name_Long}");
            AddMenuItem(Menu, $"Version {ApplicationSettings.Version}");
            AddMenuItem(Menu, $"Copyright {DateTime.Now.Year}");
            AddMenuItem(Menu, $"Urheber {ApplicationSettings.Creator}");
        }

        protected override void OnTick()
        {
        }
    }
}
