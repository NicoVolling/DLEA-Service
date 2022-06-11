using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DLEA_Lib.Shared.User
{
    public class Permission
    {
        public List<string> Menus = new List<string> { };

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

        public string Username { get; set; }
    }
}