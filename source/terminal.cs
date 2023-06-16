using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace MCLI
{
    public static class terminal
    {
        //Build version
        const int buildVer = 116;
        //Stable version
        const int stableVer = 1;

        public static void execute(string[] arg)
        {
            switch (arg[0])
            {
                case "info":
                    //Info about system
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MCLI (Main Command Line Interface Operating System)");
                    Console.WriteLine("Build #" + buildVer + "/15.06.2023");
                    Console.WriteLine("Stable version #" + stableVer);
                    Console.Write("--------------------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
                    //Easter egg
                    if (arg.Length > 1)
                    {
                        if (arg[1] == "2")
                        {
                            int[] notes = { 392, 392, 392, 311, 466, 392, 311, 466, 392, 587, 587, 587, 622, 466, 369, 311, 466, 392,
  784, 392, 392, 784, 739, 698, 659, 622, 659,415, 554, 523, 493, 466, 440, 466, 311, 369, 311, 466, 392, 311, 466, 392};
                            int[] times = {
  350, 350, 350, 250, 100, 350, 250, 100, 700, 350, 350, 350, 250, 100, 350, 250, 100, 700, 350, 250, 100, 350, 250, 100, 100, 100, 450,
  150, 350, 250, 100, 100, 100, 450, 150, 350, 250, 100, 250, 250, 100, 750};
                            for (int i = 0; i < 42; i++)
                            {
                                Cosmos.System.PCSpeaker.Beep(Convert.ToUInt32(notes[i]), Convert.ToUInt32(times[i]));
                                Cosmos.HAL.Global.PIT.Wait(Convert.ToUInt32(times[i]));
                            }
                        }
                    }
                    break;
                case "readFile":
                    string[] text = File.ReadAllLines(FS.curPath + arg[1]);
                    foreach(string x in text)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "setStr":
                    //DON'T TOUCH THIS SHIT. IT MAY NOT WORK.
                    string name = arg[1];
                    string textToWrite = uniteArgs(arg, 2, true);
                    api.setStr(FS.curPath, name, textToWrite);
                    break;
                case "crtFile":
                    //Creates file
                    api.crtFile(FS.curPath, arg[1]);
                    break;
                case "delFile":
                    //Deletes file
                    api.delFile(FS.curPath, arg[1]);
                    break;
                case "crtDir":
                    //Creates directory
                    api.crtDir(FS.curPath, arg[1]);
                    break;
                case "delDir":
                    //Deletes directory
                    api.delDir(FS.curPath, arg[1]);
                    break;
                case "cd":
                    //cd from windows
                    changeDir(arg[1]);
                    break;
                case "cls":
                    api.cls();
                    break;
                case "dir":
                    dir();
                    break;
                case "exe":
                    //Executes .exe file
                    interpreter.executeProgramm(FS.curPath, arg[1]);
                    break;
                case "editExe":
                    //Opens .exe editor
                    interpreter.editFile(FS.curPath, arg[1]);
                    break;
                case "say":
                    //Writes text to console.
                    api.say(uniteArgs(arg, 1, true));
                    break;
                case "shutdown":
                    //SUDDENLY, shuts off system.
                    Cosmos.System.Power.Shutdown();
                    break;
                case "root":
                    //Go to root
                    FS.curPath = @"0:\";
                    break;
                case "up":
                    //Go upper
                    FS.curPath = up();
                    break;
                case "chngCol":
                    //SUDDENLY, changes color of output. 
                    // 0 - white; 1 - gray; 2 - yellow; 3 - red; 4 - dark blue;
                    // 5 - blue; 6 - dark green; 7 - green; 8 - pink; 9 - cyan.
                    api.chngCol(Convert.ToChar(arg[1]));
                    break;
                case "commander":
                    //Commander parody
                    api.commander();
                    break;
                case "help":
                    //Execute through interpreter "0:\System\Apps\Help.exe".
                    interpreter.executeProgramm(@"0:\System\Apps\", "Help.exe");
                    break;
                case "waitForKey":
                    //Wait for key press and erase symbol.
                    Console.ReadKey();
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                    Console.Write(" ");
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
                    break;
                default:
                    api.notification(2, "Unknown command. Use /help");
                    break;
            }
        }

        static string up()
        {
            if (FS.curPath != @"0:\")
            {
                //Holy fucking magic with strings
                string[] pieces = FS.curPath.Split(@"\");
                string newPath = "";
                for (int x = 0; x != pieces.Length - 2; x++)
                {
                    newPath += pieces[x] + @"\";
                }
                return newPath;
            }
            else
            {
                api.notification(1, "Can't go upper than root folder!");
                return @"0:\";
            }
        }

        static void dir()
        {
            //Displays content of directory
            string[] dirs = Directory.GetDirectories(FS.curPath);
            int totalLen = 25;
            int count = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("--------------------------------------------------------------------------------");
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
                Console.Write("<dir>");

                string[] dirsInDir = Directory.GetDirectories(FS.curPath + dir);
                string[] filesInDir = Directory.GetFiles(FS.curPath + dir);

                if (dirsInDir.Length < 1 && filesInDir.Length < 1)
                {
                    Console.WriteLine("   (empty)");
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("--------------------------------------------------------------------------------");
        }

        private static void changeDir(string dirName)
        {
            if (!String.IsNullOrEmpty(dirName))
            {
                string path = FS.curPath + dirName;
                if (Directory.Exists(path))
                {
                    FS.curPath = path + @"\";
                }
                else
                {
                    api.notification(2, "Directory '" + path + "' doesn't exist!");
                }
            }
        }

        static string uniteArgs(string[] arg, int argsToRemove)
        {
            //Deletes [argsToRempove] args and unites remaining strings to one.
            //Delete some args
            for (int x = 0; x != argsToRemove; x++)
            {
                arg[x] = "";
            }
            return string.Concat(arg);
        }

        static string uniteArgs(string[] arg, int argsToRemove, bool addSpace)
        {
            //Deletes [argsToRempove] args and unites remaining strings to one. Also adds " " to every remaining string.
            //Add " " to every arg
            for (int x = 0; x != arg.Length; x++)
            {
                arg[x] += " ";
            }
            //Delete some args
            for (int x = 0; x != argsToRemove; x++)
            {
                arg[x] = "";
            }
            return string.Concat(arg);
        }
    }
}
