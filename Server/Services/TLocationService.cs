using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using System;

namespace Server.Services
{
    public class TLocationService : TService
    {
        public TLocationService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnSendLocations = OnSendLocations;
        }

        public override string Name => nameof(TLocationService);

        #region "Events"

        public Action<int> EventOnSendLocations { get; }

        #endregion "Events"

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