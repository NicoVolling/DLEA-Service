using Newtonsoft.Json;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.Game;

namespace DLEA_Lib.Shared.User
{
    public class ExtendedUser : StoredUser
    {
        [JsonIgnore]
        public virtual string Name { get => $"{Rang}{Vorname} {Nachname}"; }

        public DVector3 Position { get; set; }

        public DVector3 Waypoint { get; set; }

        public bool IsWaypointActive { get; set; }

        public int PlayerSprite { get; set; }

        public bool IsSirenOn { get; set; }

        public bool IsSirenSoundOn { get; set; }

        public double? Heading { get; set; }

        public bool Visible { get; set; }

        public double? Velocity { get; set; }

        public bool IsAiming { get; set; }

        public bool IsShooting { get; set; }

        public bool IsAutoaimActive { get; set; }

        public string Rang { get => string.IsNullOrEmpty(_Rang) ? "" : $"{_Rang} "; set => _Rang = value; }

        public string Status { get { if (string.IsNullOrEmpty(_Status)) { _Status = "nicht im Dienst"; } return _Status; } set => _Status = value; }

        public string Departement { get => _Departement; set => _Departement = value; }

        protected string _Rang { get; set; }

        protected string _Status { get; set; } = "nicht im Dienst";

        protected string _Departement { get; set; }


        public static new ExtendedUser GetData(string RAW)
        {
            return Json.Deserialize<ExtendedUser>(RAW);
        }

        public new string GetUserRAW()
        {
            return Json.Serialize(this);
        }

        public string GetStoredUserRaw()
        {
            return ToStoredUser().GetUserRAW();
        }

        public StoredUser ToStoredUser() 
        {
            return Json.Deserialize<StoredUser>(Json.Serialize(this));
        }
    }
}
