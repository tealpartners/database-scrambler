using System.Data.SqlClient;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleSetContent : IScramble
    {
        private string SetContentQuery = "UPDATE [{0}] SET [{1}] = '{2}';";

        public void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            var sql = string.Format(SetContentQuery, configuration.TableName  //0
                                            , configuration.ColumnName      //1
                                            , configuration.Value);         //2

            using (var sqlCommand = new SqlCommand(sql, connection, transaction))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
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
