using CitizenFX.Core.Native;
using CitizenFX.Core;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ClientHelper;
using Client.Objects.CommonVehicle;
using Client.Services;
using DLEA_Lib.Shared.Application;

namespace Client.Menu.Submenus
{
    internal class Submenu_VehicleSpawner : MenuBase
    {

        int vehiclehandle = -1;
        int bliphandle = -1;

        public Submenu_VehicleSpawner(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Fahrzeuge";

        protected override void InitializeMenu(UIMenu Menu)
        {
            UIMenu MenuVehicleSpawnEmergency = AddSubMenu(Menu, "Einsatzfahrzeuge", "Einsatzfahrzeuge");

            foreach (string Cat in ClientObject.GetService<VehicleService>().Addons.SelectMany(o => o.Categories).Select(o => o.Name).Distinct().OrderBy(o => o))
            {
                UIMenu MenuVehicleSpawnEmergencyCategory = AddSubMenu(MenuVehicleSpawnEmergency, Cat, Cat);

                foreach (DLEA_Lib.Shared.Vehicles.Vehicle Veh in ClientObject.GetService<VehicleService>().Addons.Where(o => o.Categories.Any(p => p.Name == Cat)))
                {
                    string vehModelName = Veh.Modelname;
                    string properCasedModelName = Veh.Modelname[0].ToString().ToUpper() + Veh.Modelname.ToLower().Substring(1);
                    string vehName = /*CommonFunctions.GetVehDisplayNameFromModel(KVP.Key) != "NULL" ? CommonFunctions.GetVehDisplayNameFromModel(KVP.Key) : */properCasedModelName;
                    uint model = (uint)API.GetHashKey(vehModelName);

                    if (CommonFunctions.DoesModelExist(Veh.Modelname))
                    {
                        UIMenuItem spawnVehicle = AddMenuItem(MenuVehicleSpawnEmergencyCategory, $"{vehName}", $"{vehName} rufen", o =>
                        {
                            try
                            {
                                Random rnd = new Random();
                                int livery = Veh.Categories.FirstOrDefault(p => p.Name == Cat)?.Livery ?? 0;

                                VehicleInfo vi = new VehicleInfo()
                                {
                                    bulletProofTires = true,
                                    livery = livery,
                                    plateText = CurrentUser.Vorname[0].ToString() + CurrentUser.Nachname[0].ToString() + " - " + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString() + rnd.Next(0, 9).ToString(),
                                    turbo = true,
                                    mods = new Dictionary<int, int>()
                                    {
                                        { (int)VehicleModType.Engine, 4 },
                                        { (int)VehicleModType.Transmission, 3 },
                                        { (int)VehicleModType.Brakes, 3},
                                        { (int)VehicleModType.Armor, 5 }
                                    },
                                };
                                Spawn(model, vi).Wait(100);

                                CitizenFX.Core.Vehicle Vehicle = Game.PlayerPed.CurrentVehicle;
                            }
                            catch (Exception ex)
                            {
                                Tracing.Trace(ex);
                            }
                        });
                    }
                }
            }

            UIMenu MenuVehicleSpawnAll = AddSubMenu(Menu, "Alle Fahrzeuge", "Alle Fahrzeuge");

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
                                    Spawn(model, new VehicleInfo()).Wait(100);
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

                    foreach (string Vehicle in ClientObject.GetService<VehicleService>().Addons.Where(o => API.GetVehicleClassFromName((uint)API.GetHashKey(o.Modelname)) == vehClass).Select(o => o.Modelname))
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

        protected override void OnTick()
        {
            if (Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle.Handle == vehiclehandle)
            {
                API.SetBlipAlpha(bliphandle, 0);
            }
            else
            {
                API.SetBlipAlpha(bliphandle, 255);
            }
        }

        private async Task<int> Spawn(uint vehicleHash, VehicleInfo vehicleInfo)
        {
            int handle = await CommonFunctions.SpawnVehicle(vehicleHash, true, true, false, vehicleInfo);

            if (handle != -1)
            {
                CitizenFX.Core.Vehicle veh = CitizenFX.Core.Vehicle.FromHandle(handle) as CitizenFX.Core.Vehicle;
                if (veh != null)
                {
                    this.bliphandle = API.AddBlipForEntity(veh.Handle);
                    API.SetBlipSprite(bliphandle, 225);
                    API.BeginTextCommandSetBlipName("STRING");
                    API.AddTextComponentString("Dein Fahrzeug");
                    API.EndTextCommandSetBlipName(bliphandle);
                    API.SetBlipAsShortRange(bliphandle, true);
                }
            }

            this.vehiclehandle = handle;

            return handle;
        }
    }
}
