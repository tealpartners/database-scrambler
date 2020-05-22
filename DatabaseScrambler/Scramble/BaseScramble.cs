using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using DatabaseScrambler.Domain;

namespace DatabaseScrambler.Scramble
{
    public interface IScramble
    {
        void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration);

        void AddScrambleData(SqlConnection connection, SqlTransaction transaction);

        bool CanScramble(ScrambleType type);
    }

    public abstract class BaseScramble : IScramble
    {
        protected readonly ScrambleType ScrambleType;

        private static Random _random = new Random();

        public BaseScramble(ScrambleType scrambleType)
        {
            ScrambleType = scrambleType;
        }

        public virtual void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            var sqlScript = GetSqlScript("SingleColumnSramble.sql");
            var sql = string.Format(sqlScript, configuration.TableName  //0
                                            , configuration.ColumnName  //1
                                            , ScrambleType              //2
                                            , configuration.Identifier  //3
                                            , _random.Next(30000)       //4
                                            , configuration.Schema);    //5

            using (var sqlCommand = new SqlCommand(sql, connection, transaction))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }
        }

        public virtual void AddScrambleData(SqlConnection connection, SqlTransaction transaction)
        {
            var scrambleData = GetScrambleData();

            var dataTable = new DataTable();
            dataTable.Columns.Add("@type", typeof(string));
            dataTable.Columns.Add("@value", typeof(string));
            dataTable.Columns.Add("@valueIndex", typeof(int));

            for (var i = 0; i < scrambleData.Count; i++)
            {
                dataTable.Rows.Add(ScrambleType.ToString(), scrambleData[i], i);
            }
            
            // make sure to enable triggers
            // more on triggers in next post
            var bulkCopy = new SqlBulkCopy(
                connection,
                SqlBulkCopyOptions.KeepIdentity,
                transaction
            );

            // set the destination table name
            bulkCopy.BatchSize = 500;
            bulkCopy.DestinationTableName = "#ScrambleData";

            // write the data in the "dataTable"
            bulkCopy.WriteToServer(dataTable);
        }

        protected abstract IList<string> GetScrambleData();

        protected string GetSqlScript(string fileName)
        {
            var location = $@"DatabaseScrambler.Scripts.{fileName}";
            return GetEmbededResource(location);
        }

        protected string GetResource(string fileName)
        {
            var location = $@"DatabaseScrambler.Resources.{fileName}";
            return GetEmbededResource(location);
        }

        private string GetEmbededResource(string location)
        {
            string script;

            var assembly = Assembly.GetExecutingAssembly();            

            using (var stream = assembly.GetManifestResourceStream(location))
            {
                if (stream == null)
                    throw new Exception($"file not found '{location}' is not found!");

                using (var reader = new StreamReader(stream))
                {
                    script = reader.ReadToEnd();
                }
            }

            return script;
        }

        public bool CanScramble(ScrambleType type)
        {
            return type == ScrambleType;
        }
    }
}
