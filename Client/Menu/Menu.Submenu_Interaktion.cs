using CitizenFX.Core;
using Client.Menu.Menus.Menu_Interaktion;
using NativeUI;
using System;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private Action A_OnTick_Interaktion;
        private Menu_Interaktion Menu_Interaktion;
        private Ped SelectedPed = null;
        private UIMenu Submenu_Interaktion;

        private void AddSubmenu_Interaktion()
        {
            //Submenu_Interaktion = MenuPool.AddSubMenu(this, "Interaktion", "Interaktion");

            //UIMenuItem Item_Anim_HandsUp = AddMenuItem(Submenu_Interaktion, "Interaktionsmenü", "Öffnet das Interaktionsmenü für den nächstgelegenen Ped", (item) =>
            //{
            //    Ped ped = CommonFunctions.GetClosestPed(5);
            //    if (ped != null)
            //    {
            //        MenuPool.CloseAllMenus();
            //        SelectedPed = ped;
            //        Menu_Interaktion = new Menu_Interaktion(ClientObject, out A_OnTick_Interaktion, SelectedPed);
            //        Menu_Interaktion.Visible = true;
            //    }
            //    else
            //    {
            //        Ped closest = CommonFunctions.GetClosestPed();
            //        if (closest != null)
            //        {
            //            ClientObject.SendMessage($"~r~Die nächste Person ist ~o~{CommonFunctions.GetDistanceAir(closest.Position)} ~r~entfernt");
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

            //    if (CommonFunctions.DistanceToPlayer(SelectedPed.Position, Game.PlayerPed.Position) > 5 || !SelectedPed.IsAlive || !SelectedPed.Exists())
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
            //        if (!API.DoesBlipExist(blip))
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