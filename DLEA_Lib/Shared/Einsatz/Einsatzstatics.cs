using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Einsatz
{
    public static class Einsatzstatics
    {
        public static List<Einsatzkategorie> Einsatzkategorien = new List<Einsatzkategorie>()
        {
            new Einsatzkategorie("Einsatz", 58),
            new Einsatzkategorie("Einsatz n. n. bez.", 455),
            new Einsatzkategorie("Polizeieinsatz", 60),
            new Einsatzkategorie("Feuerwehreinsatz", 436),
            new Einsatzkategorie("Sanitätereinsatz", 61),
            new Einsatzkategorie("Drogenfahndung", 51),
            new Einsatzkategorie("Hubschraubereinsatz", 64),
            new Einsatzkategorie("Abschleppeinsatz", 68),
            new Einsatzkategorie("Transporteinsatz", 318),
            new Einsatzkategorie("Privater Einsatz", 272),
            new Einsatzkategorie("Rettungsschwimmer", 280),
            new Einsatzkategorie("Küstenwacheneinsatz", 455),
            new Einsatzkategorie("Bombenentschärfung", 486),
            new Einsatzkategorie("Immigrationsbehörde", 419),
        };
    }

    public class Einsatzkategorie
    {
        public Einsatzkategorie(string Bezeichnung, int BlipSprite)
        {
            this.BlipSprite = BlipSprite;
            this.Bezeichnung = Bezeichnung;
        }

        public string Bezeichnung { get; set; }
        public int BlipSprite { get; set; }
    }
}