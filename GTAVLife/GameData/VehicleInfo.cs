using GTA;

namespace GTAVLife.GameData
{
    public class VehicleInfo
    {
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

        public Vehicle SpawnVehicle()
        {
            return null;
        }

        public VehicleInfo(Vehicle vehicle)
        {
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