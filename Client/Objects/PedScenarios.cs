using System.Collections.Generic;

namespace Client.Objects
{
    public class PedScenarios
    {
        public static List<string> PositionBasedScenarios = new List<string>()
        {
            "PROP_HUMAN_SEAT_ARMCHAIR",
            "PROP_HUMAN_SEAT_BAR",
            "PROP_HUMAN_SEAT_BENCH",
            "PROP_HUMAN_SEAT_BUS_STOP_WAIT",
            "PROP_HUMAN_SEAT_CHAIR",
            "PROP_HUMAN_SEAT_CHAIR_UPRIGHT",
            "PROP_HUMAN_SEAT_CHAIR_MP_PLAYER",
            "PROP_HUMAN_SEAT_COMPUTER",
            "PROP_HUMAN_SEAT_DECKCHAIR",
            //"PROP_HUMAN_SEAT_DECKCHAIR_DRINK",
            //"PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS",
            //"PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS_PRISON",
            "PROP_HUMAN_SEAT_STRIP_WATCH",
            "PROP_HUMAN_SEAT_SUNLOUNGER",
            "WORLD_HUMAN_SEAT_LEDGE",
            "WORLD_HUMAN_SEAT_STEPS",
            "WORLD_HUMAN_SEAT_WALL",
        };

