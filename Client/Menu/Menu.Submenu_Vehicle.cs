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

namespace Client.Menu
{
    public partial class MainMenu
    {
        private void AddSubmenu_Vehicle()
        {
            try 
            { 
                UIMenu MenuVehicle = MenuPool.AddSubMenu(this, "Mechaniker", "Lieferung, Tuning, etc");
                UIMenu MenuVehicleSpawn = MenuPool.AddSubMenu(MenuVehicle, "Lieferung", "Fahrzeuge liefernlassen");

                for (var vehClass = 0; vehClass < 23; vehClass++)
                {
                    // Get the class name.
                    string className = API.GetLabelText($"VEH_CLASS_{vehClass}");

                    // Create a button & a menu for it, add the menu to the menu pool and add & bind the button to the menu.
                    UIMenu classMenu = MenuPool.AddSubMenu(MenuVehicleSpawn, $"{className}", $"{className} ({Vehicles.VehicleClasses.Where(o => o.Key == className).Count()})");

                    List<string> VehNames = new List<string>();

                    Action<string> VehicleSpawnItem = Vehicle => 
                    {
                        string properCasedModelName = Vehicle[0].ToString().ToUpper() + Vehicle.ToLower().Substring(1);
                        string vehName = ClientHelper.GetVehDisplayNameFromModel(Vehicle) != "NULL" ? ClientHelper.GetVehDisplayNameFromModel(Vehicle) : properCasedModelName;
                        string vehModelName = Vehicle;

                        uint model = (uint)API.GetHashKey(vehModelName);

                        if (ClientHelper.DoesModelExist(Vehicle) && !VehNames.Contains($"{className}_{vehName}"))
                        {
                            VehNames.Add($"{className}_{vehName}");
                            UIMenuItem spawnVehicle = AddMenuItem(classMenu, $"{vehName}", $"{vehName} rufen", o =>
                            {
                                Task<int> task = ClientHelper.SpawnVehicle(model, true, true, false, new VehicleInfo(), vehName);
                                task.Start();
                            });
                        }
                    };

                    foreach(string Vehicle in Vehicles.VehicleClasses[className]) 
                    {
                        VehicleSpawnItem(Vehicle);
                    }

                    foreach(string Vehicle in ClientObject.GetService<VehicleService>().Addons.Where(o => API.GetVehicleClassFromName((uint)API.GetHashKey(o)) == vehClass)) 
                    {
                        VehicleSpawnItem(Vehicle);
                    }
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}
