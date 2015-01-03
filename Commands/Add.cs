using System;
using System.IO;
using System.IO.Compression;

namespace NetZip.Commands
{
    class Add
    {
        ZipArchive archive;
        string filePath;
        string archiveName;

        public Add(string[] args) // need to support directories too
        {
            archiveName = args[0];
            filePath = args[2];

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            archive = ZipFile.Open(archiveName, ZipArchiveMode.Update);
        }

        public void Execute()
        {
            string fileName = Path.GetFileName(filePath);
            if(!File.Exists(fileName))
            {
                Console.WriteLine("The file \"{0}\" does not exist", filePath);
                return;
            }

            archive.CreateEntryFromFile(filePath, fileName);
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
