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
        private const float OPEN_THRESHOLD = 6f;
        private const float CLOSE_THRESHOLD = 15f;
        private static Model[] barrierIDs = { BARRIER01, BARRIER02 };

        public static void ControlGate()
        {
            Prop[] barrierGates = World.GetNearbyProps(Game.Player.Character.Position, 30f, barrierIDs);
            foreach (var gate in barrierGates)
            {
                if (Game.Player.Character.IsInVehicle())
                {
                    float gateDist = World.GetDistance(Game.Player.Character.RightPosition, gate.RightPosition);

                    if (gateDist <= OPEN_THRESHOLD)
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
                    else if (gateDist > CLOSE_THRESHOLD)
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