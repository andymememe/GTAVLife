using System.IO;

namespace GTAVLife.Helper
{
    public class SaveHelper
    {
        private static SaveHelper instance = null;
        private string path;

        public static SaveHelper GetInstance(string path)
        {
            return instance ?? new SaveHelper(path);
        }

        public string Path => path;

        public bool Exist()
        {
            return File.Exists(this.path);
        }

        public void Write(string content)
        {
            File.WriteAllText(this.path, content);
        }

        public string Read()
        {
            return File.ReadAllText(this.path);
        }

        private SaveHelper(string path)
        {
            this.path = path;
            instance = this;
        }
    }
}