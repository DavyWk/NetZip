using System;
using System.Collections.Generic;

using NetZip.Commands;

namespace NetZip
{
    class Parser
    {
        private static string[] cList = new string[] { "list", "-l" };
        private static string[] cExtract = new string[] { "extract", "-e" };
        private static string[] cAdd = new string[] { "add", "-a" };
        private static string[] cDelete = new string[] { "delete", "del", "-d" };
        private static string[] cCreate = new string[] { "create", "-c" };
        private static string[] cQuit = new string[] { "x", "quit" };
        private static string[] cHelp = new string[] { "h", "help" };

        string[] args;
        bool standAlone;

        public Parser(string[] arguments, bool isStandAlone)
        {
            standAlone = isStandAlone;
            args = arguments;
        }

        public void Parse()
        {
            if (args.Length == 0)
            {
                Console.WriteLine("\t\t\tNetZip- Ready for input");

                if (!GetArgs(Console.ReadLine().Split()))
                    return;
            }

            var mainCommand = string.Empty;

            do
            {
                if (!args[0].Equal(cHelp))
                    Console.WriteLine("\tOperating on file: {0}", args[0]);
                else
                {
                    args[0] = args[1];
                    mainCommand = args[1] = "help";
                }
                mainCommand = args[1].ToLower();

                if (mainCommand.Equal(cList))
                    new List(args).Execute();
                else if (mainCommand.Equal(cExtract))
                    new Extract(args).Execute();
                else if (mainCommand.Equal(cAdd))
                    new Add(args).Execute();
                else if (mainCommand.Equal(cDelete))
                    new Delete(args).Execute();
                else if (mainCommand.Equal(cCreate))
                    new Create(args).Execute();
                else if (mainCommand.Equal(cHelp))
                    new Help(args[0]).Execute();
                else
                    Console.WriteLine("Unknown command: {0}", mainCommand);

                if (!standAlone)
                    break;

                Console.WriteLine("\t\t\tNetZip- Ready for input");
            } while (GetArgs(Console.ReadLine().Split(' ')));

        }

        private bool GetArgs(string[] source)
        {
            if (source.Contains(cQuit))
                return false;

            var argList = new List<string>(source);

            if (!argList[0].EndsWith(".zip") && !source.Contains(cHelp))
            {
                var tempList = argList;
                argList = new List<string>();
                argList.Add(args[0]); // add old filename
                argList.AddRange(tempList); // add the rest of the command
            }

            args = argList.ToArray();

            return true;
        }
    }
}
