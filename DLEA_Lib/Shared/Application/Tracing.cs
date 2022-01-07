using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEA_Lib.Shared.Application
{
    public static class Tracing
    {
        public static string GetTrace(string Playername, string Information)
        {
            //{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}/
            return $"{Playername}: {Information}";
        }

        public static Action<string> TraceAction;
        public static Action<Exception> TraceExAction;

        public static void TraceString(string Information) 
        {
            if (TraceAction != null)
            {
                TraceAction(Information);
            }
        }

        public static void Trace(Object InformationObject) 
        {
            TraceString(InformationObject.ToString());
        }

        public static void Trace(Exception ex) 
        {
            try 
            { 
                if (TraceAction != null)
                {
                    TraceAction($"Error: {ex?.Source} : {ex?.Message}; {ex?.StackTrace}");
                }
                if (TraceExAction != null) 
                {
                    TraceExAction(ex);
                }
            }
            catch 
            {
            }
        }
    }
}