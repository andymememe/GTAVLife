using GTA;
using GTAVLife.View;
using GTAVLife.GameData;
using GTAVLife.Helper;

namespace GTAVLife.Controller
{
    public class GarageController : SimpleSingletonBase<GarageController>, IController
    {
        private GarageView garageView;

        public IView View => garageView;

        public void Process()
        {

        }

        public void Show()
        {
            garageView.Show();
        }

        private GarageController()
        {
            garageView = GarageView.Instance;
        }

        private Vehicle setupVehicle(Vehicle vehicle, VehicleInfo vehicleInfo)
        {
            vehicle.Mods.WheelType = (VehicleWheelType) vehicleInfo.WheelType;
            vehicle.Mods.LicensePlate = vehicleInfo.Plate;
            vehicle.Mods.LicensePlateStyle = (LicensePlateStyle) vehicleInfo.LicensePlateStyle;
            vehicle.Mods.PrimaryColor = (VehicleColor) vehicleInfo.PrimaryColor;
            vehicle.Mods.SecondaryColor = (VehicleColor) vehicleInfo.SecondaryColor;
            vehicle.Mods.PearlescentColor = (VehicleColor) vehicleInfo.PearlescentColor;
            vehicle.Mods.RimColor = (VehicleColor) vehicleInfo.RimColor;
            vehicle.Mods.TrimColor = (VehicleColor) vehicleInfo.TrimColor;
            vehicle.Mods.WindowTint = (VehicleWindowTint) vehicleInfo.WindowTint;

            return vehicle;
        }

        private VehicleInfo genVehicleInfo(Vehicle vehicle, string nickname)
        {
            VehicleInfo vehicleInfo = new VehicleInfo(nickname);

            vehicleInfo.Hash = vehicle.Model.Hash;
            vehicleInfo.Name = vehicle.DisplayName;
            vehicleInfo.WheelType = (int)vehicle.Mods.WheelType;
            vehicleInfo.Plate = vehicle.Mods.LicensePlate;
            vehicleInfo.LicensePlateStyle = (int)vehicle.Mods.LicensePlateStyle;
            vehicleInfo.PrimaryColor = (int)vehicle.Mods.PrimaryColor;
            vehicleInfo.SecondaryColor = (int)vehicle.Mods.SecondaryColor;
            vehicleInfo.PearlescentColor = (int)vehicle.Mods.PearlescentColor;
            vehicleInfo.RimColor = (int)vehicle.Mods.RimColor;
            vehicleInfo.TrimColor = (int)vehicle.Mods.TrimColor;
            vehicleInfo.WindowTint = (int)vehicle.Mods.WindowTint;

            return vehicleInfo;
        }
    }
}