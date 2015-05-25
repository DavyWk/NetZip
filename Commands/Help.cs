using System;

namespace NetZip.Commands
{
    
    class Help : ICommand
    {
        string cmd;

        public Help(string command)
        {
            cmd = command;
        }

        public void Execute()
        {
            Console.WriteLine(GetHelpText());
        }

        private string GetHelpText()
        {
            if (cmd == "add")
                return hAdd;
            else if (cmd == "create")
                return hCreate;
            else if (cmd == "extract")
                return hExtract;
            else if (cmd == "delete")
                return hDelete;
            else if (cmd == "list")
                return hList;
            else if (cmd == "help" || string.IsNullOrEmpty(cmd))
                return hHelp;
            else
                return string.Format("Unkown command: {0}", cmd);
        }

        private string hAdd = @"ADD (-a): Adds an item to a zip archive
Usage: add test.zip test.txt";

        private string hCreate = @"CREATE (-c): Creates a new archive
Usage: create test.zip test.txt
       create test.zip fileDir -d
Options:
    -d: Include a directory";

        private string hDelete = @"DELETE (-d): Deletes an entry from the archive
Usage: del test.zip test.txt
       del test.zip fileDir -d
Options:
    -d: Delete a directory";

        private string hExtract = @"EXTRACT (-e): Extracts an archive to a directory
Usage: extract test.zip dirName
Info: If dirName is already taken, the directory will be named Unzipped(x)";

        private string hList = @"LIST (-l): Displays the content of the archive
Usage: list test.zip [options]
Options:
    -s: Display size
    -cs: Display compressed size";

        private string hHelp = @"HELP (h): Displays help
Usage: help [funtionName]

ADD (-a): Adds an item to a zip archive
CREATE (-c): Creates a new archive
DELETE (-d): Deletes an entry from the archive
EXTRACT (-e): Extracts an archive to a directory
LIST (-l): Displays the content of the archive";
    }
}
