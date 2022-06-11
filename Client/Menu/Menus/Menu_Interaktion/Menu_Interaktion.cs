using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using System;

namespace Client.Menu.Menus.Menu_Interaktion
{
    public class Menu_Interaktion : BaseMenu
    {
        public Menu_Interaktion(ClientObject ClientObject, out Action Tick, Ped SelectedPed) : base(ClientObject, out Tick, "Interaktionsmenu", "Human-Interaction-Interface")
        {
            this.SelectedPed = SelectedPed;
        }

        public Ped SelectedPed { get; }

        protected override void AddSubmenus()
        {
            base.AddSubmenus();
            bool IsFollowing = false;
            AddMenuItem(this, "Folgen", "Weist die Person an zu folgen", (item) =>
            {
                if (IsFollowing)
                {
                    API.ClearPedTasks(SelectedPed.Handle);
                }
                else
                {
                    CommonFunctions.AddBlipForEntity(SelectedPed.Handle, 1, 5, 2, "Folgt dir");
                    API.TaskGoToEntity(SelectedPed.Handle, Game.PlayerPed.Handle, -1, -1.0f, 10.0f, 1073741824, 0);
                    API.SetPedKeepTask(SelectedPed.Handle, true);
                }
                IsFollowing = !IsFollowing;
            });
        }

        protected override void OnTick()
        {
            base.OnTick();
        }
    }
}