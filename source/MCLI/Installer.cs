using System;
using System.IO;

namespace MCLI
{
    public static class Installer
    {

        //Build version
        const int buildVer = 210;
        //Stable version
        const int stableVer = 2;

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
            Directory.CreateDirectory(@"0:\System\Params");
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

            log.writeLine("COSMOS OS test files deleted or not found, creating 'User' directory...");

            Directory.CreateDirectory(@"0:\User");

            log.writeLine("'User' directory created, creating apps...");

            //Creates "Help.exe" file
            api.crtFile(@"0:\System\Apps\", "Help.exe");
            string[] helpToWrite = {
                                "cls",
                                "chngCol 8",
                                "writeLine Help list 1",
                                "chngCol 0",
                                "writeLine crtFile [name] - create file",
                                "chngCol 1",
                                "writeLine delFile [name] - delete file",
                                "chngCol 0",
                                "writeLine crtDir [name] - create dir",
                                "chngCol 1",
                                "writeLine delDir [name] - delete dir",
                                "chngCol 0",
                                "writeLine dir - displays containing of current directory",
                                "chngCol 1",
                                "writeLine cd [name] - change dir",
                                "chngCol 0",
                                @"writeLine root - go to 0:\ directory",
                                "chngCol 1",
                                "writeLine up - go to upper directory",
                                "chngCol 0",
                                "writeLine setStr [name] [textToBeSet] - set first string to file",
                                "chngCol 1",
                                "writeLine readFile [name] - reads and writes all strings of file",
                                "chngCol 0",
                                "writeLine chngCol [index ] - change color of text",
                                "chngCol 1",
                                "writeLine editExe [name] - .exe editor. Empty string + enter to exit",
                                "chngCol 0",
                                "writeLine exe [name] - execute .exe file",
                                "chngCol 1",
                                "writeLine commander - commander. Empty string + enter to exit.",
                                "chngCol 0",
                                "writeLine write [text] - write text to console",
                                "chngCol 1",
                                "writeLine writeLine [text] - write line to console",
                                "chngCol 0",
                                "writeLine shutdown - turn off computer",
                                "chngCol 1",
                                "writeLine info - information about system",
                                "chngCol 0",
                                "writeLine waitForKey - wait for any key to be pressed",
                                "chngCol 7",
                                "writeLine Press any key to continue...",
                                "waitForKey"};
            File.WriteAllLines(@"0:\System\Apps\Help.exe", helpToWrite);

            api.crtFile(@"0:\System\Apps\", "info.exe");
            string[] infoToWrite =
            {
                "chngCol 7",
                "writeLine MCLI (Main Command Line Interface OS)",
                "write Build #", "write $os_buildver",
                "writeLine",
                "write Stable version #", "write $os_stablever",
                "writeLine",
                "write --------------------------------------------------------------------------------",
                "chngCol 0"
            };
            File.WriteAllLines(@"0:\System\Apps\info.exe", infoToWrite);

            api.crtFile(@"0:\System\Params\", "stablever.opt");
            File.WriteAllText(@"0:\System\Params\stablever.opt", stableVer.ToString());
            api.crtFile(@"0:\System\Params\", "buildver.opt");
            File.WriteAllText(@"0:\System\Params\buildver.opt", buildVer.ToString());

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
