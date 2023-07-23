using System;
using System.IO;

namespace MCLI
{
    public static class terminal
    {

        public static void execute(string[] arg)
        {
            arg[0] = arg[0].ToLower();

            switch (arg[0])
            {
                case "info":
                    //Info about system
                    interpreter.executeProgramm(@"0:\System\Apps\", "info.exe");
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
                                Cosmos.System.PCSpeaker.Beep((uint)notes[i], (uint)times[i]);
                                Cosmos.HAL.Global.PIT.Wait((uint)times[i]);
                            }
                        }
                    }
                    break;
                case "readfile":
                    string[] text = File.ReadAllLines(FS.curPath + arg[1]);
                    foreach(string x in text)
                    {
                        Console.WriteLine(x);
                    }
                    break;
                case "setstr":
                    //DON'T TOUCH THIS SHIT. IT MAY NOT WORK.
                    string name = arg[1];
                    string textToWrite = uniteArgs(arg, 2, true);
                    api.setStr(FS.curPath, name, textToWrite);
                    break;
                case "crtfile":
                    //Creates file
                    api.crtFile(FS.curPath, arg[1]);
                    break;
                case "delfile":
                    //Deletes file
                    api.delFile(FS.curPath, arg[1]);
                    break;
                case "crtdir":
                    //Creates directory
                    api.crtDir(FS.curPath, arg[1]);
                    break;
                case "deldir":
                    //Deletes directory
                    api.delDir(FS.curPath, arg[1]);
                    break;
                case "cd":
                    //cd from windows
                    changeDir(arg[1]);
                    break;
                case "cls":
                    api.cls(ConsoleColor.Black);
                    break;
                case "dir":
                    dir();
                    break;
                case "exe":
                    //Executes .exe file
                    interpreter.executeProgramm(FS.curPath, arg[1]);
                    break;
                case "editexe":
                    //Opens .exe editor
                    interpreter.editFile(FS.curPath, arg[1]);
                    break;
                case "write":
                    //Like Console.Write().
                    if (arg[1].StartsWith("$"))
                    {
                        switch (arg[1])
                        {
                            case "$os_stablever":
                                api.write(File.ReadAllText(@"0:\System\Params\stablever.opt"));
                                break;
                            case "$os_buildver":
                                api.write(File.ReadAllText(@"0:\System\Params\buildver.opt"));
                                break;
                            default:
                                api.notification(2, "Variable '" + arg[1] + "' doesn't exist!");
                                break;
                        }
                    }
                    else
                    {
                        api.write(uniteArgs(arg, 1, true));
                    }
                    break;
                case "writeline":
                    api.writeLine(uniteArgs(arg, 1, true));
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
                    FS.curPath = api.upDir();
                    break;
                case "chngcol":
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
                case "waitforkey":
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

        static void dir()
        {
            //Displays content of directory
            int totalLen = 25;
            bool isWhite = true;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("--------------------------------------------------------------------------------");
            foreach (string dir in Directory.GetDirectories(FS.curPath))
            {
                if (isWhite == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                isWhite = !isWhite;

                Console.Write(dir);
                for (int x = 0; x != totalLen - dir.Length; x++)
                {
                    Console.Write(" ");
                }
                Console.Write("<dir>");

                if (Directory.GetDirectories(FS.curPath + dir).Length < 1 && Directory.GetFiles(FS.curPath + dir).Length < 1)
                {
                    Console.WriteLine("   (empty)");
                }
                else
                {
                    Console.Write("\n");
                }
            }
            foreach (string file in Directory.GetFiles(FS.curPath))
            {
                if (isWhite == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                isWhite = !isWhite;

                Console.Write(file);
                int size = (int)new FileInfo(FS.curPath + file).Length;
                for (int x = 0; x != totalLen - file.Length; x++)
                {
                    Console.Write(" ");
                }
                Console.Write(size.ToString() + "B\n");
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
