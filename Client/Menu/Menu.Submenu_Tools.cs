using CitizenFX.Core;
using CitizenFX.Core.Native;
using NativeUI;
using System;

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

            AddMenuItem(Submenu_Tools_Vehicle, "Blinker Links", "Blinker Links", new Action<UIMenuItem>((item) =>
            {
                if (Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle is Vehicle CurrentVehicle)
                {
                    int Indicator = API.GetVehicleIndicatorLights(CurrentVehicle.Handle);
                    if (Indicator == 1)
                    {
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 1, false);
                    }
                    else
                    {
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 1, true);
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 0, false);
                    }
                }
            }));

            AddMenuItem(Submenu_Tools_Vehicle, "Blinker Rechts", "Blinker Rechts", new Action<UIMenuItem>((item) =>
            {
                if (Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle is Vehicle CurrentVehicle)
                {
                    int Indicator = API.GetVehicleIndicatorLights(CurrentVehicle.Handle);
                    if (Indicator == 2)
                    {
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 0, false);
                    }
                    else
                    {
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 0, true);
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 1, false);
                    }
                }
            }));

            AddMenuItem(Submenu_Tools_Vehicle, "Warnblinker", "Warnblinker", new Action<UIMenuItem>((item) =>
            {
                if (Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle is Vehicle CurrentVehicle)
                {
                    int Indicator = API.GetVehicleIndicatorLights(CurrentVehicle.Handle);
                    if (Indicator == 3)
                    {
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 1, false);
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 0, false);
                    }
                    else
                    {
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 1, true);
                        API.SetVehicleIndicatorLights(CurrentVehicle.Handle, 0, true);
                    }
                }
            }));

            UIMenu Submenu_Tools_Player = MenuPool.AddSubMenu(Submenu_Tools, "Spieler", "Spieler");

            AddMenuItem(Submenu_Tools_Player, "Schnellreinigung", "Spieler reinigen", (item) =>
            {
                Game.PlayerPed.ClearBloodDamage();
                Game.PlayerPed.ClearLastWeaponDamage();
            });
        }
    }
}