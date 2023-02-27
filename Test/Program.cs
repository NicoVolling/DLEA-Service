using DLEA_Lib.Shared.Vehicles;
using DLEA_Lib.Shared.Wardrobe;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        public static List<Vehicle> GetEmergencyVehicles()
        {
            List<Vehicle> Emergency = new List<Vehicle>()
            {
                new Vehicle("AMBULANCE", new List<VehicleCategory>() { new VehicleCategory("Rettungsdienst", 0) } ),
                new Vehicle("FBI", new List<VehicleCategory>() { new VehicleCategory("FBI", 0) }),
                new Vehicle("FBI2",new List<VehicleCategory>() { new VehicleCategory("FBI", 0) }),
                new Vehicle("FIRETRUK",new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("LGUARD",new List<VehicleCategory>() { new VehicleCategory("Rettungsdienst", 0) }),
                new Vehicle("PBUS",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLICE",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLICEB",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLICE2",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLICE3",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLICE4",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLICET",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("POLMAV",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("PRANGER",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("PREDATOR",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("RIOT",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("RIOT2",new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("SHERIFF",new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("SHERIFF2",new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),

                //Addon
                new Vehicle("hpbuffalo",new List<VehicleCategory>() { new VehicleCategory("Autobahn", 0) }),
                new Vehicle("hpbuffalo2",new List<VehicleCategory>() { new VehicleCategory("Autobahn", 0) }),
                new Vehicle("hpumkbuffalo", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("hpumkbuffalo", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("pdbuffalo", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("pdumkbuffalo", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("pdumkbuffalo", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("sdbuffalo", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("sdumkbuffalo", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("umkbuffalo", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("trubuffalo", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("trubuffalo2", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("sdumkbuffalo", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("umkbuffalo", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("trubuffalo", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("trubuffalo2", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("barricade", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("barricade2", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("intscout", new List<VehicleCategory>() { new VehicleCategory("LSPD", 1) }),
                new Vehicle("polsc1", new List<VehicleCategory>() { new VehicleCategory("Autobahn", 0) }),
                new Vehicle("cgexecutioner", new List<VehicleCategory>() { new VehicleCategory("Küstenwache", 0) }),
                new Vehicle("halfback", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("roadrunner", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("roadrunner2", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("watchtower", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("usssminigun", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("usssminigun", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("cat", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("idcar", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("ussssuv", new List<VehicleCategory>() { new VehicleCategory("Verdeckt (Gepanzert)", 0) }),
                new Vehicle("hazard", new List<VehicleCategory>() { new VehicleCategory("Transport", 0) }),
                new Vehicle("hazard2", new List<VehicleCategory>() { new VehicleCategory("Transport", 0) }),
                new Vehicle("usssvan", new List<VehicleCategory>() { new VehicleCategory("Transport", 0) }),
                new Vehicle("usssvan2", new List<VehicleCategory>() { new VehicleCategory("Transport", 0) }),
                new Vehicle("lsfdtruck3", new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("lsfdtruck", new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("lsfdtruck2", new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("lsfd5", new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("lsfd", new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("lsfd2", new List<VehicleCategory>() { new VehicleCategory("Feuerwehr", 0) }),
                new Vehicle("lsfd3", new List<VehicleCategory>() { new VehicleCategory("Rettungsdienst", 0) }),
                new Vehicle("lsfd4", new List<VehicleCategory>() { new VehicleCategory("Rettungsdienst", 0) }),
                new Vehicle("polalamog", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polalamog", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polalamog2", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polalamog2", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polbisong", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polbisong", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polbuffalog2", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polbuffalog2", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polcarag", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polcarag", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polcoquetteg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polcoquetteg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("poldmntg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("poldmntg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polfugitiveg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polfugitiveg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polgauntletg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polgauntletg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polgresleyg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polgresleyg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polroamerg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polroamerg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polscoutg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polscoutg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polstalkerg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polstalkerg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polstanierg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polstanierg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("poltorenceg", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("poltorenceg", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("polvigerog", new List<VehicleCategory>() { new VehicleCategory("Verdeckt", 0) }),
                new Vehicle("polvigerog", new List<VehicleCategory>() { new VehicleCategory("BCSO / LSSD", 1) }),
                new Vehicle("swatvanr", new List<VehicleCategory>() { new VehicleCategory("SWAT", 0) }),
                new Vehicle("swatvanr2", new List<VehicleCategory>() { new VehicleCategory("SWAT", 0) }),
                new Vehicle("swatvans", new List<VehicleCategory>() { new VehicleCategory("SWAT", 0) }),
                new Vehicle("swatvans2", new List<VehicleCategory>() { new VehicleCategory("SWAT", 0) }),
                new Vehicle("swatinsur", new List<VehicleCategory>() { new VehicleCategory("SWAT", 0) }),
                new Vehicle("swatstoc", new List<VehicleCategory>() { new VehicleCategory("SWAT", 0) }),
            };
            return Emergency;
        }

        private static void Main(string[] args)
        {
            List<Vehicle> list = GetEmergencyVehicles();
            List<Vehicle> newlist = new List<Vehicle>();

            foreach (Vehicle item in list)
            {
                if (newlist.FirstOrDefault(o => o.Modelname == item.Modelname) is Vehicle Vehicle)
                {
                    foreach (VehicleCategory Category in item.Categories)
                    {
                        if (!Vehicle.Categories.Any(o => o.Livery == Category.Livery))
                        {
                            Vehicle.Categories.Add(Category);
                        }
                    }
                }
                else
                {
                    newlist.Add(item);
                }
            }

            string json = JsonConvert.SerializeObject(newlist);
            System.IO.File.WriteAllText(@".\Vehicles.json", json);
            Console.Read();
        }
    }
}