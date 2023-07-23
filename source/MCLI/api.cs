using Cosmos.Core.Memory;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCLI
{
    public static class api
    {

        public static bool commanderOpened;

        public static void commander()
        {
            commanderOpened = true;
            int curPos = 4;
            api.cls(ConsoleColor.DarkBlue);

            for (; ; )
            {
                //💀
                if (commanderOpened == true)
                {
                    //Clear screen
                    for(int x = 0; x != 25; x++)
                    {
                        Console.SetCursorPosition(1, x);
                        Console.Write("                                                                               ");
                    }


                    ///DRAW GRAPHICS
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, 1);
                    for (int x = 1; x != 23; x++)
                    {
                        Console.SetCursorPosition(0, x);
                        Console.Write("|");
                    }

                    for (int x = 1; x != 23; x++)
                    {
                        Console.SetCursorPosition(79, x);
                        Console.Write("|");
                    }

                    Console.SetCursorPosition(0, 0);
                    Console.Write("-------------------------------");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("MCLI commander 0.2");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("-------------------------------");

                    Console.SetCursorPosition(1, 2);
                    Console.Write("------------------------------------------------------------------------------");

                    Console.SetCursorPosition(0, 23);
                    Console.Write("--------------------------------------------------------------------------------");

                    Console.SetCursorPosition(1, 1);
                    Console.Write((FS.fs.GetTotalFreeSpace(@"0:\") / 1024).ToString() + "KB of " + (FS.fs.GetTotalSize(@"0:\") / 1024).ToString() + "KB available");
                    Console.SetCursorPosition(1, 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(FS.curPath);
                    Console.ForegroundColor  = ConsoleColor.White;


                    ///FUCKING MAGICK BEGINS
                    Console.SetCursorPosition(1, 4);
                    Console.WriteLine();

                    //Get dirs
                    string[] dirs = Directory.GetDirectories(FS.curPath);
                    
                    int totalLen = 25;
                    
                    bool isWhite = true;

                    //Write dirs
                    foreach (string dir in dirs)
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

                        //Yellow background if it's selected now
                        Console.CursorLeft = 1;
                        if(Console.CursorTop == curPos)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }

                        //Write cur dir
                        Console.Write(dir);
                        for (int x = 0; x != totalLen - dir.Length; x++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write("<dir>   ");

                        //Is folder empty?
                        if (Directory.GetDirectories(FS.curPath + dir).Length < 1 && Directory.GetFiles(FS.curPath + dir).Length < 1)
                        {
                            Console.WriteLine("(empty)");
                        }
                        else
                        {
                            Console.Write("\n");
                        }
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }

                    string[] files = Directory.GetFiles(FS.curPath);
                    foreach (string file in files)
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


                        //Yellow background if it's selected now
                        Console.CursorLeft = 1;
                        if (Console.CursorTop == curPos)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                        }

                        //Write cur file with it's size
                        Console.Write(file);
                        int size = (int)new FileInfo(FS.curPath + file).Length;
                        for (int x = 0; x != totalLen - file.Length; x++)
                        {
                            Console.Write(" ");
                        }
                        Console.Write(size.ToString() + "B\n");

                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }

                    //current position of cursor
                    int cPos = Console.CursorTop;

                    //Writing upper button
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(1, 4);
                    if(Console.CursorTop == curPos)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;


                    }
                    Console.Write("[^]");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(1, cPos);

                    //Selectables is things that can be selected by user - files and dirs
                    List<string> selectable = new List<string>();

                    //Adds "upper dir" button
                    selectable.Add("[^]");
                    //Add dirs and files to selectables
                    selectable.AddRange(dirs); selectable.AddRange(files);

                    //terminal in last line
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(0, 24);
                    Console.Write(FS.curPath + ">");

                    //terminal command
                    List<char> commandSymb = new List<char>();

                    for (; ; )
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Enter)
                        {
                            if(commandSymb.Count == 0)
                            {
                                //Position
                                int id = curPos - 4;
                                //Go to upper directory
                                if(id == 0)
                                {
                                    FS.curPath = upDir();
                                    curPos = 4;
                                    break;
                                }
                                //Change directory
                                if(id >= files.Length && dirs.Length != 0)
                                {
                                    string dirName = selectable[id];
                                    string path = FS.curPath + dirName;
                                    FS.curPath = path + @"\";
                                    curPos = 4;
                                }
                            }

                            Console.WriteLine();
                            break;
                        }
                        else if(key.Key == ConsoleKey.Delete && curPos - 4 != 0)
                        {
                            int id = curPos - 4;

                            if (id <= files.Length && files.Length != 0)
                            {
                                string fileName = selectable[id];
                                api.delFile(FS.curPath, fileName);
                                curPos--;
                                break;
                            }
                            else
                            {
                                if (dirs.Length != 0)
                                {
                                    string dirName = selectable[id];
                                    string path = FS.curPath + dirName;
                                    if (Directory.GetDirectories(path).Length < 1 && Directory.GetFiles(path).Length < 1)
                                    {
                                        api.delDir(FS.curPath, dirName);
                                    }
                                    curPos--;
                                    break;
                                }
                            }
                        }
                        else if (key.Key == ConsoleKey.Escape)
                        {
                            //Close commander
                            commanderOpened = false;
                            api.cls(ConsoleColor.Black);
                            break;
                        }
                        else if (key.Key == ConsoleKey.Backspace)
                        {
                            if (commandSymb.Count > 0)
                            {

                                Console.CursorLeft--;
                                Console.Write(" ");
                                Console.CursorLeft--;
                                commandSymb.RemoveAt(commandSymb.Count - 1);
                            }
                        }
                        else if(key.Key == ConsoleKey.DownArrow)
                        {
                            if (curPos < 3 + selectable.Count)
                            {
                                //select thing down
                                curPos++;
                                break;
                            }
                        }
                        else if(key.Key == ConsoleKey.UpArrow)
                        {
                            if(curPos > 4)
                            {
                                //select thing upper
                                curPos--;
                                break;
                            }
                        }
                        else
                        {
                            //Write single symbol
                            Console.Write(key.KeyChar);
                            commandSymb.Add(key.KeyChar);
                        }
                    }
                    //if user typed command, execute it
                    if (commandSymb.Count > 0)
                    {
                        terminal.execute(String.Concat(commandSymb).Split(' ', StringSplitOptions.TrimEntries));
                    }
                }
                else
                {
                    break;
                }
                
                Heap.Collect();
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

        public static string readFile(string path, string name)
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

        public static void cls(ConsoleColor col)
        {
            Console.Clear();
            Console.BackgroundColor = col;
            for (int x = 0; x != 80 * 25; x++)
            {
                Console.Write(" ");
            }
            Console.Clear();
        }

        public static void write(string text)
        {
            Console.Write(text);
        }

        public static void writeLine(string text)
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

        public static string upDir()
        {
            if (FS.curPath != @"0:\")
            {
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
    }
}
