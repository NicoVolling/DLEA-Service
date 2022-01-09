using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Locations;
using DLEA_Lib.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class LocationService : Service
    { 
        public override string Name => nameof(LocationService);

        public override string UserFriendlyName => "Orte";

        #region "Events"
        public Action<string> EventOnGetLocations { get; }
        #endregion

        public LocationService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetLocations = OnGetLocations;
        }

        protected override void InitializeSettings()
        {
            Settings.Add(new ServiceSetting(Name, "Markierungen", "Markierungen auf der Karte anzeigen", true));
            base.InitializeSettings();
        }

        private void OnGetLocations(string LocationsRAW)
        {
            try 
            {
                Location.Deserialize(LocationsRAW);
                foreach (KeyValuePair<Location, int> KVP in Blips)
                {
                    int blip = KVP.Value;
                    API.RemoveBlip(ref blip);
                }
                Blips.Clear();
            } 
            catch(Exception ex) { Tracing.Trace(ex); } 
        }

        public override void Start()
        {
            ClientObject.TriggerServerEvent(ServerEvents.LocationService_GetLocations, Game.Player.ServerId);
        }

        public Dictionary<Location, int> Blips { get; set; } = new Dictionary<Location, int>();

        private void InitializeLocations() 
        {
            try
            {
                if (GetSettingValue("Markierungen"))
                {
                    foreach (Location Location in Location.List)
                    {
                        int blipID = API.AddBlipForCoord(Location.coordinates.X, Location.coordinates.Y, Location.coordinates.Z);
                        API.SetBlipSprite(blipID, Location.spriteID);
                        API.BeginTextCommandSetBlipName("STRING");
                        API.AddTextComponentSubstringPlayerName(Location.name);
                        API.EndTextCommandSetBlipName(blipID);
                        API.SetBlipColour(blipID, Location.color);
                        API.SetBlipAsShortRange(blipID, true);
                        Blips.Add(Location, blipID);
                    }
                }
                else 
                {
                    foreach(KeyValuePair<Location, int> KVP in Blips) 
                    {
                        int blip = KVP.Value;
                        API.RemoveBlip(ref blip);
                    }

                    Blips.Clear();
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        public override void OnTick()
        {
            if ((Blips.Count > 0 && !GetSettingValue("Markierungen")) || (Blips.Count == 0 && GetSettingValue("Markierungen")))
            {
                InitializeLocations();
            }
        }
    }
}
