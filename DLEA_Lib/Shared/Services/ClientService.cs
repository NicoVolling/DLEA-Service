using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Services
{
    public abstract class ClientService : BaseService
    {
        public List<ServiceSetting> Settings { get; set; } = new List<ServiceSetting>();

        public bool GetSettingValue(string Name)
        {
            if (Settings != null)
            {
                if (Settings.Any(o => o.SettingName == Name))
                {
                    return Settings.Where(o => o.SettingName == Name).First().Value;
                }
            }
            return false;
        }

        public ClientService() 
        {
            DefaultSettings();
        }

        public void DefaultSettings() 
        {
            Settings = new List<ServiceSetting>();
            InitializeSettings();
        }

        protected virtual void InitializeSettings() { }
    }
}
