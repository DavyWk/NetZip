using System;

namespace NetZip
{
    class Program
    {
        static bool standAlone = false; // standalone console window

        static void Main(string[] args)
        {
            DetermineStandAlone();

            Console.Title = "NetZip";
            Console.WriteLine("Made by DavyWk: https://github.com/DavyWk/NetZip");

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: netzip [commands]");
                Exit();
            }

            //TODO: Need exception for Create
            // check in each class ?
            //if (!System.IO.File.Exists(args[0]) &&)
            //{
            //    Console.WriteLine("File does not exist !");
            //    Exit();
            //}

            new Parser(args).Parse();

            Pause();
        }

        static void Exit()
        {
            Console.WriteLine("Exiting ...");
            Pause();
            Environment.Exit(0);
        }

        private static void Pause()
        {
            if (standAlone)
                Console.ReadLine();
        }

        private static void DetermineStandAlone()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            standAlone = (left == 0 && top == 0);
        }
    }
}
