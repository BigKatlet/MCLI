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
        public static string stdPath = @"0:\User\";
        public static string curPath;

        public static CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static void initializeFs()
        {
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
        }
    }
}
