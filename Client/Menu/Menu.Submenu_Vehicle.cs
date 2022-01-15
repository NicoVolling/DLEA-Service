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
using Client.Objects;
using Client.Objects.CommonVehicle;
using Client.ClientHelper;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private async Task<int> Spawn(uint vehicleHash, bool spawnInside, bool replacePrevious, bool skipLoad, VehicleInfo vehicleInfo, string saveName = null) 
        {
            return await CommonFunctions.SpawnVehicle(vehicleHash, true, true, false, new VehicleInfo(), saveName);
        }

        private void AddSubmenu_Vehicle()
        {
            try 
            { 
                UIMenu MenuVehicle = MenuPool.AddSubMenu(this, "Mechaniker", "Lieferung, Tuning, etc");
                UIMenu MenuVehicleSpawn = MenuPool.AddSubMenu(MenuVehicle, "Lieferung", "Fahrzeuge liefernlassen");

                

                foreach (int vehClass in Vehicles.VehicleClassesInt)
                {
                    if (vehClass != 21)
                    {
                        // Get the class name.
                        string className = API.GetLabelText($"VEH_CLASS_{vehClass}");

                        // Create a button & a menu for it, add the menu to the menu pool and add & bind the button to the menu.
                        UIMenu classMenu = MenuPool.AddSubMenu(MenuVehicleSpawn, $"{className}", $"{className} ({Vehicles.VehicleClasses.Where(o => o.Key == className).Count()})");

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

                        foreach(KeyValuePair<string, string> KVP in VehicleSortList.OrderBy(o => o.Key)) 
                        {
                            VehicleSpawnItem(KVP.Key, KVP.Value);
                        }
                    }
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}
