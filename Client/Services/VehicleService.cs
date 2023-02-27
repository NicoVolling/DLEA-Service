using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Vehicles;
using System;
using System.Collections.Generic;
using Vehicle = CitizenFX.Core.Vehicle;

namespace Client.Services
{
    public class VehicleService : Service
    {
        public VehicleService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetVehicleList = OnGetVehicleList;
        }

        public List<DLEA_Lib.Shared.Vehicles.Vehicle> Addons { get; } = new List<DLEA_Lib.Shared.Vehicles.Vehicle>();

        public override string Name => nameof(VehicleService);

        public override string UserFriendlyName => "Fahrzeuge";

        #region "Events"

        public Action<string> EventOnGetVehicleList { get; }

        #endregion "Events"

        public override void OnTick()
        {
            base.OnTick();
            OnTick_SpeedLimiter();
        }

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
                List<DLEA_Lib.Shared.Vehicles.Vehicle> Vehicles = Json.Deserialize<List<DLEA_Lib.Shared.Vehicles.Vehicle>>(VehiclesRAW);
                if (Vehicles != null)
                {
                    Addons.Clear();
                    Addons.AddRange(Vehicles);
                    ClientObject.MainMenu.Login();
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        private void OnTick_SpeedLimiter()
        {
            Ped Ped = new PlayerList()[CurrentUser.ServerID].Character;
            if (API.IsPedInAnyVehicle(Ped.Handle, false))
            {
                Vehicle Vehicle = new Vehicle(API.GetVehiclePedIsIn(Ped.Handle, false));
                if (Vehicle.GetPedOnSeat(VehicleSeat.Driver) == Ped)
                {
                    uint streetname = 1;
                    uint crossingroad = 1;
                    string Street = "";
                    API.GetStreetNameAtCoord(Ped.Position.X, Ped.Position.Y, Ped.Position.Z, ref streetname, ref crossingroad);
                    Street = API.GetStreetNameFromHashKey(streetname);

                    int SpeedLimit = 30;
                    if (SpeedLimits.List.ContainsKey(Street))
                    {
                        SpeedLimit = SpeedLimits.List[Street];
                    }

                    Textdisplay.SpeedLimit = (int)Math.Round(1.60934 * SpeedLimit);
                }
            }
        }
    }
}