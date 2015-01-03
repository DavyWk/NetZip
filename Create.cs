using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace NetZip
{
    class Create
    {
        ZipArchive archive;
        string sourcePath;
        string archiveName;

        // args[2] can be a file or a directory
        public Create(string[] args)
        {
            archiveName = args[0];
            sourcePath = args[2];
            archive = ZipFile.Open(archiveName, ZipArchiveMode.Create);
        }

        public void Execute()
        {
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
