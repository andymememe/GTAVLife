using System;
using System.Runtime.CompilerServices;
using System.IO;

namespace GTAVLife.Helper
{
    public enum LogLevel
    {
        Panic = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Debug = 4,
    }

    public class Logger
    {
        private static Logger instance = null;
        private string path;
        private LogLevel loglevel = LogLevel.Debug;

        public static Logger GetInstance()
        {
            if (instance == null)
            {
                throw new InvalidOperationException("logger instance is not initialized");
            }

            return instance;
        }

        public string Path => path;

        public static Logger GetInstance(string path, LogLevel logLevel)
        {
            return instance ?? new Logger(path, logLevel);
        }

        public void Raw(string content)
        {
            writeToFileRaw(content);
        }

        public void Debug(string content, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLineNo = -1, [CallerMemberName] string callerName = "")
        {
            writeToFile(content, LogLevel.Debug, callerPath, callerLineNo, callerName);
        }

        public void Info(string content, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLineNo = -1, [CallerMemberName] string callerName = "")
        {
            writeToFile(content, LogLevel.Info, callerPath, callerLineNo, callerName);
        }

        public void Warning(string content, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLineNo = -1, [CallerMemberName] string callerName = "")
        {
            writeToFile(content, LogLevel.Warning, callerPath, callerLineNo, callerName);
        }

        public void Error(string content, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLineNo = -1, [CallerMemberName] string callerName = "")
        {
            writeToFile(content, LogLevel.Error, callerPath, callerLineNo, callerName);
        }

        public void Panic(string content, [CallerFilePath] string callerPath = "", [CallerLineNumber] int callerLineNo = -1, [CallerMemberName] string callerName = "")
        {
            writeToFile(content, LogLevel.Panic, callerPath, callerLineNo, callerName);
        }

        private Logger(string path, LogLevel logLevel)
        {
            this.path = path;
            this.loglevel = logLevel;
            instance = this;
        }

        private void writeToFile(string content, LogLevel logLevel, string callerPath, int callerLineNo, string callerName)
        {
            if (logLevel <= this.loglevel)
            {
                DateTime now = DateTime.Now;
                string formatString = string.Format("[{0}][{1}:{2}][{3}][{4}]{5}\n", now, callerPath, callerLineNo, callerName, logLevel.ToString(), content);
                File.AppendAllText(this.path, formatString);
            }
        }

        private void writeToFileRaw(string content)
        {
            File.AppendAllText(this.path, content);
        }
    }
}