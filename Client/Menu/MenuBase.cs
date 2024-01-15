using CitizenFX.Core.Native;
using DLEA_Lib.Shared.User;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Menu
{
    public abstract class MenuBase
    {
        public MenuBase(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu)
        {
            this.ClientObject = ClientObject;
            this.MenuPool = MenuPool;
            this.MainMenu = MainMenu;
        }

        protected abstract string Title { get; }

        protected Action<string> CurrentTextCallback { get; set; }
        public ClientObject ClientObject { get; }
        protected MenuPool MenuPool { get; }

        public MainMenuBase MainMenu { get; }

        public UIMenu Parent { get; private set; }

        protected StoredUser CurrentUser { get => MainMenu.CurrentUser; }

        protected UIMenu Menu { get; private set; }

        public void AttachToParent(UIMenu Parent)
        {
            this.Parent = Parent;
            this.Menu = AddSubMenu(Parent, Title);
            try 
            { 
                InitializeMenu(Menu);
            } 
            catch (Exception ex) { ClientObject.Trace(ex.ToString()); }
        }

        protected abstract void InitializeMenu(UIMenu Menu);

        protected UIMenuCheckboxItem AddMenuCheckboxItem(UIMenu Parent, string Name, string Description, bool Check, Action<bool> OnItemClick = null)
        {
            UIMenuCheckboxItem Item = new UIMenuCheckboxItem(Name, Check, Description);
            if (Item == null)
            {
                ClientObject.Trace($"Error while Creating MenuItem: {Name}");
                return null;
            }
            Parent.AddItem(Item);
            if (OnItemClick != null)
            {
                Item.CheckboxEvent += (sender, check) =>
                {
                    OnItemClick(check);
                };
            }

            return Item;
        }

        protected UIMenuCheckboxItem AddMenuCheckboxItem(UIMenu Parent, string Name, bool Check, Action<bool> OnItemClick = null)
        {
            return AddMenuCheckboxItem(Parent, Name, Name, Check, OnItemClick);
        }

        protected UIMenuItem AddMenuItem(UIMenu Parent, string Name, string Description = "", Action<UIMenuItem> OnItemClick = null)
        {
            if (Parent == null) { return null; }
            UIMenuItem Item = new UIMenuItem(Name, Description);
            if (Item == null)
            {
                ClientObject.Trace($"Error while Creating MenuItem: {Name}");
                return null;
            }
            Parent.AddItem(Item);
            if (OnItemClick != null)
            {
                Parent.OnItemSelect += (sender, item, index) =>
                {
                    if (item == Item)
                    {
                        OnItemClick(item);
                    }
                };
            }

            return Item;
        }

        protected UIMenuItem[] AddMenuItems(UIMenu[] Parents, string Name, string Description, Action<UIMenuItem> OnItemClick = null)
        {
            UIMenuItem[] Items = new UIMenuItem[Parents.Length];
            int i = 0;
            foreach (UIMenu Parent in Parents)
            {
                Items[i] = AddMenuItem(Parent, Name, Description, OnItemClick);
                i++;
            }
            return Items;
        }

        protected UIMenuItem AddMenuTextItem(UIMenu Parent, string Name, string WindowTitle, Action<string> OnTextEdited, string Description = "", string DefaultText = "", int MaxLength = 255)
        {
            string Result = DefaultText;
            UIMenuItem Item = AddMenuItem(Parent, Name, Description, (item) =>
            {
                API.AddTextEntry("FMMC_KEY_TIP1", WindowTitle);
                API.DisplayOnscreenKeyboard(1, "FMMC_KEY_TIP1", "", Result, "", "", "", MaxLength);
                CurrentTextCallback = (text) =>
                {
                    Result = text;
                    OnTextEdited(Result);
                    item.Description = $"\"{Result}\"";
                    Parent.UpdateDescription();
                };
            });
            Item.Description = $"\"{Result}\"";
            Parent.UpdateDescription();
            return Item;
        }

        protected UIMenu AddSubMenu(UIMenu Parent, string Name, string Description = null, UIMenuItem.BadgeStyle Badge = UIMenuItem.BadgeStyle.ArrowRight)
        {
            UIMenu menu = MenuPool.AddSubMenu(Parent, Name, Description ?? Name);
            menu.ParentItem.SetRightBadge(Badge);
            return menu;
        }

        public void Tick()
        {
            if (CurrentTextCallback != null && API.UpdateOnscreenKeyboard() == 1)
            {
                string text = API.GetOnscreenKeyboardResult();
                API.Wait(500);
                CurrentTextCallback(text);
                CurrentTextCallback = null;
            }
            else if (API.UpdateOnscreenKeyboard() == 0)
            {
                API.DisableAllControlActions(0);
            }

            OnTick();
        }

        protected abstract void OnTick();
    }
}
