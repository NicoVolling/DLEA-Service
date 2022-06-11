using System.Collections.Generic;

namespace Client.Objects.CommonVehicle
{
    public struct VehicleInfo
    {
        public bool bulletProofTires;
        public Dictionary<string, int> colors;
        public bool customWheels;
        public float enveffScale;
        public Dictionary<int, bool> extras;
        public int headlightColor;
        public int livery;
        public uint model;
        public Dictionary<int, int> mods;
        public string name;
        public bool neonBack;
        public bool neonFront;
        public bool neonLeft;
        public bool neonRight;
        public int plateStyle;
        public string plateText;
        public bool turbo;
        public bool tyreSmoke;
        public int version;
        public int wheelType;
        public int windowTint;
        public bool xenonHeadlights;
    };
}