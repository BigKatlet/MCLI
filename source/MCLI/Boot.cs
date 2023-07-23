using System;
using System.IO;

namespace MCLI
{
    public static class Boot
    {

        public static void afterBoot()
        {
            FS.initializeFs();
            Cosmos.System.PCSpeaker.Beep(2000, 25);
            api.cls(ConsoleColor.Black);
            //Creates system files
            if (!Directory.Exists(@"0:\System"))
            {
                Installer.firstStartup();
            }
            else
            {
                
            }
        }
    }
}
