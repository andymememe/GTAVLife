using GTAVLife.Helper;
using GTAVLife.View;
using GTAVLife.GameData;

namespace GTAVLife.Controller
{
    public class ProfileController : SimpleSingletonBase<ProfileController>, IController
    {
        public IView View => ProfileView.Instance;

        public void Process()
        {
            if (Life.Instance.Name != null && Environment.Instance.IsActivated && Environment.Instance.IsChangedSkinNeeded)
            {
                SkinChanger.Change(Life.Instance.Name);
                Environment.Instance.IsChangedSkinNeeded = false;
            }
            View.Process();
        }

        public void Show()
        {
            View.Show();
        }
    }
}