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
        private Func<UIMenu> getPlayerMenu { get; set; }
        private void AddSubmenu_Spieler()
        {
            UIMenu Menu_Players = MenuPool.AddSubMenu(this, "Spieler", "Spieler");
            getPlayerMenu = () => { return Menu_Players; };
        }
    }
}
