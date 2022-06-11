using CitizenFX.Core;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Einsatz;
using DLEA_Lib.Shared.EventHandling;
using System;
using System.Linq;

namespace Server.Services
{
    public class TEinsatzService : TService
    {
        public TEinsatzService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnSendEinsatzList = OnSendEinsatzList;
            EventOnGetEinsatz = OnGetEinsatz;
        }

        public override string Name => nameof(TEinsatzService);

        #region "Events"

        public Action<int, string> EventOnGetEinsatz { get; }
        public Action<int> EventOnSendEinsatzList { get; }

        #endregion "Events"

        public void OnSendEinsatzList(int PlayerId)
        {
            OnSendEinsatzList(new PlayerList()[PlayerId]);
        }

        public void OnSendEinsatzList(Player Player)
        {
            try
            {
                Player.TriggerEvent(ClientEvents.VehicleService_SendVehicleList, Einsatz.SerializeList());
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        private void OnGetEinsatz(int PlayerId, string EinsatzRAW)
        {
            try
            {
                Einsatz Einsatz = Einsatz.GetDATA(EinsatzRAW);
                if (Einsatz.List.FirstOrDefault(o => o.ID == Einsatz.ID) is Einsatz einsatz)
                {
                    Einsatz.List[Einsatz.List.IndexOf(einsatz)] = Einsatz;
                }
                else
                {
                    Einsatz.List.Add(Einsatz);
                }
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }
    }
}