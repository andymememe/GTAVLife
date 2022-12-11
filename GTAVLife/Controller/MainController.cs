using GTAVLife.View;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class MainController : SimpleSingletonBase<MainController>, IController
    {
        private MainView mainView;

        public IView View => mainView;

        public void Show()
        {
            mainView.Show();
        }

        public void Process()
        {
            mainView.Process();
        }

        private MainController()
        {
            mainView = MainView.Instance;
        }
    }
}