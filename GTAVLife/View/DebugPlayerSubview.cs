using System;
using System.Collections.Generic;
using LemonUI.Menus;
using GTAVLife.Helper;
using GTAVLife.Controller;
using GTAVLife.GameData;

namespace GTAVLife.View
{
    public class DebugPlayerSubview : SimpleSingletonBase<DebugPlayerSubview>, IView
    {
        public event ControllerHandler OnSetController;
        public event ProcessHandler OnProcess;
        private NativeMenu menu;
        private DebugController controller;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"X", "X"},
            {"Y", "Y"},
            {"Z", "Z"},
            {"Name", "Name"},
            {"Health", "Health"},
            {"MaxHealth", "Max Health"},
            {"Armor", "Armor"},
            {"MaxArmor", "Max Armor"},
            {"Money", "Money"},
            {"Wanted", "Wanted"},
            {"Zone", "Zone"},
            {"LocalizedZone", "Zone"},
            {"NearestPedName", "N. Ped"},
            {"NearestPedRelationshipGroup", "N. Ped R.G."},
            {"NearestPedRelationship", "N. Ped R."},
            {"IsNearestPedHuman", "N. Ped Human"},
            {"NearestVehicle", "N. Vehicle"},
            {"NearestVehicleLocalized", "N. Vehicle"},
            {"IsPlayerControlable", "Controlable"},
            {"IsPlayerInVehicle", "In Vehicle"},
        };

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
                    menu.Items[i].AltTitle = result.ToString();
                }
            }
            OnProcess?.Invoke();

        }

        public void SetController(IController controller)
        {
            this.controller = (DebugController)controller;
            OnSetController?.Invoke(controller);
        }

        public void Show()
        {
            this.menu.Visible = true;
        }

        private string getResult(string tag)
        {
            switch (tag)
            {
                case "X":
                    return PlayerInfo.Position.X.ToString();
                case "Y":
                    return PlayerInfo.Position.Y.ToString();
                case "Z":
                    return PlayerInfo.Position.Z.ToString();
                case "Name":
                    return PlayerInfo.Name;
                case "Health":
                    return PlayerInfo.Character.Health.ToString();
                case "MaxHealth":
                    return PlayerInfo.Character.MaxHealth.ToString();
                case "Armor":
                    return PlayerInfo.Character.Armor.ToString();
                case "MaxArmor":
                    return PlayerInfo.Player.MaxArmor.ToString();
                case "Money":
                    return PlayerInfo.Player.Money.ToString();
                case "Wanted":
                    return PlayerInfo.Player.WantedLevel.ToString();
                case "Zone":
                    return PlayerInfo.Zone;
                case "LocalizedZone":
                    return PlayerInfo.LocalizedZone;
                case "NearestPedName":
                    return PlayerInfo.NearestPedName;
                case "NearestPedRelationshipGroup":
                    if (PlayerInfo.NearestPed == null)
                    {
                        return "";
                    }

                    string groupName = Enum.GetName(typeof(RelationshipGroupHash), (uint)PlayerInfo.NearestPed.RelationshipGroup.GetHashCode());

                    return groupName ?? "";
                case "NearestPedRelationship":
                    if (PlayerInfo.NearestPed == null)
                    {
                        return "";
                    }
                    return PlayerInfo.NearestPed.GetRelationshipWithPed(PlayerInfo.Character).ToString();
                case "NearestVehicle":
                    if (PlayerInfo.NearestVehicle == null)
                    {
                        return "";
                    }
                    return PlayerInfo.NearestVehicle.DisplayName;
                case "NearestVehicleLocalized":
                    if (PlayerInfo.NearestVehicle == null)
                    {
                        return "";
                    }
                    return PlayerInfo.NearestVehicle.LocalizedName;
                case "IsPlayerControlable":
                    return PlayerInfo.Player.CanControlCharacter.ToString();
                case "IsPlayerInVehicle":
                    return PlayerInfo.Character.IsInVehicle().ToString();
                case "IsNearestPedHuman":
                    if (PlayerInfo.NearestPed == null)
                    {
                        return "";
                    }
                    return PlayerInfo.NearestPed.IsHuman.ToString();
                default:
                    return "";
            }
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Debug", "Player Stats");
            foreach (KeyValuePair<string, string> item in items)
            {
                NativeItem nativeItem = new NativeItem(item.Value);
                nativeItem.Tag = item.Key;
                menu.Add(nativeItem);
            }
        }

        private DebugPlayerSubview()
        {
            setupUI();
        }
    }
}