using System.Collections.Generic;
using System.Linq;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler
{
    public class ScrambleEngine
    {
        private readonly ScrambleOptions _options;

        public ScrambleEngine(ScrambleOptions options)
        {
            _options = options;
        }

        public void Run()
        {
            if (!CheckOptions(_options)) return;
            
            var container = Bootstrapper.Bootstrap();
            var scrambleCoordinator = container.Resolve<IScrambleCoordinator>();

            scrambleCoordinator.Scramble(_options.ConnectionString, _options.Configurations, _options.RunSqlFileAfterScramble);
        }

        private bool CheckOptions(ScrambleOptions options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                Cansole.WriteError("No ConnectionString provided.");
                return false;
            }
            
            if (options?.Configurations == null || !options.Configurations.Any())
            {
                Cansole.WriteError("No Configuration Items provided.");
                return false;
            }
            
            return true;
        }

    }

    public class ScrambleOptions
    {
        public string ConnectionString { get; set; }
        public IList<Configuration> Configurations { get; set; }
        public string RunSqlFileAfterScramble { get; set; }
    }
}