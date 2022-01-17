using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using Client.Objects;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Einsatz;
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
    public class EinsatzService : Service
    { 
        public override string Name => nameof(EinsatzService);

        public override string UserFriendlyName => "Einsätze";

        #region "Events"
        public Action<string> EventOnGetEinsatz { get; }
        public Action<string> EventOnGetEinsatzListe { get; }
        #endregion

        public EinsatzService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetEinsatz = OnGetEinsatz;
            EventOnGetEinsatzListe = OnGetEinsatzListe;
        }

        private void OnGetEinsatzListe(string EinsatzListeRAW)
        {
            try
            {
                Einsatz.DeserializeList(EinsatzListeRAW);
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        protected override void InitializeSettings()
        {
            base.InitializeSettings();
        }

        private void OnGetEinsatz(string EinsatzRAW)
        {
            try 
            {
                Einsatz Einsatz = Einsatz.GetDATA(EinsatzRAW);
                if(Einsatz.List.FirstOrDefault(o => o.ID == Einsatz.ID) is Einsatz einsatz) 
                {
                    Einsatz.List[Einsatz.List.IndexOf(einsatz)] = Einsatz;
                } 
                else 
                {
                    Einsatz.List.Add(Einsatz);
                }
                ClientObject.MainMenu.RefreshEinsätze();
            } 
            catch(Exception ex) { Tracing.Trace(ex); } 
        }

        public void StartEinsatz() 
        {
            
        }

        public override void Start()
        {
            int i = -1;
            EinsatzHappening.FillList(new List<EinsatzHappening>()
            {
                new EinsatzHappening(i++, "Brennendes Auto", SpawnBurningCar, (Einsatz) => { }),
            });

            //ClientObject.TriggerServerEvent(ServerEvents.EinsatzService_GetEinsatzListe, Game.Player.ServerId);
        }

        private void SpawnBurningCar(Einsatz Einsatz) 
        {
        
        }

        public Dictionary<Location, int> Blips { get; set; } = new Dictionary<Location, int>();

        public override void OnTick()
        {
            Einsatz.TickAll();
            //int ped = -1;
            //if(Game.PlayerPed.IsAiming && API.GetEntityPlayerIsFreeAimingAt(Game.PlayerPed.Handle, ref ped)) 
            //{
            //    CommonFunctions.PlayScenario(new Ped(ped), "WORLD_HUMAN_CHEERING");
            //}
        }
    }
}
