using GTAVLife.Helper;

namespace GTAVLife.GameData
{
    public class Life : SimpleSingletonBase<Life>
    {
        public bool HasTAPCard { get; set; }
        public bool HasTrainTicket { get; set; }

        private Life()
        {
            HasTAPCard = false;
            HasTrainTicket = false;
        }
    }
}