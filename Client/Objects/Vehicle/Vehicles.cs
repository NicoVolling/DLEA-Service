using CitizenFX.Core.Native;
using System.Collections.Generic;

namespace Client.Objects.CommonVehicle
{
    public static class Vehicles
    {
        #region Vehicle List Per Class

        #region Compacts

        public static List<string> Compacts { get; } = new List<string>()
        {
            "ASBO", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "BLISTA",
            "BRIOSO",
            "BRIOSO2", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "CLUB", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "DILETTANTE",
            "DILETTANTE2",
            "ISSI2",
            "ISSI3",
            "ISSI4",
            "ISSI5",
            "ISSI6",
            "KANJO", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "PANTO",
            "PRAIRIE",
            "RHAPSODY",
            "WEEVIL", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
        };

        #endregion Compacts

        #region Sedans

        public static List<string> Sedans { get; } = new List<string>()
        {
            "ASEA",
            "ASEA2",
            "ASTEROPE",
            "COG55",
            "COG552",
            "COGNOSCENTI",
            "COGNOSCENTI2",
            "EMPEROR",
            "EMPEROR2",
            "EMPEROR3",
            "FUGITIVE",
            "GLENDALE",
            "GLENDALE2", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "INGOT",
            "INTRUDER",
            "LIMO2",
            "PREMIER",
            "PRIMO",
            "PRIMO2",
            "REGINA",
            "ROMERO",
            "SCHAFTER2",
            "SCHAFTER5",
            "SCHAFTER6",
            "STAFFORD",
            "STANIER",
            "STRATUM",
            "STRETCH",
            "SUPERD",
            "SURGE",
            "TAILGATER",
            "TAILGATER2", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "WARRENER",
            "WARRENER2", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "WASHINGTON",
        };

        #endregion Sedans

        #region SUVs

        public static List<string> SUVs { get; } = new List<string>()
        {
            "BALLER",
            "BALLER2",
            "BALLER3",
            "BALLER4",
            "BALLER5",
            "BALLER6",
            "BJXL",
            "CAVALCADE",
            "CAVALCADE2",
            "CONTENDER",
            "DUBSTA",
            "DUBSTA2",
            "FQ2",
            "GRANGER",
            "GRESLEY",
            "HABANERO",
            "HUNTLEY",
            "LANDSTALKER",
            "LANDSTALKER2", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "MESA",
            "MESA2",
            "NOVAK", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "PATRIOT",
            "PATRIOT2",
            "RADI",
            "REBLA", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "ROCOTO",
            "SEMINOLE",
            "SEMINOLE2", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "SERRANO",
            "SQUADDIE", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "TOROS",
            "XLS",
            "XLS2",
        };

        #endregion SUVs

        #region Coupes

        public static List<string> Coupes { get; } = new List<string>()
        {
            "COGCABRIO",
            "EXEMPLAR",
            "F620",
            "FELON",
            "FELON2",
            "JACKAL",
            "ORACLE",
            "ORACLE2",
            "PREVION", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "SENTINEL",
            "SENTINEL2",
            "WINDSOR",
            "WINDSOR2",
            "ZION",
            "ZION2",
        };

        #endregion Coupes

        #region Muscle

        public static List<string> Muscle { get; } = new List<string>()
        {
            "BLADE",
            "BUCCANEER",
            "BUCCANEER2",
            "CHINO",
            "CHINO2",
            "CLIQUE",
            "COQUETTE3",
            "DEVIANT",
            "DOMINATOR",
            "DOMINATOR2",
            "DOMINATOR3",
            "DOMINATOR4",
            "DOMINATOR5",
            "DOMINATOR6",
            "DOMINATOR7", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "DOMINATOR8", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "DUKES",
            "DUKES2",
            "DUKES3", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "ELLIE",
            "FACTION",
            "FACTION2",
            "FACTION3",
            "GAUNTLET",
            "GAUNTLET2",
            "GAUNTLET3", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "GAUNTLET4", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "GAUNTLET5", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "HERMES",
            "HOTKNIFE",
            "HUSTLER",
            "IMPALER",
            "IMPALER2",
            "IMPALER3",
            "IMPALER4",
            "IMPERATOR",
            "IMPERATOR2",
            "IMPERATOR3",
            "LURCHER",
            "MANANA2", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "MOONBEAM",
            "MOONBEAM2",
            "NIGHTSHADE",
            "PEYOTE2", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "PHOENIX",
            "PICADOR",
            "RATLOADER",
            "RATLOADER2",
            "RUINER",
            "RUINER2",
            "RUINER3",
            "SABREGT",
            "SABREGT2",
            "SLAMVAN",
            "SLAMVAN2",
            "SLAMVAN3",
            "SLAMVAN4",
            "SLAMVAN5",
            "SLAMVAN6",
            "STALION",
            "STALION2",
            "TAMPA",
            "TAMPA3",
            "TULIP",
            "VAMOS",
            "VIGERO",
            "VIRGO",
            "VIRGO2",
            "VIRGO3",
            "VOODOO",
            "VOODOO2",
            "YOSEMITE",
            "YOSEMITE2", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
        };

