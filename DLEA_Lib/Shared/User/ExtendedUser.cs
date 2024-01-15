using DLEA_Lib.Shared.Base;
using Newtonsoft.Json;

namespace DLEA_Lib.Shared.User
{
    public class ExtendedUser : StoredUser
    {

        public DVector3? DepartmentCoords { get; set; }

        public bool IsWaypointActive { get; set; }

        [JsonIgnore]
        public override string Name
        {
            get
            {
                return $"{Vorname} {Nachname}";
            }
        }

        public bool Visible { get; set; }

        public DVector3 Waypoint { get; set; }

        public new static ExtendedUser GetData(string RAW)
        {
            return Json.Deserialize<ExtendedUser>(RAW);
        }

        public string GetStoredUserRaw()
        {
            return ToStoredUser().GetUserRAW();
        }

        public new string GetUserRAW()
        {
            return Json.Serialize(this);
        }

        public StoredUser ToStoredUser()
        {
            return Json.Deserialize<StoredUser>(Json.Serialize(this));
        }
    }
}