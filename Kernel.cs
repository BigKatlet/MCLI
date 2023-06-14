using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace MCLI
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Boot.afterBoot();
        }
        protected override void Run()
        {
            int curPosX = Console.GetCursorPosition().Left;
            int curPosY = Console.GetCursorPosition().Top;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(FS.curPath + ">");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            string[] arg = input.Split(' ', StringSplitOptions.TrimEntries);
            terminal.execute(arg);
        }
    }
}