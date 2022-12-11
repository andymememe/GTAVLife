using GTA.Math;
using GTA;

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
        public string Location { get; set; }
        public int WantedLevel { get; set; }

        public VehicleInfo(string nickname, string location)
        {
            NickName = nickname;
            Location = location;
        }

        public void ImportVehicleInfo(Vehicle vehicle)
        {
            if (vehicle.Exists())
            {
                this.Hash = vehicle.Model.Hash;
                this.Name = vehicle.DisplayName;
                this.WheelType = (int)vehicle.Mods.WheelType;
                this.Plate = vehicle.Mods.LicensePlate;
                this.LicensePlateStyle = (int)vehicle.Mods.LicensePlateStyle;
                this.PrimaryColor = (int)vehicle.Mods.PrimaryColor;
                this.SecondaryColor = (int)vehicle.Mods.SecondaryColor;
                this.PearlescentColor = (int)vehicle.Mods.PearlescentColor;
                this.RimColor = (int)vehicle.Mods.RimColor;
                this.TrimColor = (int)vehicle.Mods.TrimColor;
                this.WindowTint = (int)vehicle.Mods.WindowTint;
            }
        }
    }
}