﻿using System;
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
                if(!CheckForZIP(args)) // if no ZIP file is in the arguments
                {
                    var temp = new List<string>();
                    temp.Add(RequestFileName());
                    temp.AddRange(args);

                    args = temp.ToArray();
                }


                if (args[0].Equal(cHelp)) // help
                {
                    if (args.Length > 1)
                        args[0] = args[1];

                    mainCommand = "help";
                }
                else // any other command
                {
                    Console.WriteLine("\tOperating on file: {0}", args[0]);
                    Console.WriteLine();
                    mainCommand = args[1].ToLower();

                }

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

                Console.WriteLine();
                Console.WriteLine("\t\t\tNetZip- Ready for input");
            } while (GetArgs(Console.ReadLine().Split(' ')));

        }

        private bool GetArgs(string[] source)
        {
            if (source.Contains(cQuit))
                return false;

            var argList = new List<string>(source);

            if (!argList[0].EndsWith(".zip") && !source.Contains(cHelp)) // Re use previously specified file name
            {
                if(args.Length == 0) // if no file name was ever specified
                {
                    args = new string[] { RequestFileName() };
                }

                var tempList = argList;
                argList = new List<string>();
                argList.Add(args[0]); // add old filename
                argList.AddRange(tempList); // the actual command
            }

            args = argList.ToArray();

            return true;
        }

        private bool CheckForZIP(string[] source)
        {
            // Checks for a ZIP file in the arguments

            bool check = false;

            foreach(string arg in source)
            {
                if(arg.EndsWith(".zip"))
                {
                    check = true;
                    break;
                }
            }

            return check;
        }

        private string RequestFileName()
        {
            Console.Write("Please specify a file name: ");
            string fileName = Console.ReadLine();
            Console.WriteLine();

            return fileName;
        }
    }
}
