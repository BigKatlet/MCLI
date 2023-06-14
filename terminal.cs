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

        const int buildVer = 110;
        const int stableVer = 1;

        static string uniteArgs(string[] arg, int val)
        {
            for (int x = 0; x != val; x++)
            {
                arg[x] = "";
            }
            return string.Concat(arg);
        }

        static string uniteArgs(string[] arg, int val, bool addSpace)
        {
            for (int x = 0; x != arg.Length; x++)
            {
                arg[x] += " ";
            }
            for (int x = 0; x != val; x++)
            {
                arg[x] = "";
            }
            return string.Concat(arg);
        }

        public static void execute(string[] arg)
        {
            switch (arg[0])
            {
                case "info":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("MCLI (Main Command Line Interface Operating System)");
                    Console.WriteLine("Build #" + buildVer + "/15.06.2023");
                    Console.WriteLine("Stable version #" + stableVer);
                    Console.Write("--------------------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
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
                        else if (arg[1] == "3")
                        {
                            for(int x = 0; x != 255; x++)
                            {
                                Console.Write((char)x);
                            }
                        }
                    }
                    break;
                case "readStr":
                    string[] text = File.ReadAllLines(FS.curPath + arg[1]);
                    foreach(string x in text)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "setStr":
                    string name = arg[1];
                    string textToWrite = uniteArgs(arg, 2, true);
                    api.setStr(FS.curPath, name, textToWrite);
                    break;
                case "crtFile":
                    api.crtFile(FS.curPath, arg[1]);
                    break;
                case "delFile":
                    api.delFile(FS.curPath, arg[1]);
                    break;
                case "crtDir":
                    api.crtDir(FS.curPath, arg[1]);
                    break;
                case "delDir":
                    api.delDir(FS.curPath, arg[1]);
                    break;
                case "cd":
                    string path = ""; path = FS.curPath + arg[1] + @"\";
                    if (Directory.Exists(path))
                    {
                        FS.curPath = path;
                    }
                    else
                    {
                        api.notification(2, "Directory '" + path + "' doesn't exist!");
                    }
                    break;
                case "cls":
                    api.cls();
                    break;
                case "dir":
                    string[] dirs = Directory.GetDirectories(FS.curPath);
                    int totalLen = 25;
                    int count = 0;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("--------------------------------------------------------------------------------");
                    foreach (string dir in dirs)
                    {
                        if(count == 0) { Console.ForegroundColor = ConsoleColor.White; } 
                        else { Console.ForegroundColor = ConsoleColor.Gray;}
                        count++; if (count > 1) { count = 0; }

                        Console.Write(dir);
                        for(int x = 0; x != totalLen - dir.Length; x++)
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
                    break;
                case "exe":
                    interpreter.executeProgramm(FS.curPath, arg[1]);
                    break;
                case "editExe":
                    interpreter.editFile(FS.curPath, arg[1]);
                    break;
                case "say":
                    string textToSay = uniteArgs(arg, 1, true);
                    api.say(textToSay);
                    break;
                case "shutdown":
                    Cosmos.System.Power.Shutdown();
                    break;
                case "root":
                    FS.curPath = @"0:\";
                    break;
                case "up":
                    if(FS.curPath != @"0:\")
                    {
                        string[] pieces = FS.curPath.Split(@"\");
                        string newPath = "";
                        for (int x = 0; x != pieces.Length - 2; x++)
                        {
                            newPath += pieces[x] + @"\";
                        }
                        FS.curPath = newPath;
                    }
                    else
                    {
                        api.notification(1, "Can't go upper than root folder!");
                    }
                    break;
                case "chngCol":
                    api.chngCol(Convert.ToChar(arg[1]));
                    break;
                case "commander":
                    api.commander();
                    break;
                case "help":
                    interpreter.executeProgramm(@"0:\System\Apps\", "Help.exe");
                    break;
                case "waitForKey":
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
    }
}
