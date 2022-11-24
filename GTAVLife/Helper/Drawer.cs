using GTA;
using GTA.UI;
using System;
using System.Drawing;

namespace GTAVLife.Helper
{
    public class Drawer
    {
        public static void DrawMarkerOnPed(Ped ped)
        {
            if (ped != null)
            {
                World.DrawLightWithRange(ped.Position, Color.Aqua, 6f, 1f);
            }
        }

        public static void DrawMarkerOnVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                World.DrawLightWithRange(vehicle.Position, Color.Red, 6f, 1f);
            }
        }

        public static void DrawMarkerOnAimingTarget()
        {
            RaycastResult result = World.GetCrosshairCoordinates(IntersectFlags.Objects);
            if (result.HitEntity != null && result.HitEntity.Exists())
            {
                string msg = String.Format(
                    "{0} ({1})",
                    result.HitEntity.Model.GetHashCode(),
                    result.HitEntity.Health
                );

                Screen.ShowSubtitle(msg, 5000);
                World.DrawLightWithRange(result.HitEntity.Position, Color.LimeGreen, 6f, 1f);
            }
        }
    }
}