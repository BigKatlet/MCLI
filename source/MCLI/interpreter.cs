using System;
using System.Collections.Generic;
using System.IO;

namespace MCLI
{
    public static class interpreter
    {

        public static void executeProgramm(string path, string name){
            if (!name.EndsWith(".exe"))
            {
                api.notification(2, "unavailable to execute non .exe file!");
            }
            else
            {
                string fullPath = path + name;

                //Read all lines from file and execute all of them.
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
            //Opens .exe editor.
            //TODO: write mew file editor.
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
                api.cls(ConsoleColor.Black);
                api.notification(0, "File '" + fullPath + "' hass been succesfully changed.");
            }
        }
    }
}
