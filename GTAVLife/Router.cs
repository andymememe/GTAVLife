using GTAVLife.Controller;

namespace GTAVLife
{
    public delegate void HideHandler();
    public delegate void ProcessHandler();

    public class Router
    {
        public event HideHandler OnHide;
        public event ProcessHandler OnProcess;
        private static Router instance = null;
        private IController[] controllers;

        public static Router Instance
        {
            get {
                return instance ?? new Router();
            }
        }

        public IController[] Controllers
        {
            get
            {
                return controllers;
            }
        }

        public void Process()
        {
            OnProcess?.Invoke();
        }

        public void Route(string name)
        {
            switch (name)
            {
                case "main":
                    MainController.Instance.Show();
                    break;
                case "debug":
                    DebugController.Instance.Show();
                    break;
                case "hide":
                    OnHide?.Invoke();
                    break;
                default:
                    break;
            }

        }

        private Router()
        {
            this.OnHide += MainController.Instance.OnHide;
            this.OnHide += DebugController.Instance.OnHide;

            this.OnProcess += MainController.Instance.Process;
            this.OnProcess += DebugController.Instance.Process;

            this.controllers = new IController[] {
                MainController.Instance,
                DebugController.Instance,
            };

            instance = this;
        }
    }
}