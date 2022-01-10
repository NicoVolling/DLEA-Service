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
    public class TVehicleService : TService
    {
        public override string Name => nameof(TLocationService);

        #region "Events"
        
        public Action<int> EventOnSendVehicleList { get; }

        #endregion

        public TVehicleService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnSendVehicleList = OnSendVehicleList;
        }

        public void OnSendVehicleList(int PlayerId)
        {
            OnSendVehicleList(new PlayerList()[PlayerId]);
        }

        public void OnSendVehicleList(Player Player) 
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Vehicles/Addons.json");
                Player.TriggerEvent(ClientEvents.VehicleService_SendVehicleList, RAW);
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}
