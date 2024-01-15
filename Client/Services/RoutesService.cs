using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Navigation;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class RoutesService : Service
    {
        public RoutesService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetList = OnGetList;
        }

        public override string UserFriendlyName => "Routen";

        public override string Name => nameof(RoutesService);

        public override void Start()
        {
            ClientObject.TriggerServerEvent(ServerEvents.RoutesService_RequestRouteList, ServerID);
        }

        private void OnGetList(string RAW)
        {
            try
            {
                List<Route> RouteList = Json.Deserialize<List<Route>>(RAW);
                if (RouteList != null)
                {
                    ClientObject.MainMenu.ApplyRoutes(RouteList);
                }
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
        }

        #region Events

        public Action<string> EventOnGetList { get; set; }

        #endregion Events
    }
}
