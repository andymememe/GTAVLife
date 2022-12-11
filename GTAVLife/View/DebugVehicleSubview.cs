using System.Collections.Generic;
using System;
using LemonUI.Menus;
using GTAVLife.Helper;
using GTAVLife.GameData;

namespace GTAVLife.View
{
    public class DebugVehicleSubview : SimpleSingletonBase<DebugVehicleSubview>, IView
    {
        public event ProcessHandler OnProcess;
        private NativeMenu menu;
        private Dictionary<string, string> items = new Dictionary<string, string>
        {
            {"Name", "Name"},
            {"NameLocalized", "Name"},
            {"Class", "Class"},
            {"ClassLocalized", "Class"},
            {"Health", "Health"},
            {"MaxHealth", "Max Health"},
            {"EngineHealth", "Engine Health"},
            {"FuelLevel", "Fuel Level"},
            {"OilLevel", "Oil Level"},
            {"OilVolume", "Oil Volume"},
            {"PetrolTankVolume", "Petrol Tank Volume"},
            {"Speed", "Speed"},
            {"Doors", "Doors"},
            {"PassengerCapacity", "Passenger Capacity"},
            {"PassengerCount", "Passenger Count"},
        };

        public NativeMenu Menu => menu;

        public List<NativeMenu> Submenus => null;

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
            if (PlayerInfo.CurrentVehicle != null)
            {
                switch (tag)
                {
                    case "Name":
                        return PlayerInfo.CurrentVehicle.DisplayName;
                    case "NameLocalized":
                        return PlayerInfo.CurrentVehicle.LocalizedName;
                    case "Class":
                        return PlayerInfo.CurrentVehicle.ClassDisplayName;
                    case "ClassLocalized":
                        return PlayerInfo.CurrentVehicle.ClassLocalizedName;
                    case "Health":
                        return PlayerInfo.CurrentVehicle.Health.ToString();
                    case "MaxHealth":
                        return PlayerInfo.CurrentVehicle.MaxHealth.ToString();
                    case "EngineHealth":
                        return PlayerInfo.CurrentVehicle.EngineHealth.ToString();
                    case "FuelLevel":
                        return PlayerInfo.CurrentVehicle.FuelLevel.ToString();
                    case "OilLevel":
                        return PlayerInfo.CurrentVehicle.OilLevel.ToString();
                    case "OilVolume":
                        return PlayerInfo.CurrentVehicle.OilVolume.ToString();
                    case "PetrolTankVolume":
                        return PlayerInfo.CurrentVehicle.HandlingData.PetrolTankVolume.ToString();
                    case "Speed":
                        return Math.Round((PlayerInfo.CurrentVehicle.Speed * 3.6)).ToString();
                    case "Doors":
                        return PlayerInfo.CurrentVehicle.Doors.ToArray().Length.ToString();
                    case "PassengerCapacity":
                        return PlayerInfo.CurrentVehicle.PassengerCapacity.ToString();
                    case "PassengerCount":
                        return PlayerInfo.CurrentVehicle.PassengerCount.ToString();
                    default:
                        return "";
                }
            }
            return "";
        }

        private void setupUI()
        {
            this.menu = new NativeMenu("Debug", "Current Vehicle Stats");
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
        }
    }
}