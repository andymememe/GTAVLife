using GTAVLife.Helper;
using GTAVLife.View;

namespace GTAVLife.Controller
{
    public class MainController : IController
    {
        private static MainController instance = null;
        private MainView mainView;

        public static MainController Instance
        {
            get
            {
                return instance ?? new MainController();
            }
        }

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

        public void OnHide()
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
            mainView.SetController(this);
            instance = this;
        }
    }
}