using CitizenFX.Core.Native;
using CitizenFX.Core;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{

    public class TRouteService : TService
    {
        public TRouteService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnGetRoute = OnGetRoute;
            EventOnRouteRouteList = OnRequestRouteList;
            EventOnDeleteRoute = OnDeleteRoute;
        }

        private void OnDeleteRoute(string Routename)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Routes/Custom.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                List<Route> RouteList = Json.Deserialize<List<Route>>(RAW);

                if (RouteList == null)
                {
                    RouteList = new List<Route>();
                }

                int index = RouteList.FindIndex(o => o.Name == Routename);
                if (index != -1)
                {
                    RouteList.RemoveAt(index);
                }
                RAW = Json.Serialize(RouteList);
                API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/Routes/Custom.json", RAW, -1);
                TServerObject.Trace($"Deleted Route:{Routename}");

                foreach (Player Player in new PlayerList())
                {
                    Player.TriggerEvent(ClientEvents.RoutesService_SendRouteList, RAW);
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        public override string Name => nameof(TRouteService);

        #region Events

        public Action<string> EventOnGetRoute { get; }
        public Action<int> EventOnRouteRouteList { get; }
        public Action<string> EventOnDeleteRoute { get; }

        #endregion Events

        private void OnGetRoute(string RouteRAW)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Routes/Custom.json");
                if (RAW == null)
                {
                    RAW = "[]";
                }
                List<Route> RouteList = Json.Deserialize<List<Route>>(RAW);

                if (RouteList == null)
                {
                    RouteList = new List<Route>();
                }

                Route CurrentRoute = Json.Deserialize<Route>(RouteRAW);

                int index = RouteList.FindIndex(o => o.Name == CurrentRoute.Name);
                if (index != -1)
                {
                    RouteList[index] = CurrentRoute;
                }
                else
                {
                    RouteList.Add(CurrentRoute);
                }
                RAW = Json.Serialize(RouteList);
                API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/Routes/Custom.json", RAW, -1);
                TServerObject.Trace($"Saved Route:{CurrentRoute.Name}");

                foreach (Player Player in new PlayerList())
                {
                    Player.TriggerEvent(ClientEvents.RoutesService_SendRouteList, RAW);
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnRequestRouteList(int PlayerID)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Routes/Custom.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                List<Route> RouteList = Json.Deserialize<List<Route>>(RAW);

                if (RouteList != null)
                {
                    foreach (Player Player in new PlayerList())
                    {
                        Player.TriggerEvent(ClientEvents.RoutesService_SendRouteList, RAW);
                    }
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }
    }
}
