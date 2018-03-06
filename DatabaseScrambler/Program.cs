﻿using System;

namespace DatabaseScrambler
{
    public class Program
    {
        /// <summary>
        /// arg 1: Connection string
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                const string error = "Run program as 'DatabaseScrambler.exe <connection string> <ScrambleConfiguration.xml>'";
                Console.WriteLine(error);
                throw new Exception(error);
            }

            var container = Bootstrapper.Bootstrap();
            var scrambleCoordinator = container.Resolve<IScrambleCoordinator>();

            scrambleCoordinator.Scramble(args[0], args[1]);
        }
    }
}
