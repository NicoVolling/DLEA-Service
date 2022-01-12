using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using Client.Services;
using DLEA_Lib;
using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class Textdisplay
    {
        private static List<List<TextKörper>> UserListText { get; set; } = new List<List<TextKörper>>();

        public static void AddTextRow(params TextKörper[] Columns)
        {
            UserListText.Add(Columns.ToList());
        }

        public static void AddTextRow()
        {
            UserListText.Add(new List<TextKörper>() { new TextKörper("", 0.01f) });
        }

        public static void WriteText(ClientObject ClientObject)
        {
            //try
            //{
                bool Rechts = ClientObject.GetService<DisplayService>().GetSettingValue("Rechts");
                if (ClientObject.MainMenu.IsAnyMenuOpen)
                {
                    Rechts = true;
                }
                WriteUserListText(Rechts);
                WriteStaticText(ClientObject);
            //}
            //catch (Exception ex)
            //{
            //    Tracing.Trace(ex);
            //}
        }

        private static void WriteUserListText(bool Rechts) 
        {
            float Y = 0.01f;
            float X;
            foreach (List<TextKörper> Columns in UserListText)
            {
                X = Rechts ? 0.83f : 0.03f;
                foreach (TextKörper Text in Columns)
                {
                    Text.Draw(X, Y);
                    X += Text.Width;
                }
                Y += 0.02f;
            }
        }

        private static void WriteStaticText(ClientObject ClientObject) 
        {
            TextKörper Velocity = new TextKörper($"{Math.Round(API.GetEntitySpeed(Game.PlayerPed.Handle) * 3.6)} kmh", 0.06f, Color.Gray, 1.9f, 7);
            Velocity.Draw(0.19f, 0.964f);

            TextKörper Richtung = new TextKörper(CommonFunctions.GetDirection(Game.PlayerPed.Heading), 0.1f, Color.Gray, 3.5f, 2, Justification.Center);
            Richtung.Draw(0.175f, 0.90f);

            TextKörper Ort = new TextKörper(CommonFunctions.GetZoneLocation(Game.PlayerPed.Position), 0.5f, Color.LightBlue, 1.5f, 4, Justification.Left);
            Ort.Draw(0.19f, 0.91f);

            TextKörper Ort2 = new TextKörper(CommonFunctions.GetStreetLocation(Game.PlayerPed.Position), 0.5f, Color.LightBlue, 1.5f, 4, Justification.Left);
            Ort2.Draw(0.19f, 0.93f);

            TextKörper Zeit = new TextKörper($"{API.GetClockHours().ToString("D2")}:{API.GetClockMinutes().ToString("D2")}", 0.5f, Color.Gray, 1.5f, 4, Justification.Center);
            Zeit.Draw(0.175f, 0.964f);

            string locname = Game.PlayerPed.IsInVehicle() ? API.GetLabelText(API.GetDisplayNameFromVehicleModel((uint)Game.PlayerPed.CurrentVehicle.Model.Hash)) : "NULL";
            if(string.IsNullOrEmpty(locname)) 
            {
                locname = Game.PlayerPed.CurrentVehicle.LocalizedName;
            }
            string vehname = Game.PlayerPed.IsInVehicle() ? $"{locname} ({((Game.PlayerPed.CurrentVehicle.BodyHealth + Game.PlayerPed.CurrentVehicle.EngineHealth) / 20).ToString("N2")}%)" : "Zu Fuß";
            TextKörper Fahrzeug = new TextKörper($"{vehname}", 0.5f, Color.Gray, 1.5f, 4, Justification.Left);
            Fahrzeug.Draw(0.16f, 0.88f);

            if (ClientObject.CurrentUser != null)
            {
                if (!string.IsNullOrEmpty(ClientObject.CurrentUser.Departement))
                {
                    TextKörper Behörde = new TextKörper($"{ClientObject.CurrentUser.Departement}", 0.5f, Color.LightRed, 1.5f, 4, Justification.Left);
                    Behörde.Draw(0.16f, 0.82f);
                }

                TextKörper Status = new TextKörper($"{ClientObject.CurrentUser.Status}", 0.5f, GetStatusColor(ClientObject.CurrentUser.Status, Color.LightRed), 1.5f, 4, Justification.Left);
                Status.Draw(0.16f, 0.85f);
            }
        }

        public static void DrawText(string Text, float X, float Y, Color Color, float TextScale = 1.0f, int Font = 0, Justification Justification = Justification.Left)
        {
            if (Color == Color.Default) { Color = new Color(160, 200, 255); }
            API.SetTextFont(Font);
            API.SetTextScale(0.0f, 0.3f * TextScale);
            API.SetTextColour(Color.R, Color.G, Color.B, 255);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextEdge(1, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            if (Justification == Justification.Right)
            {
                API.SetTextWrap(0f, X);
            }
            API.SetTextJustification((int)Justification);
            API.BeginTextCommandDisplayText("STRING");
            API.AddTextComponentSubstringPlayerName(Text);
            API.EndTextCommandDisplayText(X, Y);
        }

        public static void RefreshUserList(ClientObject ClientObject, List<ExtendedUser> UserList)
        {
            UserListText.Clear();
            if (ClientObject.GetService<DisplayService>().GetSettingValue("Anzeige"))
            {

                Func<ExtendedUser, bool> Conditions = o => 
                ClientObject.GetService<DisplayService>().Users.Contains(o.Username) && 
                (o.ServerID != ClientObject.ServerID || o.GetSetting("DataService", "Debugmode")) && 
                DateTime.Now.Subtract(new DateTime(o.TimeStamp)).TotalSeconds < 10 && 
                ClientObject.GetService<DisplayService>().GetSettingValue("Spieler");

                bool FirstRow = true;

                foreach (ExtendedUser CurrentUser in UserList.Where(Conditions))
                {
                    if(FirstRow) 
                    {
                        AddTextRow(new TextKörper("DLEA-Services", 0.1f, Color.Default, true));
                        AddTextRow(new TextKörper("", 0.1f));
                        FirstRow = false;
                    }

                    string zufuss = CurrentUser.PlayerSprite == 1 ? "Zu Fuß" : (CurrentUser.Velocity.Value.ToString("N0") + " km/h");
                    double Velocity = CurrentUser.Velocity.Value;
                    if (Velocity > 180) { Velocity = 180; }
                    int R = (int)(255 * (Velocity / 180));
                    int G = (int)(255 * ((180 - Velocity) / 180));

                    string AutoAim = CurrentUser.IsAutoaimActive ? "!" : "";
                    TextKörper ShootDisplay = new TextKörper("O", 0.01f, Color.Green);

                    if (CurrentUser.IsAiming)
                    {
                        ShootDisplay.Text = "X";
                    }
                        
                    if (CurrentUser.IsShooting)
                    {
                        ShootDisplay.Color = Color.Red;
                    }

                    if (!(CurrentUser.Visible && !ClientObject.GetService<SyncService>().GetSettingValue("Unsichtbar")))
                    {
                        ShootDisplay.Text = "O";
                        ShootDisplay.Color = new Color(200, 200, 200);
                    }
                        
                    TextKörper Ping = new TextKörper("Ping: " + CurrentUser.Ping, 0.06f, Color.LightBlue);
                        
                    if (!ClientObject.CurrentUser.GetSetting("DataService", "Debugmode"))
                    {
                        Ping.Text = "";
                        Ping.Width = 0;
                    }
                        
                    AddTextRow(new TextKörper(AutoAim, 0.005f, Color.Red), ShootDisplay, Ping, new TextKörper(CurrentUser.Departement, !string.IsNullOrWhiteSpace(CurrentUser.Departement) ? 0.055f : 0.0f, Color.LightRed), new TextKörper(CurrentUser.Name, 0.15f, Color.LightBlue));
                       
                    if (CurrentUser.Visible && !ClientObject.GetService<SyncService>().GetSettingValue("Unsichtbar"))
                    {
                        if (ClientObject.CurrentUser.GetSetting("DisplayService", "Distanz"))
                        {
                            AddTextRow(new TextKörper("    ", 0.015f, new Color(R, G, 50)), new TextKörper($"Distanz:", 0.055f, Color.LightBlue), new TextKörper(CommonFunctions.GetDistanceAir(new Vector3((float)CurrentUser.Position.X, (float)CurrentUser.Position.Y, (float)CurrentUser.Position.Z)), 0.04f), new TextKörper(zufuss, 0.03f, new Color(R, G, 50)));
                        }
                            
                        AddTextRow(new TextKörper("    ", 0.015f, new Color(R, G, 50)), new TextKörper($"Status:", 0.055f, Color.LightBlue), new TextKörper(CurrentUser.Status, 0.1f, GetStatusColor(CurrentUser.Status, Color.LightRed)));
                           
                        if (CurrentUser.IsInVehicle && ClientObject.GetService<DisplayService>().GetSettingValue("Fahrzeug"))
                        {
                            string schaden = "";
                            if (ClientObject.CurrentUser.GetSetting(nameof(DisplayService), "Fahrzeugschaden"))
                            {
                                schaden = $" ({ CurrentUser.VehicleHealth}%)";
                            }
                            AddTextRow(new TextKörper("    ", 0.015f, Color.White), new TextKörper($"Fahrzeug:", 0.055f, Color.LightBlue), new TextKörper($"{CurrentUser.VehicleName}{schaden}", 0.1f, Color.LightRed));
                        }

                        if(ClientObject.GetService<DisplayService>().GetSettingValue("Standort")) 
                        {
                            AddTextRow(new TextKörper("    ", 0.015f, Color.White), new TextKörper($"{CommonFunctions.GetZoneLocation(Game.PlayerPed.Position)}", 0.65f, Color.LightRed));
                        }
                    }
                    AddTextRow(new TextKörper("", 0.1f));
                }
            }
        }
        public static Color GetStatusColor(string Status, Color Default)
        {

            if (Status == "Verfügbar")
            {
                return Color.Green;

            }
            else if (Status == "Im Einsatz")
            {
                return Color.Red;
            }
            return Default;
        }
    }

    public class TextKörper
    {
        public float Width { get => _Width * Scale; set => _Width = value; }
        private float _Width { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

        private float Scale = 0.75f;

        public int Font = 0;

        public Justification Justification { get; set; } = Justification.Left;

        public TextKörper(string Text, float Width)
        {
            Construct(Text, Width, Color.LightRed, false);
        }

        public TextKörper(string Text, float Width, Color Color)
        {
            Construct(Text, Width, Color, false);
        }

        public TextKörper(string Text, float Width, Color Color, bool Überschrift)
        {
            if (Color == Color.Default) 
            {
                Color = Color.LightRed;
            }
            Construct(Text, Width, Color, Überschrift);
        }

        public TextKörper(string Text, float Width, Color Color, float Scale, int Font = 0, Justification Justification = Justification.Left) 
        {
            Construct(Text, Width, Color, false);
            this.Scale = Scale;
            this.Font = Font;
            this.Justification = Justification;
        }

        private void Construct(string Text, float Width, Color Color, bool Überschrift)
        {
            this.Width = Width;
            this.Text = Text;
            this.Color = Color;

            if (Überschrift)
            {
                this.Scale *= 1.2f;
            }
        }

        public void Draw(float PosX, float PosY)
        {
            Textdisplay.DrawText(Text, PosX, PosY, Color, Scale, Font, Justification);
        }
    }

    public enum Justification { Center, Left, Right }
}


