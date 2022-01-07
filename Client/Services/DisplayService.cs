using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class DisplayService : Service
    {
        public override string UserFriendlyName => "Anzeigeeinstellungen";

        public override string Name => nameof(DisplayService);

        public DisplayService(ClientObject ClientObject) : base(ClientObject) 
        {
        
        }

        protected override void InitializeSettings()
        {
            base.InitializeSettings();
            Settings.Add(new ServiceSetting(Name, "Spieler", "Spieler Anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Distanz", "Distanz und Geschwindigkeit Anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Location", "Standort Anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Rechts", "Anzeige auf der Rechten Seite", true));
            Settings.Add(new ServiceSetting(Name, "Anzeige", "Anzeige Sichtbar oder nicht", true));
        }

        public override void OnTick()
        {
            base.OnTick();

            Textdisplay.WriteText(ClientObject.GetService<DisplayService>().GetSettingValue("Rechts"));
        }
    }
}
