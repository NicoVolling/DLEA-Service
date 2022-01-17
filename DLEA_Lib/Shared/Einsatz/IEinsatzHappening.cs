using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Einsatz
{
    public class EinsatzHappening
    {
        private static List<EinsatzHappening> List;

        public static List<EinsatzHappening> GetList() 
        {
            if(List == null) 
            {
                return new List<EinsatzHappening>();
            }
            return List;
        }

        public static void FillList(List<EinsatzHappening> List) 
        {
            EinsatzHappening.List = List;
        }

        public static EinsatzHappening EmptyEinsatzHappening = new EinsatzHappening(-1, "(Ohne)", e => { }, e => { });

        public int ID { get; }

        private Action<Einsatz> RunAction { get; }

        private Action<Einsatz> OnTickAction { get; }

        public string Name { get; }

        public void Run(Einsatz Einsatz) 
        {
            RunAction.Invoke(Einsatz);
        }

        public EinsatzHappening(int ID, string Name, Action<Einsatz> RunAction, Action<Einsatz> OnTickAction) 
        {
            this.ID = ID;
            this.RunAction = RunAction;
            this.OnTickAction = OnTickAction;
            this.Name = Name;
        }

        public void Tick(Einsatz Einsatz) 
        {
            OnTickAction.Invoke(Einsatz);
        }
    }
}
