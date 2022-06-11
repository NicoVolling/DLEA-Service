namespace DLEA_Lib.Shared.Base
{
    public struct Color
    {
        public static readonly Color DarkGray = new Color(150, 150, 150);
        public static readonly Color Default = new Color(-1, -1, -1);
        public static readonly Color Gray = new Color(220, 220, 220);
        public static readonly Color Green = new Color(0, 255, 0);
        public static readonly Color LightBlue = new Color(220, 220, 255);
        public static readonly Color LightRed = new Color(255, 220, 220);
        public static readonly Color Red = new Color(255, 0, 0);
        public static readonly Color White = new Color(255, 255, 255);
        public int B;
        public int G;
        public int R;

        public Color(int R = 255, int G = 255, int B = 255)
        {
            this.R = R;
            this.B = B;
            this.G = G;
        }

        public static bool operator !=(Color a, Color b)
        {
            return !(a == b);
        }

        public static bool operator ==(Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }
    }
}