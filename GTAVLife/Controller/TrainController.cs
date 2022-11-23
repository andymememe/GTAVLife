using GTA;
using GTAVLife.View;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class TrainController : SimpleSingletonBase<TrainController>, IController
    {
        private const int TRAINTICKET01 = -1700277466;
        private const int TRAINTICKET02 = -455396574;
        private const int TURNSTYLEBAR = 1531047580;

        private static Model[] trainTicketIDs = { TRAINTICKET01, TRAINTICKET02 };

        public IView View
        {
            get
            {
                return null;
            }
        }

        public void Hide() { }

        public void Process()
        {

        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}