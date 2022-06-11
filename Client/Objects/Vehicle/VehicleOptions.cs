using CitizenFX.Core;
using CitizenFX.Core.Native;
using Client.ClientHelper;
using System.Collections.Generic;
using System.Linq;

namespace Client.Objects.CommonVehicle
{
    public class VehicleOptions
    {
        #region Variables

        public bool DisablePlaneTurbulence { get; private set; } = false;

        public bool FlashHighbeamsOnHonk { get; private set; } = false;

        public bool VehicleBikeSeatbelt { get; private set; } = false;

        public bool VehicleEngineAlwaysOn { get; private set; } = false;

        public bool VehicleFrozen { get; private set; } = false;

        public bool VehicleGodAutoRepair { get; private set; } = false;

        public bool VehicleGodEngine { get; private set; } = false;

        public bool VehicleGodInvincible { get; private set; } = false;

        // Public variables (getters only), return the private variables.
        public bool VehicleGodMode { get; private set; } = false;

        public bool VehicleGodRamp { get; private set; } = false;
        public bool VehicleGodStrongWheels { get; private set; } = false;
        public bool VehicleGodVisual { get; private set; } = false;
        public bool VehicleInfiniteFuel { get; private set; } = false;
        public bool VehicleNeverDirty { get; private set; } = false;
        public bool VehicleNoBikeHelemet { get; private set; } = false;
        public bool VehicleNoSiren { get; private set; } = false;
        public bool VehiclePowerMultiplier { get; private set; } = false;
        public float VehiclePowerMultiplierAmount { get; private set; } = 2f;
        public bool VehicleShowHealth { get; private set; } = false;
        public bool VehicleTorqueMultiplier { get; private set; } = false;
        public float VehicleTorqueMultiplierAmount { get; private set; } = 2f;

        #endregion Variables

        #region Update Vehicle Mods Menu

        internal static int _GetHeadlightsColorFromVehicle(Vehicle vehicle)
        {
            if (vehicle != null && vehicle.Exists())
            {
                if (API.IsToggleModOn(vehicle.Handle, 22))
                {
                    int val = API.GetVehicleHeadlightsColour(vehicle.Handle);
                    if (val > -1 && val < 13)
                    {
                        return val;
                    }
                    return -1;
                }
            }
            return -1;
        }

        internal static void _SetHeadlightsColorOnVehicle(Vehicle veh, int newIndex)
        {
            if (veh != null && veh.Exists() && veh.Driver == Game.PlayerPed)
            {
                if (newIndex > -1 && newIndex < 13)
                {
                    API.SetVehicleHeadlightsColour(veh.Handle, newIndex);
                }
                else
                {
                    API.SetVehicleHeadlightsColour(veh.Handle, -1);
                }
            }
        }

        #endregion Update Vehicle Mods Menu

        #region GetColorFromIndex function (underglow)

        private readonly List<int[]> _VehicleNeonLightColors = new List<int[]>()
        {
            { new int[3] { 255, 255, 255 } },   // White
            { new int[3] { 2, 21, 255 } },      // Blue
            { new int[3] { 3, 83, 255 } },      // Electric blue
            { new int[3] { 0, 255, 140 } },     // Mint Green
            { new int[3] { 94, 255, 1 } },      // Lime Green
            { new int[3] { 255, 255, 0 } },     // Yellow
            { new int[3] { 255, 150, 5 } },     // Golden Shower
            { new int[3] { 255, 62, 0 } },      // Orange
            { new int[3] { 255, 0, 0 } },       // Red
            { new int[3] { 255, 50, 100 } },    // Pony Pink
            { new int[3] { 255, 5, 190 } },     // Hot Pink
            { new int[3] { 35, 1, 255 } },      // Purple
            { new int[3] { 15, 3, 255 } },      // Blacklight
        };

        /// <summary>
        /// Converts a list index to a <see cref="System.Drawing.Color"/> struct.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private System.Drawing.Color GetColorFromIndex(int index)
        {
            if (index >= 0 && index < 13)
            {
                return System.Drawing.Color.FromArgb(_VehicleNeonLightColors[index][0], _VehicleNeonLightColors[index][1], _VehicleNeonLightColors[index][2]);
            }
            return System.Drawing.Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Returns the color index that is applied on the current vehicle.
        /// If a color is active on the vehicle which is not in the list, it'll return the default index 0 (white).
        /// </summary>
        /// <returns></returns>
        private int GetIndexFromColor()
        {
            Vehicle veh = CommonFunctions.GetVehicle();

            if (veh == null || !veh.Exists() || !veh.Mods.HasNeonLights)
            {
                return 0;
            }

            int r = 255, g = 255, b = 255;

            API.GetVehicleNeonLightsColour(veh.Handle, ref r, ref g, ref b);

            if (r == 255 && g == 0 && b == 255) // default return value when the vehicle has no neon kit selected.
            {
                return 0;
            }

            if (_VehicleNeonLightColors.Any(a => { return a[0] == r && a[1] == g && a[2] == b; }))
            {
                return _VehicleNeonLightColors.FindIndex(a => { return a[0] == r && a[1] == g && a[2] == b; });
            }

            return 0;
        }

        #endregion GetColorFromIndex function (underglow)
    }
}