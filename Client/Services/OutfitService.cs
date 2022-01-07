using CitizenFX.Core.Native;
using DLEA_Lib;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class OutfitService : Service
    {
        public override string UserFriendlyName => "Outfiteinstellungen";

        public override string Name => nameof(OutfitService);

        public OutfitService(ClientObject ClientObject) : base(ClientObject) 
        {
            EventOnGetResult = OnGetResult;
        }

        public override void Start() 
        {
            ClientObject.TriggerServerEvent(ServerEvents.OutfitService_RequestOutfit, ServerID);
        }

        private void OnGetResult(string RAW)
        {
            try 
            {
                List<Outfit> OutfitList = Json.Deserialize<List<Outfit>>(RAW);
                if(OutfitList != null) 
                {
                    Outfits.CustomOutfitList = OutfitList;
                    ClientObject.MainMenu.Login();
                }
            }
            catch(Exception ex) 
            {
                Tracing.Trace(ex);
            }
        }

        #region Events
        public Action<string> EventOnGetResult { get; set; }
        #endregion
    }
}
