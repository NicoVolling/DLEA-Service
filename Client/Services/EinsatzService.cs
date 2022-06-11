using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Einsatz;
using DLEA_Lib.Shared.Locations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client.Services
{
    public class EinsatzService : Service
    {
        public EinsatzService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetEinsatz = OnGetEinsatz;
            EventOnGetEinsatzListe = OnGetEinsatzListe;
        }

        public Dictionary<Location, int> Blips { get; set; } = new Dictionary<Location, int>();
        public override string Name => nameof(EinsatzService);

        public override string UserFriendlyName => "Einsätze";

        #region "Events"

        public Action<string> EventOnGetEinsatz { get; }
        public Action<string> EventOnGetEinsatzListe { get; }

        #endregion "Events"

        public override void OnTick()
        {
            Einsatz.TickAll();
        }

        public override void Start()
        {
            int i = -1;
            EinsatzHappening.FillList(new List<EinsatzHappening>()
            {
                new EinsatzHappening(i++, "Brennendes Auto", SpawnBurningCar, (Einsatz) => { }),
            });
        }

        public void StartEinsatz()
        {
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
                if (Einsatz.List.FirstOrDefault(o => o.ID == Einsatz.ID) is Einsatz einsatz)
                {
                    Einsatz.List[Einsatz.List.IndexOf(einsatz)] = Einsatz;
                }
                else
                {
                    Einsatz.List.Add(Einsatz);
                }
                ClientObject.MainMenu.RefreshEinsätze();
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        private void OnGetEinsatzListe(string EinsatzListeRAW)
        {
            try
            {
                Einsatz.DeserializeList(EinsatzListeRAW);
            }
            catch (Exception ex) { Tracing.Trace(ex); }
        }

        private void SpawnBurningCar(Einsatz Einsatz)
        {
        }
    }
}