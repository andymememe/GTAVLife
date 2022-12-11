using System.Collections.Generic;
using GTAVLife.Helper;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class GarageView : SimpleSingletonBase<GarageView>, IView
    {
        public NativeMenu Menu => menu;

        public List<NativeMenu> Submenus => null;

        public event ProcessHandler OnProcess;

        private NativeMenu menu;

        public void Process()
        {
            OnProcess?.Invoke();
        }

        public void Show()
        {
            
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Garage", "Fake Address");
        }

        private GarageView()
        {
            setupUI();
        }
    }
}