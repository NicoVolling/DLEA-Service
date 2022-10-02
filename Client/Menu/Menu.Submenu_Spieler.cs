using NativeUI;
using System;

namespace Client.Menu
{
    public partial class MainMenu
    {
        private Func<UIMenu> getPlayerMenu { get; set; }

        private void AddSubmenu_Spieler()
        {
            UIMenu Menu_Players = AddSubMenu(this, "Spieler", "Spieler");
            getPlayerMenu = () => { return Menu_Players; };
        }
    }
}