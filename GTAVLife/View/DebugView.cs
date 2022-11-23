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

        private void setupUI()
        {
            this.menu = new NativeMenu("Debug", "View Debug Info");
            this.submenus = new List<NativeMenu>();

            // Game Submenu
            this.menu.AddSubMenu(DebugGameSubview.Instance.Menu);
            this.submenus.Add(DebugGameSubview.Instance.Menu);

            // Player Submenu
            this.menu.AddSubMenu(DebugPlayerSubview.Instance.Menu);
            this.submenus.Add(DebugPlayerSubview.Instance.Menu);

            // Vehicle Submenu
            this.menu.AddSubMenu(DebugVehicleSubview.Instance.Menu);
            this.submenus.Add(DebugVehicleSubview.Instance.Menu);
        }

        private DebugView()
        {
            this.OnSetController += DebugGameSubview.Instance.SetController;
            this.OnSetController += DebugPlayerSubview.Instance.SetController;
            this.OnSetController += DebugVehicleSubview.Instance.SetController;

            this.OnProcess += DebugGameSubview.Instance.Process;
            this.OnProcess += DebugPlayerSubview.Instance.Process;
            this.OnProcess += DebugVehicleSubview.Instance.Process;

            setupUI();
        }
    }
}