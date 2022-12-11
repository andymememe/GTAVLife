using GTA;
using GTA.Math;
using GTAVLife.GameData;

namespace GTAVLife.Helper
{
    public class Spawner
    {
        public static Vehicle SpawnVehicle(int hash, string license, float heading, bool temporary)
        {
            Vehicle vehicle = World.CreateVehicle(hash, PlayerInfo.Position, heading);
            if (vehicle.Exists())
            {
                vehicle.Mods.LicensePlate = license;
                vehicle.PlaceOnNextStreet();
                
                if (temporary)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }
            return vehicle;
        }

        public static Vehicle SpawnVehicle(int hash, string license, Vector3 position, float heading, bool temporary)
        {
            Vehicle vehicle = World.CreateVehicle(hash, position, heading);
            if (vehicle.Exists())
            {
                vehicle.Mods.LicensePlate = license;
                vehicle.PlaceOnGround();

                if (temporary)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }
            return vehicle;
        }

        public static Vehicle SpawnVehicle(VehicleInfo vehicleInfo, Vector3 position, float heading, bool temporary)
        {
            Vehicle vehicle = World.CreateVehicle(vehicleInfo.Hash, position, heading);

            if (vehicle.Exists())
            {
                vehicle.Mods.WheelType = (VehicleWheelType) vehicleInfo.WheelType;
                vehicle.Mods.LicensePlate = vehicleInfo.Plate;
                vehicle.Mods.LicensePlateStyle = (LicensePlateStyle) vehicleInfo.LicensePlateStyle;
                vehicle.Mods.PrimaryColor = (VehicleColor) vehicleInfo.PrimaryColor;
                vehicle.Mods.SecondaryColor = (VehicleColor) vehicleInfo.SecondaryColor;
                vehicle.Mods.PearlescentColor = (VehicleColor) vehicleInfo.PearlescentColor;
                vehicle.Mods.RimColor = (VehicleColor) vehicleInfo.RimColor;
                vehicle.Mods.TrimColor = (VehicleColor) vehicleInfo.TrimColor;
                vehicle.Mods.WindowTint = (VehicleWindowTint) vehicleInfo.WindowTint;

                if (temporary)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }

            return vehicle;
        }
    }
}