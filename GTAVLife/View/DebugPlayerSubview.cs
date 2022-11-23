using System;
using System.Collections.Generic;
using LemonUI.Menus;
using GTAVLife.Helper;
using GTAVLife.Controller;
using GTAVLife.GameData;

namespace GTAVLife.View
{
    public class DebugPlayerSubview : IView
    {
        private static DebugPlayerSubview instance = null;
        private NativeMenu menu;
        private DebugController controller;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"X", "X"},
            {"Y", "Y"},
            {"Z", "Z"},
            {"Health", "Health"},
            {"MaxHealth", "Max Health"},
            {"Armor", "Armor"},
            {"MaxArmor", "Max Armor"},
            {"Money", "Money"},
            {"Wanted", "Wanted"},
            {"Zone", "Zone"},
            {"NearestPedRelationshipGroup", "Nearest Ped Relationship Group"},
            {"NearestPedRelationship", "Nearest Ped Relationship"},
            {"IsNearestPedHuman", "Nearest Ped Human"},
            {"NearestVehicle", "Nearest Vehicle"},
            {"IsPlayerControlable", "Controlable"},
            {"IsPlayerInVehicle", "In Vehicle"},
        };

        public static DebugPlayerSubview Instance
        {
            get
            {
                return instance ?? new DebugPlayerSubview();
            }
        }

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

                if (rawTag != null) {
                    string tag = (string)rawTag;
                    string result = getResult(tag);
                    menu.Items[i].AltTitle = result.ToString();
                }
            }
        }

        public void SetController(IController controller)
        {
            this.controller = (DebugController)controller;
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
                    return PlayerInfo.Zone.ToString();
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
            this.menu = new NativeMenu("Player", "Show Player Data");
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
            instance = this;
        }
    }
}