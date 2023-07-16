using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private static string[] ModelNames = new string[] { "prop_barrier_work05", "prop_roadcone02a", "prop_roadcone01b", "prop_fire_exting_3a", "prop_worklight_02a", "prop_ld_health_pack" };

        private UIMenu SubmenuObjects;

        private bool SubmenuObjects_Add_Open = false;

        private bool SubmenuObjects_Open = false;

        private void AddSubmenu_Objects()
        {
            SubmenuObjects = AddSubMenu(this, "Verkehr", $"Absperrungen, Manager");

            SubmenuObjects.OnMenuOpen += (menu) =>
            {
                if (menu == SubmenuObjects)
                {
                    SubmenuObjects_Open = true;
                }
            };
            SubmenuObjects.OnMenuClose += (menu) =>
            {
                if (menu == SubmenuObjects)
                {
                    SubmenuObjects_Open = false;
                }
            };

            UIMenu MenuSpawn = AddSubMenu(SubmenuObjects, "Hinzufügen", $"Objekte hinzufügen");
            MenuSpawn.OnMenuOpen += (menu) =>
            {
                if (menu == MenuSpawn)
                {
                    SubmenuObjects_Add_Open = true;
                }
            };
            MenuSpawn.OnMenuClose += (menu) =>
            {
                if (menu == MenuSpawn)
                {
                    SubmenuObjects_Add_Open = false;
                }
            };

            AddMenuItem(MenuSpawn, "Polizeiabsperrung", "Polizeiabsperrung hinzuifügen", (item) =>
            {
                SpawnedObject.SpawnObject("prop_barrier_work05");
            });

            AddMenuItem(MenuSpawn, "Pylon (groß)", "Pylon hinzuifügen", (item) =>
            {
                SpawnedObject.SpawnObject("prop_roadcone01b");
            });

            AddMenuItem(MenuSpawn, "Pylon (klein)", "Pylon hinzuifügen", (item) =>
            {
                SpawnedObject.SpawnObject("prop_roadcone02a");
            });

            AddMenuItem(MenuSpawn, "Feuerlöscher", "Feuerlöscher hinzuifügen", (item) =>
            {
                SpawnedObject.SpawnObject("prop_fire_exting_3a");
            });

            AddMenuItem(MenuSpawn, "Arbeitsleuchte ", "Arbeitsleuchte hinzuifügen", (item) =>
            {
                SpawnedObject.SpawnObject("prop_worklight_02a");
            });

            AddMenuItem(MenuSpawn, "Erste-Hilfe-Set", "Erste-Hilfe-Set hinzuifügen", (item) =>
            {
                SpawnedObject.SpawnObject("prop_ld_health_pack");
            });

            AddMenuItem(SubmenuObjects, "Objekt entfernen", "Nächstgelegenes Objekt entfernen", (item) =>
            {
                SpawnedObject.DeleteObject(ModelNames);
            });

            AddMenuItem(SubmenuObjects, "Meine Objekte entfernen", "Nächstgelegenes Objekt entfernen", (item) =>
            {
                SpawnedObject.DeleteAllObjects();
            });
            AddMenuItem(SubmenuObjects, "Alle Objekte entfernen", "Nächstgelegenes Objekt entfernen", (item) =>
            {
                SpawnedObject.DeleteObject(ModelNames, 1000, true);
            });
        }

        Vector3? start = null;

        Vector3? end = null;

        bool SubMenuDrawCylinderBig = false;

        Dictionary<int, KeyValuePair<Vector3, Vector3>> Umleitungen = new Dictionary<int, KeyValuePair<Vector3, Vector3>>(); 

        private void OnTick_Submenu_Objects()
        {
            SpawnedObject.OnTickAll();
            if (SubmenuObjects_Add_Open)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, Game.PlayerPed.Position + (API.GetEntityForwardVector(Game.PlayerPed.Handle) * 1.5f) - new Vector3(0, 0, API.GetEntityHeightAboveGround(Game.PlayerPed.Handle)), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 0.5f), System.Drawing.Color.FromArgb(0, 255, 0));
            }
            if (SubmenuObjects_Open)
            {
                int NearObject = SpawnedObject.GetNearestObject(ModelNames);
                if (SubmenuObjects.MenuItems.Any(o => o.Selected && o.Text == "Alle Objekte entfernen"))
                {
                    foreach (int Object in SpawnedObject.GetNearObjects(ModelNames, 1000))
                    {
                        World.DrawMarker(MarkerType.VerticalCylinder, API.GetEntityCoords(Object, true) - new Vector3(0, 0, API.GetEntityHeightAboveGround(Object)), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 0.5f), System.Drawing.Color.FromArgb(255, 100, 0));
                    }
                }
                else
                {
                    System.Drawing.Color SelectColor = System.Drawing.Color.FromArgb(200, 120, 0);
                    if (SubmenuObjects.MenuItems.Any(o => o.Selected && o.Text == "Meine Objekte entfernen"))
                    {
                        SelectColor = System.Drawing.Color.FromArgb(255, 100, 0);
                    }

                    foreach (SpawnedObject CurrenObject in SpawnedObject.SpawnedObjects)
                    {
                        World.DrawMarker(MarkerType.VerticalCylinder, API.GetEntityCoords(CurrenObject.ObjectID, true) - new Vector3(0, 0, API.GetEntityHeightAboveGround(CurrenObject.ObjectID)), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 0.5f), SelectColor);
                    }
                    foreach (int Object in SpawnedObject.GetNearObjects(ModelNames, 1000).Where(o => !SpawnedObject.SpawnedObjects.Any(o2 => o == o2.ObjectID)))
                    {
                        World.DrawMarker(MarkerType.VerticalCylinder, API.GetEntityCoords(Object, true) - new Vector3(0, 0, API.GetEntityHeightAboveGround(Object)), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 0.5f), System.Drawing.Color.FromArgb(200, 100, 100));
                    }
                }
                if (SubmenuObjects.MenuItems.Any(o => o.Selected && o.Text == "Objekt entfernen"))
                {
                    if (NearObject != -1)
                    {
                        World.DrawMarker(MarkerType.VerticalCylinder, API.GetEntityCoords(NearObject, true) - new Vector3(0, 0, API.GetEntityHeightAboveGround(NearObject)), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 0.5f), System.Drawing.Color.FromArgb(255, 100, 0));
                    }
                }
            }

            float cylSize = 4f;
            float distance = (float)Math.Sqrt((double)((cylSize * cylSize) + (cylSize * cylSize))) * 2;
            if (SubMenuDrawCylinderBig)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, Game.PlayerPed.Position + (API.GetEntityForwardVector(Game.PlayerPed.Handle) * 1.5f) - new Vector3(0, 0, API.GetEntityHeightAboveGround(Game.PlayerPed.Handle)), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(cylSize, cylSize, 0.5f), System.Drawing.Color.FromArgb(0, 255, 0));
            }
            foreach(KeyValuePair<int, KeyValuePair<Vector3, Vector3>> item in Umleitungen)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, item.Value.Key, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(cylSize, cylSize, 0.5f), System.Drawing.Color.FromArgb(200, 120, 0));
                World.DrawMarker(MarkerType.VerticalCylinder, item.Value.Value, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(cylSize, cylSize, 0.5f), System.Drawing.Color.FromArgb(200, 120, 0));

                IEnumerable<Ped> peds = World.GetAllPeds().Where(o => !o.IsPlayer).Where(o => o.Position.DistanceToSquared(item.Value.Key) < distance).Where(o => o.IsInVehicle() && o.CurrentVehicle.GetPedOnSeat(VehicleSeat.Driver) == o);
                foreach (Ped ped in peds)
                {
                    if(!API.GetIsTaskActive(ped.Handle, 157))
                    {
                        ped.Task.DriveTo(ped.CurrentVehicle, item.Value.Value, distance, 8, 263103);
                    }
                }
            }

            if(start.HasValue)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, start.Value, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(cylSize, cylSize, 0.5f), System.Drawing.Color.FromArgb(255, 100, 0));
            }
            if(end.HasValue)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, end.Value, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(cylSize, cylSize, 0.5f), System.Drawing.Color.FromArgb(255, 100, 0));
            }
        }
    }
}