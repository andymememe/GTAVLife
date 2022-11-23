using System.Collections.Generic;
using System;
using LemonUI.Menus;
using GTAVLife.Controller;
using GTAVLife.GameData;

namespace GTAVLife.View
{
    public class DebugVehicleSubview : IView
    {
        private static DebugVehicleSubview instance = null;
        private NativeMenu menu;
        private DebugController controller;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"Name", "Name"},
            {"Class", "Class"},
            {"Health", "Health"},
            {"MaxHealth", "Max Health"},
            {"EngineHealth", "Engine Health"},
            {"Speed", "Speed"},
            {"Doors", "Doors"},
            {"PassengerCapacity", "Passenger Capacity"},
            {"PassengerCount", "Passenger Count"},
        };

        public static DebugVehicleSubview Instance
        {
            get
            {
                return instance ?? new DebugVehicleSubview();
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
            if (PlayerInfo.Vehicle != null)
            {
                switch (tag)
                {
                    case "Name":
                        return PlayerInfo.Vehicle.LocalizedName;
                    case "Class":
                        return PlayerInfo.Vehicle.ClassLocalizedName;
                    case "Health":
                        return PlayerInfo.Vehicle.Health.ToString();
                    case "MaxHealth":
                        return PlayerInfo.Vehicle.MaxHealth.ToString();
                    case "EngineHealth":
                        return PlayerInfo.Vehicle.EngineHealth.ToString();
                    case "Speed":
                        return Math.Round((PlayerInfo.Vehicle.Speed * 3.6)).ToString();
                    case "Doors":
                        return PlayerInfo.Vehicle.Doors.ToArray().Length.ToString();
                    case "PassengerCapacity":
                        return PlayerInfo.Vehicle.PassengerCapacity.ToString();
                    case "PassengerCount":
                        return PlayerInfo.Vehicle.PassengerCount.ToString();
                    default:
                        return "";
                }
            }
            return "";
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Vehicle", "Show Current Vehicle Data");
            foreach (KeyValuePair<string, string> item in items)
            {
                NativeItem nativeItem = new NativeItem(item.Value);
                nativeItem.Tag = item.Key;
                menu.Add(nativeItem);
            }
        }

        private DebugVehicleSubview()
        {
            setupUI();
            instance = this;
        }
    }
}