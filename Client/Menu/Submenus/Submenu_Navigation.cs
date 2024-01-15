using CitizenFX.Core.Native;
using CitizenFX.Core;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.Navigation;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ClientHelper;
using System.Threading;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Locations;

namespace Client.Menu.Submenus
{
    internal class Submenu_Navigation : MenuBase
    {
        private List<dynamic> BlipNames = new List<dynamic>() { "Polizeiwache", "Feuerwehrwache", "N.O.O.S.E", "Krankenhaus", "Dienststelle" };

        private int dienststelleblip = -1;

        private Route currentRouteEdit = new Route();

        private UIMenu Navigation_RouteListMenu;

        private UIMenu stopsMenu = null;

        private Dictionary<DVector3, int> BlipIdsForRouteStops = new Dictionary<DVector3, int>();

        private Route currentNavigationRoute = null;

        private DVector3? currentNavigationRouteStop = null;

        private bool currentNavigationRouteAbort = false;

        Stack<int> BlipsToRemove = new Stack<int>();

        private int currentNavigationRouteBlip = -1;

        private int currentNavigationRouteIndex = 0;

        public Submenu_Navigation(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Navigation";

        protected override void InitializeMenu(UIMenu Menu)
        {
            UIMenuItem DepartmentSetzen = AddMenuItem(Menu, "Dienststelle", "Setze Dienststelle auf aktuelle Makierung", o =>
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

            UIMenuItem DepartmentEntfernen = AddMenuItem(Menu, "Dienststelle entfernen", "Entferne die Dienststelle wieder", o =>
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
            Menu.AddItem(FastNaviDepartment);
            Menu.OnListSelect += Navigation_OnListSelect;

            AddMenuItem(Menu, "Navigation beenden", "Navigation beenden", o => { API.ClearGpsPlayerWaypoint(); API.DeleteWaypoint(); });

            Navigation_Routes_AddSubMenu(Menu);
        }

        protected override void OnTick()
        {
            while (BlipsToRemove.Count > 0)
            {
                int blip = BlipsToRemove.Pop();

                API.RemoveBlip(ref blip);
            }

            if (currentNavigationRouteAbort)
            {
                BlipsToRemove.Push(currentNavigationRouteBlip);

                currentNavigationRoute = null;
                currentNavigationRouteBlip = -1;
                currentNavigationRouteIndex = 0;
                currentNavigationRouteStop = null;
                currentNavigationRouteAbort = false;
            }

            if (currentNavigationRoute != null && currentNavigationRoute.Stops.Count > 0)
            {
                if (currentNavigationRouteStop == null)
                {
                    currentNavigationRouteStop = currentNavigationRoute.Stops[currentNavigationRouteIndex];

                    currentNavigationRouteBlip = AddBlipForStop(currentNavigationRouteStop.Value, 162, 48, currentNavigationRoute);

                    API.SetBlipRoute(currentNavigationRouteBlip, true);
                }

                if (API.GetDistanceBetweenCoords(currentNavigationRouteStop.Value.X, currentNavigationRouteStop.Value.Y, currentNavigationRouteStop.Value.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z, true) < 50)
                {

                    BlipsToRemove.Push(currentNavigationRouteBlip);

                    currentNavigationRouteIndex++;
                    if (currentNavigationRoute.Stops.Count > currentNavigationRouteIndex)
                    {
                        currentNavigationRouteStop = currentNavigationRoute.Stops[currentNavigationRouteIndex];

                        currentNavigationRouteBlip = AddBlipForStop(currentNavigationRouteStop.Value, 162, 48, currentNavigationRoute);

                        API.SetBlipRoute(currentNavigationRouteBlip, true);
                    }
                    else
                    {
                        BlipsToRemove.Push(currentNavigationRouteBlip);

                        currentNavigationRoute = null;
                        currentNavigationRouteBlip = -1;
                        currentNavigationRouteIndex = 0;
                        currentNavigationRouteStop = null;
                    }
                }
            }
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

        private void Navigation_Routes_AddSubMenu(UIMenu NavigationMenu)
        {
            UIMenu RoutesMenu = AddSubMenu(NavigationMenu, "Routen", "Routen");
            Navigation_RouteListMenu = AddSubMenu(RoutesMenu, "Alle Routen", "Alle Routen");
            Navigation_Route_NewRoute_AddSubMenu(RoutesMenu);
        }

        private void Navigation_Route_NewRoute_AddSubMenu(UIMenu RoutesMenu)
        {
            UIMenu NewRoutesMenu = AddSubMenu(RoutesMenu, "Neue Route", "Neue Route");

            var name = AddMenuTextItem(NewRoutesMenu, "Bezeichnung", "Bezeichnung", o => { currentRouteEdit.Name = o; });

            AddMenuItem(NewRoutesMenu, "Änderungen verwerfen", "Änderungen verwerfen", o => { currentRouteEdit.Stops.Clear(); currentRouteEdit = new Route(); name.Description = "Eingabe: "; stopsMenu?.MenuItems.Clear(); Navigation_Route_NewRoute_Stops_Init(stopsMenu); ClientObject.TriggerServerEvent(ServerEvents.RoutesService_RequestRouteList, ClientObject.CurrentUser.ServerID); });

            AddMenuItem(NewRoutesMenu, "Änderungen speichern", "Änderungen speichern", o => { ClientObject.TriggerServerEvent(ServerEvents.RoutesService_SendRoute, Json.Serialize(currentRouteEdit)); currentRouteEdit.Stops.Clear(); currentRouteEdit = new Route(); name.Description = "Eingabe: "; stopsMenu?.MenuItems.Clear(); Navigation_Route_NewRoute_Stops_Init(stopsMenu); });

            stopsMenu = AddSubMenu(NewRoutesMenu, "Stops", "Stops");

            Navigation_Route_NewRoute_Stops_Init(stopsMenu);
        }

        private void Navigation_Route_NewRoute_Stops_Init(UIMenu stopsMenu)
        {
            BlipIdsForRouteStops.Clear();
            AddMenuItem(stopsMenu, "Stop hinzufügen", "Stop hinzufügen", o =>
            {
                AddStop(stopsMenu, Game.IsWaypointActive ? API.GetBlipCoords(API.GetFirstBlipInfoId(8)).ToDVector3() : Game.PlayerPed.Position.ToDVector3());
            });

            foreach (DVector3 stop in currentRouteEdit.Stops)
            {
                AddStop(stopsMenu, stop, null, false);
            }
        }

        private void AddStop(UIMenu stopsMenu, DVector3 Position, DVector3? AfterStop = null, bool ModifyCollection = true)
        {
            if (ModifyCollection)
            {
                if (AfterStop != null)
                {
                    currentRouteEdit.Stops.Insert(currentRouteEdit.Stops.IndexOf(AfterStop.Value) + 1, Position);
                }
                else
                {
                    currentRouteEdit.Stops.Add(Position);
                }
            }

            AddBlipForStop(Position, 161, 48, currentRouteEdit);

            UIMenu stopMenu = AddSubMenu(stopsMenu, $"{Position.X.ToString("N2")} | {Position.Y.ToString("N2")} | {Position.Z.ToString("N2")}", $"{CommonFunctions.GetStreetLocation(Position.ToVector3())}");
            AddMenuItem(stopMenu, "Stop Löschen", "Stop Löschen", o => { currentRouteEdit.Stops.Remove(Position); stopsMenu.MenuItems.Remove(stopMenu.ParentItem); });
            AddMenuItem(stopMenu, "Stop hiernach hinzufügen", "Stop hiernach hinzufügen", o => AddStop(stopsMenu, Game.PlayerPed.Position.ToDVector3()));
        }

        public void ApplyRoutes(List<Route> RouteList)
        {
            if (Navigation_RouteListMenu != null)
            {
                Navigation_RouteListMenu.MenuItems.Clear();
                foreach (Route route in RouteList)
                {
                    UIMenu routeMenu = AddSubMenu(Navigation_RouteListMenu, route.Name, route.Name);

                    AddMenuItem(routeMenu, "Route starten", "Route starten", o => { currentNavigationRoute = route; });
                    AddMenuItem(routeMenu, "Route abbrechen", "Route abbrechen", o => { currentNavigationRouteAbort = true; });
                    AddMenuItem(routeMenu, "Route bearbeiten", "Route bearbeiten", o => { currentRouteEdit = route; stopsMenu?.MenuItems?.Clear(); Navigation_Route_NewRoute_Stops_Init(stopsMenu); });
                    AddMenuItem(routeMenu, "Route löschen", "Route löschen", o => { ClientObject.TriggerServerEvent(ServerEvents.RoutesService_DeleteRoute, route.Name); });
                }
            }
        }

        private int AddBlipForStop(DVector3 Position, int Sprite, int Color, Route route)
        {
            int blip = CommonFunctions.AddBlipForCoord(Position, Sprite, Color, 2, "");
            API.SetBlipAsShortRange(blip, true);

            Timer t = null;
            t = new Timer(o =>
            {
                if (!route.Stops.Contains(Position))
                {
                    BlipsToRemove.Push(blip);
                    t?.Change(Timeout.Infinite, Timeout.Infinite);
                }
            }, null, 1000, 1000);

            return blip;
        }
    }
}
