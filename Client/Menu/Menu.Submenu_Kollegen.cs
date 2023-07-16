using CitizenFX.Core;
using CitizenFX.Core.Native;
using DLEA_Lib.Shared.Wardrobe;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client.Menu
{
	public partial class MainMenu
	{
		private void AddSubmenu_Kollegen()
		{
			UIMenu Submenu_Kollegen = AddSubMenu(this, "Kollegen", "Kollegen (KI)");
			addSubmenuKollegeSpawnen(Submenu_Kollegen);
			addSubmenuKollegenListe(Submenu_Kollegen);
		}

        private void addSubmenuKollegenListe(UIMenu submenu_Kollegen)
        {
			UIMenu menulist = AddSubMenu(submenu_Kollegen, "Liste", "Liste aller Kollegen");
            menulist.OnMenuOpen += Menulist_OnMenuOpen;
        }

        private void Menulist_OnMenuOpen(UIMenu sender)
        {
            UIMenu menu = AddSubMenu(sender, "Alle", "Alle");
            AddMenuItem(menu, "Waffe ändern", $"Gibt ~g~Allen~w~ deine Waffe", (o) =>
            {
				foreach (Ped ped in peds)
				{
					if (Game.PlayerPed.Weapons.Current != null)
					{
						API.GiveWeaponToPed(ped.Handle, ((uint)Game.PlayerPed.Weapons.Current.Hash), 500, false, true);
					}
				}
            });
            AddMenuItem(menu, "Heilen", $"Heilt, Reinigt und Panzert ~g~Alle~w~", (o) =>
            {
				foreach (Ped ped in peds)
				{
					ped.Health = ped.MaxHealth;
					ped.Armor = 200;
				}
            });
            AddMenuItem(menu, "Aussehen ändern", $"Gibt ~g~Allen~w~ deine Waffe", (o) =>
            {
				foreach (Ped ped in peds)
				{
					CopyOutfitFromPlayerToPed(ped);
				}
            });
            AddMenuItem(menu, "Löschen", $"Löscht ~g~Alle~w~", (o) =>
            {
				foreach (Ped ped in peds)
				{
					ped.Delete();
				}
            });

            sender.Clear();
			int i = 0;
			foreach (Ped ped in peds)
			{
				UIMenu pedmenu = AddSubMenu(sender, $"Kollege ~g~#{i}", $"Handle: {ped.Handle}");
				AddMenuItem(pedmenu, "Waffe ändern", $"Gibt ~g~#{i}~w~ deine Waffe", (o) =>
				{
					if (Game.PlayerPed.Weapons.Current != null)
					{
						API.GiveWeaponToPed(ped.Handle, ((uint)Game.PlayerPed.Weapons.Current.Hash), 500, false, true);
					}
				});
				AddMenuItem(pedmenu, "Heilen", $"Heilt, Reinigt und Panzert ~g~#{i}~w~", (o) =>
				{
					ped.Health = ped.MaxHealth;
					ped.Armor = 200;
				});
				AddMenuItem(pedmenu, "Aussehen ändern", $"~g~#{i}~w~ bekommt dein Outfit", (o) =>
				{
					CopyOutfitFromPlayerToPed(ped);
				});
				AddMenuItem(pedmenu, "Löschen", $"Löscht ~g~#{i}~w~", (o) =>
				{
					ped.Delete();
				});
				i++;
			}
		}

        List<Ped> peds = new List<Ped>();

        private void addSubmenuKollegeSpawnen(UIMenu Submenu_Kollegen)
        {
			AddMenuItem(Submenu_Kollegen, "Erschaffen", "Erschafft einen Kollegen", (o) =>
			{
				Ped Ped = World.CreatePed(PedHash.FreemodeMale01, Game.PlayerPed.Position + (API.GetEntityForwardVector(Game.PlayerPed.Handle) * 1.5f) - new Vector3(0, 0, API.GetEntityHeightAboveGround(Game.PlayerPed.Handle))).Result;
				peds.Add(Ped);

				Random rnd = new Random();
				for(int i = 0; i < 19 +1; i++)
				{
					API.SetPedFaceFeature(Ped.Handle, i, (float)((rnd.NextDouble() * 2) -1));
				}

				CopyOutfitFromPlayerToPed(Ped);

                API.SetPedAsGroupMember(Ped.Handle, API.GetPedGroupIndex(Game.PlayerPed.Handle));
				API.SetPedCombatAbility(Ped.Handle, 2);
				if (Game.PlayerPed.Weapons.Current != null)
				{
					API.GiveWeaponToPed(Ped.Handle, ((uint)Game.PlayerPed.Weapons.Current.Hash), 500, false, true);
				}

				Ped.DropsWeaponsOnDeath = true;
				Ped.CanSwitchWeapons = false;
				Ped.Health = Ped.MaxHealth;
				Ped.Armor = 200;

				Blip blip = Ped.AttachBlip();
				blip.Sprite = BlipSprite.PoliceOfficer;
				blip.Name = "Kollege";
				blip.Scale = 0.4f;
			});
        }

		private void CopyOutfitFromPlayerToPed(Ped Ped)
		{
			API.ClearPedFacialDecorations(Ped.Handle);
			int j = 0;
			foreach (PedComponent PC in Game.PlayerPed.Style.GetAllComponents())
			{
				API.SetPedComponentVariation(Ped.Handle, j, PC.Index, PC.TextureIndex, API.GetPedPaletteVariation(Game.PlayerPed.Handle, j));
				j++;
			}
			API.SetPedComponentVariation(Ped.Handle, 2, 2, 1, 0);
			API.SetPedFacialDecoration(Ped.Handle, (uint)(API.GetHashKey("multiplayer_overlays")), (uint)(API.GetHashKey("FM_M_Hair_001_z")));
			API.SetPedHairColor(Ped.Handle, 10, 0);

			int k = 0;
			foreach (PedProp PC in Game.PlayerPed.Style.GetAllProps())
			{
				if (PC.Index == 0)
				{
					API.ClearPedProp(Ped.Handle, k);
				}
				else
				{
					API.SetPedPropIndex(Ped.Handle, k, PC.Index - 1, PC.TextureIndex, true);
				}
				k++;
			}
		}

        void OnTick_Submenu_Kollegen()
		{
			List<Ped> pedremove = new List<Ped>();
			foreach(Ped ped in peds)
			{
				if(!ped.Exists() || ped.IsDead)
				{
					int blip = API.GetBlipFromEntity(ped.Handle);
					API.RemoveBlip(ref blip);
					pedremove.Add(ped);
				}
			}
			peds.RemoveAll(o => pedremove.Contains(o));
		}
	}
}
