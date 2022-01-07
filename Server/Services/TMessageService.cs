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
    public class TMessageService : TService
    {
        public override string Name => nameof(TOutfitService);


        #region Events
        public Action<int> EventOnGetPing { get; }
        #endregion

        public TMessageService(TServerObject ServerObject) : base(ServerObject) 
        {
            EventOnGetPing = OnGetPing;
        }

        public static void SendMessage(int PlayerID, string Message) 
        {
            SendMessage(new PlayerList()[PlayerID], Message);
        }

        public static void SendMessage(Player Player, string Message) 
        {
            try
            {
                Player.TriggerEvent(ClientEvents.MessageService_SendMessage, $"~o~[DLEA] {Message}");
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }

        private void OnGetPing(int PlayerID)
        {
            try
            {
                SendMessage(PlayerID, "~y~Server ~g~Online"); 
            }
            catch (Exception ex)
            {
                TServerObject.Trace(ex);
            }
        }
    }
}
