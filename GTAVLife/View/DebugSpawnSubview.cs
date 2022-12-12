using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTAVLife.GameData;
using GTAVLife.Helper;
using LemonUI.Menus;

namespace GTAVLife.View
{
    public class DebugSpawnSubview : SimpleSingletonBase<DebugSpawnSubview>, IView
    {
        public NativeMenu Menu => menu;

        public List<NativeMenu> Submenus => null;

        public event ProcessHandler OnProcess;

        private NativeMenu menu;

        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"SpawnVehicle", "Spawn Vehicle"},
        };

        public void Process()
        {
            this.OnProcess?.Invoke();
        }

        public void Show()
        {
            this.Menu.Visible = true;
        }

        private void OnActivate(object sender, ItemActivatedArgs e)
        {
            string nickname;

            switch ((string) e.Item.Tag)
            {
                case "SpawnVehicle":
                    nickname = Game.GetUserInput(WindowTitle.EnterMessage20, "", 20);
                    Vector3 spawnPosition = PlayerInfo.Character.BelowPosition + (Vector3.WorldEast * DistanceUtils.ToMM(2000));
                    VehicleInfo vehicleInfo = new VehicleInfo(nickname, (int) VehicleHash.Sentinel, spawnPosition, PlayerInfo.Character.Heading);
                    while (!Life.Instance.AddOwnedVehicle(vehicleInfo))
                    {
                        nickname = Game.GetUserInput(WindowTitle.InvalidMessage, "", 20);
                        vehicleInfo.NickName = nickname;
                    }
                    break;
                default:
                    break;
            }
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Spawn", "Spawn Things");
            foreach (KeyValuePair<string, string> item in items)
            {
                NativeItem nativeItem = new NativeItem(item.Value);
                nativeItem.Tag = item.Key;
                menu.Add(nativeItem);
            }

            this.menu.ItemActivated += OnActivate;
        }

        private DebugSpawnSubview()
        {
            setupUI();
        }
    }
}