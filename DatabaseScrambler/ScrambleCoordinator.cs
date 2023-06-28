using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DatabaseScrambler.Domain;
using DatabaseScrambler.Scramble;

namespace DatabaseScrambler
{
    public interface IScrambleCoordinator
    {
        void Scramble(string connectionString, string configurationFile, string runSqlFileAfterScramble);
    }

    public class ScrambleCoordinator : IScrambleCoordinator
    {
        private readonly IList<IScramble> _scrambleActions;

        public ScrambleCoordinator(IList<IScramble> scrambleActions)
        {
            _scrambleActions = scrambleActions;
        }

        public void Scramble(string connectionString, string configurationFile, string runSqlFileAfterScramble)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    Console.WriteLine("Opening sql connection");

                    connection.Open();
                }
                catch (SqlException exception)
                {
                    Cansole.WriteError(exception.Message);
                    return;
                }

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Console.WriteLine("Create temp table with scramble data");

                        // Load configuration from file
                        var configuration = GetConfiguration(configurationFile);

                        // Dump all scramble data in temp table.
                        CreateScrambleTable(connection, transaction, configuration.Culture);

                        foreach (var configItem in configuration.ConfigurationItems)
                        {
                            Console.WriteLine($"Running scrambler {configItem.Type} for table '{configItem.Schema}.{configItem.TableName}', column '{configItem.ColumnName}'");

                            var scrambleAction = _scrambleActions.SingleOrDefault(x => x.CanScramble(configItem.Type));
                            if (scrambleAction == null)
                            {
                                Cansole.WriteError($"No scramble action found for type '{configItem.Type}'.");
                                throw new Exception();
                            }

                            scrambleAction.Scramble(connection, transaction, configItem);
                        }

                        if (!string.IsNullOrEmpty(runSqlFileAfterScramble))
                        {
                            var sql = File.ReadAllText(runSqlFileAfterScramble);

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.CommandTimeout = 0;
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        Cansole.WriteError(exception.Message);
                        transaction.Rollback();
                    }
                }
            }
        }

        private static XmlConfiguration GetConfiguration(string configurationFile)
        {
            XmlConfiguration returnValue;

            Console.WriteLine("Fetching configuration file");

            using (var reader = new StreamReader(configurationFile))
            {
                Console.WriteLine("Parsing configuration file");

                var serializer = new XmlSerializer(typeof(XmlConfiguration));
                returnValue = (XmlConfiguration)serializer.Deserialize(reader);
            }

            return returnValue;
        }

        private void CreateScrambleTable(SqlConnection sqlConnection, SqlTransaction transaction, string culture)
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
                scrambleAction.AddScrambleData(sqlConnection, transaction, culture);
            }
        }
    }
}