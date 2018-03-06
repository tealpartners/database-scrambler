using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DatabaseScrambler.Domain;
using DatabaseScrambler.Scramble;

namespace DatabaseScrambler
{
    public interface IScrambleCoordinator
    {
        void Scramble(string connectionString, string configurationFile);
    }

    public class ScrambleCoordinator : IScrambleCoordinator
    {
        private readonly IList<IScramble> _scrambleActions;

        public ScrambleCoordinator(IList<IScramble> scrambleActions)
        {
            _scrambleActions = scrambleActions;
        }

        public void Scramble(string connectionString, string configurationFile)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Opening sql connection");

                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    Console.WriteLine("Create temp table with scramble data");

                    //Dump all scramble data in temp table.
                    CreateScrambleTable(connection, transaction);

                    foreach (var configuration in GetConfiguration(configurationFile))
                    {
                        Console.WriteLine("Running scrambler {2} for table '{0}', column '{1}'", configuration.TableName, configuration.ColumnName, configuration.Type);

                        var scrambleAction = _scrambleActions.SingleOrDefault(x => x.CanScramble(configuration.Type));
                        if (scrambleAction == null)
                        {
                            throw new Exception($"No scramble action found for type '{configuration.Type}'.");
                        }

                        scrambleAction.Scramble(connection, transaction, configuration);
                    }

                    transaction.Commit();
                } 
            }
        }

        private IEnumerable<Configuration> GetConfiguration(string configurationFile)
        {
            List<Configuration> returnValue;

            Console.WriteLine("Fetching configuration file");

            using (var reader = new StreamReader(configurationFile))
            {
                Console.WriteLine("Parsing configuration file");

                var serializer = new XmlSerializer(typeof (XmlConfiguration));
                returnValue = ((XmlConfiguration)serializer.Deserialize(reader)).ConfigurationItems;
            }

            return returnValue;
        }

        private void CreateScrambleTable(SqlConnection sqlConnection, SqlTransaction transaction)
        {
            const string query = @"CREATE TABLE #ScrambleData
                        (
                            [Type] nvarchar(255),
                            Value nvarchar(255),
                            ValueIndex int
                        );
                        
                        CREATE INDEX IX_ScrambleData_Type ON #ScrambleData ([Type]);
                        CREATE INDEX IX_ScrambleData_ValueIndex ON #ScrambleData ([ValueIndex]);";

            using (var command = new SqlCommand(query, sqlConnection, transaction))
            {
                command.CommandTimeout = 0;
                command.ExecuteNonQuery();
            }

            foreach (var scrambleAction in _scrambleActions)
            {
                scrambleAction.AddScrambleData(sqlConnection, transaction);
            }
        }
    }
}
