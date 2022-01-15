using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.Objects;
using Client.Objects.CommonVehicle;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.Game;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ClientHelper
{
    internal class CommonFunctions
    {
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
                Peds = Peds.OrderBy(OrderBy);
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

        public static int AddBlipForCoord(DVector3 Coords, int Sprite, int Color, int Display, string Name, float? Heading = null) 
        {
            int Blip = API.AddBlipForCoord(Coords.X, Coords.Y, Coords.Z);
            API.SetBlipSprite(Blip, Sprite);
            API.SetBlipColour(Blip, Color);
            API.SetBlipDisplay(Blip, Display);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(Name);
            API.EndTextCommandSetBlipName(Blip);
            if (Heading.HasValue) 
            {
                API.SetBlipSquaredRotation(Blip, Heading.Value); 
            }
            API.SetBlipCategory(Blip, 2);
            return Blip;
        }

        public static int AddBlipForEntity(int Handle, int Sprite, int Color, int Display, string Name, float? Heading = null)
        {
            int Blip = API.AddBlipForEntity(Handle);
            API.SetBlipSprite(Blip, Sprite);
            API.SetBlipColour(Blip, Color);
            API.SetBlipDisplay(Blip, Display);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(Name);
            API.EndTextCommandSetBlipName(Blip);
            if (Heading.HasValue)
            {
                API.SetBlipSquaredRotation(Blip, Heading.Value);
            }
            API.SetBlipCategory(Blip, 2);
            return Blip;
        }

        public static void RefreshBlip(int Blip, DVector3 Coords, int Sprite, int Color, int Display, string Name,  float? Heading = null) 
        {
            API.SetBlipCoords(Blip, Coords.X, Coords.Y, Coords.Z);
            API.SetBlipSprite(Blip, Sprite);
            API.SetBlipColour(Blip, Color);
            API.SetBlipDisplay(Blip, Display);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(Name);
            API.EndTextCommandSetBlipName(Blip);
            if (Heading.HasValue)
            {
                API.SetBlipSquaredRotation(Blip, Heading.Value);
            }
            API.SetBlipCategory(Blip, 2);
        }

        public static void RefreshBlip(int Blip, int Sprite, int Color, int Display, string Name, float? Heading = null)
        {
            API.SetBlipSprite(Blip, Sprite);
            API.SetBlipColour(Blip, Color);
            API.SetBlipDisplay(Blip, Display);
            API.BeginTextCommandSetBlipName("STRING");
            API.AddTextComponentString(Name);
            API.EndTextCommandSetBlipName(Blip);
            if (Heading.HasValue)
            {
                API.SetBlipSquaredRotation(Blip, Heading.Value);
            }
            API.SetBlipCategory(Blip, 2);
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
            try
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
                //if (saveName != null)
                //{
                //    ApplyVehicleModsDelayed(vehicle, vehicleInfo, 500);
                //}

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
            catch (Exception ex)
            {
                Tracing.Trace(ex);
                return -1;
            }
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
            return Zones.GetZoneFromShort(API.GetNameOfZone(Location.X, Location.Y, Location.Z));
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

        private static string _currentScenario = "";

        public static void PlayScenario(string scenarioName)
        {
            // If there's currently no scenario playing, or the current scenario is not the same as the new scenario, then..
            if (_currentScenario == "" || _currentScenario != scenarioName)
            {
                // Set the current scenario.
                _currentScenario = scenarioName;
                // Clear all tasks to make sure the player is ready to play the scenario.
                API.ClearPedTasks(Game.PlayerPed.Handle);

                var canPlay = true;
                // Check if the player CAN play a scenario... 
                if (API.IsPedRunning(Game.PlayerPed.Handle))
                {
                    ClientObject.SendMessage("Szenario kann nicht während des Rennens gestartet werden.");
                    canPlay = false;
                }
                if (API.IsEntityDead(Game.PlayerPed.Handle))
                {
                    ClientObject.SendMessage("Szenario kann nicht gestartet werden, wenn der Spieler tot ist.");
                    canPlay = false;
                }
                if (API.IsPlayerInCutscene(Game.PlayerPed.Handle))
                {
                    ClientObject.SendMessage("Szenario kann nicht während einer Cutscene gestartet werden.");
                    canPlay = false;
                }
                if (API.IsPedFalling(Game.PlayerPed.Handle))
                {
                    ClientObject.SendMessage("Szenario kann nicht während des Fallens gestartet werden.");
                    canPlay = false;
                }
                if (API.IsPedRagdoll(Game.PlayerPed.Handle))
                {
                    ClientObject.SendMessage("Szenarion kann nicht während eines Ragdolls gestartet werden.");
                    canPlay = false;
                }
                if (!API.IsPedOnFoot(Game.PlayerPed.Handle))
                {
                    ClientObject.SendMessage("Szenario kann nur zu Fuß gestartet werden.");
                    canPlay = false;
                }
                if (API.NetworkIsInSpectatorMode())
                {
                    ClientObject.SendMessage("Szenario kann nicht während des Zuschauens gestartet werden.");
                    canPlay = false;
                }
                if (API.GetEntitySpeed(Game.PlayerPed.Handle) > 5.0f)
                {
                    ClientObject.SendMessage("Szenario kann nicht gestartet werden, da sich der Spieler zu schnell bewegt.");
                    canPlay = false;
                }

                if (canPlay)
                {
                    // If the scenario is a "sit" scenario, then the scenario needs to be played at a specific location.
                    if (PedScenarios.PositionBasedScenarios.Contains(scenarioName))
                    {
                        // Get the offset-position from the player. (0.5m behind the player, and 0.5m below the player seems fine for most scenarios)
                        var pos = API.GetOffsetFromEntityInWorldCoords(Game.PlayerPed.Handle, 0f, -0.5f, -0.5f);
                        var heading = API.GetEntityHeading(Game.PlayerPed.Handle);
                        // Play the scenario at the specified location.
                        API.TaskStartScenarioAtPosition(Game.PlayerPed.Handle, scenarioName, pos.X, pos.Y, pos.Z, heading, -1, true, false);
                    }
                    // If it's not a sit scenario (or maybe it is, but using the above native causes other
                    // issues for some sit scenarios so those are not registered as "sit" scenarios), then play it at the current player's position.
                    else
                    {
                        API.TaskStartScenarioInPlace(Game.PlayerPed.Handle, scenarioName, 0, true);
                    }
                }
            }
            // If the new scenario is the same as the currently playing one, cancel the current scenario.
            else
            {
                _currentScenario = "";
                API.ClearPedTasks(Game.PlayerPed.Handle);
                API.ClearPedSecondaryTask(Game.PlayerPed.Handle);
            }

            // If the scenario name to play is called "forcestop" then clear the current scenario and force any tasks to be cleared.
            if (scenarioName == "forcestop")
            {
                _currentScenario = "";
                API.ClearPedTasks(Game.PlayerPed.Handle);
                API.ClearPedTasksImmediately(Game.PlayerPed.Handle);
            }

        }
        public static void ApplyOutfit(Outfit Outfit)
        {
            if (API.GetEntityModel(Game.PlayerPed.Handle) != API.GetHashKey(Outfit.Ped))
            {
                ClientObject.SendMessage($"~r~Outfit passt nur auf {Outfit.Ped}");
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
    }
}