        #endregion Muscle

        #region SportsClassics

        public static List<string> SportsClassics { get; } = new List<string>()
        {
            "ARDENT",
            "BTYPE",
            "BTYPE2",
            "BTYPE3",
            "CASCO",
            "CHEBUREK",
            "CHEETAH2",
            "COQUETTE2",
            "DELUXO",
            "DYNASTY", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "FAGALOA",
            "FELTZER3", // Stirling GT
            "GT500",
            "INFERNUS2",
            "JB700",
            "JB7002", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "JESTER3",
            "MAMBA",
            "MANANA",
            "MICHELLI",
            "MONROE",
            "NEBULA", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "PEYOTE",
            "PEYOTE3", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "PIGALLE",
            "RAPIDGT3",
            "RETINUE",
            "RETINUE2", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "SAVESTRA",
            "STINGER",
            "STINGERGT",
            "STROMBERG",
            "SWINGER",
            "TOREADOR", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "TORERO",
            "TORNADO",
            "TORNADO2",
            "TORNADO3",
            "TORNADO4",
            "TORNADO5",
            "TORNADO6",
            "TURISMO2",
            "VISERIS",
            "Z190",
            "ZION3", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "ZTYPE",
        };

        #endregion SportsClassics

        #region Sports

        public static List<string> Sports { get; } = new List<string>()
        {
            "ALPHA",
            "BANSHEE",
            "BESTIAGTS",
            "BLISTA2",
            "BLISTA3",
            "BUFFALO",
            "BUFFALO2",
            "BUFFALO3",
            "CALICO", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "CARBONIZZARE",
            "COMET2",
            "COMET3",
            "COMET4",
            "COMET5",
            "COMET6", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "COQUETTE",
            "COQUETTE4", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "CYPHER", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "DRAFTER", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "ELEGY",
            "ELEGY2",
            "EUROS", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "FELTZER2",
            "FLASHGT",
            "FUROREGT",
            "FUSILADE",
            "FUTO",
            "FUTO2", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "GB200",
            "GROWLER", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "HOTRING",
            "IMORGON", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "ISSI7", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "ITALIGTO",
            "ITALIRSX", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "JESTER",
            "JESTER2",
            "JESTER4", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "JUGULAR", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "KHAMELION",
            "KOMODA", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "KURUMA",
            "KURUMA2",
            "LOCUST", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "LYNX",
            "MASSACRO",
            "MASSACRO2",
            "NEO", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "NEON",
            "NINEF",
            "NINEF2",
            "OMNIS",
            "PARAGON", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "PARAGON2", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "PARIAH",
            "PENUMBRA",
            "PENUMBRA2", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "RAIDEN",
            "RAPIDGT",
            "RAPIDGT2",
            "RAPTOR",
            "REMUS", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "REVOLTER",
            "RT3000", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "RUSTON",
            "SCHAFTER2",
            "SCHAFTER3",
            "SCHAFTER4",
            "SCHAFTER5",
            "SCHLAGEN",
            "SCHWARZER",
            "SENTINEL3",
            "SEVEN70",
            "SPECTER",
            "SPECTER2",
            "SUGOI", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "SULTAN",
            "SULTAN2", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "SULTAN3", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "SURANO",
            "TAMPA2",
            "TROPOS",
            "VERLIERER2",
            "VECTRE", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "VETO", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "VETO2", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "VSTR", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "ZR350", // LS TUNERS (MPTUNER) DLC - Requires b2372
            "ZR380",
            "ZR3802",
            "ZR3803",
        };

