using System.Collections.Generic;
using GTAVLife.Helper;
using GTAVLife.Controller;

namespace GTAVLife
{
    public delegate void ProcessHandler();

    public class Router : SimpleSingletonBase<Router>
    {
        public event ProcessHandler OnProcess;
        private List<IController> controllers;

        public List<IController> Controllers
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
                default:
                    break;
            }

        }

        private Router()
        {
            this.controllers = new List<IController> {
                MainController.Instance,
                DebugController.Instance,
                TrainController.Instance,
                EntryPointController.Instance,
                InventoryController.Instance,
                CarShopController.Instance,
                CarSpawnerController.Instance,
            };

            foreach (IController controller in controllers)
            {
                this.OnProcess += controller.Process;
            }
        }
    }
}