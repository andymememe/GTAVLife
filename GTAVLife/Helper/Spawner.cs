using GTA;
using GTA.Math;

namespace GTAVLife.Helper
{
    public class Spawner
    {
        public static Vehicle SpawnVehicle(int hash, Vector3 position, float heading)
        {
            return World.CreateVehicle(hash, position, heading);
        }
    }
}