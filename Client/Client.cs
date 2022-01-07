using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using NativeUI;
using NativeUI.PauseMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Main2 : BaseScript
    {
        private Dictionary<int, string> AvailablePlayers = new Dictionary<int, string>();

        private UIMenu Einsätze_Einsatzübersicht;

        /// <summary>
        /// int Host, bool Sonderrechte, int Blip, int BlipSprite, Vector3 Coords, bool Selected, string Einsatzbezeichnung
        /// </summary>
        private List<Tuple<int, bool, int, int, Vector3, bool, string>> Einsatzliste = new List<Tuple<int, bool, int, int, Vector3, bool, string>>();

        private UIMenu mainmenu;
        private MenuPool menuPool;
        private List<int> MultiRoute_BlipList = new List<int>();
        private Dictionary<int, int> PlayerLocationBlips = new Dictionary<int, int>();
        private Dictionary<int, int> PlayerWaypointBlips = new Dictionary<int, int>();
        private List<int> SelectedPlayers = new List<int>();
        private bool Sonderrechte = true;

        public Main2()
        {
            AddEventhandlers();
            InitializeMenu();
        }

        private void AddEventhandlers()
        {
            //EventHandlers["DLEA-GPS-Service:Client:EINSATZACCEPT"] += new Action<Vector3>((coords) =>
            //{
            //    var einsatz = Einsatzliste.Where(o => o.Item5 == coords);
            //    if (einsatz.Any())
            //    {
            //        AcceptEinsatz(Einsatzliste.IndexOf(einsatz.First()));
            //    }
            //});
            //EventHandlers["DLEA-GPS-Service:Client:EINSATZDENY"] += new Action<Vector3>((coords) =>
            //{
            //    var einsatz = Einsatzliste.Where(o => o.Item5 == coords);
            //    if (einsatz.Any())
            //    {
            //        DenyEinsatz(Einsatzliste.IndexOf(einsatz.First()));
            //    }
            //});
            EventHandlers["DLEA-GPS-Service:Client:EINSATZACCEPTED"] += new Action<int>((player) =>
            {
                if (Game.Player.ServerId != player)
                {
                    Screen.ShowNotification($"~c~{new PlayerList()[player].Name} nimmt an einem Einsatz teil.");
                }
            });
            EventHandlers["DLEA-GPS-Service:Client:EINSATZDENIED"] += new Action<int>((player) =>
            {
                if (Game.Player.ServerId != player)
                {
                    Screen.ShowNotification($"~c~{new PlayerList()[player].Name} hat einen Einsatz abgelehnt.");
                }
            });
            EventHandlers["DLEA-GPS-Service:Client:EINSATZSEND"] += new Action<int, Vector3, int, string, bool>((player, location, blipSprite, Einsatzbezeichnung, Sonderrechte) =>
            {
                GetEinsatzFromServer(player, location, blipSprite, Einsatzbezeichnung, Sonderrechte);
            });
            //EventHandlers["DLEA-GPS-Service:Client:EINSATZQUIT"] += new Action<Vector3>((coords) =>
            //{
            //    var einsatz = Einsatzliste.Where(o => o.Item5 == coords);
            //    if (einsatz.Any())
            //    {
            //        QuitEinsatz(Einsatzliste.IndexOf(einsatz.First()));
            //    }
            //});
            EventHandlers["DLEA-GPS-Service:Client:EINSATZQUITTED"] += new Action<string, int>((source, player) =>
            {
                if (Einsatzliste.Where(o => o.Item6).FirstOrDefault().Item1 == player)
                {
                    Screen.ShowNotification($"~c~{source} hat die Einsatzfahrt beendet.");
                }
            });
            EventHandlers["DLEA-GPS-Service:Client:SyncGPS"] += new Action<int, bool, Vector3>((source, active, waypointLocation) =>
            {
                if (!active && PlayerWaypointBlips.ContainsKey(source))
                {
                    int temp = PlayerWaypointBlips[source];
                    API.RemoveBlip(ref temp);
                    PlayerWaypointBlips.Remove(source);
                }
                else
                if (active && !PlayerWaypointBlips.ContainsKey(source))
                {
                    PlayerWaypointBlips.Add(source, API.AddBlipForCoord(waypointLocation.X, waypointLocation.Y, waypointLocation.Z));
                    API.SetBlipSprite(PlayerWaypointBlips[source], 364);
                    API.SetBlipColour(PlayerWaypointBlips[source], 2);
                    API.SetBlipDisplay(PlayerWaypointBlips[source], 3);
                    API.BeginTextCommandSetBlipName("STRING");
                    API.AddTextComponentString($"{new PlayerList()[source].Name}s Wegpunkt");
                    API.EndTextCommandSetBlipName(PlayerWaypointBlips[source]);
                    API.SetBlipCategory(PlayerWaypointBlips[source], 2);
                }
                else if (active && PlayerWaypointBlips.ContainsKey(source))
                {
                    API.SetBlipCoords(PlayerWaypointBlips[source], waypointLocation.X, waypointLocation.Y, waypointLocation.Z);
                }
            });
            EventHandlers["DLEA-GPS-Service:Client:SyncPlayers"] += new Action<int, Vector3, int, string>((source, playerLocation, isVehicle, playerName) =>
            {
                if (!AvailablePlayers.ContainsKey(source))
                {
                    AvailablePlayers.Add(source, playerName);
                }
                else
                {
                    AvailablePlayers[source] = playerName;
                }

                int sprite = 1;
                if (isVehicle == 1)
                {
                    sprite = 326;
                }
                else if (isVehicle == 2)
                {
                    sprite = 589;
                }
                if (!PlayerLocationBlips.ContainsKey(source))
                {
                    PlayerLocationBlips.Add(source, API.AddBlipForCoord(playerLocation.X, playerLocation.Y, playerLocation.Z));
                    API.SetBlipSprite(PlayerLocationBlips[source], sprite);
                    API.SetBlipColour(PlayerLocationBlips[source], 18);
                    API.SetBlipDisplay(PlayerLocationBlips[source], 2);
                    API.BeginTextCommandSetBlipName("STRING");
                    API.AddTextComponentString($"{new PlayerList()[source].Name}");
                    API.EndTextCommandSetBlipName(PlayerLocationBlips[source]);
                    API.SetBlipCategory(PlayerLocationBlips[source], 7);
                }
                else
                {
                    int blip = PlayerLocationBlips[source];
                    API.SetBlipCoords(blip, playerLocation.X, playerLocation.Y, playerLocation.Z);
                    API.SetBlipSprite(PlayerLocationBlips[source], sprite);
                    API.SetBlipColour(PlayerLocationBlips[source], 18);
                }
            });
        }

        #region Menu

        private void InitializeMenu()
        {
            Debug.WriteLine("Init");
            try
            {
                menuPool = new MenuPool();
                mainmenu = new UIMenu("DLEA-Service", "Digital Law Enforcement Assistant", false);

                mainmenu.ResetCursorOnOpen = false;
                menuPool.Add(mainmenu);

                #region Tools

                UIMenu ToolMenu = menuPool.AddSubMenu(mainmenu, "Tools", "Tools");

                #region Vehicle

                UIMenu ToolMenu_Vehicle = menuPool.AddSubMenu(ToolMenu, "Fahrzeug", "Fahrzeug");
                UIMenuItem ToolMenu_Vehicle_Repair = new UIMenuItem("Schnelreperatur", "Fahrzeug reparieren");
                ToolMenu_Vehicle.AddItem(ToolMenu_Vehicle_Repair);
                ToolMenu_Vehicle.OnItemSelect += (sender, item, index) =>
                {
                    if (ToolMenu_Vehicle_Repair == item)
                    {
                        if (Game.PlayerPed.CurrentVehicle != null)
                        {
                            Game.PlayerPed.CurrentVehicle.Repair();
                        }
                        else
                        {
                            Screen.ShowNotification($"~r~Sie befinden sich in keinem Fahrzeug");
                        }
                    }
                };

                #endregion Vehicle

                #region Player

                //UIMenu ToolMenu_Player = menuPool.AddSubMenu(ToolMenu, "Spieler", "Spieler");
                //UIMenuItem ToolMenu_Player_Saved = new UIMenuItem("Gespeicherte Charaktere", "Gespeicherte Charaktere und Outfits aufrufen");
                //ToolMenu_Player.AddItem(ToolMenu_Player_Saved);
                //ToolMenu_Player.OnItemSelect += (sender, item, index) =>
                //{
                //    if (ToolMenu_Player_Saved == item)
                //    {
                //    }
                //};

                #endregion Player

                #endregion Tools

                #region Wardrobe

                UIMenu WardrobeMenu = menuPool.AddSubMenu(mainmenu, "Gaderobe", "Outfit ändern");
                UIMenu WardrobeMenu_Male = menuPool.AddSubMenu(WardrobeMenu, "Männlich", "Nur Outfits für männliche Charaktere");
                UIMenu WardrobeMenu_Female = menuPool.AddSubMenu(WardrobeMenu, "Weiblich", "Nur Outfits für weibliche Charaktere");
                UIMenu WardrobeMenu_Both = menuPool.AddSubMenu(WardrobeMenu, "Alle", "Alle Outftis");
                Dictionary<string, UIMenu> categoryMenus_Both = new Dictionary<string, UIMenu>();
                foreach (string str in Outfits.EUP_Categories)
                {
                    if (!categoryMenus_Both.ContainsKey(str))
                    {
                        UIMenu uIMenu = menuPool.AddSubMenu(WardrobeMenu_Both, str, $"Wardrobe-Service-Menu [{str}]");
                        categoryMenus_Both.Add(str, uIMenu);
                    }
                }
                UIMenu uiAll_Both = menuPool.AddSubMenu(WardrobeMenu_Both, "Alle", "Alle Kategorien");
                categoryMenus_Both.Add("Alle", uiAll_Both);
                foreach (Outfit outfit in Outfits.EUP_Outfits)
                {
                    UIMenuItem Item = new UIMenuItem(outfit.Name.Replace("Male", "[M]").Replace("Female", "[W]"), "Outfit übernehmen");
                    if (outfit.Name.StartsWith("Male"))
                    {
                        Item.TextColor = Colors.Blue;
                        Item.HighlightColor = Colors.LightBlue;
                    }
                    else
                    if (outfit.Name.StartsWith("Female"))
                    {
                        Item.TextColor = Colors.DeepPink;
                        Item.HighlightColor = Colors.LightCoral;
                    }
                    categoryMenus_Both[outfit.Category].AddItem(Item);
                    categoryMenus_Both["Alle"].AddItem(Item);
                    categoryMenus_Both["Alle"].OnItemSelect += (sender, item, index) =>
                    {
                        if (item == Item)
                        {
                            SetOutfit(outfit);
                        }
                    };
                    categoryMenus_Both[outfit.Category].OnItemSelect += (sender, item, index) =>
                    {
                        if (item == Item)
                        {
                            SetOutfit(outfit);
                        }
                    };
                }
                Dictionary<string, UIMenu> categoryMenus_Male = new Dictionary<string, UIMenu>();
                UIMenu uiAll_Male = menuPool.AddSubMenu(WardrobeMenu_Male, "Alle", "Alle Kategorien");
                categoryMenus_Male.Add("Alle", uiAll_Male);
                foreach (string str in Outfits.EUP_Categories)
                {
                    if (!categoryMenus_Male.ContainsKey(str))
                    {
                        UIMenu uIMenu = menuPool.AddSubMenu(WardrobeMenu_Male, str, $"{str}");
                        categoryMenus_Male.Add(str, uIMenu);
                    }
                }
                foreach (Outfit outfit in Outfits.EUP_Outfits_Male)
                {
                    UIMenuItem Item = new UIMenuItem(outfit.Name.Replace("Male ", ""), "Outfit übernehmen");
                    categoryMenus_Male[outfit.Category].AddItem(Item);
                    categoryMenus_Male["Alle"].AddItem(Item);
                    categoryMenus_Male["Alle"].OnItemSelect += (sender, item, index) =>
                    {
                        if (item == Item)
                        {
                            SetOutfit(outfit);
                        }
                    };
                    categoryMenus_Male[outfit.Category].OnItemSelect += (sender, item, index) =>
                    {
                        if (item == Item)
                        {
                            SetOutfit(outfit);
                        }
                    };
                }
                Dictionary<string, UIMenu> categoryMenus_Female = new Dictionary<string, UIMenu>();
                UIMenu uiAll_Female = menuPool.AddSubMenu(WardrobeMenu_Female, "Alle", "Alle Kategorien");
                categoryMenus_Female.Add("Alle", uiAll_Female);
                foreach (string str in Outfits.EUP_Categories)
                {
                    if (!categoryMenus_Female.ContainsKey(str))
                    {
                        UIMenu uIMenu = menuPool.AddSubMenu(WardrobeMenu_Female, str, $"{str}");
                        categoryMenus_Female.Add(str, uIMenu);
                    }
                }
                foreach (Outfit outfit in Outfits.EUP_Outfits_Female)
                {
                    UIMenuItem Item = new UIMenuItem(outfit.Name.Replace("Female ", ""), "Outfit übernehmen");
                    categoryMenus_Female[outfit.Category].AddItem(Item);
                    categoryMenus_Female["Alle"].AddItem(Item);
                    categoryMenus_Female["Alle"].OnItemSelect += (sender, item, index) =>
                    {
                        if (item == Item)
                        {
                            SetOutfit(outfit);
                        }
                    };
                    categoryMenus_Female[outfit.Category].OnItemSelect += (sender, item, index) =>
                    {
                        if (item == Item)
                        {
                            SetOutfit(outfit);
                        }
                    };
                }

                #endregion Wardrobe

                #region Einsätze

                UIMenu Einsätze_Menu = menuPool.AddSubMenu(mainmenu, "Einsätze", "Einsätze");
                GetPlayerSelectionMenu(Einsätze_Menu);
                GetEinsatzSelectionMenu(Einsätze_Menu);

                UIMenuListItem Einsätze_SendState = new UIMenuListItem("Sender", new List<dynamic>() { "Aktuelle Position", "GPS-Position" }, 0);
                Einsätze_SendState.OnListChanged += (sender, index) =>
                {
                    if (!API.IsWaypointActive())
                    {
                        Einsätze_SendState.Index = 0;
                    }
                };
                Einsätze_Menu.AddItem(Einsätze_SendState);

                UIMenuListItem Einsätze_Kategory = new UIMenuListItem("Einsatzkategorie", Einsatzstatics.Einsatzkategorien.Select(o => o.Bezeichnung).ToList<dynamic>(), 0);
                Einsätze_Menu.AddItem(Einsätze_Kategory);

                bool Einsätze_sonderrechte = true;
                UIMenuCheckboxItem Einsätze_Sonderrechte = new UIMenuCheckboxItem("Sonderrechte", true);
                Einsätze_Sonderrechte.CheckboxEvent += (sender, check) =>
                {
                    Einsätze_sonderrechte = check;
                };
                Einsätze_Menu.AddItem(Einsätze_Sonderrechte);

                UIMenuItem Einsätze_Send = new UIMenuItem("Einsatz auslösen");
                Einsätze_Send.TextColor = Colors.Green;
                Einsätze_Send.HighlightColor = Colors.GreenLight;
                Einsätze_Menu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == Einsätze_Send)
                    {
                        if (SelectedPlayers.Count > 0)
                        {
                            Screen.ShowNotification("~c~Einsatz wird an ausgewählte Einheiten gesendet");
                            SendEinsatzToServer(Einsätze_SendState.Index, Einsätze_Kategory.Index, Einsätze_sonderrechte, SelectedPlayers);
                        }
                        else
                        {
                            Screen.ShowNotification("~c~Einsatz wird an alle Einheiten gesendet");
                            SendEinsatzToServer(Einsätze_SendState.Index, Einsätze_Kategory.Index, Einsätze_sonderrechte, AvailablePlayers.Select(o => o.Key).ToList());
                        }
                    }
                };
                Einsätze_Menu.AddItem(Einsätze_Send);

                //Einsätze_Einsatzübersicht = new UIMenu("DLEA-Service", "Einsatzübersicht", false);
                //menuPool.Add(Einsätze_Einsatzübersicht);

                //UIMenuItem Einsätze_Accept = new UIMenuItem("Einsatz übernehmen");
                //Einsätze_Accept.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
                //Einsätze_Accept.TextColor = Colors.Green;
                //Einsätze_Accept.HighlightColor = Colors.GreenLight;
                //Einsätze_Menu.OnItemSelect += (sender, item, index) =>
                //{
                //    if (item == Einsätze_Accept)
                //    {
                //        AcceptEinsatz();
                //    }
                //};
                //Einsätze_Einsatzübersicht.OnItemSelect += (sender, item, index) =>
                //{
                //    if (item == Einsätze_Accept)
                //    {
                //        AcceptEinsatz();
                //        bool mainMenuOpen = mainmenu.Visible;
                //        item.Selected = false;
                //        menuPool.CloseAllMenus();
                //        mainmenu.Visible = mainMenuOpen;
                //    }
                //};
                //Einsätze_Einsatzübersicht.AddItem(Einsätze_Accept);

                //UIMenuItem Einsätze_Deny = new UIMenuItem("Einsatz ablehnen");
                //Einsätze_Deny.TextColor = Colors.Red;
                //Einsätze_Deny.HighlightColor = Colors.RedLight;
                //Einsätze_Menu.OnItemSelect += (sender, item, index) =>
                //{
                //    if (item == Einsätze_Deny)
                //    {
                //        DenyEinsatz();
                //    }
                //};
                //Einsätze_Einsatzübersicht.OnItemSelect += (sender, item, index) =>
                //{
                //    if (item == Einsätze_Deny)
                //    {
                //        DenyEinsatz();
                //        bool mainMenuOpen = mainmenu.Visible;
                //        item.Selected = false;
                //        menuPool.CloseAllMenus();
                //        mainmenu.Visible = mainMenuOpen;
                //    }
                //};
                //Einsätze_Einsatzübersicht.AddItem(Einsätze_Deny);

                UIMenuItem Einsätze_Quit = new UIMenuItem("Einsatz beenden");
                Einsätze_Quit.TextColor = Colors.Red;
                Einsätze_Quit.HighlightColor = Colors.LightCoral;
                Einsätze_Menu.OnItemSelect += (sender, item, index) =>
                {
                    if (item == Einsätze_Quit)
                    {
                        QuitEinsatz();
                    }
                };
                Einsätze_Menu.AddItem(Einsätze_Quit);

                #endregion Einsätze

                #region Navigation

                UIMenu Navigation_Menu = menuPool.AddSubMenu(mainmenu, "Navigation", "Navigation");
                UIMenuCheckboxItem Navigation_MultiRoute = new UIMenuCheckboxItem("Multiroute", false);
                Navigation_Menu.AddItem(Navigation_MultiRoute);
                Navigation_MultiRoute.CheckboxEvent += (item, check) =>
                {
                    foreach (int blip in MultiRoute_BlipList)
                    {
                        int temp = blip;
                        API.RemoveBlip(ref temp);
                    }
                    MultiRoute_BlipList.Clear();
                    API.SetGpsMultiRouteRender(check);
                    API.ClearGpsMultiRoute();
                    if (check)
                    {
                        API.StartGpsMultiRoute(9, true, true);
                    }
                };

                #endregion Navigation

                Tick += async () =>
                {
                    #region Menu

                    {
                        menuPool.ProcessMenus();
                        if (Game.IsControlPressed(0, Control.FrontendRight) && Game.IsControlPressed(0, Control.FrontendAccept) && !menuPool.IsAnyMenuOpen())
                        {
                            mainmenu.Visible = !mainmenu.Visible;
                            if (API.IsWaypointActive())
                            {
                                Einsätze_SendState.Index = 1;
                            }
                            else
                            {
                                Einsätze_SendState.Index = 0;
                                Einsätze_Kategory.Index = 0;
                            }
                        }
                    }

                    #endregion Menu

                    #region Navigation

                    {
                        if (Navigation_MultiRoute.Checked && API.IsWaypointActive())
                        {
                            Vector3 coords = API.GetBlipInfoIdCoord(API.GetFirstBlipInfoId(8));
                            int blip = API.AddBlipForCoord(coords.X, coords.Y, coords.Z);
                            MultiRoute_BlipList.Add(blip);
                            API.SetBlipSprite(blip, 1);
                            API.SetBlipColour(blip, 3);
                            API.AddPointToGpsMultiRoute(coords.X, coords.Y, coords.Z);
                            API.SetGpsMultiRouteRender(true);
                            API.DeleteWaypoint();
                        }
                        else if (Navigation_MultiRoute.Checked && MultiRoute_BlipList.Count > 0)
                        {
                            int blip1 = MultiRoute_BlipList[0];

                            Vector3 playerlocation = Game.PlayerPed.Position;
                            Vector3 blipLocation = API.GetBlipCoords(blip1);
                            if (!API.IsBlipFlashing(blip1))
                            {
                                API.SetBlipFlashes(blip1, true);
                            }
                            if (API.GetDistanceBetweenCoords(playerlocation.X, playerlocation.Y, playerlocation.Z, blipLocation.X, blipLocation.Y, blipLocation.Z, false) < 30)
                            {
                                int temp = blip1;
                                API.RemoveBlip(ref temp);
                                MultiRoute_BlipList.Remove(blip1);
                                if (MultiRoute_BlipList.Count == 0)
                                {
                                    API.ClearGpsMultiRoute();
                                    API.SetGpsMultiRouteRender(false);
                                    Navigation_MultiRoute.Checked = false;
                                }
                            }
                        }
                    }

                    #endregion Navigation

                    int vehicle = Game.PlayerPed.IsInVehicle() ? 1 : 0;
                    if (Game.PlayerPed.IsInFlyingVehicle)
                    {
                        vehicle = 2;
                    }
                    TriggerServerEvent("DLEA-GPS-Service:Server:SyncGPS", Game.Player.ServerId, API.IsWaypointActive(), API.GetBlipInfoIdCoord(API.GetFirstBlipInfoId(8)), Game.PlayerPed.Position, vehicle);
                };
            }
            catch { }
        }

        #endregion Menu

        private class MyPlayer
        {
            private int Index;

            private string Name;

            public MyPlayer(int index, string name)
            {
                this.Index = index;
                this.Name = name;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        #region Wardrobe

        private async void SetOutfit(Outfit Outfit)
        {
            if (API.GetEntityModel(Game.PlayerPed.Handle) != API.GetHashKey(Outfit.Ped))
            {
                Screen.ShowNotification($"~r~Outfit passt nur auf {Outfit.Ped}");
            }
            else
            {
                foreach (Component Comp in Outfit.Components)
                {
                    API.SetPedComponentVariation(Game.PlayerPed.Handle, Comp.ComponentId, Comp.DrawableId - 1, Comp.TextureId - 1, Comp.PaletteId);
                }

                foreach (Prop Prop in Outfit.Props)
                {
                    if (Prop.DrawableId == 0)
                    {
                        API.ClearPedProp(Game.PlayerPed.Handle, Prop.ComponentId);
                    }
                    else
                    {
                        API.SetPedPropIndex(Game.PlayerPed.Handle, Prop.ComponentId, Prop.DrawableId - 1, Prop.TextureId - 1, true);
                    }
                }
            }
        }

        #endregion Wardrobe

        #region Einsätze

        private void AcceptEinsatz(int Einsatz)
        {
            int blip = -1;
            var einsatz = Einsatzliste.GetRange(Einsatz, 1);
            if (!einsatz.Any())
            {
                Screen.ShowNotification("~c~Kein GPS-Signal gefunden");
            }
            else
            {
                TriggerServerEvent("DLEA-GPS-Service:Server:EINSATZACCEPT", einsatz.First().Item1, Game.Player.ServerId);

                blip = einsatz.First().Item3;
                string streetloc = GetStreetLocation(einsatz.First().Item5);
                string color = Sonderrechte ? "r" : "y";
                string sr = Sonderrechte ? "Sonderrechte frei" : "ohne Sonderrechte";
                Screen.ShowNotification($"~g~Angenommen\n~o~{streetloc}\n~w~Anfahrt ~{color}~{sr}", true);
                API.RemoveBlip(ref blip);
                blip = API.AddBlipForCoord(einsatz.First().Item5.X, einsatz.First().Item5.Y, einsatz.First().Item5.Z);
                API.SetBlipSprite(blip, einsatz.First().Item4);
                API.SetBlipColour(blip, Sonderrechte ? 1 : 5);
                API.SetBlipRoute(blip, true);
                API.DeleteWaypoint();
            }
            Einsatzliste[Einsatz] = new Tuple<int, bool, int, int, Vector3, bool, string>(einsatz.First().Item1, einsatz.First().Item2, blip, einsatz.First().Item4, einsatz.First().Item5, true, einsatz.First().Item7);
        }

        private void DenyEinsatz(int Einsatz)
        {
            int blip = -1;
            var einsatz = Einsatzliste.GetRange(Einsatz, 1);
            if (!einsatz.Any())
            {
                Screen.ShowNotification("~c~Kein GPS-Signal gefunden");
            }
            else
            {
                TriggerServerEvent("DLEA-GPS-Service:Server:EINSATZDENY", einsatz.First().Item1, Game.Player.ServerId);
                Screen.ShowNotification($"~c~Abgelehnt", true);
                int IncommingBlip = einsatz.First().Item3;
                API.RemoveBlip(ref IncommingBlip);
                Einsatzliste.RemoveAt(Einsatz);
            }
        }

        private string GetDistanceAir(Vector3 location)
        {
            float meters = 0;
            meters = API.GetDistanceBetweenCoords(location.X, location.Y, location.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z, true);
            string metersf = "";
            if (meters >= 1000)
            {
                try
                {
                    metersf = Math.Round(meters / 1000, 2).ToString("n") + "km";
                }
                catch { }
            }
            else
            {
                metersf = Math.Round(meters, 0).ToString() + "m";
            }
            return metersf;
        }

        private string GetDistanceStreet(Vector3 location)
        {
            float meters2 = 0;
            meters2 = API.CalculateTravelDistanceBetweenPoints(location.X, location.Y, location.Z, Game.PlayerPed.Position.X, Game.PlayerPed.Position.Y, Game.PlayerPed.Position.Z);
            string meters2f = "";
            if (meters2 >= 1000)
            {
                try
                {
                    meters2f = Math.Round(meters2 / 1000, 2).ToString("n") + "km";
                }
                catch { }
            }
            else
            {
                meters2f = Math.Round(meters2, 0).ToString() + "m";
            }
            return meters2f;
        }

        private void GetEinsatzFromServer(int player, Vector3 location, int blipSprite, string Einsatzbezeichnung, bool Sonderrechte)
        {
            int blip = -1;
            Einsatzliste.Add(new Tuple<int, bool, int, int, Vector3, bool, string>(player, Sonderrechte, blip, blipSprite, location, false, Einsatzbezeichnung));

            string color = Sonderrechte ? "r" : "y";
            string sr = Sonderrechte ? "Sonderrechte frei" : "ohne Sonderrechte";

            string sender = Einsatzbezeichnung == "GPS-Position" ? new PlayerList()[player].Name : "Leitstelle";
            Screen.ShowNotification($"~w~{sender}: ~{color}~{Einsatzbezeichnung}\n~w~Entfernung: ~o~{GetDistanceStreet(location)} ({GetDistanceAir(location)})\n~w~Anfahrt ~{color}~{sr}", true);
            blip = API.AddBlipForCoord(location.X, location.Y, location.Z);

            API.SetBlipSprite(blip, 161);
            API.SetBlipColour(blip, Sonderrechte ? 1 : 5);
        }

        private UIMenu GetEinsatzSelectionMenu(UIMenu Parent)
        {
            UIMenu EinsatzSelectionMenu = menuPool.AddSubMenu(Parent, "Verfügbare Einsätze");
            Tick += async () => { EinsatzSelectionMenu.ParentItem.Text = $"Verfügbare Einsätze ({Einsatzliste.Where(o => !o.Item6).Count()})"; };
            EinsatzSelectionMenu.OnMenuOpen += (senderr) =>
            {
                EinsatzSelectionMenu.MenuItems.Clear();

                foreach (var einsatz in Einsatzliste)
                {
                    string sr = einsatz.Item2 ? "Sonderrechte frei" : "ohne Sonderrechte";
                    string player = Game.Player.Name;
                    if (AvailablePlayers.ContainsKey(einsatz.Item1))
                    {
                        player = AvailablePlayers[einsatz.Item1];
                    }
                    UIMenuItem einsatzItem = new UIMenuItem($"Einsatz: {GetDistanceStreet(einsatz.Item5)}", $"{einsatz.Item7}: ausgelöst von {player}, {Sonderrechte} bei {GetStreetLocation(einsatz.Item5)}");
                    EinsatzSelectionMenu.OnItemSelect += (sender, item, index) =>
                    {
                        if (item == einsatzItem)
                        {
                            try
                            {
                                Parent.RemoveItemAt(index);
                                AcceptEinsatz(Einsatzliste.IndexOf(einsatz));
                                EinsatzSelectionMenu.RemoveItemAt(index);
                            }
                            catch { }
                        }
                    };
                    Tick += async () =>
                    {
                        einsatzItem.Text = $"Einsatz: {GetDistanceStreet(einsatz.Item5)}";
                        einsatzItem.Description = $"{einsatz.Item7}: ausgelöst von {player}, {Sonderrechte} bei {GetStreetLocation(einsatz.Item5)}";
                    };
                    EinsatzSelectionMenu.AddItem(einsatzItem);
                }
            };

            return EinsatzSelectionMenu;
        }

        private UIMenu GetPlayerSelectionMenu(UIMenu Parent)
        {
            UIMenu PlayerSelectionMenu = menuPool.AddSubMenu(Parent, "Einheiten auswählen");
            Tick += async () => { PlayerSelectionMenu.ParentItem.Text = $"Einheiten auswählen ({SelectedPlayers.Count()} / {AvailablePlayers.Count()})"; };
            PlayerSelectionMenu.OnMenuOpen += (senderr) =>
            {
                PlayerSelectionMenu.MenuItems.Clear();
                foreach (KeyValuePair<int, string> keyValuePair in AvailablePlayers)
                {
                    UIMenuCheckboxItem Player_Checkbox = new UIMenuCheckboxItem(keyValuePair.Value, SelectedPlayers.Contains(keyValuePair.Key));
                    Player_Checkbox.CheckboxEvent += (sender, check) =>
                    {
                        try
                        {
                            if (!check && SelectedPlayers.Contains(keyValuePair.Key))
                            {
                                SelectedPlayers.Remove(keyValuePair.Key);
                            }
                            else if (check && !SelectedPlayers.Contains(keyValuePair.Key))
                            {
                                SelectedPlayers.Add(keyValuePair.Key);
                            }
                        }
                        catch { }
                    };
                    PlayerSelectionMenu.AddItem(Player_Checkbox);
                }
            };
            return PlayerSelectionMenu;
        }

        private string GetStreetLocation(Vector3 location)
        {
            uint streetname = 1;
            uint crossingroad = 1;
            string Street = "";
            string CrossingRoad = "";
            API.GetStreetNameAtCoord(location.X, location.Y, location.Z, ref streetname, ref crossingroad);
            Street = API.GetStreetNameFromHashKey(streetname);
            CrossingRoad = API.GetStreetNameFromHashKey(crossingroad);
            if (!string.IsNullOrWhiteSpace(CrossingRoad))
            {
                CrossingRoad = $"~w~ / ~o~{CrossingRoad}";
            }
            return Street + CrossingRoad;
        }

        private void QuitEinsatz()
        {
            int blip = -1;
            var einsatz = Einsatzliste.Where(o => o.Item6 == true);
            if (einsatz.Any() && !API.DoesBlipExist(einsatz.First().Item3))
            {
                Screen.ShowNotification("~c~Kein GPS-Signal gefunden");
                Einsatzliste.Remove(einsatz.First());
            }
            else if (einsatz.Any())
            {
                blip = einsatz.First().Item3;
                Screen.ShowNotification("~c~Teilnahme am Einsatz beendet");
                TriggerServerEvent("DLEA-GPS-Service:Server:EINSATZQUIT", einsatz.First().Item1, Game.Player.ServerId);
                API.RemoveBlip(ref blip);
                blip = -1;
            }
        }

        private void SendEinsatzToServer(int sendstate, int Kategory, bool sonderrechte, List<int> Players)
        {
            #region SetStats

            int blipSprite = 456;
            string einsatzbezeichnung = "GPS-Position";
            blipSprite = Einsatzstatics.Einsatzkategorien[Kategory].Blip;
            einsatzbezeichnung = Einsatzstatics.Einsatzkategorien[Kategory].Bezeichnung;

            #endregion SetStats

            if (sendstate == 0 || !Game.IsWaypointActive)
            {
                foreach (int player in Players)
                {
                    TriggerServerEvent("DLEA-GPS-Service:Server:EINSATZGET", Game.Player.ServerId, Game.Player.Character.Position, blipSprite, einsatzbezeichnung, sonderrechte, player);
                }
            }
            else
            {
                foreach (int player in Players)
                {
                    TriggerServerEvent("DLEA-GPS-Service:Server:EINSATZGET", Game.Player.ServerId, API.GetBlipInfoIdCoord(API.GetFirstBlipInfoId(8)), blipSprite, einsatzbezeichnung, sonderrechte, player);
                }
            }
        }

        #endregion Einsätze
    }
}