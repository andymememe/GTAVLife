using GTAVLife.View;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class MainController : SimpleSingletonBase<MainController>, IController
    {
        public IView View => MainView.Instance;

        public void Show()
        {
            View.Show();
        }

        public void Process()
        {
            View.Process();
        }
    }
}