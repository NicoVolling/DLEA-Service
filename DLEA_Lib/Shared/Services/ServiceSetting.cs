namespace DLEA_Lib.Shared.Services
{
    public class ServiceSetting
    {
        public ServiceSetting(string ServiceName, string SettingName, string UserFriendlyName, bool Value = false)
        {
            this.SettingName = SettingName;
            this.Value = Value;
            this.ServiceName = ServiceName;
            this.UserFriendlyName = UserFriendlyName;
        }

        public string ServiceName { get; }

        public string SettingName { get; }
        public string UserFriendlyName { get; }
        public bool Value { get; set; }
    }
}