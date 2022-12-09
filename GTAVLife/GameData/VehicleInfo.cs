using GTA;
using GTA.Math;

namespace GTAVLife.GameData
{
    public class VehicleInfo
    {
        public string NickName { get; set; }
        public bool NeedSpawn { get; set; }
        public Vector3 SpawnPosition { get; set; }
        public float SpawnHeading { get; set; }
        public string Name { get; set; }
        public int Hash { get; set; }
        public int WheelType { get; set; }
        public string Plate { get; set; }
        public int LicensePlateStyle { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
        public int PearlescentColor { get; set; }
        public int RimColor { get; set; }
        public int TrimColor { get; set; }
        public int WindowTint { get; set; }

        public Vehicle ModVehicle(Vehicle vehicle)
        {
            vehicle.Mods.WheelType = (VehicleWheelType) WheelType;
            vehicle.Mods.LicensePlate = Plate;
            vehicle.Mods.LicensePlateStyle = (LicensePlateStyle) LicensePlateStyle;
            vehicle.Mods.PrimaryColor = (VehicleColor) PrimaryColor;
            vehicle.Mods.SecondaryColor = (VehicleColor) SecondaryColor;
            vehicle.Mods.PearlescentColor = (VehicleColor) PearlescentColor;
            vehicle.Mods.RimColor = (VehicleColor) RimColor;
            vehicle.Mods.TrimColor = (VehicleColor) TrimColor;
            vehicle.Mods.WindowTint = (VehicleWindowTint) WindowTint;

            return vehicle;
        }

        public VehicleInfo(Vehicle vehicle, string nickname)
        {
            NickName = nickname;
            Hash = vehicle.Model.Hash;
            Name = vehicle.DisplayName;
            WheelType = (int)vehicle.Mods.WheelType;
            Plate = vehicle.Mods.LicensePlate;
            LicensePlateStyle = (int)vehicle.Mods.LicensePlateStyle;
            PrimaryColor = (int)vehicle.Mods.PrimaryColor;
            SecondaryColor = (int)vehicle.Mods.SecondaryColor;
            PearlescentColor = (int)vehicle.Mods.PearlescentColor;
            RimColor = (int)vehicle.Mods.RimColor;
            TrimColor = (int)vehicle.Mods.TrimColor;
            WindowTint = (int)vehicle.Mods.WindowTint;
        }
    }
}