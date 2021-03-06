﻿using System;

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

            try
            {
                new Parser(args, standAlone).Parse();
            }
            catch (Exception ex)
            {
                if (ex is System.IO.FileNotFoundException)
                    Console.WriteLine((ex as System.IO.FileNotFoundException).Message);
                else if (ex is System.IO.DirectoryNotFoundException)
                    Console.WriteLine((ex as System.IO.DirectoryNotFoundException).Message);
                else if (ex is System.IO.IOException)
                    Console.WriteLine(ex.Message);
                else
                    throw;

                Exit();
            }

            Console.WriteLine();
            Console.WriteLine("Thanks for using NetZip");
            System.Threading.Tasks.Task.Delay(1000).ContinueWith(_ => { Environment.Exit(0); });
            Console.ReadLine();
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
