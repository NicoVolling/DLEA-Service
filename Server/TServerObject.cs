using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Wardrobe;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class TServerObject : BaseScript
    {
        public List<TService> Services { get; set; } = new List<TService>();

        public TServerObject() 
        {
            InitializeTracing();
            InitializeServices();
            AddEventhandlers();
            DoTick();
        }

        private void InitializeTracing() 
        {
            Tracing.TraceAction = (message) =>
            {
                TServerObject.Trace(message);
            };
        }

        private void InitializeServices()
        {
            Services.Add(new TSyncService(this));
            Services.Add(new TDataService(this));
            Services.Add(new TOutfitService(this));
            Services.Add(new TMessageService(this));
            Services.Add(new TLocationService(this));
            Services.Add(new TVehicleService(this));
            Trace($"Initialized All Services");
        }

        private T GetService<T>() where T : TService
        {
            return Services.Where(o => o is T).Select(o => o as T).FirstOrDefault();
        }

        private void AddEventhandlers()
        {
            EventHandlers[ServerEvents.SyncService_SendData]           += GetService<TSyncService>().EventOnGetPlayerData;
            EventHandlers[ServerEvents.SyncService_ChangeWeather]      += GetService<TSyncService>().EventOnChangeWeather;
            EventHandlers[ServerEvents.DataService_SendPlayerData]     += GetService<TDataService>().EventOnGetPlayerData;
            EventHandlers[ServerEvents.DataService_RequestPlayerData]  += GetService<TDataService>().EventOnRequestPlayerData;
            EventHandlers[ServerEvents.DataService_ChangePlayerData]   += GetService<TDataService>().EventOnChangePlayerData;
            EventHandlers[ServerEvents.DataService_DeletePlayerData]   += GetService<TDataService>().EventOnDeletePlayer;
            EventHandlers[ServerEvents.DataService_Login]              += GetService<TDataService>().EventOnLogin;
            EventHandlers[ServerEvents.DataService_GetLogin]           += GetService<TDataService>().EventOnSendAutoLoginData;
            EventHandlers[ServerEvents.OutfitService_GetOutfit]        += GetService<TOutfitService>().EventOnGetOutfit;
            EventHandlers[ServerEvents.OutfitService_RequestOutfit]    += GetService<TOutfitService>().EventOnRequestOutfitList;
            EventHandlers[ServerEvents.MessageService_GetPing]         += GetService<TMessageService>().EventOnGetPing;
            EventHandlers[ServerEvents.DataService_SetPermissions]     += GetService<TDataService>().EventOnGetPermissions;
            EventHandlers[ServerEvents.LocationService_GetLocations]   += GetService<TLocationService>().EventOnSendLocations;
            EventHandlers[ServerEvents.VehicleService_GetVehicleList]  += GetService<TVehicleService>().EventOnSendVehicleList;
            EventHandlers["playerDropped"]                             += GetService<TSyncService>().EventOnPlayerLeft;
            Trace($"Initialized Eventhandlers");
        }

        public static void Trace(string Message)
        {
            Debug.WriteLine(Message);
        }

        public static void Trace(Exception ex) 
        {
            Debug.WriteLine($"{ex.Source} : {ex.Message}; {ex.StackTrace}");
        }

        private void DoTick()
        {
            Tick += async () =>
            {
                foreach (TService Service in Services)
                {
                    Service.OnTick();
                }
            };
            Trace($"Initialized Tick-Events");
        }
    }
}