        #endregion Sports

        #region Super

        public static List<string> Super { get; } = new List<string>()
        {
            "ADDER",
            "AUTARCH",
            "BANSHEE2",
            "BULLET",
            "CHEETAH",
            "CYCLONE",
            "DEVESTE",
            "EMERUS", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "ENTITYXF",
            "ENTITY2",
            "FMJ",
            "FURIA", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "GP1",
            "INFERNUS",
            "ITALIGTB",
            "ITALIGTB2",
            "KRIEGER", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "LE7B",
            "NERO",
            "NERO2",
            "OSIRIS",
            "PENETRATOR",
            "PFISTER811",
            "PROTOTIPO",
            "REAPER",
            "S80", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "SC1",
            "SCRAMJET",
            "SHEAVA", // ETR1
            "SULTANRS",
            "T20",
            "TAIPAN",
            "TEMPESTA",
            "TEZERACT",
            "THRAX", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "TIGON", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "TURISMOR",
            "TYRANT",
            "TYRUS",
            "VACCA",
            "VAGNER",
            "VIGILANTE",
            "VISIONE",
            "VOLTIC",
            "VOLTIC2",
            "XA21",
            "ZENTORNO",
            "ZORRUSSO", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
        };

        #endregion Super

        #region Motorcycles

        public static List<string> Motorcycles { get; } = new List<string>()
        {
            "AKUMA",
            "AVARUS",
            "BAGGER",
            "BATI",
            "BATI2",
            "BF400",
            "CARBONRS",
            "CHIMERA",
            "CLIFFHANGER",
            "DAEMON",
            "DAEMON2",
            "DEFILER",
            "DEATHBIKE",
            "DEATHBIKE2",
            "DEATHBIKE3",
            "DIABLOUS",
            "DIABLOUS2",
            "DOUBLE",
            "ENDURO",
            "ESSKEY",
            "FAGGIO",
            "FAGGIO2",
            "FAGGIO3",
            "FCR",
            "FCR2",
            "GARGOYLE",
            "HAKUCHOU",
            "HAKUCHOU2",
            "HEXER",
            "INNOVATION",
            "LECTRO",
            "MANCHEZ",
            "MANCHEZ2", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "NEMESIS",
            "NIGHTBLADE",
            "OPPRESSOR",
            "OPPRESSOR2",
            "PCJ",
            "RATBIKE",
            "RROCKET", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "RUFFIAN",
            "SANCHEZ",
            "SANCHEZ2",
            "SANCTUS",
            "SHOTARO",
            "SOVEREIGN",
            "STRYDER", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "THRUST",
            "VADER",
            "VINDICATOR",
            "VORTEX",
            "WOLFSBANE",
            "ZOMBIEA",
            "ZOMBIEB",
        };

        #endregion Motorcycles

        #region OffRoad

        public static List<string> OffRoad { get; } = new List<string>()
        {
            "BFINJECTION",
            "BIFTA",
            "BLAZER",
            "BLAZER2",
            "BLAZER3",
            "BLAZER4",
            "BLAZER5",
            "BODHI2",
            "BRAWLER",
            "BRUISER",
            "BRUISER2",
            "BRUISER3",
            "BRUTUS",
            "BRUTUS2",
            "BRUTUS3",
            "CARACARA",
            "CARACARA2", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "DLOADER",
            "DUBSTA3",
            "DUNE",
            "DUNE2",
            "DUNE3",
            "DUNE4",
            "DUNE5",
            "EVERON", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "FREECRAWLER",
            "HELLION", // CASINO AND RESORT (MPVINEWOOD) DLC - Requires b2060
            "INSURGENT",
            "INSURGENT2",
            "INSURGENT3",
            "KALAHARI",
            "KAMACHO",
            "MARSHALL",
            "MENACER",
            "MESA3",
            "MONSTER",
            "MONSTER3",
            "MONSTER4",
            "MONSTER5",
            "NIGHTSHARK",
            "OUTLAW", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "RANCHERXL",
            "RANCHERXL2",
            "RCBANDITO",
            "REBEL",
            "REBEL2",
            "RIATA",
            "SANDKING",
            "SANDKING2",
            "TECHNICAL",
            "TECHNICAL2",
            "TECHNICAL3",
            "TROPHYTRUCK",
            "TROPHYTRUCK2",
            "VAGRANT", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "VERUS", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "WINKY", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "YOSEMITE3", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "ZHABA", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
        };

