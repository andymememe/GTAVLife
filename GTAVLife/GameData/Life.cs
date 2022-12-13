using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using GTA;

namespace GTAVLife.GameData
{
    public class Life
    {
        public static Life Instance
        {
            get
            {
                return instance ?? new Life();
            }

            set
            {
                instance = value;
            }
        }

        public int FAPCard
        {
            get
            {
                return fapCard;
            }

            set
            {
                isDirty = true;
                fapCard = value;
            }
        }

        public bool HasTrainTicket
        {
            get
            {
                return hasTrainTicket;
            }

            set
            {
                isDirty = true;
                hasTrainTicket = value;
            }
        }

        [JsonIgnore]
        public bool IsActivate
        {
            get
            {
                return isActivate;
            }

            set
            {
                isActivate = value;
                isDirty = true;
            }
        }

        [JsonIgnore]
        public bool IsDirty => isDirty;

        [JsonIgnore]
        public int CurrentEntryPointIndex { get; set; }

        [JsonIgnore]
        public ReadOnlyCollection<VehicleInfo> OwnedVehicles => ownedVehicles.AsReadOnly();

        [JsonProperty("OwnedVehicles")]
        private List<VehicleInfo> ownedVehicles;

        private static Life instance;
        private int fapCard;
        private bool hasTrainTicket;
        private bool isDirty;
        private bool isActivate;

        public bool AddOwnedVehicle(VehicleInfo vehicleInfo)
        {
            if (this.ownedVehicles.Find(info => info.NickName == vehicleInfo.NickName) != null)
            {
                return false;
            }

            ownedVehicles.Add(vehicleInfo);
            return true;
        }

        public bool ChangePlate(int index, string plate)
        {
            if (index < this.ownedVehicles.Count)
            {
                if (this.ownedVehicles.Find(info => info.Plate == plate) != null)
                {
                    return false;
                }

                LicensePlateStyle style = (LicensePlateStyle) this.ownedVehicles[index].LicensePlateStyle;
                this.ownedVehicles[index].ChangePlate(plate, style);
                return true;
            }

            return false;
        }

        public bool ChangePlateStyle(int index, LicensePlateStyle style)
        {
            if (index < this.ownedVehicles.Count)
            {
                this.ownedVehicles[index].ChangePlate(this.ownedVehicles[index].Plate, style);
                return true;
            }

            return false;
        }

        public string Serializer()
        {
            string result = JsonConvert.SerializeObject(this, Formatting.Indented);
            this.isDirty = false;

            return result;
        }

        public void Deserializer(string content)
        {
            instance = JsonConvert.DeserializeObject<Life>(content);
            this.isDirty = false;
        }

        public void ForceDirty()
        {
            this.isDirty = true;
        }

        private Life()
        {
            this.isActivate = false;

            this.fapCard = -1;
            this.hasTrainTicket = false;
            this.isDirty = true;

            this.ownedVehicles = new List<VehicleInfo>();
            this.CurrentEntryPointIndex = -1;

            instance = this;
        }
    }
}