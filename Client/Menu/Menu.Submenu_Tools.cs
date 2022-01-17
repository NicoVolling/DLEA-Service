﻿using CitizenFX.Core;
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

        private void AddSubmenu_Tools()
        {
            UIMenu Submenu_Tools = MenuPool.AddSubMenu(this, "Tools", "Tools");

            UIMenu Submenu_Tools_Vehicle = MenuPool.AddSubMenu(Submenu_Tools, "Fahrzeug", "Fahrzeug");

            AddMenuItem(Submenu_Tools_Vehicle, "Schnellreperatur", "Fahrzeug reparieren", new Action<UIMenuItem>((item) =>
            {
                Game.PlayerPed.CurrentVehicle?.Repair();
            }));

            //AddMenuItem(Submenu_Tools_Vehicle, "Blinker Links", "Blinker Links", new Action<UIMenuItem>((item) =>
            //{
            //    if(Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle is Vehicle CurrentVehicle) 
            //    {
            //        CurrentVehicle.IsLeftIndicatorLightOn = !CurrentVehicle.IsLeftIndicatorLightOn;
            //    }
            //}));

            //AddMenuItem(Submenu_Tools_Vehicle, "Blinker Rechts", "Blinker Rechts", new Action<UIMenuItem>((item) =>
            //{
            //    if (Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle is Vehicle CurrentVehicle)
            //    {
            //        CurrentVehicle.IsRightIndicatorLightOn = !CurrentVehicle.IsRightIndicatorLightOn;
            //    }
            //}));

            //AddMenuItem(Submenu_Tools_Vehicle, "Warnblinker", "Warnblinker", new Action<UIMenuItem>((item) =>
            //{
            //    if (Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle is Vehicle CurrentVehicle)
            //    {
            //        CurrentVehicle.IsLeftIndicatorLightOn = !(CurrentVehicle.IsLeftIndicatorLightOn && CurrentVehicle.IsRightIndicatorLightOn);
            //        CurrentVehicle.IsRightIndicatorLightOn = !(CurrentVehicle.IsLeftIndicatorLightOn && CurrentVehicle.IsRightIndicatorLightOn);
            //    }
            //}));

            UIMenu Submenu_Tools_Player = MenuPool.AddSubMenu(Submenu_Tools, "Spieler", "Spieler");

            AddMenuItem(Submenu_Tools_Player, "Schnellreinigung", "Spieler reinigen", (item) => 
            { 
                Game.PlayerPed.ClearBloodDamage(); 
                Game.PlayerPed.ClearLastWeaponDamage();
            });
        }
    }
}
