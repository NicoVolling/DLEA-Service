using DLEA_Lib.Shared.Application;
using NativeUI;
using System;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private void AddSubmenu_About()
        {
            UIMenu MenuAbout = AddSubMenu(this, "Infos", $"Infos über {ApplicationSettings.Name_Long}");
            AddMenuItem(MenuAbout, $"{ApplicationSettings.Name}");
            AddMenuItem(MenuAbout, $"{ApplicationSettings.Name_Long}");
            AddMenuItem(MenuAbout, $"Version {ApplicationSettings.Version}");
            AddMenuItem(MenuAbout, $"Copyright {DateTime.Now.Year}");
            AddMenuItem(MenuAbout, $"Urheber {ApplicationSettings.Creator}");
        }
    }
}