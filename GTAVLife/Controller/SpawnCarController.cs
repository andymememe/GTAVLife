using System.Collections.Generic;
using GTA;
using GTA.UI;
using GTAVLife.Helper;
using GTAVLife.View;
using GTAVLife.GameData;

namespace GTAVLife.Controller
{
    public class SpawnCarController : SimpleSingletonBase<SpawnCarController>, IController
    {
        public IView View => null;

        public void Process()
        {
            VehicleInfo vehicleInfo;
            Vehicle vehicle;
            for (int i = 0; i < Life.Instance.OwnedVehicles.Count; i++)
            {
                vehicleInfo = Life.Instance.OwnedVehicles[i];
                if (vehicleInfo.NeedSpawn)
                {
                    vehicleInfo.NeedSpawn = false;
                    vehicle = VehicleSpawner.SpawnVehicle(vehicleInfo, false);
                    vehicleInfo.ImportVehicleInfo(ref vehicle);
                    vehicleInfo.Location = VehicleInfo.OUTSIDE;
                }
                else
                {
                    if (vehicleInfo.Location == VehicleInfo.OUTSIDE && vehicleInfo.IsVehicleDeleted())
                    {
                        vehicleInfo.Location = VehicleInfo.INPOUDING;
                    }
                }
            }
        }

        public void Show()
        {

        }
    }
}