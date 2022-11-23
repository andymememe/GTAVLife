using System.Collections.Generic;
using GTAVLife.Helper;
using GTAVLife.Controller;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class DebugView : SimpleSingletonBase<DebugView>, IView
    {
        public event ControllerHandler OnSetController;
        public event ProcessHandler OnProcess;
        private DebugController controller;

        // UI Related
        private NativeMenu menu;
        private List<NativeMenu> submenus;

        public NativeMenu Menu
        {
            get
            {
                return menu;
            }

        }

        public List<NativeMenu> Submenus
        {
            get
            {
                return submenus;
            }
        }

        public void SetController(IController controller)
        {
            this.controller = (DebugController)controller;
            OnSetController?.Invoke(controller);
        }

        public void Hide()
        {
            this.menu.Visible = false;
        }

        public void Show()
        {
            this.menu.Visible = true;
        }

        public void Process()
        {
            OnProcess?.Invoke();
        }

        private DebugView()
        {
            IView[] views = {
                DebugGameSubview.Instance,
                DebugPlayerSubview.Instance,
                DebugVehicleSubview.Instance
            };

            this.menu = new NativeMenu("Debug", "View Debug Info");
            this.submenus = new List<NativeMenu>();

            foreach (IView view in views)
            {
                this.menu.AddSubMenu(view.Menu);
                this.submenus.Add(view.Menu);
                this.OnSetController += view.SetController;
                this.OnProcess += view.Process;
            }
        }
    }
}