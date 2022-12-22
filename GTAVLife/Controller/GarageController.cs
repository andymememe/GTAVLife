using GTAVLife.View;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class GarageController : SimpleSingletonBase<GarageController>, IController
    {
        public IView View => GarageView.Instance;

        public void Process()
        {
            View.Process();
        }

        public void Show()
        {
            View.Show();
        }
    }
}