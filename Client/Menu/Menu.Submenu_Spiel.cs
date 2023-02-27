using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using Client.Services;
using DLEA_Lib.Shared.Game;
using DLEA_Lib.Shared.Wardrobe;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private UIMenu Submenu_Spiel_Welt_Wetter;

        private UIMenu Submenu_Spiel_Welt_Zeit;

        private void AddSubmenu_Spiel()
        {
            UIMenu Submenu_Spiel = AddSubMenu(this, "Spiel", "Spiel");

            UIMenu Submenu_Spiel_Welt = AddSubMenu(Submenu_Spiel, "Welt", "Welt");
            Submenu_Spiel_Welt_Wetter = AddSubMenu(Submenu_Spiel_Welt, "Wetter", "Wetter ändern");

            //Submenu_Spiel_Welt_Zeit = MenuPool.AddSubMenu(Submenu_Spiel_Welt, "Zeit", "Zeit ändern");

            foreach (KeyValuePair<string, EnumWeather> kvp in EnumWeatherHelper.GetUserFriendlyNames())
            {
                AddMenuItem(Submenu_Spiel_Welt_Wetter, kvp.Key, $"Wetter umstellen: {kvp.Key}", new Action<UIMenuItem>((item) =>
                {
                    ClientObject.GetService<SyncService>().ChangeWeather(kvp.Value);
                }));
            }

            //List<dynamic> hours = new List<dynamic>();
            //List<dynamic> minutes = new List<dynamic>();

            //for (int i = 0; i < 25; i++)
            //{
            //    hours.Add(i);
            //}
            //for (int i = 0; i < 61; i++)
            //{
            //    minutes.Add(i);s
            //}

            //UIMenuListItem ItemHour = new UIMenuListItem("Stunden", hours, 0);
            //UIMenuListItem ItemMinutes = new UIMenuListItem("Minuten", minutes, 0);

            //Submenu_Spiel_Welt_Zeit.AddItem(ItemHour);
            //Submenu_Spiel_Welt_Zeit.AddItem(ItemMinutes);

            //Submenu_Spiel_Welt_Zeit.OnListSelect += (sender, item, index) =>
            //{
            //    if (item == ItemHour)
            //    {
            //        API.SetClockTime((int)hours[index], API.GetClockMinutes(), 0);
            //    }
            //    else if (item == ItemMinutes)
            //    {
            //        API.SetClockTime(API.GetClockHours(), (int)minutes[index], 0);
            //    }
            //};
        }

        private void OnTick_Submenu_Spiel()
        {
            if (CurrentUser != null && CheckPermission("Menu.Spiel") && Submenu_Spiel_Welt_Wetter != null)
            {
                foreach (UIMenuItem item in Submenu_Spiel_Welt_Wetter.MenuItems)
                {
                    if (CommonFunctions.GetWeather() == EnumWeatherHelper.GetEnumWeather(item.Text))
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