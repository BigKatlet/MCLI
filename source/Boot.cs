using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLI
{
    public static class Boot
    {

        public static void afterBoot()
        {
            FS.initializeFs();
            FS.curPath = FS.stdPath;
            Cosmos.System.PCSpeaker.Beep(2000, 25);
            Console.SetCursorPosition(0, 2);
            Cosmos.System.KeyboardManager.ScrollLock = false;
            api.cls();

            if (!Directory.Exists(@"0:\System"))
            {
                Installer.firstStartup();
            }
        }
    }
}
