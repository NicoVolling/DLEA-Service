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
        private UIMenu Submenu_Dienst;

        private void AddSubmenu_Dienst()
        {
            Submenu_Dienst = MenuPool.AddSubMenu(this, "Dienst", "Dienst");

            List<dynamic> Stati = new List<dynamic>() { "nicht im Dienst", "Verfügbar", "Im Einsatz" };
            List<dynamic> Ränge = new List<dynamic>() { "Ohne", "Officer", "Detective", "Sergeant", "Lieutenant", "Captain", "Assistant Chief", "Chief", "Agent", "Special Agent" };

            List<dynamic> Dep = Outfits.CategorieList.Where(o => o.Type != CategoryType.Civil && o.Type != CategoryType.Default && o.Type != CategoryType.Custom).OrderBy(o => o.ID).Select(o => o.ShortName).ToList<dynamic>();
            Dep.Insert(0, "Ohne");
            Dep.Add("US Marshals");

            UIMenuListItem ItemStatus = new UIMenuListItem("Status", Stati, 0);
            UIMenuListItem ItemRang = new UIMenuListItem("Rang", Ränge, 0);
            UIMenuListItem ItemDep = new UIMenuListItem("Departement", Dep, 0);

            Submenu_Dienst.OnListSelect += (sender, item, index) =>
            {
                if (item == ItemStatus)
                {
                    ClientObject.CurrentUser.Status = Stati[ItemStatus.Index];
                }
                else if (item == ItemRang)
                {
                    if (Ränge[ItemRang.Index] != "Ohne")
                    {
                        ClientObject.CurrentUser.Rang = Ränge[ItemRang.Index];
                    }
                    else
                    {
                        ClientObject.CurrentUser.Rang = "";
                    }
                }
                else if (item == ItemDep)
                {
                    if (Dep[ItemDep.Index] != "Ohne")
                    {
                        ClientObject.CurrentUser.Department = Dep[ItemDep.Index];
                    }
                    else
                    {
                        ClientObject.CurrentUser.Department = "";
                    }
                }
            };

            Submenu_Dienst.AddItem(ItemStatus);
            Submenu_Dienst.AddItem(ItemRang);
            Submenu_Dienst.AddItem(ItemDep);
        }
    }
}
