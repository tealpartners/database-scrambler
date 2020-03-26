namespace DatabaseScrambler.Runner
{
    public class Program
    {
        /// <summary>
        /// arg 1: Connection string
        /// arg 2: ScrambleConfiguration.xml
        /// arg 3: custom_sql.sql
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                const string error = "Run program as 'DatabaseScrambler.Runner.exe <connection string> <ScrambleConfiguration.xml> <custom_sql.sql (optional)>'";
                Cansole.WriteError(error);
                return;
            }

            string runSqlFileAfterScramble = null;
            if (args.Length > 2)
            {
                runSqlFileAfterScramble = args[2];
            }

            var container = Bootstrapper.Bootstrap();
            var scrambleCoordinator = container.Resolve<IScrambleCoordinator>();

            scrambleCoordinator.Scramble(args[0], args[1], runSqlFileAfterScramble);
        }
    }
}
