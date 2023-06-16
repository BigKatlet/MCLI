using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using Cosmos.System.FileSystem;

namespace MCLI
{
    public static class FS
    {
        //After-boot directory.
        private static string stdPath = @"0:\User\";
        //Current directory.
        public static string curPath;

        public static CosmosVFS fs = new CosmosVFS();
        public static void initializeFs()
        {
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            FS.curPath = FS.stdPath;
        }
    }
}
