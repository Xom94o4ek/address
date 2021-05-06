using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace address.Log
{
    class Logger : ILog
    {
        const string WarningFile = "Warning.txt";
        const string InfoFile = "Info.txt";
        const string DebugFile = "Debug.txt";

        public void Warning(string message)
        {
            WriteToFile(WarningFile, TextFormating("WARNING", message));
        }
        public void Info(string message)
        {
            WriteToFile(InfoFile, TextFormating("INFO", message));
        }
        public void Info(string message, params object[] args)
        {
            WriteToFile(InfoFile, TextFormating("INFO", message, args));
        }
        public void Debug(string message)
        {
            WriteToFile(DebugFile, TextFormating("DEBUG", message));
        }
        public void Debug(string message, Exception e)
        {
            WriteToFile(DebugFile, TextFormating("DEBUG", message, e));
        }
        public void DebugFormat(string message, params object[] args)
        {
            WriteToFile(DebugFile, TextFormating("DEBUG", message, args));
        }
        private void WriteToFile(string FileName, string message)
        {
            DateTime now = DateTime.Now;
            string path = @String.Format("Log/{0}", now.ToString("yyyy-MM-dd"));
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists) dirInfo.Create(); 
            File.AppendAllText(@String.Format("{0}/{1}", path, FileName), $"{now.ToString("dd.MM.yyyy")} {now.ToString("HH:mm:ss")} {message}\n");
        }
        private string TextFormating(string nametype, string message)
        {
            return $"({nametype}): {message}";
        }
        private string TextFormating(string nametype, string message, Exception e)
        {
            string ExcInfo = $"\n\t\t\t\tИсключение: {e.Message}\n\t\t\t\tМетод: {e.TargetSite}\n\t\t\t\tТрассировка стека: {e.StackTrace}";
            return $"({nametype}): {message}{ExcInfo}";
        }
        private string TextFormating(string nametype, string message, params object[] args)
        {
            string AllArgs = "";
            for (int i = 0; i < args.Length; i++)
            {
                AllArgs += "\n\t\t\t\t" + args[i];
            }
            return $"({nametype}): {message}{AllArgs}";
        }
    }
}
