using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleClearTable : BaseScramble
    {
        public ScrambleClearTable()
            : base(ScrambleType.ClearTable)
        {
        }

        public override void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            const string sqlScript = "TRUNCATE TABLE [{0}].[{1}]";
            var sql = string.Format(sqlScript, configuration.Schema, configuration.TableName);

            using (var sqlCommand = new SqlCommand(sql, connection, transaction))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }
        }

        protected override IList<string> GetScrambleData()
        {
            return new List<string>();
        }
    }
}
