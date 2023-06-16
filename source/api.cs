using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCLI
{
    public static class api
    {

        //Parody on DOS commanders
        public static void commander()
        {
            for(; ; )
            {
                api.cls();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("MCLI commander 0.1");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("-------------------------------");
                Console.WriteLine((FS.fs.GetTotalFreeSpace(@"0:\")/1024).ToString() + "KB of " + (FS.fs.GetTotalSize(@"0:\")/1024).ToString() + "KB available");

                string[] dirs = Directory.GetDirectories(FS.curPath);
                int totalLen = 25;
                int count = 0;
                Console.Write("--------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(FS.curPath);
                foreach (string dir in dirs)
                {
                    if (count == 0) { Console.ForegroundColor = ConsoleColor.White; }
                    else { Console.ForegroundColor = ConsoleColor.Gray; }
                    count++; if (count > 1) { count = 0; }

                    Console.Write(dir);
                    for (int x = 0; x != totalLen - dir.Length; x++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("<dir>   ");

                    string[] dirsInDir = Directory.GetDirectories(FS.curPath + dir);
                    string[] filesInDir = Directory.GetFiles(FS.curPath + dir);

                    if(dirsInDir.Length < 1 && filesInDir.Length < 1)
                    {
                        Console.WriteLine("(empty)");
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
                string[] files = Directory.GetFiles(FS.curPath);
                foreach (string file in files)
                {
                    if (count == 0) { Console.ForegroundColor = ConsoleColor.White; }
                    else { Console.ForegroundColor = ConsoleColor.Gray; }
                    count++; if (count > 1) { count = 0; }

                    Console.Write(file);
                    int size = (int)new FileInfo(FS.curPath + file).Length;
                    for (int x = 0; x != totalLen - file.Length; x++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(size.ToString());
                    Console.WriteLine("B");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("--------------------------------------------------------------------------------");
                string[] arg = Console.ReadLine().Split(' ', StringSplitOptions.TrimEntries);
                if (arg[0] == "" || arg[0] == null)
                {
                    break;
                }
                else
                {
                    terminal.execute(arg);
                }
            }
        }

        public static void setStr(string path, string name, string text)
        {
            string fullPath = path + name;
            if (name == null || !File.Exists(fullPath))
            {
                notification(2, "file '" + fullPath + "' can't be found!");
            }
            else
            {
                File.WriteAllText(fullPath, text);
            }
        }

        public static string readTxt(string path, string name)
        {
            string fullPath = path + name;
            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }
            else
            {
                notification(2, "file '" + fullPath + "' can't be found!");
                return "";
            }
        }

        public static void crtFile(string path, string name)
        {
            string fullPath = fullPath = path + name;
            if (name == null || name == "")
            {
                notification(2, "empty file name!");
            }
            else
            {
                File.Create(fullPath);
            }
        }

        public static void delFile(string path, string name)
        {
            string fullPath = path + name;
            if (name == null || !File.Exists(fullPath))
            {
                notification(2, "file '" + fullPath + "' can't be found!");
            }
            else
            {
                File.Delete(fullPath);
                notification(0, "file '" + fullPath + "' has been deleted");
            }
        }

        public static void crtDir(string path, string name)
        {
            string fullPath = path + name;
            if(name == null || name == "")
            {
                notification(2, "empty file name!");
            }
            else
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public static void delDir(string path, string name)
        {
            string fullPath = path + name;
            if (name == null || (!Directory.Exists(fullPath)))
            {
                notification(2, "directory '" + fullPath + "' can't be found!");
            }
            else
            {
                Directory.Delete(fullPath);
                notification(0, "directory '" + fullPath + "' has been deleted");
            }
        }

        public static void chngCol(char color)
        {
            //SUDDENLY, changes color of output. 
            // 0 - white; 1 - gray; 2 - yellow; 3 - red; 4 - dark blue;
            // 5 - blue; 6 - dark green; 7 - green; 8 - pink; 9 - cyan.
            switch (color) {
                case '0':
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case '1':
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case '2':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case '3':
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case '4':
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case '5':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case '6':
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case '7':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case '8':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case '9':
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
            }
        }

        public static void cls()
        {
            Console.Clear();
        }

        public static void say(string text)
        {
            Console.WriteLine(text);
        }

        public static void notification(int type, string msg)
        {
            //Types: 0 - INFO (gray); 1 - WARN (yellow); 2 - ERR (red).
            //Write logs if file "0:\System\Logs\doNotif" exists
            if (File.Exists(@"0:\System\Logs\doNotif"))
            {
                Logger log = new Logger(@"0:\System\Logs\notifications.log");
                switch (type)
                {
                    case 0:
                        log.write("[INFO]: ");
                        break;
                    case 1:
                        log.write("[WARN]: ");
                        break;
                    case 2:
                        log.write("[ERR]: ");
                        break;
                    default:
                        api.notification(2, "Notification error: unexpected nofitication index '" + type  +"'");
                        break;
                }
                log.writeLine(msg);
            }
            //Writes notification to console.
            switch (type)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[INFO]: ");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[WARN]: ");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[ERR]: ");
                    break;
                default:
                    api.notification(2, "Notification error: unexpected nofitication index '" + type + "'");
                    break;
            }  
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
