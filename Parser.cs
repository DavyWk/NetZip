using System;
using System.IO.Compression;
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
        string[] args;

        public Parser(string[] arguments)
        {
            args = arguments;
        }

        public bool Parse()
        {
            if (args.Contains(new string[] {"x", "exit"}))
                return false;
            var mainCommand = args[1];

            if (mainCommand.Equals(cList))
                new List(args).Execute();
            else if (mainCommand.Equals(cExtract))
                new Extract(args).Execute();
            else if (mainCommand.Equals(cAdd))
                new Add(args).Execute();
            else if (mainCommand.Equals(cDelete))
                new Delete(args).Execute();
            else if (mainCommand.Equals(cCreate))
                new Create(args).Execute();

            return true;
        }
    }
}
