using System;
using System.Collections.Generic;

namespace DLEA_Lib.Shared.Einsatz
{
    public class EinsatzHappening
    {
        public static EinsatzHappening EmptyEinsatzHappening = new EinsatzHappening(-1, "(Ohne)", e => { }, e => { });
        private static List<EinsatzHappening> List;

        public EinsatzHappening(int ID, string Name, Action<Einsatz> RunAction, Action<Einsatz> OnTickAction)
        {
            this.ID = ID;
            this.RunAction = RunAction;
            this.OnTickAction = OnTickAction;
            this.Name = Name;
        }

        public int ID { get; }

        public string Name { get; }

        private Action<Einsatz> OnTickAction { get; }

        private Action<Einsatz> RunAction { get; }

        public static void FillList(List<EinsatzHappening> List)
        {
            EinsatzHappening.List = List;
        }

        public static List<EinsatzHappening> GetList()
        {
            if (List == null)
            {
                return new List<EinsatzHappening>();
            }
            return List;
        }

        public void Run(Einsatz Einsatz)
        {
            RunAction.Invoke(Einsatz);
        }

        public void Tick(Einsatz Einsatz)
        {
            OnTickAction.Invoke(Einsatz);
        }
    }
}