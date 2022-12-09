using GTA;
using GTA.UI;
using GTA.Math;
using System;
using System.Drawing;

namespace GTAVLife.Helper
{
    public class Drawer
    {
        public static void DrawMarkerOnEntity(Vector3 entityPos, Color color)
        {
            World.DrawMarker(MarkerType.UpsideDownCone, entityPos + (Vector3.WorldUp * 0.5f), Vector3.Zero, Vector3.Zero, new Vector3(.3f, .3f, .3f), color, bobUpAndDown: true, faceCamera: false, drawOnEntity: false);
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
                World.DrawMarker(MarkerType.UpsideDownCone, result.HitEntity.AbovePosition + (Vector3.WorldUp * 0.5f), Vector3.Zero, Vector3.Zero, new Vector3(.3f, .3f, .3f), Color.LimeGreen, bobUpAndDown: true, faceCamera: false, drawOnEntity: false);
            }
        }
    }
}