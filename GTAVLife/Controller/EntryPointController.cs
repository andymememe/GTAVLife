using GTA;
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

            int index = 0;
            foreach (EntryPoint entryPoint in EntryPointList.Instance.EntryPoints)
            {
                switch (entryPoint.Status)
                {
                    case EntryPointStatus.Add:
                        if (entryPoint.EntryType == EntryType.Vehicle || entryPoint.EntryType == EntryType.TowedVehicle)
                        {
                            entryPoint.Checkpoint = EntryPointHelper.SetVehicleCheckpoint(
                                entryPoint.Position,
                                entryPoint.PointTo,
                                entryPoint.Color
                            );
                        }
                        else
                        {
                            entryPoint.Checkpoint = EntryPointHelper.SetPlayerCheckpoint(
                                entryPoint.Position,
                                entryPoint.PointTo,
                                entryPoint.Color
                            );
                        }

                        entryPoint.Blip = EntryPointHelper.SetBlip(
                            entryPoint.Position,
                            entryPoint.BlipSpirte,
                            entryPoint.Name
                        );
                        entryPoint.Status = EntryPointStatus.Enabled;
                        break;
                    case EntryPointStatus.Delete:
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

                        entryPoint.Status = EntryPointStatus.Disabled;
                        break;
                    case EntryPointStatus.Enabled:
                        if (entryPoint.EntryType == EntryType.Vehicle)
                        {
                            if (PlayerInfo.Character.IsInVehicle() && Distance.IsInRange(entryPoint.Position, PlayerInfo.CurrentVehicle.BelowPosition, 1500))
                            {
                                Life.Instance.CurrentEntryPointIndex = index;
                            }
                        }
                        else if (entryPoint.EntryType == EntryType.TowedVehicle)
                        {
                            if (PlayerInfo.Character.IsInVehicle() &&
                                PlayerInfo.CurrentVehicle.TowedVehicle != null &&
                                Distance.IsInRange(entryPoint.Position, PlayerInfo.CurrentVehicle.TowedVehicle.BelowPosition, 1500))
                            {
                                Life.Instance.CurrentEntryPointIndex = index;
                            }
                        }
                        else
                        {
                            if (!PlayerInfo.Character.IsInVehicle() && Distance.IsInRange(entryPoint.Position, PlayerInfo.Character.BelowPosition, 250))
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

        public void Show()
        {

        }
    }
}