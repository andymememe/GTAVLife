using System;
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
                if (vehicleInfo.Plate != null)
                {
                    vehicle.Mods.LicensePlate = vehicleInfo.Plate;
                }
                
                vehicle.Mods.LicensePlateStyle = (LicensePlateStyle) vehicleInfo.LicensePlateStyle;
                vehicle.Mods.PrimaryColor = (VehicleColor) vehicleInfo.PrimaryColor;
                vehicle.Mods.SecondaryColor = (VehicleColor) vehicleInfo.SecondaryColor;
                vehicle.Mods.PearlescentColor = (VehicleColor) vehicleInfo.PearlescentColor;
                vehicle.Mods.RimColor = (VehicleColor) vehicleInfo.RimColor;
                vehicle.Mods.WindowTint = (VehicleWindowTint) vehicleInfo.WindowTint;
                
                if (vehicleInfo.WheelType >= 0 && Array.IndexOf(vehicle.Mods.AllowedWheelTypes, (VehicleWheelType)vehicleInfo.WheelType) >= 0)
                {
                    vehicle.Mods.WheelType = (VehicleWheelType)vehicleInfo.WheelType;
                }

                if (vehicleInfo.FrontWheel >= 0)
                {
                    vehicle.Mods[VehicleModType.FrontWheel].Index = vehicleInfo.FrontWheel;
                }

                if (vehicleInfo.RearWheel >= 0)
                {
                    vehicle.Mods[VehicleModType.RearWheel].Index = vehicleInfo.RearWheel;
                }

                if (!isMissionUsed)
                {
                    vehicle.MarkAsNoLongerNeeded();
                }
            }

            return vehicle;
        }
    }
}