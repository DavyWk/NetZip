using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace NetZip.Commands
{
    class List : ICommand
    {
        ZipArchive archive;
        List<string> formats = new List<string> { "{0}",
        "{0}\tSize: {1} bytes",
        "{0}\tCompressedSize: {1} bytes",
        "{0}\tSize: {1} bytes\tCompressedSize: {2} bytes"};
        int formatIndex = 0;

        public List(string[] args)
        {
            if (!File.Exists(args[0]))
                throw new FileNotFoundException(args[0]);

            archive = ZipFile.OpenRead(args[0]);
            Parse(args);
        }

        private void Parse(string[] args)
        {
            bool fileSize = args.Contains("-s");
            bool compressedFileSize = args.Contains("-cs");

            if (fileSize)
                formatIndex = 1;
            if (compressedFileSize)
                formatIndex = 2;
            if (fileSize && compressedFileSize)
                formatIndex = 3;
        }

        public void Execute()
        {
            string format = formats[formatIndex];

            foreach(ZipArchiveEntry entry in archive.Entries)
            {
                Console.WriteLine(format, entry.FullName, formatIndex == 2 ? entry.CompressedLength : entry.Length, entry.CompressedLength);
            }

            archive.Dispose();
        }

        

    }
}
