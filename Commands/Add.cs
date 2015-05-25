using System;
using System.IO;
using System.IO.Compression;

namespace NetZip.Commands
{
    class Add // Can't add a whole directory
    {
        ZipArchive archive;
        string path;
        string archiveName;
        bool isDirectory;

        public Add(string[] args)
        {
            archiveName = args[0];
            path = args[2];
            isDirectory = args.Contains("-d");

            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            archive = ZipFile.Open(archiveName, ZipArchiveMode.Update);
        }

        public void Execute()
        {
            string fileName = Path.GetFileName(path);

            archive.CreateEntryFromFile(path, fileName);
            archive.Dispose();
            
            using(ZipArchive newArchive = ZipFile.OpenRead(archiveName))
            {
                var entry = newArchive.GetEntry(fileName);
                Console.WriteLine("Successfully added {0} to the archive {1}", fileName, archiveName);
                Console.WriteLine("FileSize: from {0} to {1} bytes", entry.Length, entry.CompressedLength);
                Console.WriteLine("Compression ratio: {0}%", Math.Ceiling((double)entry.CompressedLength / (double)entry.Length * 100));
            }

        }

    }
}
