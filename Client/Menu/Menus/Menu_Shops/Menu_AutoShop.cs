using System;

namespace Client.Menu.Menus.Menu_Shops
{
    public class Menu_AutoShop : BaseMenu
    {
        public Menu_AutoShop(ClientObject ClientObject, out Action Tick, string Title, string Subtitle) : base(ClientObject, out Tick, Title, Subtitle)
        {
        }

        protected override void OnTick()
        {
            base.OnTick();
        }
    }
}