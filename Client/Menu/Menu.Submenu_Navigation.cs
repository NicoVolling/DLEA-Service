﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.Locations;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private List<dynamic> BlipNames = new List<dynamic>() { "Polizeiwache", "Feuerwehrwache", "N.O.O.S.E", "Krankenhaus", "Dienststelle" };

        private int dienststelleblip = -1;

        private void AddSubmenu_Navigation()
        {
            UIMenu Navigation = AddSubMenu(this, "Navigation", "Navigation");

            UIMenuItem DepartmentSetzen = AddMenuItem(Navigation, "Dienststelle", "Setze Dienststelle auf aktuelle Makierung", o =>
            {
                if (API.IsWaypointActive())
                {
                    int blipid = API.GetFirstBlipInfoId(8);
                    Vector3 co = API.GetBlipCoords(blipid);
                    DVector3 Coords = new DVector3(co.X, co.Y, co.Z);
                    ClientObject.CurrentUser.DepartmentCoords = Coords;
                    ClientObject.SendMessage("~g~Dienststelle aktualisiert.");
                }
                else
                {
                    Vector3 Coords = Game.PlayerPed.Position;
                    ClientObject.CurrentUser.DepartmentCoords = new DVector3(Coords.X, Coords.Y, Coords.Z);
                    ClientObject.SendMessage("~g~Dienststelle aktualisiert.");
                }

                if (API.DoesBlipExist(dienststelleblip))
                {
                    API.RemoveBlip(ref dienststelleblip);
                    dienststelleblip = -1;
                }
                dienststelleblip = CommonFunctions.AddBlipForCoord(ClientObject.CurrentUser.DepartmentCoords.Value, 58, 27, 2, "Aktuelle Dienststelle");
            });

            UIMenuItem DepartmentEntfernen = AddMenuItem(Navigation, "Dienststelle entfernen", "Entferne die Dienststelle wieder", o =>
            {
                if (API.DoesBlipExist(dienststelleblip))
                {
                    API.RemoveBlip(ref dienststelleblip);
                    dienststelleblip = -1;
                    ClientObject.CurrentUser.DepartmentCoords = null;
                    ClientObject.SendMessage("~g~Dienststelle entfernt.");
                }
            });

            UIMenuListItem FastNaviDepartment = new UIMenuListItem("Schnellnavi", BlipNames, 0, "Navigiert zur nächstgelegenen Dienststelle");
            Navigation.AddItem(FastNaviDepartment);
            Navigation.OnListSelect += Navigation_OnListSelect;

            AddMenuItem(Navigation, "Navigation beenden", "Navigation beenden", o => { API.ClearGpsPlayerWaypoint(); API.DeleteWaypoint(); });
        }

        private void Navigation_OnListSelect(UIMenu sender, UIMenuListItem listItem, int newIndex)
        {
            string name = BlipNames[newIndex];

            float distance = -1;
            Vector3 Coords = new Vector3();

            if (name == "Dienststelle" && ClientObject.CurrentUser.DepartmentCoords.HasValue)
            {
                Coords = new Vector3(ClientObject.CurrentUser.DepartmentCoords.Value.X, ClientObject.CurrentUser.DepartmentCoords.Value.Y, ClientObject.CurrentUser.DepartmentCoords.Value.Z);
            }
            else
            {
                foreach (Location Location in Location.List.Where(o => o.name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    if (distance == -1)
                    {
                        Vector3 coords = new Vector3(Location.coordinates.X, Location.coordinates.Y, Location.coordinates.Z);
                        distance = API.CalculateTravelDistanceBetweenPoints(coords.X, coords.Y, coords.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z);
                        Coords = coords;
                    }
                    else
                    {
                        Vector3 coords = new Vector3(Location.coordinates.X, Location.coordinates.Y, Location.coordinates.Z);
                        float dist = API.CalculateTravelDistanceBetweenPoints(coords.X, coords.Y, coords.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z);
                        if (dist < distance)
                        {
                            distance = dist;
                            Coords = coords;
                        }
                    }
                }
            }

            API.SetNewWaypoint(Coords.X, Coords.Y);
            ClientObject.SendMessage("~g~Navigation gestartet.");
        }
    }
}