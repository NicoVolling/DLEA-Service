using DLEA_Lib.Shared.Services;
using System.Collections.Generic;

namespace Client.Services
{
    public class DisplayService : Service
    {
        public DisplayService(ClientObject ClientObject) : base(ClientObject)
        {
        }

        public override string Name => nameof(DisplayService);
        public override string UserFriendlyName => "Anzeigeeinstellungen";
        public List<string> Users { get; set; } = new List<string>();

        public override void OnTick()
        {
            base.OnTick();

            Textdisplay.WriteText(ClientObject);
        }

        protected override void InitializeSettings()
        {
            base.InitializeSettings();
        }
    }
}