        #endregion OffRoad

        #region Industrial

        public static List<string> Industrial { get; } = new List<string>()
        {
            "BULLDOZER",
            "CUTTER",
            "DUMP",
            "FLATBED",
            "GUARDIAN",
            "HANDLER",
            "MIXER",
            "MIXER2",
            "RUBBLE",
            "TIPTRUCK",
            "TIPTRUCK2",
        };

        #endregion Industrial

        #region Utility

        public static List<string> Utility { get; } = new List<string>()
        {
            "AIRTUG",
            "CADDY",
            "CADDY2",
            "CADDY3",
            "DOCKTUG",
            "FORKLIFT",
            "MOWER", // Lawnmower
            "RIPLEY",
            "SADLER",
            "SADLER2",
            "SCRAP",
            "SLAMTRUCK", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "TOWTRUCK",
            "TOWTRUCK2",
            "TRACTOR", // Tractor (rusted/old)
            "TRACTOR2", // Fieldmaster
            "TRACTOR3", // Fieldmaster
            "UTILLITRUCK",
            "UTILLITRUCK2",
            "UTILLITRUCK3",

            /// Trailers

            /// Army Trailers
            "ARMYTRAILER", // Military
            "ARMYTRAILER2", // Civillian
            "FREIGHTTRAILER", // Extended
            "ARMYTANKER", // Army Tanker
            "TRAILERLARGE", // Mobile Operations Center

            /// Large Trailers
            "DOCKTRAILER", // Shipping Container Trailer
            "TR3", // Large Boat Trailer (Sailboat)
            "TR2", // Large Vehicle Trailer
            "TR4", // Large Vehicle Trailer (Mission Cars)
            "TRFLAT", // Large Flatbed Empty Trailer
            "TRAILERS", // Container/Curtain Trailer
            "TRAILERS4", // White Container Trailer
            "TRAILERS2", // Box Trailer
            "TRAILERS3", // Ramp Box Trailer
            "TVTRAILER", // Fame or Shame Trailer
            "TRAILERLOGS", // Logs Trailer
            "TANKER", // Ron Oil Tanker Trailer
            "TANKER2", // Ron Oil Tanker Trailer (Heist Version)

            /// Medium Trailers
            "BALETRAILER", // (Tractor Hay Bale Trailer)
            "GRAINTRAILER", // (Tractor Grain Trailer)

            // Ortega's trailer, we don't want this one because you can't drive them.
            //"PROPTRAILER",

            /// Small Trailers
            "BOATTRAILER", // Small Boat Trailer
            "RAKETRAILER", // Tractor Tow Plow/Rake
            "TRAILERSMALL", // Small Utility Trailer
        };

        #endregion Utility

        #region Vans

        public static List<string> Vans { get; } = new List<string>()
        {
            "BISON",
            "BISON2",
            "BISON3",
            "BOBCATXL",
            "BOXVILLE",
            "BOXVILLE2",
            "BOXVILLE3",
            "BOXVILLE4",
            "BOXVILLE5",
            "BURRITO",
            "BURRITO2",
            "BURRITO3",
            "BURRITO4",
            "BURRITO5",
            "CAMPER",
            "GBURRITO",
            "GBURRITO2",
            "JOURNEY",
            "MINIVAN",
            "MINIVAN2",
            "PARADISE",
            "PONY",
            "PONY2",
            "RUMPO",
            "RUMPO2",
            "RUMPO3",
            "SPEEDO",
            "SPEEDO2",
            "SPEEDO4",
            "SURFER",
            "SURFER2",
            "TACO",
            "YOUGA",
            "YOUGA2",
            "YOUGA3", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
        };

