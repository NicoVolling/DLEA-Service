using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Objects.CommonVehicle
{
    public static class VehicleDoors
    {
        public static Dictionary<KeyValuePair<VehicleDoorIndex, string>, string> Doors = new Dictionary<KeyValuePair<VehicleDoorIndex, string>, string>()
            {
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.BackLeftDoor, "door_dside_r" ), "Hinten Links" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.BackRightDoor, "door_pside_r"), "Hinten Rechts" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.FrontLeftDoor, "door_dside_f"), "Vorne Links" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.FrontRightDoor, "door_pside_f"), "Vorne Rechts" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.Hood, "bonnet"), "Motorhaube" },
                { new KeyValuePair<VehicleDoorIndex, string>(VehicleDoorIndex.Trunk, "boot"), "Kofferraum" }
            };
    }
}
