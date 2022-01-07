using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Services
{
    public class ServiceSetting
    {
        public string ServiceName { get; }

        public string UserFriendlyName { get; }

        public string SettingName { get; }

        public bool Value { get; set; }

        public ServiceSetting(string ServiceName, string SettingName, string UserFriendlyName, bool Value = false) 
        {
            this.SettingName = SettingName;
            this.Value = Value;
            this.ServiceName = ServiceName;
            this.UserFriendlyName = UserFriendlyName;
        }
    }
}
