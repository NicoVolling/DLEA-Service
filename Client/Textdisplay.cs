using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using Client.Services;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public enum Justification

    { Center, Left, Right }

    public static class Textdisplay
    {
        public static int SpeedLimit = 30;

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

        public static void WriteText(ClientObject ClientObject)
        {
            try
            {
                WriteStaticText(ClientObject);
            }
            catch (Exception ex)
            {
                Tracing.Trace(ex);
            }
        }

        private static void WriteStaticText(ClientObject ClientObject)
        {
            Color VelColor = new Color(50, 100, 255);
            double speed = Math.Round(API.GetEntitySpeed(Game.PlayerPed.Handle) * 3.6);
            if (speed >= SpeedLimit - 3 && speed <= SpeedLimit + 3)
            {
                VelColor = Color.Green;
            }
            else if (speed < SpeedLimit && speed > 0)
            {
                VelColor = new Color(50, 100, 255);
            }
            else if (speed > SpeedLimit + 3 && speed <= SpeedLimit + 8)
            {
                VelColor = new Color(150, 150, 0);
            }
            else if (speed > SpeedLimit + 8)
            {
                VelColor = Color.Red;
            }
            else if (speed == 0)
            {
                VelColor = Color.Gray;
            }
            if (!new PlayerList()[ClientObject.ServerID].Character.IsInVehicle())
            {
                VelColor = Color.Gray;
            }
            TextKörper Velocity = new TextKörper($"{speed} kmh", 0.06f, VelColor, 1.9f, 7);
            Velocity.Draw(0.19f, 0.964f);

            TextKörper Speedlimit = new TextKörper($"({SpeedLimit} kmh)", 0.06f, new Color(200, 150, 200), 1.9f, 7);
            Speedlimit.Draw(0.25f, 0.964f);

            TextKörper Richtung = new TextKörper(CommonFunctions.GetDirection(Game.PlayerPed.Heading), 0.1f, Color.Gray, 3.5f, 2, Justification.Center);
            Richtung.Draw(0.175f, 0.90f);

            TextKörper Ort = new TextKörper(CommonFunctions.GetZoneLocation(Game.PlayerPed.Position), 0.5f, Color.LightBlue, 1.5f, 4, Justification.Left);
            Ort.Draw(0.19f, 0.91f);

            TextKörper Ort2 = new TextKörper(CommonFunctions.GetStreetLocation(Game.PlayerPed.Position), 0.5f, Color.LightBlue, 1.5f, 4, Justification.Left);
            Ort2.Draw(0.19f, 0.93f);

            TextKörper Zeit = new TextKörper($"{API.GetClockHours().ToString("D2")}:{API.GetClockMinutes().ToString("D2")}", 0.5f, Color.Gray, 1.5f, 4, Justification.Center);
            Zeit.Draw(0.175f, 0.964f);

            string locname = Game.PlayerPed.IsInVehicle() ? API.GetLabelText(API.GetDisplayNameFromVehicleModel((uint)Game.PlayerPed.CurrentVehicle.Model.Hash)) : "NULL";
            if (string.IsNullOrEmpty(locname))
            {
                locname = Game.PlayerPed.CurrentVehicle.LocalizedName;
            }
            string vehname = Game.PlayerPed.IsInVehicle() ? $"{locname} ({((Game.PlayerPed.CurrentVehicle.BodyHealth + Game.PlayerPed.CurrentVehicle.EngineHealth) / 20).ToString("N2")}%)" : "Zu Fuß";
            TextKörper Fahrzeug = new TextKörper($"{vehname}", 0.5f, Color.Gray, 1.5f, 4, Justification.Left);
            Fahrzeug.Draw(0.16f, 0.88f);
        }
    }

    public class TextKörper
    {
        public int Font = 0;

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

        public TextKörper(string Text, float Width, Color Color, float Scale, int Font = 0, Justification Justification = Justification.Left)
        {
            Construct(Text, Width, Color, false);
            this.Scale = Scale;
            this.Font = Font;
            this.Justification = Justification;
        }

        public Color Color { get; set; }

        public Justification Justification { get; set; } = Justification.Left;

        public string Text { get; set; }

        public float Width { get => _Width * Scale; set => _Width = value; }

        private float _Width { get; set; }

        public void Draw(float PosX, float PosY)
        {
            Textdisplay.DrawText(Text, PosX, PosY, Color, Scale, Font, Justification);
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
    }
}