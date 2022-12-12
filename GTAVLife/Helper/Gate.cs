using GTA;
using GTA.Native;

namespace GTAVLife.Helper
{
    public class Gate
    {
        private const int BARRIER01 = -1184516519;
        private const int BARRIER02 = 1230099731;
        private const float OPEN = 1f;
        private const float CLOSE = -0.1f;
        private const float OPEN_THRESHOLD = 5000f;
        private const float CLOSE_THRESHOLD = 12000f;
        private const float NEARBY_RADIUS = 24000f;
        private static Model[] barrierIDs = { BARRIER01, BARRIER02 };

        public static void ControlGate()
        {
            Prop[] barrierGates = World.GetNearbyProps(Game.Player.Character.Position, DistanceUtils.ToGameWorldDistance(NEARBY_RADIUS), barrierIDs);
            foreach (var gate in barrierGates)
            {
                if (Game.Player.Character.IsInVehicle())
                {
                    float gateDist = World.GetDistance(Game.Player.Character.RightPosition, gate.RightPosition);

                    if (gateDist <= DistanceUtils.ToGameWorldDistance(OPEN_THRESHOLD))
                    {
                        if (gate.Model.GetHashCode() == BARRIER01)
                        {
                            Function.Call(
                                Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, BARRIER01,
                                gate.Position.X, gate.Position.Y, gate.Position.Z,
                                false, OPEN
                            );
                        }
                        else if (gate.Model.GetHashCode() == BARRIER02)
                        {
                            Function.Call(
                                Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, BARRIER02,
                                gate.Position.X, gate.Position.Y, gate.Position.Z,
                                false, OPEN
                            );
                        }
                    }
                    else if (gateDist > DistanceUtils.ToGameWorldDistance(CLOSE_THRESHOLD))
                    {
                        if (gate.Model.GetHashCode() == BARRIER01)
                        {
                            Function.Call(
                                Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, BARRIER01,
                                gate.Position.X, gate.Position.Y, gate.Position.Z,
                                false, CLOSE
                            );
                        }
                        else if (gate.Model.GetHashCode() == BARRIER02)
                        {
                            Function.Call(
                                Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, BARRIER02,
                                gate.Position.X, gate.Position.Y, gate.Position.Z,
                                false, CLOSE
                            );
                        }
                    }
                }
            }
        }
    }
}