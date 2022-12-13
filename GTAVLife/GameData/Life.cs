using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

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

        public bool HasTAPCard
        {
            get
            {
                return hasTAPCard;
            }

            set
            {
                isDirty = true;
                hasTAPCard = value;
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
        private bool hasTAPCard;
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

            this.hasTAPCard = false;
            this.hasTrainTicket = false;
            this.isDirty = true;

            this.ownedVehicles = new List<VehicleInfo>();
            this.CurrentEntryPointIndex = -1;

            instance = this;
        }
    }
}