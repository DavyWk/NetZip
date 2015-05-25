using System;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace NetZip.Commands
{
    class Delete : ICommand
    {
        ZipArchive archive;
        string entryName;
        bool isDirectory = false;

        // netzip delete test.zip file.txt
        public Delete(string[] args)
        {
            entryName = args[2];

            archive = ZipFile.Open(args[0], ZipArchiveMode.Update);

            if (args[1] == "-d") // in case the delete command was written as '-d'
                isDirectory = (from a in args where a == "-d" select a).Count() > 1;
            else
            {
                isDirectory = args.Contains("-d");
            }
        }

        public void Execute()
        {
            long originalSize = archive.GetSize();
            var list = new List<ZipArchiveEntry>();

            foreach (var entry in archive.Entries)
            {
                if (isDirectory)
                {
                    if (entry.FullName.Contains(entryName))
                        if(entry.FullName[entry.FullName.LastIndexOf(entryName) + entryName.Length] == '\\')
                            // Crashes if file extension is the same as name
                        list.Add(entry);
                }
                else
                {
                    if (entry.Name == entryName)
                        list.Add(entry);
                }
            }

            if (list.Count > 1 && !isDirectory)
            {
                Console.Write("The archive contains more than one entry named \"{0}\", delete all of them or just the first one ? (y/n) ", entryName);
                bool deleteAll = Console.ReadLine()[0] == 'y';

                if (!deleteAll)
                    list.RemoveRange(1, list.Count - 1); // delete all but the first one from the list
            }
            else if (list.Count == 0)
            {
                if (!isDirectory)
                    Console.WriteLine("The archive does not contain any entry named \"{0}\"", entryName);
                else
                    Console.WriteLine("The archive does not contain the folder \"{0}\"", entryName);
                Console.WriteLine("Make sure to provide the full path if it is a subfolder");
                archive.Dispose();
                return;
            }

            foreach (var entry in list)
                entry.Delete();


            if (!isDirectory)
                Console.WriteLine("Deleted {0} entries of \"{1}\" from the archive", list.Count, entryName);
            else
                Console.WriteLine("Delete {0} files from the folder {1} in the archive", list.Count, entryName);

            Console.WriteLine("Size went from {0} bytes to {1} bytes", originalSize, archive.GetSize());

            archive.Dispose();
        }


    }
}
