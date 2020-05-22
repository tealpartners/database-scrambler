using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleClearColumn : BaseScramble
    {
        public ScrambleClearColumn() 
            : base(ScrambleType.ClearColumn)
        {
        }

        public override void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            const string sqlScript = "UPDATE [{0}].[{1}] SET [{2}] = null";
            var sql = string.Format(sqlScript, configuration.Schema, configuration.TableName, configuration.ColumnName);

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
