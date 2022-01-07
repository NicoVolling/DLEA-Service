using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.User
{
    public class StoredUser
    {
        public string Username { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        [JsonIgnore]
        public virtual string Name { get => $"{Vorname} {Nachname}"; }

        public string Password { get; set; }

        public bool LoggedIn { get; set; }

        public bool JustCreated { get; set; }

        public bool Exists { get; set; }

        public List<ServiceSetting> Settings { get; set; } = new List<ServiceSetting>();

        public int ServerID { get; set; }

        public long TimeStamp { get; set; }

        public bool Admin { get; set; }

        public string LastPlayedVersion { get; set; }

        public Permission Permissions { get; set; } = new Permission();

        [JsonIgnore]
        public string Ping { 
            get 
            {
                return ((int)DateTime.Now.Subtract(new DateTime(TimeStamp)).TotalMilliseconds).ToString() + " ms";
                if (DateTime.Now.Subtract(new DateTime(TimeStamp)).TotalMilliseconds > 1500)
                {
                    return ((int)DateTime.Now.Subtract(new DateTime(TimeStamp)).TotalSeconds).ToString() + " s";
                }
            } 
        }

        public bool GetSetting(string ServiceName, string SettingName)
        {
            if (Settings.Any(o => o.ServiceName == ServiceName && o.SettingName == SettingName))
            {
                return Settings.Where(o => o.ServiceName == ServiceName && o.SettingName == SettingName).FirstOrDefault().Value;
            }
            else
            {
                return false;
            }
        }

        public void LoadSettings(IEnumerable<ClientService> Services)
        {
            if (Settings == null) { Settings = new List<ServiceSetting>(); }
            foreach (ClientService Service in Services)
            {
                Service.DefaultSettings();
                foreach (ServiceSetting Setting in Settings)
                {
                    if (Setting.ServiceName == Service.Name && Service.Settings.Any(o => o.SettingName == Setting.SettingName))
                    {
                        Service.Settings.RemoveAll(o => o.SettingName == Setting.SettingName && o.ServiceName == Setting.ServiceName);
                        Service.Settings.Add(Setting);
                    }
                }
            }
        }

        public void SaveSettings(IEnumerable<ClientService> Services)
        {
            Settings = new List<ServiceSetting>();
            foreach (ClientService Service in Services)
            {
                Settings.AddRange(Service.Settings);
            }
        }

        public static StoredUser GetData(string RAW)
        {
            return Json.Deserialize<StoredUser>(RAW);
        }

        public string GetUserRAW()
        {
            if(this is ExtendedUser CU) 
            {
                return CU.GetStoredUserRaw();
            }
            return Json.Serialize(this);
        }

        public ExtendedUser ToExtendedUser()
        {
            return ExtendedUser.GetData(this.GetUserRAW());
        }
    }
}
