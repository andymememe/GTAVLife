using System.Collections.Generic;
using GTAVLife.Helper;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class DebugView : SimpleSingletonBase<DebugView>, IView
    {
        public event ProcessHandler OnProcess;

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
                DebugVehicleSubview.Instance,
                DebugMissionSubview.Instance
            };

            this.menu = new NativeMenu("Debug", "View Debug Info");
            this.submenus = new List<NativeMenu>();

            foreach (IView view in views)
            {
                this.menu.AddSubMenu(view.Menu);
                this.submenus.Add(view.Menu);
                this.OnProcess += view.Process;
            }
        }
    }
}