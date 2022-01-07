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

        public static void SendMessage(int PlayerID, params string[] Messages) 
        {
            SendMessage(new PlayerList()[PlayerID], Messages);
        }

        public static void SendMessage(Player Player, params string[] Messages)
        {
            if(Messages == null || Messages.Length == 0) { throw new Exception("Messages may not be null or empty."); }
            Messages = Messages.Select(o => $"~o~[DLEA] {o}").ToArray();
            try
            {
                    Player.TriggerEvent(ClientEvents.MessageService_SendMessages, Messages);
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
