using System.Collections.Generic;
using GTAVLife.Helper;
using GTAVLife.GameData;
using GTAVLife.View;

namespace GTAVLife.Controller
{
    public class EntryPointController : SimpleSingletonBase<EntryPointController>, IController
    {
        public IView View => null;

        public void Process()
        {
            Life.Instance.CurrentEntryPointIndex = -1;
            EntryPoint entryPoint;
            int index = 0;

            for (int i = 0; i < EntryPointList.Instance.EntryPoints.Count; i++)
            {
                entryPoint = EntryPointList.Instance.EntryPoints[i];
                switch (entryPoint.Status)
                {
                    case EntryPointStatus.Add:
                        setupIndicator(ref entryPoint);
                        entryPoint.Status = EntryPointStatus.Enabled;
                        break;
                    case EntryPointStatus.Delete:
                        deleteIdicator(ref entryPoint);
                        entryPoint.Status = EntryPointStatus.Disabled;
                        break;
                    case EntryPointStatus.Enabled:
                        if (entryPoint.EntryType == EntryType.Vehicle)
                        {
                            if (PlayerInfo.Character.IsInVehicle() && DistanceUtils.IsInRange(entryPoint.Position, PlayerInfo.CurrentVehicle.BelowPosition, 1500))
                            {
                                Life.Instance.CurrentEntryPointIndex = index;
                            }
                        }
                        else if (entryPoint.EntryType == EntryType.TowedVehicle)
                        {
                            if (PlayerInfo.Character.IsInVehicle() &&
                                PlayerInfo.CurrentVehicle.TowedVehicle != null &&
                                DistanceUtils.IsInRange(entryPoint.Position, PlayerInfo.CurrentVehicle.TowedVehicle.BelowPosition, 1500))
                            {
                                Life.Instance.CurrentEntryPointIndex = index;
                            }
                        }
                        else
                        {
                            if (!PlayerInfo.Character.IsInVehicle() && DistanceUtils.IsInRange(entryPoint.Position, PlayerInfo.Character.BelowPosition, 250))
                            {
                                Life.Instance.CurrentEntryPointIndex = index;
                            }
                        }
                        break;
                    default:
                        break;
                }

                index++;
            }
        }

        private void setupIndicator(ref EntryPoint entryPoint)
        {
            if (entryPoint.EntryType == EntryType.Vehicle || entryPoint.EntryType == EntryType.TowedVehicle)
            {
                entryPoint.Checkpoint = IndicatorHelper.SetVehicleCheckpoint(
                    entryPoint.Position,
                    entryPoint.PointTo,
                    entryPoint.Color
                );
            }
            else
            {
                entryPoint.Checkpoint = IndicatorHelper.SetPlayerCheckpoint(
                    entryPoint.Position,
                    entryPoint.PointTo,
                    entryPoint.Color
                );
            }

            entryPoint.Blip = IndicatorHelper.SetBlip(
                entryPoint.Position,
                entryPoint.BlipSpirte,
                entryPoint.Name
            );
        }

        private void deleteIdicator(ref EntryPoint entryPoint)
        {
            if (entryPoint.Blip != null && entryPoint.Blip.Exists())
            {
                entryPoint.Blip.Delete();
                entryPoint.Blip = null;
            }

            if (entryPoint.Checkpoint != null && entryPoint.Checkpoint.Exists())
            {
                entryPoint.Checkpoint.Delete();
                entryPoint.Checkpoint = null;
            }
        }

        public void Show()
        {

        }
    }
}