using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLI
{
    public class Logger
    {

        public string pathToLogFile;

        public Logger(string path)
        {
            pathToLogFile = path;
        }

        public void write(string text)
        {
            if(pathToLogFile != null)
            {
                StreamWriter writer = new StreamWriter(pathToLogFile, true);
                writer.Write(text);
                writer.Close();
            }
            else
            {
                api.notification(2, "path to log file is empty!");
            }
        }

        public void writeLine(string text)
        {
            if (pathToLogFile != null)
            {
                StreamWriter writer = new StreamWriter(pathToLogFile, true);
                writer.WriteLine(text);
                writer.Close();
            }
            else
            {
                api.notification(2, "path to log file is empty!");
            }
        }

    }
}
