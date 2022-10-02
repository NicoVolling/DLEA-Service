using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.ClientHelper;
using Client.Objects.CommonVehicle;
using Client.Services;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private Dictionary<KeyValuePair<VehicleDoorIndex, string>, string> Doors = new Dictionary<KeyValuePair<VehicleDoorIndex, string>, string>()
            {
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.BackLeftDoor, "door_dside_r" ), "Hinten Links" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.BackRightDoor, "door_pside_r"), "Hinten Rechts" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.FrontLeftDoor, "door_dside_f"), "Vorne Links" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.FrontRightDoor, "door_pside_f"), "Vorne Rechts" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.Hood, "bonnet"), "Motorhaube" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.Trunk, "boot"), "Kofferraum" }
            };

        private UIMenu Submenu_Tool_Vehicle_Doors;

        protected void OnTick_Submenu_Tools()
        {
            if (Submenu_Tool_Vehicle_Doors != null)
            {
                foreach (UIMenuItem menuitem in Submenu_Tool_Vehicle_Doors.MenuItems)
                {
                    KeyValuePair<KeyValuePair<VehicleDoorIndex, string>, string> KVP = Doors.First(o => o.Value == menuitem.Text);
                    Vehicle Vehicle = GetVehicle(false, KVP.Key.Key, KVP.Key.Value, KVP.Value);
                    if (Vehicle != null)
                    {
                        if (Vehicle.Doors[KVP.Key.Key].IsBroken)
                        {
                            menuitem.SetRightLabel("Defekt");
                        }
                        else
                        {
                            if (Vehicle.Doors[KVP.Key.Key].IsOpen)
                            {
                                menuitem.SetRightLabel("Offen");
                            }
                            else
                            {
                                menuitem.SetRightLabel("Geschlossen");
                            }
                        }
                    }
                    else
                    {
                        menuitem.SetRightLabel("Nicht erreichbar");
                    }
                }
            }
        }

        private void AddSubmenu_Tools()
        {
            UIMenu Submenu_Tools = AddSubMenu(this, "Tools", "Tools");
            UIMenu Submenu_Tools_Vehicle = AddSubMenu(Submenu_Tools, "Fahrzeug", "Fahrzeug");

            AddMenuItem(Submenu_Tools_Vehicle, "Schnellreperatur", "Fahrzeug reparieren", new Action<UIMenuItem>((item) =>
            {
                Game.PlayerPed.CurrentVehicle?.Repair();
            }));

            AddMenuItem(Submenu_Tools_Vehicle, "Motor (an/aus)", "Motor ein- oder ausschalten", new Action<UIMenuItem>((item) =>
            {
                if (Game.PlayerPed.CurrentVehicle != null)
                {
                    Game.PlayerPed.CurrentVehicle.IsEngineRunning = !Game.PlayerPed.CurrentVehicle.IsEngineRunning;
                }
            }));

            Submenu_Tool_Vehicle_Doors = AddSubMenu(Submenu_Tools_Vehicle, "Türen", "Türen");

            foreach (KeyValuePair<KeyValuePair<VehicleDoorIndex, string>, string> KVP in Doors)
            {
                AddMenuItem(Submenu_Tool_Vehicle_Doors, KVP.Value, $"{KVP.Value} umschalten", new Action<UIMenuItem>((item) =>
                {
                    Vehicle Vehicle = GetVehicle(true, KVP.Key.Key, KVP.Key.Value, KVP.Value);

                    if (Vehicle != null)
                    {
                        if (Vehicle.Doors[KVP.Key.Key].IsOpen)
                        {
                            Vehicle.Doors[KVP.Key.Key].Close();
                        }
                        else
                        {
                            Vehicle.Doors[KVP.Key.Key].Open();
                        }
                    }
                }));
            }

            {
                UIMenuItem Item = AddMenuItem(Submenu_Tools_Vehicle, "Fahrzeug löschen", "Fahrzeug löschen", new Action<UIMenuItem>((item) =>
                {
                    if (Game.PlayerPed.CurrentVehicle != null)
                    {
                        Game.PlayerPed.CurrentVehicle.Delete();
                    }
                }));
                Item.SetRightBadge(UIMenuItem.BadgeStyle.Alert);
            }

            UIMenu Submenu_Tools_Player = AddSubMenu(Submenu_Tools, "Spieler", "Spieler");

            AddMenuItem(Submenu_Tools_Player, "Schnellreinigung", "Spieler reinigen", (item) =>
            {
                Game.PlayerPed.ClearBloodDamage();
                Game.PlayerPed.ClearLastWeaponDamage();
            });
        }

        private Vehicle GetVehicle(bool ShowMessages, VehicleDoorIndex DoorIndex, string BoneIndexName, string UserfriendlyName)
        {
            Vehicle Vehicle;
            if (Game.PlayerPed.CurrentVehicle != null)
            {
                Vehicle = Game.PlayerPed.CurrentVehicle;
            }
            else
            {
                Vehicle = CitizenFX.Core.World.GetAllVehicles().Select(o => new KeyValuePair<Vehicle, float>(o, CommonFunctions.DistanceToPlayer(API.GetWorldPositionOfEntityBone(o.Handle, API.GetEntityBoneIndexByName(o.Handle, BoneIndexName)), Game.PlayerPed.Position))).OrderBy(o => o.Value).FirstOrDefault(o => o.Value < 3).Key;
            }

            if (Vehicle != null)
            {
                if (Vehicle.Doors.HasDoor(DoorIndex))
                {
                    if (!Game.PlayerPed.IsInVehicle(Vehicle) || (Game.PlayerPed.IsInVehicle(Vehicle) && Game.PlayerPed.SeatIndex == VehicleSeat.Driver))
                    {
                        return Vehicle;
                    }
                    else
                    {
                        if (ShowMessages)
                        {
                            ClientObject.SendMessage("~r~Du bist nicht Fahrer des Fahrzeugs");
                        }
                    }
                }
                else
                {
                    if (ShowMessages)
                    {
                        ClientObject.SendMessage($"~r~Diese Fahrzeug hat die Türe nicht: {UserfriendlyName}");
                    }
                }
            }
            else
            {
                if (ShowMessages)
                {
                    ClientObject.SendMessage("~r~Es ist kein Fahrzeug in Reichweite");
                }
            }
            return null;
        }
    }
}