using System.Collections.Generic;
using GTA;
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
            List<VehicleInfo>.Enumerator enumerator = Life.Instance.GetOwnedVehicleEnumerator();
            VehicleInfo vehicleInfo;
            Vehicle vehicle;
            while (enumerator.MoveNext())
            {
                vehicleInfo = enumerator.Current;
                if (vehicleInfo.NeedSpawn)
                {
                    vehicle = VehicleSpawner.SpawnVehicle(vehicleInfo, false);
                    vehicleInfo.ImportVehicleInfo(ref vehicle);
                    vehicleInfo.NeedSpawn = false;
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
            enumerator.Dispose();
        }

        public void Show()
        {
            
        }
    }
}