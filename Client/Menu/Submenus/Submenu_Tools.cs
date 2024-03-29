﻿using CitizenFX.Core.Native;
using CitizenFX.Core;
using Client.ClientHelper;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Objects.CommonVehicle;

namespace Client.Menu.Submenus
{
    internal class Submenu_Tools : MenuBase
    {
        public Submenu_Tools(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Tools";

        private UIMenu Submenu_Tool_Vehicle_Doors;

        protected override void InitializeMenu(UIMenu Menu)
        {
            UIMenu Submenu_Tools = AddSubMenu(Menu, "Tools", "Tools");
            addSubMenuTools_Vehicles(Submenu_Tools);
            addSubMenuTools_Player(Submenu_Tools);
        }

        private void addSubMenuTools_Player(UIMenu Submenu_Tools)
        {
            UIMenu Submenu_Tools_Player = AddSubMenu(Submenu_Tools, "Spieler", "Spieler");

            AddMenuItem(Submenu_Tools_Player, "Schnellreinigung", "Spieler reinigen", (item) =>
            {
                Game.PlayerPed.ClearBloodDamage();
                Game.PlayerPed.ClearLastWeaponDamage();
            });

            AddMenuItem(Submenu_Tools_Player, "Heilung + Panzerung", "Heilen, Reinigen und Panzerung geben", (item) =>
            {
                Game.PlayerPed.Health = 200;
                Game.PlayerPed.Armor = 100;
                Game.PlayerPed.ClearBloodDamage();
                Game.PlayerPed.ClearLastWeaponDamage();
            });
        }

        private void addSubMenuTools_Vehicles(UIMenu Submenu_Tools)
        {
            UIMenu Submenu_Tools_Vehicle = AddSubMenu(Submenu_Tools, "Fahrzeug", "Fahrzeug");

            AddMenuItem(Submenu_Tools_Vehicle, "Schnellreperatur", "Fahrzeug reparieren", new Action<UIMenuItem>((item) =>
            {
                Game.PlayerPed.CurrentVehicle?.Repair();
            }));

            AddMenuItem(Submenu_Tools_Vehicle, "Schnellreinigung", "Fahrzeug reinigen", new Action<UIMenuItem>((item) =>
            {
                Game.PlayerPed.CurrentVehicle?.Wash();
            }));

            AddMenuItem(Submenu_Tools_Vehicle, "Motor (an/aus)", "Motor ein- oder ausschalten", new Action<UIMenuItem>((item) =>
            {
                if (Game.PlayerPed.CurrentVehicle != null)
                {
                    Game.PlayerPed.CurrentVehicle.IsEngineRunning = !Game.PlayerPed.CurrentVehicle.IsEngineRunning;
                }
            }));

            Submenu_Tool_Vehicle_Doors = AddSubMenu(Submenu_Tools_Vehicle, "Türen", "Türen");

            foreach (KeyValuePair<KeyValuePair<VehicleDoorIndex, string>, string> KVP in VehicleDoors.Doors)
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

        protected override void OnTick()
        {
            if (Submenu_Tool_Vehicle_Doors != null)
            {
                foreach (UIMenuItem menuitem in Submenu_Tool_Vehicle_Doors.MenuItems)
                {
                    KeyValuePair<KeyValuePair<VehicleDoorIndex, string>, string> KVP = VehicleDoors.Doors.First(o => o.Value == menuitem.Text);
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
    }
}
