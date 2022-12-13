using System.Collections.Generic;
using GTA.UI;
using GTAVLife.GameData;
using GTAVLife.Helper;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class MainLifeSubview : SimpleSingletonBase<MainLifeSubview>, IView
    {
        public NativeMenu Menu => menu;

        public List<NativeMenu> Submenus => null;

        public event ProcessHandler OnProcess;
        private NativeMenu menu;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"TrainTicket", "Train Ticket"},
            {"FAPCard", "FAP Card"},
        };

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
                case "FAPCard":
                    return Life.Instance.FAPCard.ToString();
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