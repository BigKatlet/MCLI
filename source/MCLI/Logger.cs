using System.IO;

namespace MCLI
{
    public class Logger
    {

        //Logger [name] = new Logger([string with path to log file, use "0:\System\Logs"])

        public string pathToLogFile;

        public Logger(string path)
        {
            pathToLogFile = path;
        }

        //It's like Console.Write(), but writes to log file.
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

        //It's like Console.WriteLine(), but writes to log file.
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
