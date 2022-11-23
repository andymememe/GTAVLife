using System.Collections.Generic;
using GTA.UI;
using LemonUI.Menus;
using GTAVLife.Helper;
using GTAVLife.Controller;

namespace GTAVLife.View
{
    public class MainView : SimpleSingletonBase<MainView>, IView
    {
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

        private void setupUI()
        {
            this.menu = new NativeMenu("Life", "Living in LS");

            this.menu.Add(new NativeItem("Title", "Description", ">>>"));
            this.menu.Add(new NativeItem("1", "111", ">>>"));
            this.menu.Add(new NativeItem("2", "222", ">>>"));
            this.menu.Add(new NativeItem("3", "333", ">>>"));
            this.submenus = new List<NativeMenu>();

            this.menu.ItemActivated += OnItemActivated;
        }

        private MainView()
        {
            setupUI();
        }
    }
}