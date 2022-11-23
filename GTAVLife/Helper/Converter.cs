namespace GTAVLife.Helper
{
    public class Distance {
        private const float MM_PER_GAME_WORLD_DISTANCE = 800f;

        public static float ToMM(float gameWorldDist)
        {
            return gameWorldDist * MM_PER_GAME_WORLD_DISTANCE;
        }

        public static float ToGameWorldDistance(float mm)
        {
            return mm / MM_PER_GAME_WORLD_DISTANCE;
        }
    }
}