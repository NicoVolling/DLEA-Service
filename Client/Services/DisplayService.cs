using DLEA_Lib.Shared.Services;
using System.Collections.Generic;

namespace Client.Services
{
    public class DisplayService : Service
    {
        public DisplayService(ClientObject ClientObject) : base(ClientObject)
        {
        }

        public override string Name => nameof(DisplayService);
        public override string UserFriendlyName => "Anzeigeeinstellungen";
        public List<string> Users { get; set; } = new List<string>();

        public override void OnTick()
        {
            base.OnTick();

            Textdisplay.WriteText(ClientObject);
        }

        protected override void InitializeSettings()
        {
            base.InitializeSettings();
            Settings.Add(new ServiceSetting(Name, "Spieler", "Details anderer Spieler Anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Distanz", "Distanz und Geschwindigkeit anderer Spieler Anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Fahrzeug", "Fahrzeuge anderer Spieler anziegen", true));
            Settings.Add(new ServiceSetting(Name, "Fahrzeugschaden", "Fahrzeugschaden anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Standort", "Standort anderer Spieler Anzeigen", true));
            Settings.Add(new ServiceSetting(Name, "Rechts", "Anzeige auf der Rechten Seite", true));
            Settings.Add(new ServiceSetting(Name, "Anzeige", "Anzeige Sichtbar oder nicht", true));
            Settings.Add(new ServiceSetting(Name, "Geschwindigkeit", "Zeigt die eigene Geschwindigkeit an", true));
        }
    }
}