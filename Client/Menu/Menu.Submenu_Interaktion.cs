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
using Colorrr = System.Drawing;
using System.Drawing;
using Client.Menu.Menus.Menu_Interaktion;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private UIMenu Submenu_Interaktion;
        private Ped SelectedPed = null;
        private Menu_Interaktion Menu_Interaktion;
        private Action A_OnTick_Interaktion;
        private void AddSubmenu_Interaktion()
        {
            //Submenu_Interaktion = MenuPool.AddSubMenu(this, "Interaktion", "Interaktion");

            //UIMenuItem Item_Anim_HandsUp = AddMenuItem(Submenu_Interaktion, "Interaktionsmenü", "Öffnet das Interaktionsmenü für den nächstgelegenen Ped", (item) =>
            //{
            //    Ped ped = ClientHelper.GetClosestPed(5);
            //    if (ped != null)
            //    {
            //        MenuPool.CloseAllMenus();
            //        SelectedPed = ped;
            //        Menu_Interaktion = new Menu_Interaktion(ClientObject, out A_OnTick_Interaktion, SelectedPed);
            //        Menu_Interaktion.Visible = true;
            //    }
            //    else 
            //    {
            //        Ped closest = ClientHelper.GetClosestPed();
            //        if (closest != null)
            //        {
            //            ClientObject.SendMessage($"~r~Die nächste Person ist ~o~{ClientHelper.GetDistanceAir(closest.Position)} ~r~entfernt");
            //        }
            //        else
            //        {
            //            ClientObject.SendMessage($"~r~Keine Person in der Nähe");
            //        }
            //    }
            //});
        }

        private void OnTick_Submenu_Interkation() 
        {
            //if (A_OnTick_Interaktion != null) 
            //{
            //    A_OnTick_Interaktion();
            //}

            //if (SelectedPed != null) 
            //{
            //    World.DrawMarker(MarkerType.VerticalCylinder, SelectedPed.Position, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f), System.Drawing.Color.FromArgb(0, 255, 0));

            //    if (ClientHelper.DistanceToPlayer(SelectedPed.Position, Game.PlayerPed.Position) > 5 || !SelectedPed.IsAlive || !SelectedPed.Exists())
            //    {
            //        int blip = API.GetBlipFromEntity(SelectedPed.Handle);
            //        API.RemoveBlip(ref blip);
            //        blip = API.GetBlipFromEntity(SelectedPed.Handle);
            //        SelectedPed = null;
            //        if (Menu_Interaktion != null) 
            //        {
            //            Menu_Interaktion.Visible = false;
            //            Menu_Interaktion = null;
            //        }
            //    }
            //    else 
            //    {
            //        int blip = API.GetBlipFromEntity(SelectedPed.Handle);
            //        if(!API.DoesBlipExist(blip))
            //        {
            //            blip = API.AddBlipForEntity(SelectedPed.Handle);
            //            API.SetBlipSprite(blip, 270);
            //            API.SetBlipColour(blip, 5);
            //            API.SetBlipScale(blip, 0.5f);
            //        }
            //    }
            //}
        }
    }
}
