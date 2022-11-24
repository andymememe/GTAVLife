using System.Collections.Generic;
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

        [JsonProperty("OwnedVehicles")]
        private List<VehicleInfo> OwnedVehicles
        {
            get
            {
                return ownedVehicles;
            }
            set
            {
                ownedVehicles = value;
            }
        }

        [JsonIgnore]
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
        }

        private static Life instance;
        private bool hasTAPCard;
        private bool hasTrainTicket;
        private List<VehicleInfo> ownedVehicles;
        private bool isDirty;

        public void AddOwnedCar(VehicleInfo vehicle)
        {
            ownedVehicles.Add(vehicle);
            this.isDirty = true;
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

        private Life()
        {
            this.hasTAPCard = false;
            this.hasTrainTicket = false;
            this.isDirty = true;
            instance = this;
        }
    }
}