        #endregion Vans

        #region Cycles

        public static List<string> Cycles { get; } = new List<string>()
        {
            "BMX",
            "CRUISER",
            "FIXTER",
            "SCORCHER",
            "TRIBIKE",
            "TRIBIKE2",
            "TRIBIKE3",
        };

        #endregion Cycles

        #region Boats

        public static List<string> Boats { get; } = new List<string>()
        {
            "AVISA", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "DINGHY",
            "DINGHY2",
            "DINGHY3",
            "DINGHY4",
            "DINGHY5", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "JETMAX",
            "KOSATKA", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "LONGFIN", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "MARQUIS",
            "PATROLBOAT", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "PREDATOR",
            "SEASHARK",
            "SEASHARK2",
            "SEASHARK3",
            "SPEEDER",
            "SPEEDER2",
            "SQUALO",
            "SUBMERSIBLE",
            "SUBMERSIBLE2",
            "SUNTRAP",
            "TORO",
            "TORO2",
            "TROPIC",
            "TROPIC2",
            "TUG",
        };

        #endregion Boats

        #region Helicopters

        public static List<string> Helicopters { get; } = new List<string>()
        {
            "AKULA",
            "ANNIHILATOR",
            "ANNIHILATOR2", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "BUZZARD",
            "BUZZARD2",
            "CARGOBOB",
            "CARGOBOB2",
            "CARGOBOB3",
            "CARGOBOB4",
            "FROGGER",
            "FROGGER2",
            "HAVOK",
            "HUNTER",
            "MAVERICK",
            "POLMAV",
            "SAVAGE",
            "SEASPARROW",
            "SEASPARROW2", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "SEASPARROW3", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "SKYLIFT",
            "SUPERVOLITO",
            "SUPERVOLITO2",
            "SWIFT",
            "SWIFT2",
            "VALKYRIE",
            "VALKYRIE2",
            "VOLATUS",
        };

        #endregion Helicopters

        #region Planes

        public static List<string> Planes { get; } = new List<string>()
        {
            "ALKONOST", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
            "ALPHAZ1",
            "AVENGER",
            "AVENGER2",
            "BESRA",
            "BLIMP",
            "BLIMP2",
            "BLIMP3",
            "BOMBUSHKA",
            "CARGOPLANE",
            "CUBAN800",
            "DODO",
            "DUSTER",
            "HOWARD",
            "HYDRA",
            "JET",
            "LAZER",
            "LUXOR",
            "LUXOR2",
            "MAMMATUS",
            "MICROLIGHT",
            "MILJET",
            "MOGUL",
            "MOLOTOK",
            "NIMBUS",
            "NOKOTA",
            "PYRO",
            "ROGUE",
            "SEABREEZE",
            "SHAMAL",
            "STARLING",
            "STRIKEFORCE",
            "STUNT",
            "TITAN",
            "TULA",
            "VELUM",
            "VELUM2",
            "VESTRA",
            "VOLATOL",
        };

        #endregion Planes

        #region Service

        public static List<string> Service { get; } = new List<string>()
        {
            "AIRBUS",
            "BRICKADE",
            "BUS",
            "COACH",
            "PBUS2",
            "RALLYTRUCK",
            "RENTALBUS",
            "TAXI",
            "TOURBUS",
            "TRASH",
            "TRASH2",
            "WASTELANDER",
        };

        #endregion Service

        #region Emergency

        public static List<string> Emergency { get; } = new List<string>()
        {
            "AMBULANCE",
            "FBI",
            "FBI2",
            "FIRETRUK",
            "LGUARD",
            "PBUS",
            "POLICE",
            "POLICE2",
            "POLICE3",
            "POLICE4",
            "POLICEB",
            "POLICEOLD1",
            "POLICEOLD2",
            "POLICET",
            "POLMAV",
            "PRANGER",
            "PREDATOR",
            "RIOT",
            "RIOT2",
            "SHERIFF",
            "SHERIFF2",
        };

        #endregion Emergency

        #region Military

