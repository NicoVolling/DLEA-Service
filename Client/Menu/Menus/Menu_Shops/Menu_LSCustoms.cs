using System;

namespace Client.Menu.Menus.Menu_Shops
{
    public class Menu_LSCustoms : BaseMenu
    {
        public Menu_LSCustoms(ClientObject ClientObject, out Action Tick, string Title, string Subtitle) : base(ClientObject, out Tick, Title, Subtitle)
        {
        }

        protected override void OnTick()
        {
            base.OnTick();
        }
    }
}