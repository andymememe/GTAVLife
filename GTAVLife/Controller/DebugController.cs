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

        public void Process()
        {
            Drawer.DrawMarkerOnPed(PlayerInfo.NearestPed);
            Drawer.DrawMarkerOnVehicle(PlayerInfo.NearestVehicle);
            if (PlayerInfo.Player.IsAiming)
            {
                Drawer.DrawMarkerOnAimingTarget();
            }
            
            debugView.Process();
        }

        private DebugController()
        {
            debugView = DebugView.Instance;
        }
    }
}