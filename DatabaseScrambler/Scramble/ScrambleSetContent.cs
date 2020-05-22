using System;
using System.Data.SqlClient;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleSetContent : IScramble
    {
        private string SetContentQuery = "UPDATE [{0}].[{1}] SET [{2}] = {3};";

        public void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            var sql = string.Format(SetContentQuery, configuration.Schema, // 0
                                            configuration.TableName               // 1
                                            , configuration.ColumnName            // 2
                                            , configuration.Value);               // 3

            try
            {
                using (var sqlCommand = new SqlCommand(sql, connection, transaction))
                {
                    sqlCommand.CommandTimeout = 0;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                Console.WriteLine($"Query: '{sql}'");
                throw;
            }
        }

        public void AddScrambleData(SqlConnection connection, SqlTransaction transaction)
        {
        }

        public bool CanScramble(ScrambleType type)
        {
            return type == ScrambleType.SetContent;
        }
    }
}
