using DLEA_Lib.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Navigation
{
    public class Route
    {
        public List<DVector3> Stops { get; set; } = new List<DVector3>();

        public string Name { get; set; }
    }
}
