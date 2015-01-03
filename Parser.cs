using System;
using System.IO.Compression;
using System.Collections.Generic;

using NetZip.Commands;

namespace NetZip
{
    class Parser
    {
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

            if (mainCommand == "list" || mainCommand == "-l")
                new List(args).Execute();
            else if (mainCommand == "extract" || mainCommand == "-e")
                new Extract(args).Execute();
            else if (mainCommand == "add" || mainCommand == "-a")
                new Add(args).Execute();

            return true;
        }
    }
}
