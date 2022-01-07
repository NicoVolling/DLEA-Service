﻿using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Services;
using DLEA_Lib.Shared.Wardrobe;
using DLEA_Lib.Shared.User;
using DLEA_Lib.Shared.Services;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Application;
using NativeUI;
using System;
using CitizenFX.Core.Native;

namespace Client.Menu
{
    public class BaseMenu : UIMenu
    {
        protected Action<string> CurrentTextCallback;
        protected StoredUser CurrentUser = null;

        public BaseMenu(ClientObject ClientObject, out Action Tick, string Title, string Subtitle) : base(Title, Subtitle, false)
        {
            this.ClientObject = ClientObject;
            if (MenuPool == null)
            {
                MenuPool = new MenuPool();
            }
            MenuPool.Add(this);

            ResetCursorOnOpen = false;

            InitializeMenu();

            ClientObject.Trace($"Initialized Menu:{Title}");

            Tick = OnTick;
        }
        protected virtual void InitializeMenu() 
        {
            AddSubmenus();
        }

        protected virtual void OnTick() 
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
            else
            {
                API.Wait(500);
                MenuPool.ProcessMenus();
            }
        }
        protected virtual void AddSubmenus() { }

        #region Static Properties

        protected static MenuPool MenuPool;

        #endregion Static Properties

        protected ClientObject ClientObject { get; }

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
                    item.Description = $"Eingabe: {Result}";
                    Parent.UpdateDescription();
                };

            });
            Item.Description = $"Eingabe: {Result}";
            Parent.UpdateDescription();
            return Item;
        }

        protected UIMenuCheckboxItem AddMenuCheckboxItem(UIMenu Parent, string Name, bool Check, Action<bool> OnItemClick = null)
        {
            UIMenuCheckboxItem Item = new UIMenuCheckboxItem(Name, Check);
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
    }
}
