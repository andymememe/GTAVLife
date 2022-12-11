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
                        if (entryPoint.ForVehicle)
                        {
                            entryPoint.Checkpoint = MissionHelper.SetVehicleCheckpoint(
                                entryPoint.Position,
                                entryPoint.PointTo,
                                entryPoint.Color
                            );
                        }
                        else
                        {
                            entryPoint.Checkpoint = MissionHelper.SetPlayerCheckpoint(
                                entryPoint.Position,
                                entryPoint.PointTo,
                                entryPoint.Color
                            );
                        }

                        entryPoint.Blip = MissionHelper.SetBlip(
                            entryPoint.Position,
                            entryPoint.BlipSpirte,
                            entryPoint.Name
                        );
                        entryPoint.Status = EntryPointStatus.Enabled;
                        break;
                    case EntryPointStatus.Delete:
                        entryPoint.Blip.Delete();
                        entryPoint.Blip = null;
                        
                        entryPoint.Checkpoint.Delete();
                        entryPoint.Checkpoint = null;
                        
                        entryPoint.Status = EntryPointStatus.Disabled;
                        break;
                    case EntryPointStatus.Enabled:
                        if (entryPoint.ForVehicle)
                        {
                            if (PlayerInfo.Character.IsInVehicle() && Distance.IsInRange(entryPoint.Position, PlayerInfo.CurrentVehicle.Position, 6000))
                            {
                                Life.Instance.CurrentEntryPointIndex = index;
                            }
                        }
                        else
                        {
                            if (Distance.IsInRange(entryPoint.Position, PlayerInfo.Character.Position, 1000))
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