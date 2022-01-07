using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class TOutfitService : TService
    {
        public override string Name => nameof(TOutfitService);


        #region Events
        public Action<string> EventOnGetOutfit { get; }
        public Action<int> EventOnRequestOutfitList { get; }
        #endregion

        public TOutfitService(TServerObject ServerObject) : base(ServerObject) 
        {
            EventOnGetOutfit = OnGetOutfit;
            EventOnRequestOutfitList = OnRequestOutfitList;
        }

        private void OnRequestOutfitList(int PlayerID)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Outfits/Custom.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                List<Outfit> OutfitList = Json.Deserialize<List<Outfit>>(RAW);

                if (OutfitList != null)
                {
                    foreach (Player Player in new PlayerList())
                    {
                        Player.TriggerEvent(ClientEvents.OutfitService_GetOutfit, RAW);
                    }
                }
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnGetOutfit(string OutfitRAW)
        {
            try
            {
                string RAW = API.LoadResourceFile(API.GetCurrentResourceName(), "./Data/Outfits/Custom.json");
                if (RAW == null)
                {
                    RAW = string.Empty;
                }
                List<Outfit> OutfitList = Json.Deserialize<List<Outfit>>(RAW);

                if(OutfitList == null) 
                {
                    OutfitList = new List<Outfit>();
                }

                Outfit CurrentOutfit = Json.Deserialize<Outfit>(OutfitRAW);

                int index = OutfitList.FindIndex(o => o.Name == CurrentOutfit.Name && o.IsMale == CurrentOutfit.IsMale);
                if (index != -1)
                {
                    OutfitList[index] = CurrentOutfit;
                }
                else
                {
                    OutfitList.Add(CurrentOutfit);
                }
                RAW = Json.Serialize(OutfitList);
                API.SaveResourceFile(API.GetCurrentResourceName(), "./Data/Outfits/Custom.json", RAW, -1);
                TServerObject.Trace($"Saved Outfit:{CurrentOutfit.Name}");

                foreach (Player Player in new PlayerList())
                {
                    Player.TriggerEvent(ClientEvents.OutfitService_GetOutfit, RAW);
                }
                
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }
    }
}
