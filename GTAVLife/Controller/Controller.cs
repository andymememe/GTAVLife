using GTAVLife.View;

namespace GTAVLife.Controller
{
    public interface IController
    {
        IView View { get; }
        void Show();
        void OnHide();
        void Process();
    }
}