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
using DLEA_Lib.Shared.Einsatz;
using Client.ClientHelper;

namespace Client.Menu
{
    public partial class MainMenu
    {
        List<dynamic> Categories = Einsatzstatics.EinsatzKategorien.Select(o => o.Key).ToList<dynamic>();

        UIMenuListItem MenuItemCodes;

        string Einsatzcode = "";

        private void AddSubmenu_Einsatz()
        {
            //UIMenu MenuEinsätze = MenuPool.AddSubMenu(this, "Einsätze", "Einsätze");
            //UIMenu MenuEinsatzAuslösen = MenuPool.AddSubMenu(MenuEinsätze, "Auslösen", "Einsatz auslösen");


            //UIMenuListItem CodeCat = new UIMenuListItem("Kategorie", Categories, 0, "Kategorie des Einsatzes");
            //MenuEinsatzAuslösen.AddItem(CodeCat);

            //CodeCat.OnListSelected += CodeCat_OnListSelected;
            //CodeCat.OnListChanged += CodeCat_OnListSelected;

            //MenuItemCodes = new UIMenuListItem("Einsatzcode", Einsatzstatics.EinsatzKategorien[Categories[0]], 0);
            //MenuEinsatzAuslösen.AddItem(MenuItemCodes);

            //Einsatzcode = Einsatzstatics.EinsatzKategorien[Categories[0]][0];

            //MenuItemCodes.OnListChanged += MenuItemCodes_OnListChanged;
            //MenuItemCodes.OnListSelected += MenuItemCodes_OnListChanged;

            //bool sonderreche = false;
            //AddMenuCheckboxItem(MenuEinsatzAuslösen, "Sonderrechte", sonderreche, o => { sonderreche = o; });

            //AddMenuItem(MenuEinsatzAuslösen, "Einsatzauslösen", "Einsatz auslösen", o => 
            //{
            //    if (API.IsWaypointActive())
            //    {
            //        int blipid = API.GetFirstBlipInfoId(8);
            //        var co = API.GetBlipCoords(blipid);
            //        DVector3 Coords = new DVector3(co.X, co.Y, co.Z);
            //        Einsatz Einsatz = new Einsatz()
            //        {
            //            Position = Coords,
            //            StartTime = DateTime.Now,
            //            Code = Einsatzcode,
            //            Sonderrechte = sonderreche,
            //            Location = CommonFunctions.GetStreetLocation(co),
            //            Active = true,
            //            BlipSprite = 161,
            //            Happening = -1
            //        };
            //        ClientObject.TriggerServerEvent(ServerEvents.EinsatzService_SendEinsatz, Einsatz.GetRAW());
            //    }
            //    else 
            //    {
            //        ClientObject.SendMessage("~r~Es wurde keine Makierung gefunden");
            //    }
            //});
        }

        private void MenuItemCodes_OnListChanged(UIMenuListItem sender, int newIndex)
        {
            Einsatzcode = MenuItemCodes.Items[newIndex].ToString();
        }

        private void CodeCat_OnListSelected(UIMenuListItem sender, int newIndex)
        {
            MenuItemCodes.Items.Clear();
            MenuItemCodes.Items = Einsatzstatics.EinsatzKategorien[Categories[newIndex]];
        }

        public void RefreshEinsätze() 
        {
            
        }
    }
}
