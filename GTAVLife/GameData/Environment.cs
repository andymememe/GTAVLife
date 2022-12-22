using GTAVLife.Helper;

namespace GTAVLife.GameData
{
    public class Environment : SimpleSingletonBase<Environment>
    {
        public bool IsActivated { get; set; }
        public bool IsJustInitialized { get; set; }
        public bool IsChangedSkinNeeded { get; set; }

        private Environment()
        {
            this.IsActivated = false;
            this.IsJustInitialized = false;
            this.IsChangedSkinNeeded = true;
        }
    }
}