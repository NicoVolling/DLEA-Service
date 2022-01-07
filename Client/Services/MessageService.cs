using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
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
    public class MessageService : Service
    {
        public override string UserFriendlyName => "Outfiteinstellungen";

        public override string Name => nameof(MessageService);

        public MessageService(ClientObject ClientObject) : base(ClientObject) 
        {
            EventOnGetResult = OnGetResult;
        }

        public override void Start()
        {
            ClientObject.SendMessage("~y~Client ~g~Online");
            ClientObject.TriggerServerEvent(ServerEvents.MessageService_GetPing, ServerID);
        }

        private void OnGetResult(string[] Messages)
        {
            foreach (string Message in Messages)
            {
                try
                {
                    Screen.ShowNotification(Message);
                }
                catch (Exception ex)
                {
                    Tracing.Trace(ex);
                }
            }
        }

        #region Events
        public Action<string[]> EventOnGetResult { get; set; }
        #endregion
    }
}
