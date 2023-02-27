using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Vehicles
{
    public class Vehicle
    {
        public Vehicle()
        { }

        public Vehicle(string modelname, List<VehicleCategory> categories)
        {
            Categories = categories;
            Modelname = modelname;
        }

        public List<VehicleCategory> Categories { get; set; }

        public string Modelname { get; set; }
    }
}