using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.LoggerHelper
{
    public class LoggerPersistance
    {
        private static LoggerPersistance instance;
        private static string LOG_DIRECTORY =  Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Logs";
        private static Object lockObject = new object();

        public static LoggerPersistance getInstance()
        {
            lock (lockObject)
            {
                if (instance == null)
                    instance =  new LoggerPersistance();
                return instance;
            }
        }

        public void AddLine(string line)
        {
            string fileName = ConfigurationManager.AppSettings["logsFileName"];
            string filePath = LOG_DIRECTORY + "\\" + fileName;
            if (File.Exists(filePath))
            {
                File.AppendAllText(filePath, line);
            }
            else
            {
                FileStream fs = new FileStream(LOG_DIRECTORY + "\\" + fileName, FileMode.Append, FileAccess.Write);
                fs.Write(Encoding.ASCII.GetBytes(line), 0, line.Length);
                fs.Close();
            }
            
        }
    }
}
