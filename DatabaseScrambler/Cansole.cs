using System;

namespace DatabaseScrambler
{
    /// <summary>
    /// The Cansole does what the Console won't
    /// </summary>
    internal static class Cansole
    {
        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }
    }
}
