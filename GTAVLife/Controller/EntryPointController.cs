using System.Collections.Generic;
using GTA;
using GTAVLife.Helper;
using GTAVLife.GameData;
using GTAVLife.View;

namespace GTAVLife.Controller
{
    public class EntryPointController : SimpleSingletonBase<EntryPointController>, IController
    {
        public IView View => null;
        private Dictionary<string, Checkpoint> checkpoints;
        private Dictionary<string, Blip> blips;

        public void Process()
        {
            foreach (EntryPoint entryPoint in EntryPointList.Instance.EntryPoints)
            {
                switch (entryPoint.Status)
                {
                    case EntryPointStatus.Add:
                        switch (entryPoint.PointType)
                        {
                            case PointType.Vehicle:
                                checkpoints.Add(
                                    entryPoint.Name,
                                    MissionHelper.SetVehicleCheckpoint(
                                        entryPoint.Position,
                                        entryPoint.PointTo,
                                        entryPoint.Color
                                    )
                                );
                                blips.Add(
                                    entryPoint.Name,
                                    MissionHelper.SetBlip(
                                        entryPoint.Position,
                                        BlipSprite.CarShowroom,
                                        entryPoint.Name
                                    )
                                );
                                break;
                            case PointType.Player:
                            default:
                                checkpoints.Add(
                                    entryPoint.Name,
                                    MissionHelper.SetPlayerCheckpoint(
                                        entryPoint.Position,
                                        entryPoint.PointTo,
                                        entryPoint.Color
                                    )
                                );
                                blips.Add(
                                    entryPoint.Name,
                                    MissionHelper.SetBlip(
                                        entryPoint.Position,
                                        BlipSprite.CaptureAmericanFlag,
                                        entryPoint.Name
                                    )
                                );
                                break;
                        }
                        entryPoint.Status = EntryPointStatus.Enabled;
                        break;
                    case EntryPointStatus.Delete:
                        Checkpoint checkpoint;
                        Blip blip;
                        if (checkpoints.TryGetValue(entryPoint.Name, out checkpoint))
                        {
                            checkpoint.Delete();
                            checkpoints.Remove(entryPoint.Name);
                        }
                        if (blips.TryGetValue(entryPoint.Name, out blip))
                        {
                            blip.Delete();
                            blips.Remove(entryPoint.Name);
                        }
                        entryPoint.Status = EntryPointStatus.Disabled;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Show()
        {

        }

        private EntryPointController()
        {
            this.checkpoints = new Dictionary<string, Checkpoint>();
            this.blips = new Dictionary<string, Blip>();
        }
    }
}