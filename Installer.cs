using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCLI
{
    public static class Installer
    {
        public static void firstStartup()
        {
            Console.WriteLine("Setting up MCLI. \nThis may take some time...");
            install();
        }

        private static void install()
        {
            Directory.CreateDirectory(@"0:\System");
            Directory.CreateDirectory(@"0:\System\Apps");
            Directory.CreateDirectory(@"0:\System\Logs");
            ///
            File.Create(@"0:\System\Logs\doNotif");

            Logger log = new Logger(@"0:\System\Logs\install.log");
            log.writeLine("System directories created, deleting COSMOS OS test files.");

            api.tryToDelFile(@"0:\test\DirInTest\Readme.txt");
            api.tryToDelDir(@"0:\test\DirInTest");
            api.tryToDelDir(@"0:\test");

            api.tryToDelDir(@"0:\Dir Testing");
            api.tryToDelFile(@"0:\Kudzu.txt");
            api.tryToDelFile(@"0:\Root.txt");

            log.writeLine("COSMOS OS test files deleted or not found, creating 'User' directory");

            Directory.CreateDirectory(@"0:\User");

            log.writeLine("'User' directory created, creating apps");

            api.crtFile(@"0:\System\Apps\", "Help.exe");
            string[] helpToWrite = {
                                "cls",
                                "chngCol 8",
                                "say Help list 1",
                                "chngCol 0",
                                "say crtFile [name] - create file",
                                "chngCol 1",
                                "say delFile [name] - delete file",
                                "chngCol 0",
                                "say crtDir [name] - create dir",
                                "chngCol 1",
                                "say delDir [name] - delete dir",
                                "chngCol 0",
                                "say dir - displays containing of current directory",
                                "chngCol 1",
                                "say cd [name] - change dir",
                                "chngCol 0",
                                @"say root - go to 0:\ directory",
                                "chngCol 1",
                                "say up - go to upper directory",
                                "chngCol 0",
                                "say setStr [name] [textToBeSet] - set first string to file",
                                "chngCol 1",
                                "say readStr [name] - read first string of file",
                                "chngCol 0",
                                "say chngCol [index] - change color of text",
                                "chngCol 1",
                                "say editExe [name] - edit in editor .exe file",
                                "chngCol 0",
                                "say exe [name] - execute .exe file",
                                "chngCol 1",
                                "say say [text] - write text to console",
                                "chngCol 0",
                                "say shutdown - turn off computer",
                                "chngCol 1",
                                "say info - information about system",
                                "chngCol 0",
                                "say waitForKey - wait for key press",
                                "chngCol 7",
                                "say Press any key to continue...",
                                "waitForKey"};
            File.WriteAllLines(@"0:\System\Apps\Help.exe", helpToWrite);

            log.writeLine("Apps created");

            log.writeLine("End of an installation.");
            Console.WriteLine("Installation finished!");
        }
    }
}
