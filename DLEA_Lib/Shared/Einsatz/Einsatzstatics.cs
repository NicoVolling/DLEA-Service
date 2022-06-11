using System.Collections.Generic;

namespace DLEA_Lib.Shared.Einsatz
{
    public static class Einsatzstatics
    {
        public static Dictionary<string, List<string>> EinsatzKategorien = new Dictionary<string, List<string>>()
        {
            { "[V] Vekehr", new List<string>()
                {
                    "[V-21] Verkehrsunfall",
                    "[V-22] Erhöhtegeschwindigkeit",
                    "[V-23] Verkehrsstörung",
                    "[V-24] Fahrerflucht",
                    "[V-26] Illegales Rennen"
                }
            },

            { "[S] Sachen", new List<string>()
                {
                    "[S-21] Diebstahl",
                    "[S-22] Raub",
                    "[S-23] Bewaffneter Raubüberfall",
                    "[S-24] Gestohlener Gegenstand",
                    "[S-25] Vandalismus"
                }
            },

            { "[G] Gewalt", new List<string>()
                {
                    "[G-21] Gewalttätige Auseinandersetzung",
                    "[G-22] Suizid",
                    "[G-23] Sexualdelikt",
                    "[G-24] Schuss in der Nähe",
                    "[G-25] Tötung / Leichenfund",
                    "[G-26] Entführung",
                    "[G-27] Geiselnahme",
                    "[G-28] Freiheitsberaubung"
                }
            },

            { "[A] Störung", new List<string>()
                {
                    "[A-21] Ruhestörung",
                    "[A-22] Haus-, Landfriedensbruch",
                    "[A-23] Auseinandersetzung, Streit"
                }
            },

            { "[T] Terror", new List<string>()
                {
                    "[T-21] Bombendrohung",
                    "[T-22] Bombenfund",
                    "[T-23] Staatsfeind",
                    "[T-24] Terror"
                }
            },

            { "[F] Feuer", new List<string>()
                {
                    "[F-21] Kleinbrand",
                    "[F-22] Brand",
                    "[F-23] Wohnungsbrand",
                    "[F-24] Großbrand"
                }
            },

            { "[E] Personen", new List<string>()
                {
                    "[E-21] Verwundete Person",
                    "[E-22] Schwer verwundete Person",
                    "[E-23] Bewusstlose Person",
                    "[E-24] Herzinfarkt",
                    "[E-25] Schlaganfall",
                    "[E-26] Verstorbene Person",
                    "[E-27] Schwächeanfall",
                    "[E-28] SOS-Signal",
                    "[E-29] Person im Wasser in Not",
                    "[E-30] Massenanfall von Verletzten"
                }
            },

            { "[D] Drogen", new List<string>()
                {
                    "[D-21] Drogenfund",
                    "[D-22] Drogenrazzia",
                    "[D-23] Person unter Einfluss von Drogen",
                    "[D-24] Drogenhandel",
                    "[D-25] Drogenbesitz"
                }
            },

            { "[H] Gegenstände", new List<string>()
                {
                    "[H-21] Illegales Mitführen von Waffen",
                    "[H-22] Illegales Mitführen von Schusswaffen",
                    "[H-23] Schmuggelware"
                }
            },

            { "[W] Widerstand", new List<string>()
                {
                    "[W-21] Selbsständiger Entzug der Haftstrafe",
                    "[W-22] Verhindern einer Verhaftung",
                    "[W-23] Aufhalten / Behindern von Rettungskräften"
                }
            },

            { "[SF] Schusswechel", new List<string>()
                {
                    "[SF-1] Schusswechsel in unmittelbarer Nähe",
                    "[SF-2] Scgzsswechsel mit Verletzten Personen",
                    "[SF-3] Schusswechsel mit Vollautomatischen Schusswaffen"
                }
            },

            { "[J] Divers", new List<string>()
                {
                    "[J-21] Eskorte",
                    "[J-22] Transport",
                    "[J-23] Warnung der Bevölkerung",
                    "[J-24] Evakuierung eines Bereichs"
                }
            },

            { "[K] Sonstiges", new List<string>()
                {
                    "[K-21] Sonstiges"
                }
            },
        };
    }
}