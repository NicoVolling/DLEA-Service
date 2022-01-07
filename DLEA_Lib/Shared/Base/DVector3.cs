using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Base
{
    public struct DVector3
    {
        public float X;
        public float Y;
        public float Z;

        public DVector3(float X = 0, float Y = 0, float Z = 0) 
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
    }
}
