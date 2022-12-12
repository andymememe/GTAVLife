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
        
        [JsonIgnore]
        public bool NeedSpawn { get; set; }

        [JsonIgnore]
        public Vector3 SpawnPosition { get; set; }

        [JsonIgnore]
        public float SpawnHeading { get; set; }
        
        private Vehicle vehicle;

        public VehicleInfo(string nickname, int hash, Vector3 spawnPosition, float spawnHeading)
        {
            this.NickName = nickname;
            this.Hash = hash;
            this.SpawnPosition = spawnPosition;
            this.SpawnHeading = spawnHeading;
            this.Location = VehicleInfo.OUTSIDE;
            this.NeedSpawn = true;
            this.vehicle = null;
        }

        public VehicleInfo(string nickname, int hash, string location)
        {
            if (location == VehicleInfo.OUTSIDE)
            {
                location = VehicleInfo.INPOUDING;
            }
            
            this.NickName = nickname;
            this.Hash = hash;
            this.SpawnPosition = Vector3.Zero;
            this.SpawnHeading = 0f;
            this.Location = location;
            this.NeedSpawn = false;
            this.vehicle = null;
        }

        public void ImportVehicleInfo(ref Vehicle vehicle)
        {
            if (vehicle.Exists())
            {
                this.vehicle = vehicle;
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

        public bool IsVehicleDeleted()
        {
            if (this.vehicle == null)
            {
                return true;
            }
            return this.vehicle.Exists();
        }
    }
}