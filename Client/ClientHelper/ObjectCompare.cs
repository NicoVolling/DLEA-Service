using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using System;

namespace Client.ClientHelper
{
    public static class ObjectCompare
    {
        public new static bool Equals(Object Object1, Object Object2)
        {
            string str1 = Json.Serialize(Object1);
            string str2 = Json.Serialize(Object2);
            string[] strr1 = str2.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] strr2 = str1.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            bool equals = true;
            if (strr1.Length != strr2.Length)
            {
                //Tracing.TraceString($"{DateTime.Now} :: Changes: Count");
                return false;
            }
            int i = 0;
            foreach (string ln in strr1)
            {
                if (!ln.Equals(strr2[i]))
                {
                    //Tracing.TraceString($"{DateTime.Now} :: Changes: {strr2[i]} - - {strr1[i]}");
                    equals = false;
                }
                i++;
            }
            return equals;
        }
    }
}