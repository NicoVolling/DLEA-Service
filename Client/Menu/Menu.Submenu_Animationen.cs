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
using Client.Objects;
using Client.ClientHelper;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private UIMenu Submenu_Animationen;

        private void AddSubmenu_Animationen()
        {
            Submenu_Animationen = MenuPool.AddSubMenu(this, "Animationen", "Animationen");

            UIMenuItem uIMenuItem = AddMenuItem(Submenu_Animationen, "Sofort stoppen", "Animation sofort stoppen", o => 
            {
                CommonFunctions.PlayScenario(Game.PlayerPed, "forcestop");
            });

            Dictionary<string, UIMenu> CatMenus = new Dictionary<string, UIMenu>();

            foreach(var category in PedScenarios.ScenarioCategorized) 
            {
                if(!CatMenus.ContainsKey(category.Key)) 
                {
                    CatMenus.Add(category.Key, MenuPool.AddSubMenu(Submenu_Animationen, category.Key, $"{category.Key} ({category.Value.Count})"));
                }
                UIMenu categoryMenu = CatMenus[category.Key];

                foreach(var item in category.Value) 
                {
                    UIMenuItem playanim = AddMenuItem(categoryMenu, item.Key, item.Key + " abspielen", o => 
                    {
                        CommonFunctions.PlayScenario(Game.PlayerPed, item.Value);
                    });
                }
            }
        }
    }
}
