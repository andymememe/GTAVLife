using GTAVLife.Helper;
using GTAVLife.GameData;

namespace GTAVLife
{
    public class SaveManager
    {
        private static SaveManager instance = null;
        private SaveHelper save;

        public static SaveManager GetInstance(SaveHelper save)
        {
            return instance ?? new SaveManager(save);
        }

        public void Process()
        {
            if (Life.Instance.IsDirty)
            {
                save.Write(Life.Instance.Serializer());
            }
        }

        public void Load()
        {
            if (save.Exist())
            {
                Life.Instance.Deserializer(save.Read());
            }
        }

        private SaveManager(SaveHelper save)
        {
            this.save = save;
        }
    }
}