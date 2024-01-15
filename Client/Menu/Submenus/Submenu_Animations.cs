using CitizenFX.Core;
using Client.ClientHelper;
using Client.Objects;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Menu.Submenus
{
    internal class Submenu_Animations : MenuBase
    {
        public Submenu_Animations(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Animationen";

        protected override void InitializeMenu(UIMenu Menu)
        {
            UIMenuItem uIMenuItem = AddMenuItem(Menu, "Sofort stoppen", "Animation sofort stoppen", o =>
            {
                CommonFunctions.PlayScenario(Game.PlayerPed, "forcestop");
            });

            Dictionary<string, UIMenu> CatMenus = new Dictionary<string, UIMenu>();

            foreach (KeyValuePair<string, Dictionary<string, string>> category in PedScenarios.ScenarioCategorized)
            {
                if (!CatMenus.ContainsKey(category.Key))
                {
                    CatMenus.Add(category.Key, AddSubMenu(Menu, category.Key, $"{category.Key} ({category.Value.Count})"));
                }
                UIMenu categoryMenu = CatMenus[category.Key];

                foreach (KeyValuePair<string, string> item in category.Value)
                {
                    UIMenuItem playanim = AddMenuItem(categoryMenu, item.Key, item.Key + " abspielen", o =>
                    {
                        CommonFunctions.PlayScenario(Game.PlayerPed, item.Value);
                    });
                }
            }
        }

        protected override void OnTick()
        {
        }
    }
}
