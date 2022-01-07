using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Game
{
    public enum EnumWeather
    {
        BLIZZARD = 11,
        CLEAR = 1,
        CLEARING = 8,
        CLOUDS = 2,
        EXTRASUNNY = 0,
        FOGGY = 4,
        HALLOWEEN = 14,
        NEUTRAL = 9,
        OVERCAST = 5,
        RAIN = 6,
        SMOG = 3,
        SNOW = 10,
        SNOWLIGHT = 12,
        THUNDER = 7,
        XMAS = 13,
    }

    public static class EnumWeatherHelper
    {
        public static string GetWeatherName(EnumWeather weather)
        {
            return Enum.GetName(typeof(EnumWeather), weather);
        }

        private static Dictionary<string, EnumWeather> WeatherDictionary = new Dictionary<string, EnumWeather>()
            {
                { "Sonnig", EnumWeather.EXTRASUNNY },
                { "Klar", EnumWeather.CLEAR},
                { "Wolkig", EnumWeather.CLOUDS },
                { "Bedeckt", EnumWeather.OVERCAST },
                { "Smog", EnumWeather.SMOG },
                { "Nebel", EnumWeather.FOGGY },
                { "Leichter Regen", EnumWeather.CLEARING },
                { "Regen", EnumWeather.RAIN },
                { "Gewitter", EnumWeather.THUNDER },
                { "Leichter Schnee", EnumWeather.SNOWLIGHT },
                { "Schnee", EnumWeather.SNOW },
                { "Schneesturm", EnumWeather.BLIZZARD },
                { "Schnee (bleibt liegen)", EnumWeather.XMAS },
            };
        public static Dictionary<string, EnumWeather> GetUserFriendlyNames() 
        {
            return WeatherDictionary;
        }

        public static EnumWeather GetEnumWeather(string UserFriendlyName) 
        {
            if (GetUserFriendlyNames().ContainsKey(UserFriendlyName)) 
            {
                return GetUserFriendlyNames()[UserFriendlyName];
            }
            return EnumWeather.EXTRASUNNY;
        }
    }
}
