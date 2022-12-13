using Newtonsoft.Json;
using GTA.Math;
using GTA;

namespace GTAVLife.GameData
{
    public class VehicleInfo
    {
        public static readonly string INPOUDING = "inpounding";
        public static readonly string OUTSIDE = "outside";
        public string NickName { get; set; }
        public string Name { get; set; }
        public uint Hash { get; set; }
        public string Plate { get; set; }
        public int LicensePlateStyle { get; set; }
        public int PrimaryColor { get; set; }
        public int SecondaryColor { get; set; }
        public int PearlescentColor { get; set; }
        public int WheelType { get; set; }
        public int FrontWheel { get; set; }
        public int RearWheel { get; set; }
        public int RimColor { get; set; }
        public int WindowTint { get; set; }
        public string Location { get; set; }
        public int WantedLevel { get; set; }

        [JsonIgnore]
        public bool NeedSpawn { get; set; }

        [JsonIgnore]
        public Vector3 SpawnPosition { get; set; }

        [JsonIgnore]
        public float SpawnHeading { get; set; }

        private Vehicle vehicle;

        public VehicleInfo(string nickname, VehicleHash hash, Vector3 spawnPosition, float spawnHeading, VehicleColor color = VehicleColor.MetallicBlack)
        {
            this.NickName = nickname;
            this.Hash = (uint)hash;
            this.Name = Vehicle.GetModelDisplayName(new Model(hash));
            this.PrimaryColor = (int)color;
            this.SecondaryColor = (int)color;
            this.PearlescentColor = (int)color;
            this.SpawnPosition = spawnPosition;
            this.SpawnHeading = spawnHeading;
            this.Location = VehicleInfo.OUTSIDE;
            this.NeedSpawn = true;
            this.vehicle = null;

            setDefaultMod();
        }

        public VehicleInfo(string nickname, VehicleHash hash, string location, VehicleColor color = VehicleColor.MetallicBlack)
        {
            if (location == VehicleInfo.OUTSIDE)
            {
                location = VehicleInfo.INPOUDING;
            }

            this.NickName = nickname;
            this.Hash = (uint)hash;
            this.Name = Vehicle.GetModelDisplayName(new Model(hash));
            this.PrimaryColor = (int)color;
            this.SecondaryColor = (int)color;
            this.PearlescentColor = (int)color;
            this.SpawnPosition = Vector3.Zero;
            this.SpawnHeading = 0f;
            this.Location = location;
            this.NeedSpawn = false;
            this.vehicle = null;

            setDefaultMod();
        }

        public void ImportVehicleInfo(ref Vehicle vehicle)
        {
            if (vehicle.Exists())
            {
                this.vehicle = vehicle;
                this.Hash = (uint)vehicle.Model.Hash;
                this.Name = vehicle.DisplayName;
                this.Plate = vehicle.Mods.LicensePlate;
                this.LicensePlateStyle = (int)vehicle.Mods.LicensePlateStyle;
                this.PrimaryColor = (int)vehicle.Mods.PrimaryColor;
                this.SecondaryColor = (int)vehicle.Mods.SecondaryColor;
                this.PearlescentColor = (int)vehicle.Mods.PearlescentColor;
                this.WheelType = (int) vehicle.Mods.WheelType;
                this.FrontWheel = vehicle.Mods[VehicleModType.FrontWheel].Index;
                this.RearWheel = vehicle.Mods[VehicleModType.RearWheel].Index;
                this.RimColor = (int)vehicle.Mods.RimColor;
                this.WindowTint = (int)vehicle.Mods.WindowTint;
            }
        }

        public bool IsVehicleDeleted()
        {
            if (this.vehicle == null)
            {
                return true;
            }
            return this.vehicle.Exists();
        }

        public void ChangePlate(string plate, GTA.LicensePlateStyle style)
        {
            if (!IsVehicleDeleted())
            {
                this.vehicle.Mods.LicensePlate = plate;
                this.vehicle.Mods.LicensePlateStyle = style;
            }

            this.Plate = plate;
            this.LicensePlateStyle = (int)style;
        }

        private void setDefaultMod()
        {
            this.Plate = null;
            this.LicensePlateStyle = (int)GTA.LicensePlateStyle.BlueOnWhite1;
            this.WheelType = -1;
            this.FrontWheel = -1;
            this.RearWheel = -1;
            this.RimColor = (int)VehicleColor.MetallicSilver;
            this.WindowTint = (int)VehicleWindowTint.Stock;
        }
    }
}