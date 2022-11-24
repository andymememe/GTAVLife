using System.IO;

namespace GTAVLife.Helper
{
    public class Save
    {
        private static Save instance = null;
        private string path;

        public static Save GetInstance(string path)
        {
            return instance ?? new Save(path);
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

        private Save(string path)
        {
            this.path = path;
            instance = this;
        }
    }
}