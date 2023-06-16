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
            Cosmos.System.PCSpeaker.Beep(2000, 25);
            api.cls();

            //Creates system files
            if (!Directory.Exists(@"0:\System"))
            {
                Installer.firstStartup();
            }
        }
    }
}
