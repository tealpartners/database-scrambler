namespace DatabaseScrambler
{
    public class Program
    {
        /// <summary>
        /// arg 1: Connection string
        /// arg 2: ScrambleConfiguration.xml
        /// arg 3: custom_sql.sql
        /// </summary>
        /// <returns>Exit code: 0 on success, 1 on failure</returns>
        static int Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                const string error = "Run program as 'DatabaseScrambler.exe <connection string> <ScrambleConfiguration.xml> <custom_sql.sql (optional)>'";
                Cansole.WriteError(error);
                return 1;
            }

            string runSqlFileAfterScramble = null;
            if (args.Length > 2)
            {
                runSqlFileAfterScramble = args[2];
            }

            var container = Bootstrapper.Bootstrap();
            var scrambleCoordinator = container.Resolve<IScrambleCoordinator>();

            return scrambleCoordinator.Scramble(args[0], args[1], runSqlFileAfterScramble) ? 0 : 1;
        }
    }
}