        public static List<string> Military { get; } = new List<string>()
        {
            "APC",
            "BARRACKS",
            "BARRACKS2",
            "BARRACKS3",
            "BARRAGE",
            "CHERNOBOG",
            "CRUSADER",
            "HALFTRACK",
            "KHANJALI",
            "MINITANK", // CASINO HEIST (MPHEIST3) DLC - Requires b2060
            "RHINO",
            "SCARAB",
            "SCARAB2",
            "SCARAB3",
            "THRUSTER", // Jetpack
            "TRAILERSMALL2", // Anti Aircraft Trailer
            "VETIR", // CAYO PERICO (MPHEIST4) DLC - Requires b2189
        };

        #endregion Military

        #region Commercial

        public static List<string> Commercial { get; } = new List<string>()
        {
            "BENSON",
            "BIFF",
            "CERBERUS",
            "CERBERUS2",
            "CERBERUS3",
            "HAULER",
            "HAULER2",
            "MULE",
            "MULE2",
            "MULE3",
            "MULE4",
            "PACKER",
            "PHANTOM",
            "PHANTOM2",
            "PHANTOM3",
            "POUNDER",
            "POUNDER2",
            "STOCKADE",
            "STOCKADE3",
            "TERBYTE",
        };

        #endregion Commercial

        #region Trains

        public static List<string> Trains { get; } = new List<string>()
        {
            "CABLECAR",
            "FREIGHT",
            "FREIGHTCAR",
            "FREIGHTCONT1",
            "FREIGHTCONT2",
            "FREIGHTGRAIN",
            "METROTRAIN",
            "TANKERCAR",
        };

        #endregion Trains

        #region OpenWheel

        public static List<string> OpenWheel { get; } = new List<string>()
        {
            "FORMULA",
            "FORMULA2",
            "OPENWHEEL1", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
            "OPENWHEEL2", // SUMMER SPECIAL (MPSUM) DLC - Requires b2060
        };

        #endregion OpenWheel

        /*
        Compacts = 0,
        Sedans = 1,
        SUVs = 2,
        Coupes = 3,
        Muscle = 4,
        SportsClassics = 5,
        Sports = 6,
        Super = 7,
        Motorcycles = 8,
        OffRoad = 9,
        Industrial = 10,
        Utility = 11,
        Vans = 12,
        Cycles = 13,
        Boats = 14,
        Helicopters = 15,
        Planes = 16,
        Service = 17,
        Emergency = 18,
        Military = 19,
        Commercial = 20,
        Trains = 21
            */

        public static Dictionary<string, List<string>> VehicleClasses { get; } = new Dictionary<string, List<string>>()
        {
            [API.GetLabelText("VEH_CLASS_18")] = Emergency,
            [API.GetLabelText("VEH_CLASS_19")] = Military,
            [API.GetLabelText("VEH_CLASS_17")] = Service,
            [API.GetLabelText("VEH_CLASS_10")] = Industrial,
            [API.GetLabelText("VEH_CLASS_11")] = Utility,
            [API.GetLabelText("VEH_CLASS_20")] = Commercial,
            [API.GetLabelText("VEH_CLASS_12")] = Vans,
            [API.GetLabelText("VEH_CLASS_14")] = Boats,
            [API.GetLabelText("VEH_CLASS_15")] = Helicopters,
            [API.GetLabelText("VEH_CLASS_16")] = Planes,
            [API.GetLabelText("VEH_CLASS_8")] = Motorcycles,
            [API.GetLabelText("VEH_CLASS_9")] = OffRoad,
            [API.GetLabelText("VEH_CLASS_2")] = SUVs,
            [API.GetLabelText("VEH_CLASS_0")] = Compacts,
            [API.GetLabelText("VEH_CLASS_1")] = Sedans,
            [API.GetLabelText("VEH_CLASS_3")] = Coupes,
            [API.GetLabelText("VEH_CLASS_4")] = Muscle,
            [API.GetLabelText("VEH_CLASS_5")] = SportsClassics,
            [API.GetLabelText("VEH_CLASS_6")] = Sports,
            [API.GetLabelText("VEH_CLASS_7")] = Super,
            [API.GetLabelText("VEH_CLASS_13")] = Cycles,
            [API.GetLabelText("VEH_CLASS_22")] = OpenWheel,

            [API.GetLabelText("VEH_CLASS_21")] = Trains,
        };

