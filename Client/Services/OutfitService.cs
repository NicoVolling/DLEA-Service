using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;

namespace Client.Services
{
    public class OutfitService : Service
    {
        public OutfitService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetResult = OnGetResult;
        }

        public override string Name => nameof(OutfitService);
        public override string UserFriendlyName => "Outfiteinstellungen";

        public override void Start()
        {
            ClientObject.TriggerServerEvent(ServerEvents.OutfitService_RequestOutfit, ServerID);
        }

        private void OnGetResult(string RAW)
        {
            try
            {
                List<Outfit> OutfitList = Json.Deserialize<List<Outfit>>(RAW);
                if (OutfitList != null)
                {
                    Outfits.CustomOutfitList = OutfitList;
                    ClientObject.MainMenu.Login();
                }
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
        }

        #region Events

        public Action<string> EventOnGetResult { get; set; }

        #endregion Events
    }
}