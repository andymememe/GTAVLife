using GTAVLife.GameData;
using GTAVLife.View;

namespace GTAVLife.Controller
{
    public class DebugController : IController
    {
        private static DebugController instance = null;
        private DebugView debugView;

        public static DebugController Instance
        {
            get
            {
                return instance ?? new DebugController();
            }
        }

        public IView View
        {
            get
            {
                return debugView;
            }
        }

        public void Show()
        {
            debugView.Show();
        }

        public void OnHide()
        {
            debugView.Hide();
        }

        public void Process()
        {
            PlayerInfo.DrawMarkerOnNearestPed();
            PlayerInfo.DrawMarkerOnNearestVehicle();
            debugView.Process();
        }

        private DebugController()
        {
            debugView = DebugView.Instance;
            debugView.SetController(this);
            instance = this;
        }
    }
}