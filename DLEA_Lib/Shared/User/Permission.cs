using DLEA_Lib.Shared.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.User
{
    public class Permission
    {
        public string Username { get; set; }

        [JsonIgnore]
        public List<string> _Menus = new List<string> { };

        public List<string> Menus { get { return _Menus; } set { if (_Menus.Count != value.Count) { /*throw new Exception("WWWW");*/ } _Menus = value; } }

        [JsonIgnore]
        public string MenusStr
        {
            get 
            {
                return string.Join(",", Menus.ToArray());
            }
            set
            {
                var elements = value.Split(',');
                elements = elements.Where(o => o.Length > 0).ToArray();
                elements = elements.Select(o => o.Replace(" ", "")).ToArray();
                Menus = elements.Distinct().ToList();
            }
        }
    }
}
