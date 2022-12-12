using System.Drawing;
using GTA;
using GTA.Math;

namespace GTAVLife.Helper
{
    public class EntryPointHelper
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
            Checkpoint checkpoint = World.CreateCheckpoint(CheckpointIcon.Cyclinder2, position, pointTo, DistanceUtils.ToGameWorldDistance(500), color);
            checkpoint.CylinderNearHeight = DistanceUtils.ToGameWorldDistance(500);
            checkpoint.CylinderFarHeight = DistanceUtils.ToGameWorldDistance(500);

            return checkpoint;
        }

        public static Checkpoint SetVehicleCheckpoint (Vector3 position, Vector3 pointTo, Color color)
        {
            Checkpoint checkpoint = World.CreateCheckpoint(CheckpointIcon.Cyclinder2, position, pointTo, DistanceUtils.ToGameWorldDistance(3000), color);
            checkpoint.CylinderNearHeight = DistanceUtils.ToGameWorldDistance(1000);
            checkpoint.CylinderFarHeight = DistanceUtils.ToGameWorldDistance(1000);

            return checkpoint;
        }
    }
}