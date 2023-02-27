using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Vehicles
{
    public class VehicleCategory
    {
        public VehicleCategory()
        { }

        public VehicleCategory(string name, int livery)
        {
            this.Name = name;
            this.Livery = livery;
        }

        public int Livery { get; set; }

        public string Name { get; set; }
    }
}