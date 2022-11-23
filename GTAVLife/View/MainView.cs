using System.Collections.Generic;
using GTA.UI;
using LemonUI.Menus;
using GTAVLife.Helper;
using GTAVLife.Controller;

namespace GTAVLife.View
{
    public class MainView : SimpleSingletonBase<MainView>, IView
    {
        public event ControllerHandler OnSetController;
        public event ProcessHandler OnProcess;
        private NativeMenu menu;
        private List<NativeMenu> submenus;
        private MainController controller;

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
            this.controller = (MainController)controller;
        }

        public void Show()
        {
            this.menu.Visible = true;
        }

        public void Hide()
        {
            this.menu.Visible = false;
        }

        public void Process()
        {

        }

        public void OnItemActivated(object sender, ItemActivatedArgs e)
        {
            if (e.Item.Tag != null)
            {
                string tag = e.Item.Tag.ToString();
                Screen.ShowSubtitle(tag);
                switch (tag)
                {
                    default:
                        break;
                }
            }
        }

        private MainView()
        {
            IView[] views = {
                MainLifeSubview.Instance
            };

            this.menu = new NativeMenu("Living in LS", "Main");
            this.menu.TitleFont = Font.HouseScript;

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