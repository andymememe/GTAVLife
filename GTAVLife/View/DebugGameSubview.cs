using System.Collections.Generic;
using LemonUI.Menus;
using GTAVLife.Helper;
using GTAVLife.GameData;

namespace GTAVLife.View
{
    public class DebugGameSubview : SimpleSingletonBase<DebugGameSubview>, IView
    {
        public event ProcessHandler OnProcess;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"IsMissionActive", "Mission Active"},
            {"IsRandomEventActive", "Random Event Active"},
            {"IsCutsceneActive", "Cutscene Active"},
            {"IsLoading", "Loading"},
        };

        // UI
        private NativeMenu menu;

        public NativeMenu Menu
        {
            get
            {
                return menu;
            }
        }

        public List<NativeMenu> Submenus => throw new System.NotImplementedException();

        public void Hide()
        {
            this.menu.Visible = false;
        }

        public void Process()
        {
            for (int i = 0; i < menu.Items.Count; i++)
            {
                object rawTag = menu.Items[i].Tag;

                if (rawTag != null)
                {
                    string tag = (string)rawTag;
                    string result = getResult(tag);
                    menu.Items[i].AltTitle = result;
                }
            }
            OnProcess?.Invoke();
        }

        public void Show()
        {
            this.menu.Visible = true;
        }

        private string getResult(string tag)
        {
            switch (tag)
            {
                case "IsMissionActive":
                    return Enviroment.IsMissionActive.ToString();
                case "IsRandomEventActive":
                    return Enviroment.IsRandomEventActive.ToString();
                case "IsCutsceneActive":
                    return Enviroment.IsCutsceneActive.ToString();
                case "IsLoading":
                    return Enviroment.IsLoading.ToString();
                default:
                    return "";
            }
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Debug", "Game Status");
            foreach (KeyValuePair<string, string> item in items)
            {
                NativeItem nativeItem = new NativeItem(item.Value);
                nativeItem.Tag = item.Key;
                menu.Add(nativeItem);
            }
        }

        private DebugGameSubview()
        {
            setupUI();
        }
    }
}