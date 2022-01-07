using DLEA_Lib.Shared.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.User
{
    public static class Users
    {
        public static List<ExtendedUser> List { get; set; }

        public static List<ExtendedUser> Deserialize(string RAW, bool ReplaceStaticList = true)
        {
            List<ExtendedUser> UserList = new List<ExtendedUser>();
            UserList = Json.Deserialize<List<ExtendedUser>>(RAW);
            if (ReplaceStaticList)
            {
                List = UserList;
            }
            return UserList;
        }

        public static string Serialize()
        {
            return Json.Serialize(List);
        }

        public static string SerializeStoredUsers() 
        {
            return Json.Serialize(List.Select(o => o.ToStoredUser()));
        }

        public static string Serialize(List<ExtendedUser> DATA)
        {
            return Json.Serialize(DATA);
        }

        public static string SerializeLoggedInList(Dictionary<string, string> DATA)
        {
            return Json.Serialize(DATA);
        }

        public static Dictionary<string, string> DeserializeLoggedInList(string RAW)
        {
            return Json.Deserialize<Dictionary<string, string>>(RAW);
        }
    }
}
