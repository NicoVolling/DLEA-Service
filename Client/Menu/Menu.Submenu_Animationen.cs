using CitizenFX.Core;
using Client.ClientHelper;
using Client.Objects;
using NativeUI;
using System.Collections.Generic;

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

            foreach (var category in PedScenarios.ScenarioCategorized)
            {
                if (!CatMenus.ContainsKey(category.Key))
                {
                    CatMenus.Add(category.Key, MenuPool.AddSubMenu(Submenu_Animationen, category.Key, $"{category.Key} ({category.Value.Count})"));
                }
                UIMenu categoryMenu = CatMenus[category.Key];

                foreach (var item in category.Value)
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