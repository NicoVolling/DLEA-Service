using CitizenFX.Core;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using System;
using System.Collections.Generic;

namespace Client.Services
{
    public class VehicleService : Service
    {
        public VehicleService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetVehicleList = OnGetVehicleList;
        }

        public List<string> Addons { get; } = new List<string>();
        public override string Name => nameof(VehicleService);

        public override string UserFriendlyName => "Fahrzeuge";

        #region "Events"

        public Action<string> EventOnGetVehicleList { get; }

        #endregion "Events"

        public override void Start()
        {
            ClientObject.TriggerServerEvent(ServerEvents.VehicleService_GetVehicleList, Game.Player.ServerId);
        }

        protected override void InitializeSettings()
        {
            base.InitializeSettings();
        }

        private void OnGetVehicleList(string VehiclesRAW)
        {
            try
            {
                List<string> Vehicles = Json.Deserialize<List<string>>(VehiclesRAW);
                if (Vehicles != null)
                {
                    Addons.Clear();
                    Addons.AddRange(Vehicles);
                    ClientObject.MainMenu.Login();
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}