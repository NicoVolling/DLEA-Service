using CitizenFX.Core;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using System;
using System.Linq;

namespace Server.Services
{
    public class TMessageService : TService
    {
        public TMessageService(TServerObject ServerObject) : base(ServerObject)
        {
            EventOnGetPing = OnGetPing;
        }

        public override string Name => nameof(TOutfitService);

        #region Events

        public Action<int> EventOnGetPing { get; }

        #endregion Events

        public static void SendMessage(int PlayerID, params string[] Messages)
        {
            SendMessage(new PlayerList()[PlayerID], Messages);
        }

        public static void SendMessage(Player Player, params string[] Messages)
        {
            if (Messages == null || Messages.Length == 0) { throw new Exception("Messages may not be null or empty."); }
            Messages = Messages.Select(o => $"~o~[DLEA] {o}").ToArray();
            try
            {
                Player.TriggerEvent(ClientEvents.MessageService_SendMessages, Json.Serialize(Messages));
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