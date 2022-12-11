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

        public VehicleInfo(string nickname)
        {
            NickName = nickname;
        }
    }
}