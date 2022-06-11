using System.Collections.Generic;
using System.Linq;

namespace DLEA_Lib.Shared.Services
{
    public abstract class ClientService : BaseService
    {
        public ClientService()
        {
            DefaultSettings();
        }

        public List<ServiceSetting> Settings { get; set; } = new List<ServiceSetting>();

        public void DefaultSettings()
        {
            Settings = new List<ServiceSetting>();
            InitializeSettings();
        }

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

        protected virtual void InitializeSettings()
        { }
    }
}