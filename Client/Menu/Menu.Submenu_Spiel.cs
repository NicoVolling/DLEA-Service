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
using DLEA_Lib.Shared.Game;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private UIMenu Submenu_Spiel_Welt_Wetter;
        //private UIMenu Submenu_Spiel_Welt_Zeit;

        private void AddSubmenu_Spiel()
        {
            UIMenu Submenu_Spiel = MenuPool.AddSubMenu(this, "Spiel", "Spiel");

            UIMenu Submenu_Spiel_Welt = MenuPool.AddSubMenu(Submenu_Spiel, "Welt", "Welt");
            Submenu_Spiel_Welt_Wetter = MenuPool.AddSubMenu(Submenu_Spiel_Welt, "Wetter", "Wetter ändern");
            //Submenu_Spiel_Welt_Zeit = MenuPool.AddSubMenu(Submenu_Spiel_Welt, "Zeit", "Zeit ändern");

            foreach (KeyValuePair<string, EnumWeather> kvp in EnumWeatherHelper.GetUserFriendlyNames())
            {
                AddMenuItem(Submenu_Spiel_Welt_Wetter, kvp.Key, $"Wetterumstellen: {kvp.Key}", new Action<UIMenuItem>((item) =>
                {
                    ClientObject.GetService<SyncService>().ChangeWeather(kvp.Value);
                }));
            }

            //for(int i = 0; i < 24; i++) 
            //{
            //    AddMenuItem(Submenu_Spiel_Welt_Zeit, String.Format("{0:00}", i) + " Uhr", "Zeit auf " + String.Format("{0:00}", i) + " Uhr setzen", (item) => 
            //    {
            //        API.SetClockTime(i, 0, 0);
            //    });
            //}
        }

        private void OnTick_Submenu_Spiel() 
        {
            if (CurrentUser != null && CheckPermission("Menu.Spiel"))
            {
                foreach (UIMenuItem item in Submenu_Spiel_Welt_Wetter.MenuItems)
                {
                    if (ClientHelper.GetWeather() == EnumWeatherHelper.GetEnumWeather(item.Text))
                    {
                        item.SetLeftBadge(UIMenuItem.BadgeStyle.Tick);
                    }
                    else
                    {
                        item.SetLeftBadge(UIMenuItem.BadgeStyle.None);
                    }
                }
            }
        }
    }
}
