using System;
using System.IO;
using System.Linq;
using System.IO.Compression;

namespace NetZip.Commands
{
    class Extract : ICommand
    {
        ZipArchive archive;
        string extractDir;

        public Extract(string[] args)
        {
            archive = ZipFile.OpenRead(args[0]);

            if (args.Length > 2)
                extractDir = args[2];
            else
                extractDir = Path.GetFileNameWithoutExtension(args[0]);
        }

        public void Execute()
        {
            CheckForAvailability();
            
            archive.ExtractToDirectory(extractDir);
            Console.WriteLine("Successfully extracted {0} files from the archive to \"{1}\".",
                archive.Entries.Count, extractDir);
            Console.WriteLine("Size: {0} bytes", GetDirectorySize());

            archive.Dispose();
        }

        private long GetDirectorySize()
        {
            var di = new DirectoryInfo(extractDir);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            
        }

        private void CheckForAvailability()
        {
            int i = 1;
            while(Directory.Exists(extractDir))
            {
                Console.Write("The name \"{0}\" is already taken ... ", extractDir);
                extractDir = string.Format("Unzziped({0})", i++);
                Console.WriteLine("Trying with the name \"{0}\"", extractDir);
            }
        }
    }
}
