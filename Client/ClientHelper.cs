using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
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
