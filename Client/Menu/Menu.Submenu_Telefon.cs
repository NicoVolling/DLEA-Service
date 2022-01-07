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
        public void AddSubmenu_Telefon() 
        {
            //UIMenu Submenu_Telefon = MenuPool.AddSubMenu(this, "Telefon", "Telefon");

            //AddMenuItem(Submenu_Telefon, "Polizei", "Polize rufen", (item) => 
            //{
            //    int i = 0;
            //    API.EnableDispatchService(7, true);
            //    API.CreateIncidentWithEntity(7, Game.PlayerPed.Handle, 1, 10, ref i);
            //    Tracing.Trace(API.IsIncidentValid(i));
            //});

            //AddMenuItem(Submenu_Telefon, "Feuerwehr", "Feuerwehr rufen", (item) =>
            //{
            //    int i = 0;
            //    API.EnableDispatchService(3, true);
            //    API.CreateIncidentWithEntity(3, Game.PlayerPed.Handle, 1, 10, ref i);
            //    Tracing.Trace(API.IsIncidentValid(i));
            //});

            //AddMenuItem(Submenu_Telefon, "Rettungsanitäter", "Rettungsanitäter rufen", (item) =>
            //{
            //    int i = 0;
            //    API.EnableDispatchService(5, true);
            //    API.CreateIncidentWithEntity(5, Game.PlayerPed.Handle, 1, 10, ref i);
            //    Tracing.Trace(API.IsIncidentValid(i));
            //});
        }
    }
}
