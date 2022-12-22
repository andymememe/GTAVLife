using System.Collections.Generic;
using GTA.UI;
using LemonUI.Menus;
using GTAVLife.GameData;
using GTAVLife.Helper;

namespace GTAVLife.View
{
    public class ProfileView : SimpleSingletonBase<ProfileView>, IView
    {
        public event ProcessHandler OnProcess;
        private NativeMenu menu;

        public NativeMenu Menu => menu;

        public List<NativeMenu> Submenus => null;

        public void Show()
        {
            this.menu.Visible = true;
        }

        public void Process()
        {
            OnProcess?.Invoke();
        }

        public void OnItemActivated(object sender, ItemActivatedArgs e)
        {
            if (e.Item.Tag != null)
            {
                string tag = e.Item.Tag.ToString();
                switch (tag)
                {
                    case "Characters":
                        NativeListItem<string> list = (NativeListItem<string>)e.Item;
                        Life.Instance.Name = list.SelectedItem;
                        Environment.Instance.IsJustInitialized = true;
                        Environment.Instance.IsChangedSkinNeeded = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Living in LS", "Select Character");
            this.menu.TitleFont = Font.ChaletComprimeCologne;

            NativeListItem<string> charactersList = new NativeListItem<string>("Characters");
            charactersList.Tag = "Characters";

            foreach (string item in MPFreemodeModels.Instance.Models.Keys)
            {
                charactersList.Add(item);
            }

            this.menu.Add(charactersList);
            this.menu.ItemActivated += OnItemActivated;
        }

        private ProfileView()
        {
            setupUI();
        }
    }
}