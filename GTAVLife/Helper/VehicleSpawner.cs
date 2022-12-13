using GTA;
using GTA.Math;
using GTAVLife.GameData;

namespace GTAVLife.Helper
{
    public class VehicleSpawner
    {
        public static Vehicle SpawnVehicle(VehicleHash hash, string license, bool isMissionUsed)
        {
            Vehicle vehicle = World.CreateVehicle(new Model(hash), PlayerInfo.Position + (Vector3.RelativeLeft * DistanceUtils.ToGameWorldDistance(2000)), PlayerInfo.Character.Heading);
            if (vehicle.Exists())
            {
                vehicle.Mods.LicensePlate = license;
                vehicle.PlaceOnNextStreet();
                
                if (!isMissionUsed)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }
            return vehicle;
        }

        public static Vehicle SpawnVehicle(VehicleHash hash, string license, Vector3 position, float heading, bool isMissionUsed)
        {
            Vehicle vehicle = World.CreateVehicle(new Model(hash), position, heading);
            if (vehicle.Exists())
            {
                vehicle.Mods.LicensePlate = license;
                vehicle.PlaceOnGround();

                if (!isMissionUsed)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }
            return vehicle;
        }

        public static Vehicle SpawnVehicle(VehicleInfo vehicleInfo, bool isMissionUsed)
        {
            Vehicle vehicle = World.CreateVehicle(new Model((VehicleHash) vehicleInfo.Hash), vehicleInfo.SpawnPosition, vehicleInfo.SpawnHeading);

            if (vehicle.Exists())
            {
                vehicle.Mods.LicensePlate = vehicleInfo.Plate;
                vehicle.Mods.LicensePlateStyle = (LicensePlateStyle) vehicleInfo.LicensePlateStyle;
                vehicle.Mods.PrimaryColor = (VehicleColor) vehicleInfo.PrimaryColor;
                vehicle.Mods.SecondaryColor = (VehicleColor) vehicleInfo.SecondaryColor;
                vehicle.Mods.PearlescentColor = (VehicleColor) vehicleInfo.PearlescentColor;
                vehicle.Mods.RimColor = (VehicleColor) vehicleInfo.RimColor;
                vehicle.Mods.WindowTint = (VehicleWindowTint) vehicleInfo.WindowTint;

                if (!isMissionUsed)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }

            return vehicle;
        }
    }
}