using System;
using System.Collections.Generic;
using System.IO;

namespace MCLI
{
    public static class interpreter
    {
        static string _storageStrPath;
        static string _storageIntPath;


        public static void executeProgramm(string path, string name){
            if (!name.EndsWith(".exe"))
            {
                api.notification(2, "unavailable to execute non .exe file!");
            }
            else
            {
                string fullPath = path + name;

                /*_storageStrPath = fullPath.Remove(_storageStrPath.Length-4, 4);
                _storageStrPath += "_str.tmp";
                File.Create(_storageStrPath);

                _storageIntPath = fullPath;
                _storageIntPath = _storageIntPath[..^4];
                _storageIntPath += "_int.tmp";
                File.Create(_storageIntPath);*/

                string[] lines = File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    string[] arg = line.Split(' ', StringSplitOptions.TrimEntries);
                    terminal.execute(arg);
                }
            }
        }

        public static void editFile(string path, string name)
        {
            string fullPath = path + name;
            int counter = 0;

            List<string> buff = new List<string>();

            if (!fullPath.EndsWith(".exe"))
            {
                api.notification(2, "Can't edit non .exe file with exe editor!");
            }
            else
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Exe editor v1                                                                   ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Editing now: " + fullPath);
                Console.ForegroundColor = ConsoleColor.White;

                Console.ForegroundColor = ConsoleColor.Gray;
                counter++; Console.Write(counter.ToString() + " ");
                Console.ForegroundColor = ConsoleColor.White;

                string curLine = Console.ReadLine();
                buff.Add(curLine);

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    counter++; Console.Write(counter.ToString() + " ");
                    Console.ForegroundColor = ConsoleColor.White;

                    curLine = Console.ReadLine();
                    if (curLine == "")
                    {
                        break;
                    }
                    buff.Add(curLine);
                }
                File.WriteAllLines(fullPath, buff.ToArray());
                api.cls();
                api.notification(0, "File '" + fullPath + "' hass been succesfully changed.");
            }
        }
    }
}
