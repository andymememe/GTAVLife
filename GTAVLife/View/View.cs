using System.Collections.Generic;
using LemonUI.Menus;
using GTAVLife.Controller;

namespace GTAVLife.View
{
    public delegate void ControllerHandler(IController controller);
    public delegate void ProcessHandler();

    public interface IView
    {
        event ControllerHandler OnSetController;
        event ProcessHandler OnProcess;
        NativeMenu Menu { get; }
        List<NativeMenu> Submenus { get; }
        void SetController(IController controller);
        void Show();
        void Hide();
        void Process();
    }
}