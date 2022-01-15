using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.ClientHelper
{
    public static class ObjectCompare
    {
        public static new bool Equals(Object Object1, Object Object2) 
        {
            return Json.Serialize(Object1) == Json.Serialize(Object2);
        }
    }
}
