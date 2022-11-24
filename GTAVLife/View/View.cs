using System.Collections.Generic;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public delegate void ProcessHandler();

    public interface IView
    {
        event ProcessHandler OnProcess;
        NativeMenu Menu { get; }
        List<NativeMenu> Submenus { get; }
        void Show();
        void Process();
    }
}