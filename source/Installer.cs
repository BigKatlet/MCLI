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
            //Method starts if system was booted first time.
            Console.WriteLine("Setting up MCLI. \nThis may take some time...");
            install();
        }

        private static void install()
        {
            //Creates System files.
            Directory.CreateDirectory(@"0:\System");
            Directory.CreateDirectory(@"0:\System\Apps");
            Directory.CreateDirectory(@"0:\System\Logs");
            ///
            File.Create(@"0:\System\Logs\doNotif");

            Logger log = new Logger(@"0:\System\Logs\install.log");
            log.writeLine("System directories created, deleting COSMOS OS test files.");

            tryToDelFile(@"0:\test\DirInTest\Readme.txt");
            tryToDelDir(@"0:\test\DirInTest");
            tryToDelDir(@"0:\test");

            tryToDelDir(@"0:\Dir Testing");
            tryToDelFile(@"0:\Kudzu.txt");
            tryToDelFile(@"0:\Root.txt");

            log.writeLine("COSMOS OS test files deleted or not found, creating 'User' directory");

            Directory.CreateDirectory(@"0:\User");

            log.writeLine("'User' directory created, creating apps");

            //Creates "Help.exe" file
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
                                "say readTxt [name] - reads and writes all strings of file",
                                "chngCol 0",
                                "say chngCol [index] - change color of text",
                                "chngCol 1",
                                "say editExe [name] - .exe editor. Don't write anything and press Enter to exit.",
                                "chngCol 0",
                                "say exe [name] - execute .exe file",
                                "chngCol 1",
                                "say commander - commander. Don't write anything and press Enter to exit.",
                                "chngCol 0",
                                "say say [text] - write text to console",
                                "chngCol 1",
                                "say shutdown - turn off computer",
                                "chngCol 0",
                                "say info - information about system",
                                "chngCol 1",
                                "say waitForKey - wait for any key press",
                                "chngCol 7",
                                "say Press any key to continue...",
                                "waitForKey"};
            File.WriteAllLines(@"0:\System\Apps\Help.exe", helpToWrite);

            log.writeLine("Apps created");

            log.writeLine("End of an installation.");
            Console.WriteLine("Installation finished!");
        }

        //Insttaller func
        private static void tryToDelFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                api.notification(1, path + " can't be found");
            }
        }

        //Insttaller func
        private static void tryToDelDir(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path);
            }
            else
            {
                api.notification(1, path + " can't be found");
            }
        }
    }
}
