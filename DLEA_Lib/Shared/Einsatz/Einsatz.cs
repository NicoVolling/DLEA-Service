using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Einsatz
{
    public class Einsatz
    {
        public int ID { get; set; }

        public static List<Einsatz> List { get; set; }

        public List<string> ParticipantUsernames { get; set; }

        [JsonIgnore]
        public IEnumerable<ExtendedUser> Participants { get => Users.List.Where(o => ParticipantUsernames.Contains(o.Username)); }

        public DateTime StartTime { get; set; }

        public string Code { get; set; }

        public bool Sonderrechte { get; set; }

        public DVector3 Position { get; set; }

        public string Location { get; set; }

        public float Radius { get; set; }

        public bool Active { get; set; }

        public int BlipSprite { get; set; }

        public Einsatzkategorie Kategorie { get; set; }

        public static int GetNewId() 
        {
            return 0;
        }

        public string GetRAW() 
        {
            return Json.Serialize(this);
        }

        public static Einsatz GetDATA(string RAW) 
        {
            return Json.Deserialize<Einsatz>(RAW);
        }

        public static string SerializeList()
        {
            return Json.Serialize(List);
        }

        public static List<Einsatz> DeserializeList(string RAW)
        {
            List = Json.Deserialize<List<Einsatz>>(RAW);
            return List;
        }
    }
}
