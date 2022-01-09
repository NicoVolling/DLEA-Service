using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class TLocationService : TService
    {
        public override string Name => nameof(TLocationService);

        #region "Events"
        
        public Action<int> EventOnSendLocations { get; }

        #endregion

        public TLocationService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnSendLocations = OnSendLocations;
        }

        public void OnSendLocations(int PlayerId)
        {
            OnSendLocations(new PlayerList()[PlayerId]);
        }

        public void OnSendLocations(Player Player) 
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Locations/Locations.json");
                Player.TriggerEvent(ClientEvents.LocationService_SendLocations, RAW);
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}