        public static List<int> VehicleClassesInt { get; } = new List<int>()
        {
            18,
            19,
            17,
            10,
            11,
            20,
            12,
            14,
            15,
            16,
            8,
            9,
            2,
            0,
            1,
            3,
            4,
            5,
            6,
            7,
            13,
            22,
            21
        };

        #endregion Vehicle List Per Class

        public static string Category_Autobahn = "Autobahnpolizei";

        public static string Category_BCSOLSSD = "BCSO / LSSD";

        public static string Category_FBI = "FBI";

        public static string Category_Feuerwehr = "Feuerwehr";

        public static string Category_Küstenwache = "Küstenwache";

        public static string Category_LSPD = "LSPD";

        public static string Category_LSPDSWAT = "LSPD SWAT";

        public static string Category_NOOSE = "NOOSE";

        public static string Category_Rettungsdienst = "Rettungsdienst";

        public static string Category_Transport = "Transport / Vans";

        public static string Category_Verdeckt = "Verdeckt";

        public static string Category_VerdecktGepanzert = "Verdeckt (Gepanzert)";

        public static List<string> vehs = new List<string>();

        public static string[] GetAllVehicles()
        {
            if (vehs.Count == 0)
            {
                foreach (KeyValuePair<string, List<string>> vc in VehicleClasses)
                {
                    foreach (string c in vc.Value)
                    {
                        vehs.Add(c);
                    }
                }
            }
            return vehs.ToArray();
        }

        public static List<string> GetEmergencyCategories()
        {
            return new List<string>()
            {
                Category_Autobahn,
                Category_BCSOLSSD,
                Category_FBI,
                Category_Feuerwehr,
                Category_Küstenwache,
                Category_LSPD,
                Category_LSPDSWAT,
                Category_NOOSE,
                Category_Rettungsdienst,
                Category_Transport,
                Category_Verdeckt,
                Category_VerdecktGepanzert
            };
        }

