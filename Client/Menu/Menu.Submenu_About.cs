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
    public partial class MainMenu
    {
        private void AddSubmenu_About()
        {
            UIMenu MenuAbout = MenuPool.AddSubMenu(this, "Infos", $"Infos über {ApplicationSettings.Name_Long}");
            AddMenuItem(MenuAbout, $"{ApplicationSettings.Name}");
            AddMenuItem(MenuAbout, $"{ApplicationSettings.Name_Long}");
            AddMenuItem(MenuAbout, $"Version {ApplicationSettings.Version}");
            AddMenuItem(MenuAbout, $"Copyright {DateTime.Now.Year}");
            AddMenuItem(MenuAbout, $"Urheber {ApplicationSettings.Creator}");
        }
    }
}
