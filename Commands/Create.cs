using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace NetZip.Commands
{
    class Create
    {
        string sourcePath;
        string archiveName;

        // args[2] can be a file or a directory
        public Create(string[] args)
        {
            archiveName = args[0];
            sourcePath = args[2];

            if (!archiveName.EndsWith(".zip"))
               archiveName = string.Concat(archiveName, ".zip");

            if(sourcePath.IndexOf(".") == -1)
            { // is a directory
                if (!Directory.Exists(sourcePath))
                    throw new DirectoryNotFoundException(sourcePath);
            }
            else
            { // is a file
                if (!File.Exists(sourcePath))
                    throw new DirectoryNotFoundException(sourcePath);
            }

        }

        public void Execute()
        {
            if (File.Exists(archiveName))
            {
                Console.Write("A file named {0} already exists, overwrite ? (y/n) ", archiveName);
                if (Console.ReadLine()[0] == 'y')
                    File.Delete(archiveName);
                else
                {
                    Console.WriteLine("Did not create archive");
                    return;
                }
            }

            ZipArchive archive = ZipFile.Open(archiveName, ZipArchiveMode.Create);
            var files = new List<string>();
            
            if (!sourcePath.Contains("."))
            {
                files.AddRange(GetFilePaths(sourcePath));
            }
            else
                files.Add(sourcePath);

            foreach (var f in files)
            {
                string entryName = f;
                if(!sourcePath.Contains("."))
                    f.Substring(sourcePath.Length);
                archive.CreateEntryFromFile(f, entryName);
            }
            archive.Dispose();

            using (var newArchive = ZipFile.OpenRead(archiveName))
            {
                long compressedSize = newArchive.GetSize();
                long decompressedSize = newArchive.GetDecompressedSize();
                Console.WriteLine("Created archive \"{0}\": {1} files", archiveName, newArchive.Entries.Count);
                Console.WriteLine("Size: {0} to {1} bytes", decompressedSize, compressedSize);
                Console.WriteLine("Compression Ratio: {0}%", Math.Ceiling((double)compressedSize / (double)decompressedSize * 100));
            }

        }

        private List<string> GetFilePaths(string dir)
        {
            var ret = new List<string>();
            var files = new List<FileInfo>();

            files.AddRange(new DirectoryInfo(dir).EnumerateFiles("*", SearchOption.AllDirectories));

            foreach (var file in files)
            {
                var name = file.FullName;
                name = name.Substring(name.IndexOf(dir));
                ret.Add(name);
            }

            return ret;

        }
    }
}
