using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.Objects.CommonVehicle;
using DLEA_Lib;
using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Game;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class ClientHelper
    {
        public static void ApplyOutfit(Outfit Outfit)
        {
            if (API.GetEntityModel(Game.PlayerPed.Handle) != API.GetHashKey(Outfit.Ped))
            {
                Screen.ShowNotification($"~r~Outfit passt nur auf {Outfit.Ped}");
            }
            else
            {
                foreach (Component Comp in Outfit.Components)
                {
                    if (Comp != null)
                    {
                        API.SetPedComponentVariation(Game.PlayerPed.Handle, Comp.ComponentId, Comp.DrawableId - 1, Comp.TextureId - 1, Comp.PaletteId);
                    }
                }

                foreach (DLEA_Lib.Shared.Wardrobe.Prop Prop in Outfit.Props)
                {
                    if (Prop.DrawableId == 0)
                    {
                        API.ClearPedProp(Game.PlayerPed.Handle, Prop.ComponentId);
                    }
                    else
                    {
                        API.SetPedPropIndex(Game.PlayerPed.Handle, Prop.ComponentId, Prop.DrawableId - 1, Prop.TextureId - 1, true);
                    }
                }
            }
        }

        public static Ped GetClosestPed(float Radius = 100000) 
        {
            return GetClosestPed(Radius, Game.PlayerPed);
        }

        public static Ped GetClosestPed(float Radius, Ped Ped) 
        {
            return GetPed(o => !API.IsPedAPlayer(o.Handle) && DistanceToPlayer(o.Position, Ped.Position) <= Radius, o => DistanceToPlayer(o.Position, Ped.Position));
        }

        public static Ped GetPed(Func<Ped, bool> Condition, Func<Ped, Object> OrderBy = null)
        {
            Ped Result = null;

            IEnumerable<Ped> Peds = CitizenFX.Core.World.GetAllPeds().Where(Condition);
            if (OrderBy != null) 
            {
                Peds =  Peds.OrderBy(OrderBy);
            }
            if (Peds.Any())
            {
                Result = Peds.First();
            }

            return Result;
        }

        public static void SetWeather(EnumWeather weather, bool Transition = false) 
        {
            if (Transition)
            {
                API.SetWeatherTypeOvertimePersist(EnumWeatherHelper.GetWeatherName(weather), 30);
            }
            else 
            {
                API.SetWeatherTypeNowPersist(EnumWeatherHelper.GetWeatherName(weather));
            }
        }

        public static EnumWeather GetWeather() 
        {
            return (EnumWeather)(int)CitizenFX.Core.World.Weather;
        }

        public static float DistanceToPlayer(Vector3 Position, Vector3 PlayerPosition) 
        {
            return API.GetDistanceBetweenCoords(Position.X, Position.Y, Position.Z, PlayerPosition.X, PlayerPosition.Y, PlayerPosition.Z, true);
        }

        public static Vehicle GetVehicle(bool lastVehicle = false)
        {
            if (lastVehicle)
            {
                return Game.PlayerPed.LastVehicle;
            }
            else
            {
                if (Game.PlayerPed.IsInVehicle())
                {
                    return Game.PlayerPed.CurrentVehicle;
                }
            }
            return null;
        }

        private static async Task<bool> LoadModel(uint modelHash)
        {
            // Check if the model exists in the game.
            if (API.IsModelInCdimage(modelHash))
            {
                // Load the model.
                API.RequestModel(modelHash);
                // Wait until it's loaded.
                while (!API.HasModelLoaded(modelHash))
                {
                    await Delay(0);
                }
                // Model is loaded, return true.
                return true;
            }
            // Model is not valid or is not loaded correctly.
            else
            {
                // Return false.
                return false;
            }
        }

        public static async Task Delay(int time)
        {
            await BaseScript.Delay(time);
        }

        private static Vehicle _previousVehicle;

        /// <summary>
        /// Spawns a vehicle.
        /// </summary>
        /// <param name="vehicleHash">Model hash of the vehicle to spawn.</param>
        /// <param name="spawnInside">Teleports the player into the vehicle after spawning.</param>
        /// <param name="replacePrevious">Replaces the previous vehicle of the player with the new one.</param>
        /// <param name="skipLoad">Does not attempt to load the vehicle, but will spawn it right a way.</param>
        /// <param name="vehicleInfo">All information needed for a saved vehicle to re-apply all mods.</param>
        /// <param name="saveName">Used to get/set info about the saved vehicle data.</param>
        public static async Task<int> SpawnVehicle(uint vehicleHash, bool spawnInside, bool replacePrevious, bool skipLoad, VehicleInfo vehicleInfo, string saveName = null, float x = 0f, float y = 0f, float z = 0f, float heading = -1f)
        {
            float speed = 0f;
            float rpm = 0f;
            if (Game.PlayerPed.IsInVehicle())
            {
                Vehicle tmpOldVehicle = GetVehicle();
                speed = API.GetEntitySpeedVector(tmpOldVehicle.Handle, true).Y; // get forward/backward speed only
                rpm = tmpOldVehicle.CurrentRPM;
            }

            int modelClass = API.GetVehicleClassFromName(vehicleHash);

            if (!skipLoad)
            {
                bool successFull = await LoadModel(vehicleHash);
                if (!successFull || !API.IsModelAVehicle(vehicleHash))
                {
                    // Vehicle model is invalid.
                    ClientObject.SendMessage("~r~Fehler: ~w~Ungültiges Model");
                    return 0;
                }
            }

            // Get the heading & position for where the vehicle should be spawned.
            Vector3 pos = new Vector3(x, y, z);
            if (pos.IsZero)
            {
                pos = (spawnInside) ? API.GetEntityCoords(Game.PlayerPed.Handle, true) : API.GetOffsetFromEntityInWorldCoords(Game.PlayerPed.Handle, 0f, 8f, 0f);
                pos += new Vector3(0f, 0f, 1f);
            }

            heading = heading == -1 ? API.GetEntityHeading(Game.PlayerPed.Handle) + (spawnInside ? 0f : 90f) : heading;

            // If the previous vehicle exists...
            if (_previousVehicle != null)
            {
                // And it's actually a vehicle (rather than another random entity type)
                if (_previousVehicle.Exists() && _previousVehicle.PreviouslyOwnedByPlayer &&
                    (_previousVehicle.Occupants.Count() == 0 || _previousVehicle.Driver.Handle == Game.PlayerPed.Handle))
                {
                    // If the previous vehicle should be deleted:
                    if (replacePrevious)
                    {
                        // Delete it.
                        _previousVehicle.PreviouslyOwnedByPlayer = false;
                        API.SetEntityAsMissionEntity(_previousVehicle.Handle, true, true);
                        _previousVehicle.Delete();
                    }
                    // Otherwise
                    else
                    {
                        //Keep Spawned Vehicle Persistant
                        API.SetEntityAsMissionEntity(_previousVehicle.Handle, false, false);
                    }
                    _previousVehicle = null;
                }
            }

            if (Game.PlayerPed.IsInVehicle() && replacePrevious)
            {
                if (GetVehicle().Driver == Game.PlayerPed)
                {
                    var tmpveh = GetVehicle();
                    API.SetVehicleHasBeenOwnedByPlayer(tmpveh.Handle, false);
                    API.SetEntityAsMissionEntity(tmpveh.Handle, true, true);

                    if (_previousVehicle != null)
                    {
                        if (_previousVehicle.Handle == tmpveh.Handle)
                        {
                            _previousVehicle = null;
                        }
                    }
                    tmpveh.Delete();
                }
            }

            if (_previousVehicle != null)
                _previousVehicle.PreviouslyOwnedByPlayer = false;

            if (Game.PlayerPed.IsInVehicle() && x == 0f && y == 0f && z == 0f)
                pos = API.GetOffsetFromEntityInWorldCoords(Game.PlayerPed.Handle, 0, 8f, 0.1f) + new Vector3(0f, 0f, 1f);

            // Create the new vehicle and remove the need to hotwire the car.
            Vehicle vehicle = new Vehicle(API.CreateVehicle(vehicleHash, pos.X, pos.Y, pos.Z, heading, true, false))
            {
                NeedsToBeHotwired = false,
                PreviouslyOwnedByPlayer = true,
                IsPersistent = true,
                IsStolen = false,
                IsWanted = false
            };


            // If spawnInside is true
            if (spawnInside)
            {
                // Set the vehicle's engine to be running.
                vehicle.IsEngineRunning = true;

                // Set the ped into the vehicle.
                new Ped(Game.PlayerPed.Handle).SetIntoVehicle(vehicle, VehicleSeat.Driver);

                // If the vehicle is a helicopter and the player is in the air, set the blades to be full speed.
                if (vehicle.ClassType == VehicleClass.Helicopters && API.GetEntityHeightAboveGround(Game.PlayerPed.Handle) > 10.0f)
                {
                    API.SetHeliBladesFullSpeed(vehicle.Handle);
                }
                // If it's not a helicopter or the player is not in the air, set the vehicle on the ground properly.
                else
                {
                    vehicle.PlaceOnGround();
                }
            }

            // If mod info about the vehicle was specified, check if it's not null.
            if (saveName != null)
            {
                ApplyVehicleModsDelayed(vehicle, vehicleInfo, 500);
            }

            // Set the previous vehicle to the new vehicle.
            _previousVehicle = vehicle;
            //vehicle.Speed = speed; // retarded feature that randomly breaks for no fucking reason
            if (!vehicle.Model.IsTrain) // to be extra fucking safe
            {
                // workaround of retarded feature above:
                API.SetVehicleForwardSpeed(vehicle.Handle, speed);
            }
            vehicle.CurrentRPM = rpm;

            await Delay(1); // Mandatory delay - without it radio station will not set properly

            // Set the radio station to default set by player in Vehicle Menu
            vehicle.RadioStation = 0;

            // Discard the model.
            API.SetModelAsNoLongerNeeded(vehicleHash);

            return vehicle.Handle;
        }

        private static async void ApplyVehicleModsDelayed(Vehicle vehicle, VehicleInfo vehicleInfo, int delay)
        {
            if (vehicle != null && vehicle.Exists())
            {
                vehicle.Mods.InstallModKit();
                // set the extras
                foreach (var extra in vehicleInfo.extras)
                {
                    if (API.DoesExtraExist(vehicle.Handle, extra.Key))
                        vehicle.ToggleExtra(extra.Key, extra.Value);
                }

                API.SetVehicleWheelType(vehicle.Handle, vehicleInfo.wheelType);
                API.SetVehicleMod(vehicle.Handle, 23, 0, vehicleInfo.customWheels);
                if (vehicle.Model.IsBike)
                {
                    API.SetVehicleMod(vehicle.Handle, 24, 0, vehicleInfo.customWheels);
                }
                API.ToggleVehicleMod(vehicle.Handle, 18, vehicleInfo.turbo);
                API.SetVehicleTyreSmokeColor(vehicle.Handle, vehicleInfo.colors["tyresmokeR"], vehicleInfo.colors["tyresmokeG"], vehicleInfo.colors["tyresmokeB"]);
                API.ToggleVehicleMod(vehicle.Handle, 20, vehicleInfo.tyreSmoke);
                API.ToggleVehicleMod(vehicle.Handle, 22, vehicleInfo.xenonHeadlights);
                API.SetVehicleLivery(vehicle.Handle, vehicleInfo.livery);

                API.SetVehicleColours(vehicle.Handle, vehicleInfo.colors["primary"], vehicleInfo.colors["secondary"]);
                API.SetVehicleInteriorColour(vehicle.Handle, vehicleInfo.colors["trim"]);
                API.SetVehicleDashboardColour(vehicle.Handle, vehicleInfo.colors["dash"]);

                API.SetVehicleExtraColours(vehicle.Handle, vehicleInfo.colors["pearlescent"], vehicleInfo.colors["wheels"]);

                API.SetVehicleNumberPlateText(vehicle.Handle, vehicleInfo.plateText);
                API.SetVehicleNumberPlateTextIndex(vehicle.Handle, vehicleInfo.plateStyle);

                API.SetVehicleWindowTint(vehicle.Handle, vehicleInfo.windowTint);

                vehicle.CanTiresBurst = !vehicleInfo.bulletProofTires;

                API.SetVehicleEnveffScale(vehicle.Handle, vehicleInfo.enveffScale);

                VehicleOptions._SetHeadlightsColorOnVehicle(vehicle, vehicleInfo.headlightColor);

                vehicle.Mods.NeonLightsColor = System.Drawing.Color.FromArgb(red: vehicleInfo.colors["neonR"], green: vehicleInfo.colors["neonG"], blue: vehicleInfo.colors["neonB"]);
                vehicle.Mods.SetNeonLightsOn(VehicleNeonLight.Left, vehicleInfo.neonLeft);
                vehicle.Mods.SetNeonLightsOn(VehicleNeonLight.Right, vehicleInfo.neonRight);
                vehicle.Mods.SetNeonLightsOn(VehicleNeonLight.Front, vehicleInfo.neonFront);
                vehicle.Mods.SetNeonLightsOn(VehicleNeonLight.Back, vehicleInfo.neonBack);

                void DoMods()
                {
                    vehicleInfo.mods.ToList().ForEach(mod =>
                    {
                        if (vehicle != null && vehicle.Exists())
                            API.SetVehicleMod(vehicle.Handle, mod.Key, mod.Value, vehicleInfo.customWheels);
                    });
                }

                DoMods();
                // Performance mods require a delay after setting the modkit,
                // so we just do it once first so all the visual mods load instantly,
                // and after a small delay we do it again to make sure all performance
                // mods have also loaded.
                await Delay(delay);
                DoMods();
            }
        }


        public static string GetStreetLocation(Vector3 location)
        {
            uint streetname = 1;
            uint crossingroad = 1;
            string Street = "";
            string CrossingRoad = "";
            API.GetStreetNameAtCoord(location.X, location.Y, location.Z, ref streetname, ref crossingroad);
            Street = API.GetStreetNameFromHashKey(streetname);
            CrossingRoad = API.GetStreetNameFromHashKey(crossingroad);
            if (!string.IsNullOrWhiteSpace(CrossingRoad))
            {
                CrossingRoad = $" | {CrossingRoad}";
            }
            return Street + CrossingRoad;
        }

        public static string GetVehDisplayNameFromModel(string name) => API.GetLabelText(API.GetDisplayNameFromVehicleModel((uint)API.GetHashKey(name)));

        public static bool DoesModelExist(string modelName) => DoesModelExist((uint)API.GetHashKey(modelName));

        public static bool DoesModelExist(uint modelHash) => API.IsModelInCdimage(modelHash);

        public static string GetZoneLocation(Vector3 Location) 
        {
            return GetZoneFromShort(API.GetNameOfZone(Location.X, Location.Y, Location.Z));
        }

        public static string GetDirection(float Heading) 
        {
            if (Heading > 337.5 && Heading <= 22.5) 
            {
                return "N";
            }
            if (Heading > 22.5 && Heading <= 67.5)
            {
                return "NW";
            }
            if (Heading > 67.5 && Heading <= 112.5)
            {
                return "W";
            }
            if (Heading > 112.5 && Heading <= 157.5)
            {
                return "SW";
            }
            if (Heading > 157.5 && Heading <= 202.5)
            {
                return "S";
            }
            if (Heading > 202.5 && Heading <= 247.5)
            {
                return "SE";
            }
            if (Heading > 247.5 && Heading <= 292.5)
            {
                return "E";
            }
            if (Heading > 292.5 && Heading <= 337.5)
            {
                return "NE";
            }
            return "N";
        }

        public static string GetDistanceStreet(Vector3 location)
        {
            float meters2 = 0;
            meters2 = API.CalculateTravelDistanceBetweenPoints(location.X, location.Y, location.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z);
            string meters2f = "";
            if (meters2 >= 1000)
            {
                try
                {
                    meters2f = Math.Round(meters2 / 1000, 2).ToString("n") + "km";
                }
                catch { }
            }
            else
            {
                meters2f = Math.Round(meters2, 0).ToString() + "m";
            }
            return meters2f;
        }

        public static string GetDistanceAir(Vector3 location)
        {
            float meters = 0;
            meters = API.GetDistanceBetweenCoords(location.X, location.Y, location.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z, true);
            string metersf = "";
            if (meters >= 1000)
            {
                try
                {
                    metersf = Math.Round(meters / 1000, 2).ToString("n") + "km";
                }
                catch { }
            }
            else
            {
                metersf = Math.Round(meters, 0).ToString() + "m";
            }
            return metersf;
        }
        private static Dictionary<string, string> zones = new Dictionary<string, string>()
        {
            {"AIRP", "Los Santos International Airport"},
            {"ALAMO", "Alamo Sea"},
            {"ALTA", "Alta"},
            {"ARMYB", "Fort Zancudo"},
            {"BANHAMC", "Banham Canyon Drive"},
            {"BANNING", "Banning"},
            {"BEACH", "Vespucci Beach"},
            {"BHAMCA", "Banham Canyon"},
            {"BRADP", "Braddock Pass"},
            {"BRADT", "Braddock Tunnel"},
            {"BURTON", "Burton"},
            {"CALAFB", "Calafia Bridge"},
            {"CANNY", "Raton Canyon"},
            {"CCREAK", "Cassidy Creek"},
            {"CHAMH", "Chamberlain Hills"},
            {"CHIL", "Vinewood Hills"},
            {"CHU", "Chumash"},
            {"CMSW", "Chiliad Mountain State Wilderness"},
            {"CYPRE", "Cypress Flats"},
            {"DAVIS", "Davis"},
            {"DELBE", "Del Perro Beach"},
            {"DELPE", "Del Perro"},
            {"DELSOL", "La Puerta"},
            {"DESRT", "Grand Senora Desert"},
            {"DOWNT", "Downtown"},
            {"DTVINE", "Downtown Vinewood"},
            {"EAST_V", "East Vinewood"},
            {"EBURO", "El Burro Heights"},
            {"ELGORL", "El Gordo Lighthouse"},
            {"ELYSIAN", "Elysian Island"},
            {"GALFISH", "Galilee"},
            {"GOLF", "GW Cand Golfing Society"},
            {"GRAPES", "Grapeseed"},
            {"GREATC", "Great Chaparral"},
            {"HARMO", "Harmony"},
            {"HAWICK", "Hawick"},
            {"HORS", "Vinewood Racetrack"},
            {"HUMLAB", "Humane Labsand Research"},
            {"JAIL", "Bolingbroke Penitentiary"},
            {"KOREAT", "Little Seoul"},
            {"LACT", "Land Act Reservoir"},
            {"LAGO", "Lago Zancudo"},
            {"LDAM", "Land Act Dam"},
            {"LEGSQU", "Legion Square"},
            {"LMESA", "La Mesa"},
            {"LOSPUER", "La Puerta"},
            {"MIRR", "Mirror Park"},
            {"MORN", "Morningwood"},
            {"MOVIE", "Richards Majestic"},
            {"MTCHIL", "Mount Chiliad"},
            {"MTGORDO", "Mount Gordo"},
            {"MTJOSE", "Mount Josiah"},
            {"MURRI", "Murrieta Heights"},
            {"NCHU", "North Chumash"},
            {"NOOSE", "N.O.O.S.E"},
            {"OCEANA", "Pacific Ocean"},
            {"PALCOV", "Paleto Cove"},
            {"PALETO", "Paleto Bay"},
            {"PALFOR", "Paleto Forest"},
            {"PALHIGH", "Palomino Highlands"},
            {"PALMPOW", "Palmer-Taylor Power Station"},
            {"PBLUFF", "Pacific Bluffs"},
            {"PBOX", "Pillbox Hill"},
            {"PROCOB", "Procopio Beach"},
            {"RANCHO", "Rancho"},
            {"RGLEN", "Richman Glen"},
            {"RICHM", "Richman"},
            {"ROCKF", "Rockford Hills"},
            {"RTRAK", "Redwood Lights Track"},
            {"SANAND", "San Andreas"},
            {"SANCHIA", "San Chianski Mountain Range"},
            {"SANDY", "Sandy Shores"},
            {"SKID", "Mission Row"},
            {"SLAB", "Stab City"},
            {"STAD", "Maze Bank Arena"},
            {"STRAW", "Strawberry"},
            {"TATAMO", "Tataviam Mountains"},
            {"TERMINA", "Terminal"},
            {"TEXTI", "Textile City"},
            {"TONGVAH", "Tongva Hills"},
            {"TONGVAV", "Tongva Valley"},
            {"VCANA", "Vespucci Canals"},
            {"VESP", "Vespucci"},
            {"VINE", "Vinewood"},
            {"WINDF", "Ron Alternates Wind Farm"},
            {"WVINE", "West Vinewood"},
            {"ZANCUDO", "Zancudo River"},
            {"ZP_ORT", "Port of South Los Santos"},
            {"ZQ_UAR", "Davis Quartz"},
        };

        public static string GetZoneFromShort(string Short) 
        {
            return zones[Short];
        }
    }
}
