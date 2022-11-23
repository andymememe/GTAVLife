using GTA;

namespace GTAVLife.GameData
{
    public class Enviroment
    {
        public static bool IsMissionActive
        {
            get
            {
                return Game.IsMissionActive;
            }
        }

        public static bool IsRandomEventActive
        {
            get
            {
                return Game.IsRandomEventActive;
            }
        }

        public static bool IsCutsceneActive
        {
            get
            {
                return Game.IsCutsceneActive;
            }
        }
        
        public static bool IsLoading
        {
            get
            {
                return Game.IsLoading;
            }
        }
    }
}