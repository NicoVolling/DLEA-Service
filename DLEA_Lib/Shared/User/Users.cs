using DLEA_Lib.Shared.Base;
using System.Collections.Generic;
using System.Linq;

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

        public static Dictionary<string, string> DeserializeLoggedInList(string RAW)
        {
            return Json.Deserialize<Dictionary<string, string>>(RAW);
        }

        public static string Serialize()
        {
            return Json.Serialize(List);
        }

        public static string Serialize(List<ExtendedUser> DATA)
        {
            return Json.Serialize(DATA);
        }

        public static string SerializeLoggedInList(Dictionary<string, string> DATA)
        {
            return Json.Serialize(DATA);
        }

        public static string SerializeStoredUsers()
        {
            return Json.Serialize(List.Select(o => o.ToStoredUser()));
        }
    }
}