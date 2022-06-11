using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLEA_Lib.Shared.Einsatz
{
    public class Einsatz
    {
        [JsonIgnore]
        private EinsatzHappening EinsatzHappening;

        public static List<Einsatz> List { get; set; }
        public bool Active { get; set; }
        public int BlipSprite { get; set; }
        public string Code { get; set; }
        public int Happening { get; set; }
        public string ID { get; set; }
        public string Location { get; set; }

        [JsonIgnore]
        public IEnumerable<ExtendedUser> Participants { get => Users.List.Where(o => ParticipantUsernames.Contains(o.Username)); }

        public List<string> ParticipantUsernames { get; set; }
        public DVector3 Position { get; set; }
        public float Radius { get; set; }
        public bool Sonderrechte { get; set; }
        public DateTime StartTime { get; set; }

        public static List<Einsatz> DeserializeList(string RAW)
        {
            List = Json.Deserialize<List<Einsatz>>(RAW);
            return List;
        }

        public static Einsatz GetDATA(string RAW)
        {
            return Json.Deserialize<Einsatz>(RAW);
        }

        public static string GetNewId()
        {
            string id = GenerateID();
            while (List.Any(o => o.ID == id))
            {
                GenerateID();
            }
            return id;
        }

        public static string SerializeList()
        {
            return Json.Serialize(List);
        }

        public static void TickAll()
        {
            if (List != null)
            {
                foreach (Einsatz Einsatz in List)
                {
                    Einsatz.Tick();
                }
            }
        }

        public string GetRAW()
        {
            return Json.Serialize(this);
        }

        public void Run()
        {
            ID = GetNewId();
            if (EinsatzHappening.GetList().FirstOrDefault(o => o.ID == Happening) is EinsatzHappening happening)
            {
                EinsatzHappening = happening;
                happening.Run(this);
            }
        }

        public void Tick()
        {
            EinsatzHappening.Tick(this);
        }

        private static string GenerateID()
        {
            Random random = new Random();
            char[] chars = new char[6];
            for (int i = 0; i < 6; i++)
            {
                chars[i] = (char)random.Next(0, 9);
            }
            return chars.ToString();
        }
    }
}