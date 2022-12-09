using System.Drawing;
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
            if (PlayerInfo.NearestPed != null)
            {
                if (PlayerInfo.NearestPed.CurrentVehicle != null)
                {
                    Drawer.DrawMarkerOnEntity(PlayerInfo.NearestPed.CurrentVehicle.AbovePosition, Color.Red);
                }
                else
                {
                    Drawer.DrawMarkerOnEntity(PlayerInfo.NearestPed.AbovePosition, Color.Red);
                }
            }

            if (PlayerInfo.NearestVehicle != null)
            {
                Drawer.DrawMarkerOnEntity(PlayerInfo.NearestVehicle.AbovePosition, Color.Blue);
            }
            
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