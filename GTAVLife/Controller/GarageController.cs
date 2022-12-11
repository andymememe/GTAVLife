using GTAVLife.View;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class GarageController : SimpleSingletonBase<GarageController>, IController
    {
        private GarageView garageView;

        public IView View => garageView;

        public void Process()
        {
            garageView.Process();
        }

        public void Show()
        {
            garageView.Show();
        }

        private GarageController()
        {
            garageView = GarageView.Instance;
        }
    }
}