        public static List<KeyValuePair<string, string>> GetEmergencyVehicles()
        {
            List<KeyValuePair<string, string>> Emergency = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("AMBULANCE", Category_Rettungsdienst ),
                new KeyValuePair<string, string>("FBI", Category_FBI),
                new KeyValuePair<string, string>("FBI2",Category_FBI),
                new KeyValuePair<string, string>("FIRETRUK",Category_Feuerwehr),
                new KeyValuePair<string, string>("LGUARD",Category_Rettungsdienst),
                new KeyValuePair<string, string>("PBUS",Category_LSPD),
                new KeyValuePair<string, string>("POLICE",Category_LSPD),
                new KeyValuePair<string, string>("POLICEB",Category_LSPD),
                new KeyValuePair<string, string>("POLICE2",Category_LSPD),
                new KeyValuePair<string, string>("POLICE3",Category_LSPD),
                new KeyValuePair<string, string>("POLICE4",Category_LSPD),
                new KeyValuePair<string, string>("POLICET",Category_LSPD),
                new KeyValuePair<string, string>("POLMAV",Category_LSPD),
                new KeyValuePair<string, string>("PRANGER",Category_LSPD),
                new KeyValuePair<string, string>("PREDATOR",Category_LSPD),
                new KeyValuePair<string, string>("RIOT",Category_LSPD),
                new KeyValuePair<string, string>("RIOT2",Category_LSPD),
                new KeyValuePair<string, string>("SHERIFF",Category_BCSOLSSD),
                new KeyValuePair<string, string>("SHERIFF2",Category_BCSOLSSD),

                //Addon
                new KeyValuePair<string, string>("hpbuffalo",Category_Autobahn),
                new KeyValuePair<string, string>("hpbuffalo2",Category_Autobahn),
                new KeyValuePair<string, string>("hpumkbuffalo",Category_Verdeckt),
                new KeyValuePair<string, string>("pdbuffalo", Category_LSPD),
                new KeyValuePair<string, string>("pdumkbuffalo", Category_Verdeckt),
                new KeyValuePair<string, string>("pdumkbuffalo", Category_Verdeckt),
                new KeyValuePair<string, string>("sdbuffalo", Category_BCSOLSSD),
                new KeyValuePair<string, string>("sdumkbuffalo", Category_Verdeckt),
                new KeyValuePair<string, string>("umkbuffalo", Category_Verdeckt),
                new KeyValuePair<string, string>("trubuffalo", Category_Verdeckt),
                new KeyValuePair<string, string>("trubuffalo2", Category_Verdeckt),
                new KeyValuePair<string, string>("barricade", Category_LSPD),
                new KeyValuePair<string, string>("barricade2", Category_LSPD),
                new KeyValuePair<string, string>("intscout", Category_LSPD),
                new KeyValuePair<string, string>("polsc1", Category_Autobahn),
                new KeyValuePair<string, string>("cgexecutioner", Category_Küstenwache),
                new KeyValuePair<string, string>("halfback", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("roadrunner", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("roadrunner2", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("watchtower", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("usssminigun", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("usssminigun", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("cat", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("idcar", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("ussssuv", Category_VerdecktGepanzert),
                new KeyValuePair<string, string>("hazard", Category_Transport),
                new KeyValuePair<string, string>("hazard2", Category_Transport),
                new KeyValuePair<string, string>("usssvan", Category_Transport),
                new KeyValuePair<string, string>("usssvan2", Category_Transport),
                new KeyValuePair<string, string>("lsfdtruck3", Category_Feuerwehr),
                new KeyValuePair<string, string>("lsfdtruck", Category_Feuerwehr),
                new KeyValuePair<string, string>("lsfdtruck2", Category_Feuerwehr),
                new KeyValuePair<string, string>("lsfd5", Category_Feuerwehr),
                new KeyValuePair<string, string>("lsfd", Category_Feuerwehr),
                new KeyValuePair<string, string>("lsfd2", Category_Feuerwehr),
                new KeyValuePair<string, string>("lsfd3", Category_Rettungsdienst),
                new KeyValuePair<string, string>("lsfd4", Category_Rettungsdienst),
                new KeyValuePair<string, string>("polalamog", Category_Verdeckt),
                new KeyValuePair<string, string>("polalamog", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polalamog2", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polalamog2", Category_Verdeckt),
                new KeyValuePair<string, string>("polbisong", Category_Verdeckt),
                new KeyValuePair<string, string>("polbisong", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polbuffalog2", Category_Verdeckt),
                new KeyValuePair<string, string>("polbuffalog2", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polcarag", Category_Verdeckt),
                new KeyValuePair<string, string>("polcarag", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polcoquetteg", Category_Verdeckt),
                new KeyValuePair<string, string>("polcoquetteg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("poldmntg", Category_Verdeckt),
                new KeyValuePair<string, string>("poldmntg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polfugitiveg", Category_Verdeckt),
                new KeyValuePair<string, string>("polfugitiveg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polgauntletg", Category_Verdeckt),
                new KeyValuePair<string, string>("polgauntletg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polgresleyg", Category_Verdeckt),
                new KeyValuePair<string, string>("polgresleyg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polroamerg", Category_Verdeckt),
                new KeyValuePair<string, string>("polroamerg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polscoutg", Category_Verdeckt),
                new KeyValuePair<string, string>("polscoutg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polstalkerg", Category_Verdeckt),
                new KeyValuePair<string, string>("polstalkerg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polstanierg", Category_Verdeckt),
                new KeyValuePair<string, string>("polstanierg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("poltorenceg", Category_Verdeckt),
                new KeyValuePair<string, string>("poltorenceg", Category_BCSOLSSD),
                new KeyValuePair<string, string>("polvigerog", Category_Verdeckt),
                new KeyValuePair<string, string>("polvigerog", Category_BCSOLSSD),
                new KeyValuePair<string, string>("swatvanr", Category_LSPDSWAT),
                new KeyValuePair<string, string>("swatvanr2", Category_LSPDSWAT),
                new KeyValuePair<string, string>("swatvans", Category_LSPDSWAT),
                new KeyValuePair<string, string>("swatvans2", Category_LSPDSWAT),
                new KeyValuePair<string, string>("swatinsur", Category_LSPDSWAT),
                new KeyValuePair<string, string>("swatstoc", Category_LSPDSWAT),
            };
            return Emergency;
        }
    }
}