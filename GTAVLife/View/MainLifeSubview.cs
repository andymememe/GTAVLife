using System.Collections.Generic;
using GTA.UI;
using GTAVLife.GameData;
using GTAVLife.Helper;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class MainLifeSubview : SimpleSingletonBase<MainLifeSubview>, IView
    {
        public NativeMenu Menu
        {
            get
            {
                return menu;
            }
        }

        public List<NativeMenu> Submenus => throw new System.NotImplementedException();

        public event ProcessHandler OnProcess;
        private NativeMenu menu;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"TrainTicket", "Train Ticket"},
            {"TAPCard", "TAP Card"},
        };

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
                    menu.Items[i].AltTitle = result.ToString();
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
                case "TrainTicket":
                    return Life.Instance.HasTrainTicket.ToString();
                case "TAPCard":
                    return Life.Instance.HasTAPCard.ToString();
                default:
                    return "";
            }
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Living in LS", "Life");
            this.menu.TitleFont = Font.HouseScript;
            foreach (KeyValuePair<string, string> item in items)
            {
                NativeItem nativeItem = new NativeItem(item.Value);
                nativeItem.Tag = item.Key;
                menu.Add(nativeItem);
            }
        }

        private MainLifeSubview()
        {
            setupUI();
        }
    }
}