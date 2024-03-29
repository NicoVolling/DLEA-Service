﻿using CitizenFX.Core.Native;
using CitizenFX.Core;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.EventHandling;
using DLEA_Lib.Shared.Wardrobe;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ClientHelper;

namespace Client.Menu.Submenus
{
    internal class Submenu_Outfits : MenuBase
    {
        public Submenu_Outfits(ClientObject ClientObject, MenuPool MenuPool, MainMenuBase MainMenu) : base(ClientObject, MenuPool, MainMenu)
        {
        }

        protected override string Title => "Outfits";

        private UIMenu Submenu_Wardrobe_Both;

        private UIMenu Submenu_Wardrobe_Female;

        private UIMenu Submenu_Wardrobe_Male;

        protected override void InitializeMenu(UIMenu Menu)
        {
            Outfits.InitializeOutfits();

            Submenu_Wardrobe_Male = AddSubMenu(Menu, "Herren", "Nur Outfits für männliche Charaktere");
            Submenu_Wardrobe_Female = AddSubMenu(Menu, "Damen", "Nur Outfits für weibliche Charaktere");
            Submenu_Wardrobe_Both = AddSubMenu(Menu, "Alle", "Alle Outfits");

            string OutfitName = string.Empty;
            AddMenuTextItem(Menu, "Name des Outfits", "Name", (text) =>
            {
                OutfitName = text;
            });

            AddMenuItem(Menu, "Speichern", "Aktuelles Outfit speichern", (item) =>
            {
                int model = Game.PlayerPed.Model.Hash;
                int modelmale = API.GetHashKey(Outfit.MalePed);
                int modelfemale = API.GetHashKey(Outfit.FemalePed);

                int ped = Game.PlayerPed.Handle;
                if (!string.IsNullOrEmpty(OutfitName) && model == modelmale || model == modelfemale)
                {
                    Outfit CurrentOutfit = new Outfit(Outfits.Categories.Custom, model == modelmale, OutfitName)
                    {
                        Prop01 = new DLEA_Lib.Shared.Wardrobe.Prop(0, 0, 0),
                        Prop02 = new DLEA_Lib.Shared.Wardrobe.Prop(1, 0, 0),
                        Prop03 = new DLEA_Lib.Shared.Wardrobe.Prop(2, 0, 0),
                        Prop04 = new DLEA_Lib.Shared.Wardrobe.Prop(3, 0, 0),

                        Comp01 = new Component(1, 0, 0, 0),
                        Comp03 = new Component(3, 0, 0, 0),
                        Comp04 = new Component(4, 0, 0, 0),
                        Comp05 = new Component(5, 0, 0, 0),
                        Comp06 = new Component(6, 0, 0, 0),
                        Comp07 = new Component(7, 0, 0, 0),
                        Comp08 = new Component(8, 0, 0, 0),
                        Comp09 = new Component(9, 0, 0, 0),
                        Comp10 = new Component(10, 0, 0, 0),
                        Comp11 = new Component(11, 0, 0, 0)
                    };

                    foreach (Component Component in CurrentOutfit.Components)
                    {
                        Component.DrawableId = API.GetPedDrawableVariation(ped, Component.ComponentId) + 1;
                        Component.TextureId = API.GetPedTextureVariation(ped, Component.ComponentId) + 1;
                        Component.PaletteId = API.GetPedPaletteVariation(ped, Component.ComponentId);
                    }

                    foreach (DLEA_Lib.Shared.Wardrobe.Prop Prop in CurrentOutfit.Props)
                    {
                        Prop.DrawableId = API.GetPedPropIndex(ped, Prop.ComponentId) + 1;
                        Prop.TextureId = API.GetPedPropTextureIndex(ped, Prop.ComponentId) + 1;
                    }

                    ClientObject.TriggerServerEvent(ServerEvents.OutfitService_GetOutfit, Json.Serialize(CurrentOutfit));
                }
            });

            Dictionary<int, UIMenu> categoryMenus_Male = new Dictionary<int, UIMenu>() { { Outfits.Categories.Alle.ID, AddSubMenu(Submenu_Wardrobe_Male, "Alle", $"Alle Kategorien ({Outfits.CountMale()})") } };
            Dictionary<int, UIMenu> categoryMenus_Female = new Dictionary<int, UIMenu>() { { Outfits.Categories.Alle.ID, AddSubMenu(Submenu_Wardrobe_Female, "Alle", $"Alle Kategorien ({Outfits.CountFemale()})") } };
            Dictionary<int, UIMenu> categoryMenus_Both = new Dictionary<int, UIMenu>() { { Outfits.Categories.Alle.ID, AddSubMenu(Submenu_Wardrobe_Both, "Alle", $"Alle Kategorien ({Outfits.Count()})") } };

            foreach (Outfit_Category Category in Outfits.CategorieList.OrderBy(o => (int)o.Type).ThenBy(o => o.ID))
            {
                if (!categoryMenus_Male.ContainsKey(Category.ID) && !categoryMenus_Female.ContainsKey(Category.ID) && !categoryMenus_Both.ContainsKey(Category.ID))
                {
                    categoryMenus_Male.Add(Category.ID, AddSubMenu(Submenu_Wardrobe_Male, $"{Category.ShortName}", $"{Category.LongName} ({Outfits.CountMale(Category.ID)})"));
                    categoryMenus_Female.Add(Category.ID, AddSubMenu(Submenu_Wardrobe_Female, $"{Category.ShortName}", $"{Category.LongName} ({Outfits.CountFemale(Category.ID)})"));
                    categoryMenus_Both.Add(Category.ID, AddSubMenu(Submenu_Wardrobe_Both, $"{Category.ShortName}", $"{Category.LongName} ({Outfits.Count(Category.ID)})"));
                }
            }

            foreach (Outfit Outfit in Outfits.Outfits_Male)
            {
                AddMenuItems(new UIMenu[] { categoryMenus_Male[Outfit.Category.ID], categoryMenus_Male[Outfits.Categories.Alle.ID] }, Outfit.Name, "Outfit übernehmen", new Action<UIMenuItem>((item) =>
                {
                    CommonFunctions.ApplyOutfit(Outfit);
                }));
            }

            foreach (Outfit Outfit in Outfits.Outfits_Female)
            {
                AddMenuItems(new UIMenu[] { categoryMenus_Female[Outfit.Category.ID], categoryMenus_Female[Outfits.Categories.Alle.ID] }, Outfit.Name, "Outfit übernehmen", new Action<UIMenuItem>((item) =>
                {
                    CommonFunctions.ApplyOutfit(Outfit);
                }));
            }

            foreach (Outfit Outfit in Outfits.OutfitList)
            {
                UIMenuItem[] MenuItems = AddMenuItems(new UIMenu[] { categoryMenus_Both[Outfit.Category.ID], categoryMenus_Both[Outfits.Categories.Alle.ID] }, Outfit.Name, "Outfit übernehmen", new Action<UIMenuItem>((item) =>
                {
                    CommonFunctions.ApplyOutfit(Outfit);
                }));
                foreach (UIMenuItem Item in MenuItems)
                {
                    if (Outfit.IsMale)
                    {
                        Item.TextColor = Colors.Blue;
                        Item.HighlightColor = Colors.LightBlue;
                    }
                    else
                    {
                        Item.TextColor = Colors.DeepPink;
                        Item.HighlightColor = Colors.LightCoral;
                    }
                }
            }
        }

        protected override void OnTick()
        {
            
        }
    }
}
