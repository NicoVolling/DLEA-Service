using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using Client.Objects.CommonVehicle;
using Client.Services;
using DLEA_Lib.Shared.Application;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private void AddSubmenu_Vehicle()
        {
            try
            {
                UIMenu MenuVehicle = AddSubMenu(this, "Mechaniker", "Lieferung, Tuning, etc");
                UIMenu MenuVehicleSpawn = AddSubMenu(MenuVehicle, "Lieferung", "Fahrzeuge liefernlassen");

                UIMenu MenuVehicleSpawnEmergency = AddSubMenu(MenuVehicleSpawn, "Einsatzfahrzeuge", "Einsatzfahrzeuge");

                foreach (string Cat in Vehicles.GetEmergencyCategories().OrderBy(o => o))
                {
                    UIMenu MenuVehicleSpawnEmergencyCategory = AddSubMenu(MenuVehicleSpawnEmergency, Cat, Cat);

                    foreach (KeyValuePair<string, string> KVP in Vehicles.GetEmergencyVehicles().Where(o => o.Value.Equals(Cat)))
                    {
                        string vehModelName = KVP.Key;
                        string properCasedModelName = KVP.Key[0].ToString().ToUpper() + KVP.Key.ToLower().Substring(1);
                        string vehName = /*CommonFunctions.GetVehDisplayNameFromModel(KVP.Key) != "NULL" ? CommonFunctions.GetVehDisplayNameFromModel(KVP.Key) : */properCasedModelName;
                        uint model = (uint)API.GetHashKey(vehModelName);

                        if (CommonFunctions.DoesModelExist(KVP.Key))
                        {
                            UIMenuItem spawnVehicle = AddMenuItem(MenuVehicleSpawnEmergencyCategory, $"{vehName}", $"{vehName} rufen", o =>
                            {
                                try
                                {
                                    Random rnd = new Random();
                                    Spawn(model, true, true, false, new VehicleInfo()
                                    {
                                        bulletProofTires = true,
                                        livery = Cat == Vehicles.Category_Verdeckt || Cat == Vehicles.Category_VerdecktGepanzert ? 1 : 0,
                                        plateText = CurrentUser.Vorname[0].ToString() + CurrentUser.Nachname[0].ToString() + " - " + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString(),
                                        turbo = true,
                                        mods = new Dictionary<int, int>()
                                        {
                                            { (int)VehicleModType.Engine, 4 },
                                            { (int)VehicleModType.Transmission, 3 },
                                            { (int)VehicleModType.Brakes, 3},
                                            { (int)VehicleModType.Armor, 5 }
                                        }
                                    }, vehName).Wait(100);
                                    Vehicle Vehicle = Game.PlayerPed.CurrentVehicle;
                                }
                                catch (Exception ex)
                                {
                                    Tracing.Trace(ex);
                                }
                            });
                        }
                    }
                }

                UIMenu MenuVehicleSpawnAll = AddSubMenu(MenuVehicleSpawn, "Alle Fahrzeuge", "Alle Fahrzeuge");

                foreach (int vehClass in Vehicles.VehicleClassesInt)
                {
                    if (vehClass != 21)
                    {
                        // Get the class name.
                        string className = API.GetLabelText($"VEH_CLASS_{vehClass}");

                        // Create a button & a menu for it, add the menu to the menu pool and add & bind the button to the menu.
                        UIMenu classMenu = AddSubMenu(MenuVehicleSpawnAll, $"{className}", $"{className} ({Vehicles.VehicleClasses.Where(o => o.Key == className).Count()})");

                        List<string> VehNames = new List<string>();

                        Action<string, string> VehicleSpawnItem = (vehName, Vehicle) =>
                        {
                            string vehModelName = Vehicle;

                            uint model = (uint)API.GetHashKey(vehModelName);

                            if (CommonFunctions.DoesModelExist(Vehicle) && !VehNames.Contains($"{className}_{vehName}"))
                            {
                                VehNames.Add($"{className}_{vehName}");
                                UIMenuItem spawnVehicle = AddMenuItem(classMenu, $"{vehName}", $"{vehName} rufen", o =>
                                {
                                    try
                                    {
                                        Spawn(model, true, true, false, new VehicleInfo(), vehName).Wait(100);
                                    }
                                    catch (Exception ex)
                                    {
                                        Tracing.Trace(ex);
                                    }
                                });
                            }
                        };

                        Dictionary<string, string> VehicleSortList = new Dictionary<string, string>();

                        foreach (string Vehicle in Vehicles.VehicleClasses[className])
                        {
                            string properCasedModelName = Vehicle[0].ToString().ToUpper() + Vehicle.ToLower().Substring(1);
                            string vehName = CommonFunctions.GetVehDisplayNameFromModel(Vehicle) != "NULL" ? CommonFunctions.GetVehDisplayNameFromModel(Vehicle) : properCasedModelName;

                            if (!VehicleSortList.ContainsKey(vehName))
                            {
                                VehicleSortList.Add(vehName, Vehicle);
                            }
                        }

                        foreach (string Vehicle in ClientObject.GetService<VehicleService>().Addons.Where(o => API.GetVehicleClassFromName((uint)API.GetHashKey(o)) == vehClass))
                        {
                            string properCasedModelName = Vehicle[0].ToString().ToUpper() + Vehicle.ToLower().Substring(1);
                            string vehName = CommonFunctions.GetVehDisplayNameFromModel(Vehicle) != "NULL" ? CommonFunctions.GetVehDisplayNameFromModel(Vehicle) : properCasedModelName;

                            if (!VehicleSortList.ContainsKey(vehName))
                            {
                                VehicleSortList.Add(vehName, Vehicle);
                            }
                        }

                        foreach (KeyValuePair<string, string> KVP in VehicleSortList.OrderBy(o => o.Key))
                        {
                            VehicleSpawnItem(KVP.Key, KVP.Value);
                        }
                    }
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        private async Task<int> Spawn(uint vehicleHash, bool spawnInside, bool replacePrevious, bool skipLoad, VehicleInfo vehicleInfo, string saveName = null)
        {
            return await CommonFunctions.SpawnVehicle(vehicleHash, true, true, false, vehicleInfo, saveName);
        }
    }
}