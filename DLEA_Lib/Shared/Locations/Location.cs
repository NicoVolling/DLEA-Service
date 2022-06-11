using DLEA_Lib.Shared.Base;
using System.Collections.Generic;

namespace DLEA_Lib.Shared.Locations
{
    public class Location
    {
        public int color;
        public DVector3 coordinates;
        public string name;
        public int spriteID;

        public Location(string name, DVector3 coordinates, int spriteID, int color)
        {
            this.name = name;
            this.coordinates = coordinates;
            this.spriteID = spriteID;
            this.color = color;
        }

        public Location()
        {
        }

        public static List<Location> List { get; set; } = new List<Location>();

        public static List<Location> Deserialize(string RAW)
        {
            List = Json.Deserialize<List<Location>>(RAW);
            return List;
        }

        public static string Serialize()
        {
            return Json.Serialize(List);
        }
    }
}