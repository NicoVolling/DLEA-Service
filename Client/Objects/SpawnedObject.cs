using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Client.ClientHelper;
using DLEA_Lib;
using DLEA_Lib.Shared;
using DLEA_Lib.Shared.Application;
using DLEA_Lib.Shared.Base;
using DLEA_Lib.Shared.Wardrobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class SpawnedObject
    {
        public int ObjectID { get; set; } = -1;
        public List<int> SpeedZones { get; set; } = new List<int>();
        public List<int> BlockZones { get; set; } = new List<int>();

        public static List<SpawnedObject> SpawnedObjects = new List<SpawnedObject>();

        public SpawnedObject(int ObjectID) 
        {
            this.ObjectID = ObjectID;
        }

        public static async void SpawnObject(string ModelName, Action<int> OnObjectSpawned = null)
        {
            float Heading = API.GetEntityHeading(Game.PlayerPed.Handle);
            Vector3 Position = Game.PlayerPed.Position + (API.GetEntityForwardVector(Game.PlayerPed.Handle) * 1.5f);
            uint model = (uint)API.GetHashKey(ModelName);

            API.RequestModel(model);

            while (!API.HasModelLoaded(model))
            {
                await BaseScript.Delay(0);
            }

            int Object = API.CreateObject((int)model, Position.X, Position.Y, Position.Z, true, true, true);
            API.PlaceObjectOnGroundProperly(Object);
            API.SetEntityHeading(Object, Heading);
            API.FreezeEntityPosition(Object, false);
            API.SetEntityCanBeDamaged(Object, false);
            API.SetEntityDynamic(Object, true);

            SpawnedObjects.Add(new SpawnedObject(Object));

            if (OnObjectSpawned != null)
            {
                try
                {
                    OnObjectSpawned(Object);
                }
                catch (Exception ex) { Tracing.Trace(ex); }
            }
        }

        public static void DeleteObject(string[] ModelNames, float Radius = 5, bool All = false)
        {
            int Object = GetNearestObject(ModelNames, Radius);
            DeleteObject(Object);
            if (All) 
            {
                Object = GetNearestObject(ModelNames, Radius);
                foreach (int ObjectId in GetNearObjects(ModelNames,Radius))
                {
                    DeleteObject(ObjectId);
                }
            }
        }

        public static void DeleteObject(int ObjectHandle) 
        {
            if (API.DoesEntityExist(ObjectHandle))
            {
                API.DeleteObject(ref ObjectHandle);
                ClientObject.SendMessage("~g~Objekt entfernt");
            }
            else
            {
                ClientObject.SendMessage("~r~Kein Objekt gefunden");
            }
        }

        public static int GetNearestObject(string[] ModelNames, float Radius = 5) 
        {
            IEnumerable<int> Objects = GetNearObjects(ModelNames, Radius);
            int obj = -1;
            if (Objects.Any()) 
            {
                obj = Objects.First();
            }
            return obj;
        }

        public static IEnumerable<int> GetNearObjects(string[] ModelNames, float Radius = 5)
        {
            Vector3 Position = Game.PlayerPed.Position;
            List<int> FoundObjects = new List<int>();

            int ObjectID = -1;
            int FindHandle = API.FindFirstObject(ref ObjectID);
            bool found = ObjectID != -1;
            if (found) 
            {
                FoundObjects.Add(ObjectID);
            }
            int i = 0;
            while (found) 
            {
                found = API.FindNextObject(FindHandle, ref ObjectID);
                if (found && ModelNames.Any(o => API.GetEntityModel(ObjectID) == API.GetHashKey(o))) 
                {
                    FoundObjects.Add(ObjectID);
                }
            }
            API.EndFindObject(FindHandle);
            return FoundObjects.OrderBy(o => CommonFunctions.DistanceToPlayer(Game.PlayerPed.Position, API.GetEntityCoords(o, true))).Where(o => CommonFunctions.DistanceToPlayer(Game.PlayerPed.Position, API.GetEntityCoords(o, true)) < Radius);
        }

        public static void DeleteAllObjects()
        {
            int Objects = SpawnedObjects.Count;
            foreach (SpawnedObject Object in SpawnedObjects)
            {
                if (API.DoesEntityExist(Object.ObjectID))
                {
                    int Obj = Object.ObjectID;
                    API.DeleteObject(ref Obj);
                }
            }
            ClientObject.SendMessage($"~y~{Objects} ~g~Objekte entfernt");
        }

        public void OnTick() 
        {
            foreach (int SpeedZone in SpeedZones) 
            {
                API.RemoveRoadNodeSpeedZone(SpeedZone);
            }
            SpeedZones.Clear();
            foreach (int BlockZone in BlockZones)
            {
                API.RemoveNavmeshBlockingObject(BlockZone);
            }
            SpeedZones.Clear();

            if (!API.DoesEntityExist(ObjectID)) 
            {
                SpawnedObjects.Remove(this);
                return;
            }

            BlockZones.Add(API.AddNavmeshBlockingObject(
                 API.GetEntityCoords(ObjectID, true).X,
                 API.GetEntityCoords(ObjectID, true).Y,
                 API.GetEntityCoords(ObjectID, true).Z,
                 50,
                 50,
                 50,
                 API.GetEntityHeading(ObjectID),
                 false,
                 7
                 ));

            SpeedZones.Add(API.AddRoadNodeSpeedZone(
                API.GetEntityCoords(ObjectID, true).X,
                API.GetEntityCoords(ObjectID, true).Y,
                API.GetEntityCoords(ObjectID, true).Z,
                1,
                0,
                true
                ));

            SpeedZones.Add(API.AddRoadNodeSpeedZone(
                API.GetEntityCoords(ObjectID, true).X,
                API.GetEntityCoords(ObjectID, true).Y,
                API.GetEntityCoords(ObjectID, true).Z,
                3,
                3,
                true
                ));

            SpeedZones.Add(API.AddRoadNodeSpeedZone(
                API.GetEntityCoords(ObjectID, true).X,
                API.GetEntityCoords(ObjectID, true).Y,
                API.GetEntityCoords(ObjectID, true).Z,
                10,
                5,
                true
                ));

            SpeedZones.Add(API.AddRoadNodeSpeedZone(
                API.GetEntityCoords(ObjectID, true).X,
                API.GetEntityCoords(ObjectID, true).Y,
                API.GetEntityCoords(ObjectID, true).Z,
                20,
                7,
                true
                ));

            SpeedZones.Add(API.AddRoadNodeSpeedZone(
                API.GetEntityCoords(ObjectID, true).X,
                API.GetEntityCoords(ObjectID, true).Y,
                API.GetEntityCoords(ObjectID, true).Z,
                50,
                10,
                true
                ));
        }

        public static void OnTickAll() 
        {
            foreach (SpawnedObject Object in SpawnedObjects) 
            {
                Object.OnTick();
            }
        }
    }
}
