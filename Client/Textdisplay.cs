using CitizenFX.Core;
using CitizenFX.Core.Native;
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

        public static void WriteText(bool Rechts)
        {
            try
            {
                WriteUserListText(Rechts);
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
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

        public static void DrawText(string Text, float X, float Y, Color Color, float TextScale = 1.0f)
        {
            if (Color == Color.Default) { Color = new Color(160, 200, 255); }
            API.SetTextFont(0);
            API.SetTextProportional(true);
            API.SetTextScale(0.0f, 0.3f * TextScale);
            API.SetTextColour(Color.R, Color.G, Color.B, 255);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextEdge(1, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString(Text);
            API.DrawText(X, Y);
        }

        public static void RefreshUserList(ClientObject ClientObject, List<ExtendedUser> UserList)
        {
            UserListText.Clear();
            if (ClientObject.GetService<DisplayService>().GetSettingValue("Anzeige"))
            {
                AddTextRow(new TextKörper("DLEA-Services", 0.1f, Color.Default, true));
                AddTextRow(new TextKörper("", 0.1f));
                foreach (ExtendedUser CurrentUser in UserList)
                {
                    bool PrintUser = CurrentUser.ServerID != ClientObject.ServerID;
                    if (CurrentUser.GetSetting("DataService", "Debugmode"))
                    {
                        PrintUser = true;
                    }
                    if (PrintUser && DateTime.Now.Subtract(new DateTime(CurrentUser.TimeStamp)).TotalSeconds < 10 && ClientObject.GetService<DisplayService>().GetSettingValue("Spieler"))
                    {
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

                        if (!CurrentUser.Visible)
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
                       
                        if (CurrentUser.Visible)
                        {
                            if (ClientObject.CurrentUser.GetSetting("DisplayService", "Distanz"))
                            {
                                AddTextRow(new TextKörper("    ", 0.015f, new Color(R, G, 50)), new TextKörper($"Distanz:", 0.055f, Color.LightBlue), new TextKörper(ClientHelper.GetDistanceAir(new Vector3((float)CurrentUser.Position.X, (float)CurrentUser.Position.Y, (float)CurrentUser.Position.Z)), 0.04f), new TextKörper(zufuss, 0.03f, new Color(R, G, 50)));
                            }
                            
                            AddTextRow(new TextKörper("    ", 0.015f, new Color(R, G, 50)), new TextKörper($"Status:", 0.055f, Color.LightBlue), new TextKörper(CurrentUser.Status, 0.1f, GetStatusColor(CurrentUser.Status)));
                           
                            if (CurrentUser.IsInVehicle && ClientObject.CurrentUser.GetSetting(nameof(DisplayService), "Fahrzeug"))
                            {
                                string schaden = "";
                                if (ClientObject.CurrentUser.GetSetting(nameof(DisplayService), "Fahrzeugschaden"))
                                {
                                    schaden = $" ({ CurrentUser.VehicleHealth}%)";
                                }
                                AddTextRow(new TextKörper("    ", 0.015f, Color.White), new TextKörper($"Fahrzeug:", 0.055f, Color.LightBlue), new TextKörper($"{CurrentUser.VehicleName}{schaden}", 0.1f, Color.LightBlue));
                            }
                        }
                        AddTextRow(new TextKörper("", 0.1f));
                    }
                }
                if (ClientObject.CurrentUser != null)
                {
                    AddTextRow(new TextKörper(ClientObject.CurrentUser.Name, 0.01f, Color.LightBlue));
                    AddTextRow(new TextKörper("Aktueller Status: ", 0.075f, Color.LightRed), new TextKörper(ClientObject.CurrentUser.Status, 0.2f, GetStatusColor(ClientObject.CurrentUser.Status)));

                    if (ClientObject.CurrentUser.IsInVehicle && ClientObject.CurrentUser.GetSetting(nameof(DisplayService), "Fahrzeug"))
                    {
                        string schaden = "";
                        if (ClientObject.CurrentUser.GetSetting(nameof(DisplayService), "Fahrzeugschaden"))
                        {
                            schaden = $" ({ClientObject.CurrentUser.VehicleHealth}%)";
                        }
                        AddTextRow(new TextKörper($"Fahrzeug:", 0.05f, Color.LightRed), new TextKörper($"{ClientObject.CurrentUser.VehicleName}{schaden}", 0.1f, Color.LightBlue));
                    }
                    if (!string.IsNullOrWhiteSpace(ClientObject.CurrentUser.Departement))
                    {
                        AddTextRow(new TextKörper("Behörde: ", 0.05f, Color.LightRed), new TextKörper(ClientObject.CurrentUser.Departement, 0.06f, Color.LightBlue));
                    }
                    if (ClientObject.GetService<DisplayService>().GetSettingValue("Location"))
                    {
                        AddTextRow(new TextKörper(ClientHelper.GetDirection(Game.PlayerPed.Heading), 0.02f, Color.LightRed), new TextKörper(ClientHelper.GetStreetLocation(Game.PlayerPed.Position), 0.1f, Color.LightBlue));
                    }
                }
            }
        }
        public static Color GetStatusColor(string Status)
        {

            if (Status == "Verfügbar")
            {
                return Color.Green;

            }
            else if (Status == "Im Einsatz")
            {
                return Color.Red;
            }
            return Color.LightBlue;
        }
    }

    public class TextKörper
    {
        public float Width { get => _Width * Scale; set => _Width = value; }
        private float _Width { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

        private float Scale = 0.75f;

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
            Textdisplay.DrawText(Text, PosX, PosY, Color, Scale);
        }
    }
}


