using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using DLEA_Lib;
using DLEA_Lib.Shared;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Services;
using DLEA_Lib.Shared.Wardrobe;
using DLEA_Lib.Shared.User;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Application;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private UIMenu Submenu_Animationen;

        private void AddSubmenu_Animationen()
        {
            Submenu_Animationen = MenuPool.AddSubMenu(this, "Animationen", "Animationen");

            UIMenuItem Item_Anim_HandsUp = AddMenuItem(Submenu_Animationen, "Hände hoch (1s)", "Die Hände für 1s hochnehmen", (item) =>
            {
                try
                {
                    API.TaskHandsUp(Game.PlayerPed.Handle, 1000, 0, 0, true);
                }
                catch (Exception ex)
                {
                    Tracing.Trace(ex);
                }
            });
        }
    }
}
