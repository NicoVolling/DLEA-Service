using DLEA_Lib.Shared.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Base
{
    public static class Json
    {
        public static Formatting Formatting = Formatting.Indented;

        public static T Deserialize<T>(string RAW)
        {
            T CurrentObject;
            try
            {
                CurrentObject = JsonConvert.DeserializeObject<T>(RAW);
                return CurrentObject;
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
                return default(T);
            }
        }

        public static string Serialize(Object DATA) 
        {
            try
            {
                return JsonConvert.SerializeObject(DATA, Formatting);
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
                return string.Empty;
            }
        }
    }
}