        public static Dictionary<string, Dictionary<string, string>> ScenarioCategorized { get; } = new Dictionary<string, Dictionary<string, string>>()
        {
            ["Pause"] = new Dictionary<string, string>()
            {
                ["Kaffee"] = "WORLD_HUMAN_AA_COFFEE",
                ["Rauchen"] = "WORLD_HUMAN_SMOKING",
                ["Zigarette"] = "WORLD_HUMAN_AA_SMOKE",
                ["Rumhängen"] = "WORLD_HUMAN_HANG_OUT_STREET",
                ["Anlehnen"] = "WORLD_HUMAN_LEANING",
            },
            ["Job"] = new Dictionary<string, string>()
            {
                ["Cop rumstehen"] = "WORLD_HUMAN_COP_IDLES",
                ["Knien"] = "CODE_HUMAN_MEDIC_KNEEL",
                ["Knien 2"] = "CODE_HUMAN_MEDIC_TEND_TO_DEAD",
                ["Notizen"] = "CODE_HUMAN_MEDIC_TIME_OF_DEATH",
                ["Polizei rumstehen"] = "CODE_HUMAN_POLICE_CROWD_CONTROL",
                ["Polizei skeptisch rumstehen"] = "CODE_HUMAN_POLICE_INVESTIGATE",
                ["Fotograf"] = "WORLD_HUMAN_PAPARAZZI",
                ["Fernglas"] = "WORLD_HUMAN_BINOCULARS",
                ["Parkhilfe"] = "WORLD_HUMAN_CAR_PARK_ATTENDANT",
                ["Klemmbrett"] = "WORLD_HUMAN_CLIPBOARD",
                ["Umschauen"] = "WORLD_HUMAN_GUARD_PATROL",
                ["Türstehen"] = "WORLD_HUMAN_GUARD_STAND",
                ["Bohren"] = "WORLD_HUMAN_CONST_DRILL",
                ["Laubblasen"] = "WORLD_HUMAN_GARDENER_LEAF_BLOWER",
                ["Einpflanzen"] = "WORLD_HUMAN_GARDENER_PLANT",
                ["Hämmern"] = "WORLD_HUMAN_HAMMERING",
                ["Menschliche Statue"] = "WORLD_HUMAN_HUMAN_STATUE",
                ["Hausmeister"] = "WORLD_HUMAN_JANITOR",
                ["Fensterputzen"] = "WORLD_HUMAN_MAID_CLEAN",
                ["Prostituierte Premium"] = "WORLD_HUMAN_PROSTITUTE_HIGH_CLASS",
                ["Prostituierte Standard"] = "WORLD_HUMAN_PROSTITUTE_LOW_CLASS",
                ["Mit Lampe schauen"] = "WORLD_HUMAN_SECURITY_SHINE_TORCH",
                ["Karte"] = "WORLD_HUMAN_TOURIST_MAP",
                ["KFZ-Mechatronik"] = "WORLD_HUMAN_VEHICLE_MECHANIC",
                ["Schweißen"] = "WORLD_HUMAN_WELDING",
            },
            ["Obdachlos"] = new Dictionary<string, string>()
            {
                ["Papierschild"] = "WORLD_HUMAN_BUM_FREEWAY",
                ["Sumliegen"] = "WORLD_HUMAN_BUM_SLUMPED",
                ["Rumstehen"] = "WORLD_HUMAN_BUM_STANDING",
                ["Waschen"] = "WORLD_HUMAN_BUM_WASH",
                ["Mülleimer"] = "PROP_HUMAN_BUM_BIN",
                ["Einkaufswagen"] = "PROP_HUMAN_BUM_SHOPPING_CART",
            },
            ["Sozial"] = new Dictionary<string, string>()
            {
                ["Klatschen"] = "WORLD_HUMAN_CHEERING",
                ["Filmen mit Handy"] = "WORLD_HUMAN_MOBILE_FILM_SHOCKING",
            },
            ["Freizeit"] = new Dictionary<string, string>()
            {
                ["Saufen"] = "WORLD_HUMAN_DRINKING",
                ["Joint"] = "WORLD_HUMAN_DRUG_DEALER",
                ["Joint 2"] = "WORLD_HUMAN_DRUG_DEALER_HARD",
                ["Musik"] = "WORLD_HUMAN_MUSICIAN",
                ["Party"] = "WORLD_HUMAN_PARTYING",
                ["Piknik"] = "WORLD_HUMAN_PICNIC",
                ["Joint rauchen"] = "WORLD_HUMAN_SMOKING_POT",
                ["Wanderer Stehen"] = "WORLD_HUMAN_HIKER_STANDING",
                ["Stehen Stripclub"] = "WORLD_HUMAN_STRIP_WATCH_STAND",
                ["Sizen im Stripclup"] = "PROP_HUMAN_SEAT_STRIP_WATCH",
                ["Sonnen"] = "WORLD_HUMAN_SUNBATHE",
                ["Sonnen rücken"] = "WORLD_HUMAN_SUNBATHE_BACK",
                ["Handy hochhalten"] = "WORLD_HUMAN_TOURIST_MOBILE",
                ["Schaufenster stöbern"] = "WORLD_HUMAN_WINDOW_SHOP_BROWSE",
                ["ATM"] = "PROP_HUMAN_ATM",
                ["BBQ"] = "PROP_HUMAN_BBQ",
                ["Parkuhr"] = "PROP_HUMAN_PARKING_METER",
            },
            ["Sport"] = new Dictionary<string, string>()
            {
                ["Joggen (stehend)"] = "WORLD_HUMAN_JOG_STANDING",
                ["Mit Muskeln flexen"] = "WORLD_HUMAN_MUSCLE_FLEX",
                ["Gewicht heben"] = "WORLD_HUMAN_MUSCLE_FREE_WEIGHTS",
                ["Pushups"] = "WORLD_HUMAN_PUSH_UPS",
                ["Situps"] = "WORLD_HUMAN_SIT_UPS",
                ["Tennis"] = "WORLD_HUMAN_TENNIS_PLAYER",
                ["Golf"] = "WORLD_HUMAN_GOLF_PLAYER",
                ["Yoga"] = "WORLD_HUMAN_YOGA",
                ["Klimmzüge"] = "PROP_HUMAN_MUSCLE_CHIN_UPS",
                ["Klimmzüge 2"] = "PROP_HUMAN_MUSCLE_CHIN_UPS_ARMY",
                ["Klimmzüge 3"] = "PROP_HUMAN_MUSCLE_CHIN_UPS_PRISON",
                ["Bankdrücken"] = "PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS",
                ["Bankdrücken 2"] = "PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS_PRISON",
            },
            ["Sitzen"] = new Dictionary<string, string>()
            {
                ["Auf der Leiste"] = "WORLD_HUMAN_SEAT_LEDGE",
                ["Auf den Stufen"] = "WORLD_HUMAN_SEAT_STEPS",
                ["Auf der Wand"] = "WORLD_HUMAN_SEAT_WALL",
                ["Auf dem Boden"] = "WORLD_HUMAN_STUPOR",
                ["Auf Beine lehnen"] = "PROP_HUMAN_SEAT_ARMCHAIR",
                ["Barhocker"] = "PROP_HUMAN_SEAT_BAR",
                ["Auf der Bank"] = "PROP_HUMAN_SEAT_BENCH",
                ["An der Bushaltestelle"] = "PROP_HUMAN_SEAT_BUS_STOP_WAIT",
                ["Auf dem Stuhl"] = "PROP_HUMAN_SEAT_CHAIR",
                ["Auf dem Stuhl 2"] = "PROP_HUMAN_SEAT_CHAIR_UPRIGHT",
                ["Sizen"] = "PROP_HUMAN_SEAT_CHAIR_MP_PLAYER",
                ["Am PC"] = "PROP_HUMAN_SEAT_COMPUTER",
                ["Zurückgelehnt"] = "PROP_HUMAN_SEAT_DECKCHAIR",
                ["Zurückgelehnt Kaffee"] = "PROP_HUMAN_SEAT_DECKCHAIR_DRINK",
                ["Sonnenliege"] = "PROP_HUMAN_SEAT_SUNLOUNGER",
            },
            ["Stehen"] = new Dictionary<string, string>()
            {
                ["Feuer"] = "WORLD_HUMAN_STAND_FIRE",
                ["Angeln"] = "WORLD_HUMAN_STAND_FISHING",
                ["Stehen"] = "WORLD_HUMAN_STAND_IMPATIENT",
                ["Stehen 2"] = "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT",
                ["Handy"] = "WORLD_HUMAN_STAND_MOBILE",
                ["Handy 2"] = "WORLD_HUMAN_STAND_MOBILE_UPRIGHT",
                ["Vor straße warten"] = "CODE_HUMAN_CROSS_ROAD_WAIT",
            },
        };
    }
}