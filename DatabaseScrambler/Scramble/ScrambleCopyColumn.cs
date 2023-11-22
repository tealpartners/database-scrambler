using System.Collections.Generic;
using System.Data.SqlClient;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public class ScrambleCopyColumn : BaseScramble
    {
        public ScrambleCopyColumn() : base(ScrambleType.CopyColumn)
        {
        }
    
        public override void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            const string sqlScript = "UPDATE [{0}].[{1}] SET [{2}] = [{3}]";
            var sql = string.Format(
                sqlScript, 
                configuration.Schema, 
                configuration.TableName, 
                configuration.ColumnName, 
                configuration.CopyColumn);

            using (var sqlCommand = new SqlCommand(sql, connection, transaction))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }
        }

        protected override IList<string> GetScrambleData(string culture)
        {
            return new List<string>();
        }
    }
}