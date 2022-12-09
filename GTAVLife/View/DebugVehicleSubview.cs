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
            if (PlayerInfo.Vehicle != null)
            {
                switch (tag)
                {
                    case "Name":
                        return PlayerInfo.Vehicle.DisplayName;
                    case "NameLocalized":
                        return PlayerInfo.Vehicle.LocalizedName;
                    case "Class":
                        return PlayerInfo.Vehicle.ClassDisplayName;
                    case "ClassLocalized":
                        return PlayerInfo.Vehicle.ClassLocalizedName;
                    case "Health":
                        return PlayerInfo.Vehicle.Health.ToString();
                    case "MaxHealth":
                        return PlayerInfo.Vehicle.MaxHealth.ToString();
                    case "EngineHealth":
                        return PlayerInfo.Vehicle.EngineHealth.ToString();
                    case "FuelLevel":
                        return PlayerInfo.Vehicle.FuelLevel.ToString();
                    case "OilLevel":
                        return PlayerInfo.Vehicle.OilLevel.ToString();
                    case "OilVolume":
                        return PlayerInfo.Vehicle.OilVolume.ToString();
                    case "PetrolTankVolume":
                        return PlayerInfo.Vehicle.HandlingData.PetrolTankVolume.ToString();
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