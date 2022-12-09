using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.UI;
using GTAVLife.Helper;
using GTAVLife.GameData;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class DebugMissionSubview : SimpleSingletonBase<DebugMissionSubview>, IView
    {
        public NativeMenu Menu => menu;

        public List<NativeMenu> Submenus => throw new System.NotImplementedException();

        public event ProcessHandler OnProcess;

        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"SetPlayerCheckpoint", "Set Player Checkpoint"},
            {"SetVehicleCheckpoint", "Set Vehicle Checkpoint"},
            {"RemoveAll", "Remove All"},
            {"GetPlayerPosition", "Get Player Position"},
            {"GetGroundPosition", "Get Ground Position"},
            {"Note", "Note"},
        };

        private NativeMenu menu;

        public void Process()
        {
            OnProcess?.Invoke();
        }

        public void Show()
        {
            this.menu.Visible = true;
        }

        private void OnActivate(object sender, ItemActivatedArgs e)
        {
            switch((string)e.Item.Tag)
            {
                case "SetPlayerCheckpoint":
                    EntryPointList.Instance.AddTestPlayerEntryPoint("Test Player", PlayerInfo.Character.BelowPosition, PlayerInfo.Character.BelowPosition);
                    break;
                case "SetVehicleCheckpoint":
                    EntryPointList.Instance.AddTestVehicleEntryPoint("Test Vehicle", PlayerInfo.Character.BelowPosition, PlayerInfo.Character.BelowPosition);
                    break;
                case "RemoveAll":
                    EntryPointList.Instance.RemoveAllEntryPoint();
                    break;
                case "GetPlayerPosition":
                    string playerTag = Game.GetUserInput(WindowTitle.EnterMessage60, "", 60);
                    Logger.GetInstance().Raw(string.Format("{0}, {1}, {2}, {3}, {4}, P\n", playerTag, PlayerInfo.Position.X, PlayerInfo.Position.Y, PlayerInfo.Position.Z, PlayerInfo.Character.Heading));
                    break;
                case "GetGroundPosition":
                    string groundTag = Game.GetUserInput(WindowTitle.EnterMessage60, "", 60);
                    Logger.GetInstance().Raw(string.Format("{0}, {1}, {2}, {3}, {4}, G\n", groundTag, PlayerInfo.Character.BelowPosition.X, PlayerInfo.Character.BelowPosition.Y, PlayerInfo.Character.BelowPosition.Z, PlayerInfo.Character.Heading));
                    break;
                case "Note":
                    string note = Game.GetUserInput(WindowTitle.EnterMessage60, "", 60);
                    Logger.GetInstance().Info(note);
                    break;
                default:
                    break;
            }
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Debug", "Mission");
            foreach (KeyValuePair<string, string> item in items)
            {
                NativeItem nativeItem = new NativeItem(item.Value);
                nativeItem.Tag = item.Key;
                menu.Add(nativeItem);
            }

            this.menu.ItemActivated += OnActivate;
        }

        private DebugMissionSubview()
        {
            setupUI();
        }
    }
}