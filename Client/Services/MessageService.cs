using CitizenFX.Core.UI;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using System;

namespace Client.Services
{
    public class MessageService : Service
    {
        public MessageService(ClientObject ClientObject) : base(ClientObject)
        {
            EventOnGetResult = OnGetResult;
        }

        public override string Name => nameof(MessageService);
        public override string UserFriendlyName => "Nachrichteneinstellungen";

        public override void Start()
        {
            ClientObject.SendMessage("~y~Client ~g~Online");
            ClientObject.TriggerServerEvent(ServerEvents.MessageService_GetPing, ServerID);
        }

        private void OnGetResult(string RAW)
        {
            string[] Messages = Json.Deserialize<string[]>(RAW);
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

        public Action<string> EventOnGetResult { get; set; }

        #endregion Events
    }
}