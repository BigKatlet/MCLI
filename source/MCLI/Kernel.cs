using System;
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
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(FS.curPath + ">");
            Console.ForegroundColor = ConsoleColor.White;
            terminal.execute(Console.ReadLine().Split(' ', StringSplitOptions.TrimEntries));
        }
    }
}