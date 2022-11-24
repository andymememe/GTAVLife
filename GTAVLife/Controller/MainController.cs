using GTAVLife.View;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class MainController : SimpleSingletonBase<MainController>, IController
    {
        private MainView mainView;

        public IView View
        {
            get
            {
                return mainView;
            }
        }

        public void Show()
        {
            mainView.Show();
        }

        public void Hide()
        {
            mainView.Hide();
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