using System;
using GTA;
using GTA.Math;
using System.Drawing;

namespace GTAVLife.GameData
{
    class PlayerInfo
    {
        public static Player Player
        {
            get
            {
                return Game.Player;
            }
        }

        public static Ped Character
        {
            get
            {
                return Game.Player.Character;
            }
        }

        public static string Name
        {
            get
            {
                string name = Enum.GetName(typeof(PedHash), (uint)Character.Model.Hash);
                if (name == null) {
                    return "";
                }
                return name;
            }
        }

        public static Vector3 Position
        {
            get
            {
                return Character.Position;
            }
        }

        public static Vehicle Vehicle
        {
            get
            {
                return Character.CurrentVehicle;
            }
        }

        public static string Zone
        {
            get
            {
                return World.GetZoneDisplayName(Character.Position);
            }
        }

        public static string LocalizedZone
        {
            get
            {
                return World.GetZoneLocalizedName(Character.Position);
            }
        }

        public static Ped NearestPed
        {
            get
            {
                Ped closestPed = null;
                float closestDistance = 10000f;
                foreach (Ped ped in World.GetNearbyPeds(Character, 6f))
                {
                    float dist = World.GetDistance(Character.Position, ped.Position);
                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        closestPed = ped;
                    }
                }
                return closestPed;
            }
        }

        public static string NearestPedName
        {
            get
            {
                Ped closestPed = NearestPed;
                if (closestPed != null) {
                    string name = Enum.GetName(typeof(PedHash), (uint)closestPed.Model.Hash);
                    if (name == null) {
                        return "";
                    }
                    return name;
                }
                return "";
            }
        }

        public static Vehicle NearestVehicle
        {
            get
            {
                return World.GetClosestVehicle(Character.Position, 3f);
            }
        }

        public static void DrawMarkerOnNearestPed()
        {
            if (NearestPed != null)
            {
                World.DrawLightWithRange(NearestPed.Position, Color.Aqua, 6f, 1f);
            }
        }

        public static void DrawMarkerOnNearestVehicle()
        {
            if (NearestVehicle != null)
            {
                World.DrawLightWithRange(NearestVehicle.Position, Color.Red, 6f, 1f);
            }
        }
    }
}