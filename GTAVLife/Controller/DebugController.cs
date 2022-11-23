using GTAVLife.GameData;
using GTAVLife.Helper;
using GTAVLife.View;

namespace GTAVLife.Controller
{
    public class DebugController : SimpleSingletonBase<DebugController>, IController
    {
        private DebugView debugView;

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

        public void Hide()
        {
            debugView.Hide();
        }

        public void Process()
        {
            PlayerInfo.DrawMarkerOnNearestPed();
            PlayerInfo.DrawMarkerOnNearestVehicle();
            PlayerInfo.DrawMarkerOnAimingTarget();
            debugView.Process();
        }

        private DebugController()
        {
            debugView = DebugView.Instance;
            debugView.SetController(this);
        }
    }
}