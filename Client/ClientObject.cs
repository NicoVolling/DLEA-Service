using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.ClientHelper;
using Client.Menu;
using Client.Services;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class ClientObject : BaseScript
    {
        #region Menu

        private Action Tick_Menu;

        public MainMenu MainMenu { get; set; }

        #endregion Menu

        public ClientObject()
        {
            InitializeTracing();
            InitializeUser();
            InitializeServices();
            AddEventHandlers();
            InitializeMenu();
            DoTick();

            foreach (Service Service in Services)
            {
                Service.Start();
            }

            #region 21zu9

            int x = 0, y = 0;
            API.GetScreenActiveResolution(ref x, ref y);

            if (x == 3440)
            {
                Task requestscale = new Task(async () =>
                {
                    int minimap = API.RequestScaleformMovie("minimap");
                    while (!API.HasScaleformMovieLoaded(minimap))
                    {
                        await CommonFunctions.Delay(0);
                    }
                    API.SetMinimapComponentPosition("minimap", "L", "B", -0.1345f, -0.002f, 0.150f, 0.188888f);
                    API.SetMinimapComponentPosition("minimap_mask", "L", "B", -0.1345f, -0.002f, 0.150f, 0.188888f);
                    API.SetMinimapComponentPosition("minimap_blur", "L", "B", -0.160f, +0.014f, 0.265f, 0.24f);

                    API.SetRadarBigmapEnabled(true, false);
                    API.Wait(500);
                    API.SetRadarBigmapEnabled(false, false);
                });
                requestscale.Start();
            }

            #endregion 21zu9
        }

        public static int ServerID { get => API.GetPlayerServerId(API.PlayerId()); }

        public ExtendedUser CurrentUser { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();

        public static void SendMessage(string Message)
        {
            Screen.ShowNotification($"~o~[DLEA]~w~ {Message}");
        }

        public static void Trace(string Message)
        {
            try
            {
                if (Message.Contains("Error"))
                {
                    Screen.ShowNotification($"~r~{Message}");
                }
            }
            catch { }
            Debug.WriteLine(Message);
        }

        public T GetService<T>() where T : Service
        {
            return Services.Where(o => o is T).Select(o => o as T).FirstOrDefault();
        }

        public void InitializeUser()
        {
            CurrentUser = new ExtendedUser() { Vorname = Game.Player.ServerId.ToString(), Nachname = Game.Player.Name, ServerID = ServerID, Visible = true };
        }

        private void AddEventHandlers()
        {
            try
            {
                EventHandlers[ClientEvents.SyncService_SendPlayerList] += GetService<SyncService>().EventOnGetPlayerList;
                EventHandlers[ClientEvents.SyncService_ChangeWeather] += GetService<SyncService>().EventOnChangeWeather;
                EventHandlers[ClientEvents.DataService_SendPlayerData] += GetService<DataService>().EventOnGetResult;
                EventHandlers[ClientEvents.DataService_AutoLogin] += GetService<DataService>().EventOnGetAutoLoginData;
                EventHandlers[ClientEvents.OutfitService_GetOutfit] += GetService<OutfitService>().EventOnGetResult;
                EventHandlers[ClientEvents.MessageService_SendMessages] += GetService<MessageService>().EventOnGetResult;
                EventHandlers[ClientEvents.DataService_PermissiosChanged] += GetService<DataService>().EventOnPermissionsChanged;
                EventHandlers[ClientEvents.LocationService_SendLocations] += GetService<LocationService>().EventOnGetLocations;
                EventHandlers[ClientEvents.VehicleService_SendVehicleList] += GetService<VehicleService>().EventOnGetVehicleList;
                //EventHandlers[ClientEvents.EinsatzService_SendEinsatz] += GetService<EinsatzService>().EventOnGetEinsatz;
                //EventHandlers[ClientEvents.EinsatzService_SendEinsatzlist] += GetService<EinsatzService>().EventOnGetEinsatzListe;
                EventHandlers[ClientEvents.RoutesService_SendRouteList] += GetService<RoutesService>().EventOnGetList;
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
        }

        private void DoTick()
        {
            Tick += async () =>
            {
                try
                {
                    Tick_Menu();
                    foreach (Service Service in Services)
                    {
                        Service.OnTick();
                    }
                    OnTick_Gameplay();
                }
                catch (Exception ex)
                {
                    Trace(ex.ToString());
                    SendMessage("~r~KRITISCHER FEHLER");
                    throw;
                }
            };
        }

        private void OnTick_Gameplay()
        {
            
        }

        private void InitializeMenu()
        {
            MainMenu = new MainMenu(this, out Tick_Menu);
        }

        private void InitializeServices()
        {
            Services.Add(new MessageService(this));
            Services.Add(new SyncService(this));
            Services.Add(new DataService(this));
            Services.Add(new DisplayService(this));
            Services.Add(new OutfitService(this));
            Services.Add(new LocationService(this));
            Services.Add(new VehicleService(this));
            //Services.Add(new EinsatzService(this));
            Services.Add(new RoutesService(this));
        }

        private void InitializeTracing()
        {
            Tracing.TraceAction = (message) =>
            {
                Trace(message);
            };
            Tracing.TraceExAction = (ex) =>
            {
                SendMessage("~r~Fehler ~w~bei ~y~" + ex.Source);
            };
        }
    }
}