using System.Drawing;
using GTA;
using GTA.Math;

namespace GTAVLife.Helper
{
    public class MissionHelper
    {
        public static Blip SetBlip (Vector3 position, BlipSprite sprite, string name)
        {
            Blip blip = World.CreateBlip(position);
            blip.DisplayType = BlipDisplayType.BothMapSelectable;
            blip.CategoryType = BlipCategoryType.DistanceShown;
            blip.Sprite = sprite;
            blip.Name = name;

            return blip;
        }

        public static Checkpoint SetPlayerCheckpoint (Vector3 position, Vector3 pointTo, Color color)
        {
            Checkpoint checkpoint = World.CreateCheckpoint(CheckpointIcon.Cyclinder2, position, pointTo, Distance.ToGameWorldDistance(500), color);
            checkpoint.CylinderNearHeight = Distance.ToGameWorldDistance(500);
            checkpoint.CylinderFarHeight = Distance.ToGameWorldDistance(500);

            return checkpoint;
        }

        public static Checkpoint SetVehicleCheckpoint (Vector3 position, Vector3 pointTo, Color color)
        {
            Checkpoint checkpoint = World.CreateCheckpoint(CheckpointIcon.Cyclinder2, position, pointTo, Distance.ToGameWorldDistance(3000), color);
            checkpoint.CylinderNearHeight = Distance.ToGameWorldDistance(1000);
            checkpoint.CylinderFarHeight = Distance.ToGameWorldDistance(1000);

            return checkpoint;
        }

        public static void RemoveBlip (Blip blip)
        {
            blip.Delete();
        }

        public static void RemoveCheckpoint (Checkpoint checkpoint)
        {
            checkpoint.Delete();
        }

        public static void CleanUpMission ()
        {
            foreach (Checkpoint ckpt in World.GetAllCheckpoints())
            {
                ckpt.Delete();
            }

            foreach (Blip b in World.GetAllBlips())
            {
                b.Delete();
            }
        }
    }
}