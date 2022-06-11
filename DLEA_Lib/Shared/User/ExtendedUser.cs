using DLEA_Lib.Shared.Base;
using Newtonsoft.Json;

namespace DLEA_Lib.Shared.User
{
    public class ExtendedUser : StoredUser
    {
        public string Department { get => _Department; set => _Department = value; }

        public DVector3? DepartmentCoords { get; set; }

        public bool IsAutoaimActive { get; set; }

        public bool IsWaypointActive { get; set; }

        [JsonIgnore]
        public override string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Rang))
                {
                    return $"{Vorname} {Nachname}";
                }
                return $"{Rang} {Vorname} {Nachname}";
            }
        }

        public string Rang { get; set; }

        public string Status
        {
            get
            {
                if (string.IsNullOrEmpty(_Status)) { _Status = "nicht im Dienst"; }
                return _Status;
            }
            set => _Status = value;
        }

        public bool Visible { get; set; }

        public DVector3 Waypoint { get; set; }

        protected string _Department { get; set; }

        protected string _Status { get; set; } = "nicht im Dienst";

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