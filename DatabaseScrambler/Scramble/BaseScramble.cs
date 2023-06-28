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

        void AddScrambleData(SqlConnection connection, SqlTransaction transaction, string culture);

        bool CanScramble(ScrambleType type);
    }

    public abstract class BaseScramble : IScramble
    {
        private readonly ScrambleType _scrambleType;
        private static readonly Random Random = new Random();

        public BaseScramble(ScrambleType scrambleType)
        {
            _scrambleType = scrambleType;
        }

        public virtual void Scramble(SqlConnection connection, SqlTransaction transaction, Configuration configuration)
        {
            var sqlScript = GetSqlScript("SingleColumnScramble.sql");
            var sql = string.Format(sqlScript, configuration.TableName  //0
                                            , configuration.ColumnName  //1
                                            , _scrambleType             //2
                                            , configuration.Identifier  //3
                                            , Random.Next(30000)        //4
                                            , configuration.Schema);    //5

            using (var sqlCommand = new SqlCommand(sql, connection, transaction))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }
        }

        public virtual void AddScrambleData(SqlConnection connection, SqlTransaction transaction, string culture)
        {
            var scrambleData = GetScrambleData(culture);

            var dataTable = new DataTable();
            dataTable.Columns.Add("@type", typeof(string));
            dataTable.Columns.Add("@value", typeof(string));
            dataTable.Columns.Add("@valueIndex", typeof(int));

            for (var i = 0; i < scrambleData.Count; i++)
            {
                dataTable.Rows.Add(_scrambleType.ToString(), scrambleData[i], i);
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

        protected abstract IList<string> GetScrambleData(string culture);

        /// <summary>
        /// Currently we only support DE as an optional culture
        /// </summary>
        protected static string ParseCulture(string culture)
        {
            switch (culture?.ToUpperInvariant())
            {
                case "DE":
                    return "de.";
                default:
                    return "";
            }
        }

        private static string GetSqlScript(string fileName)
        {
            var location = $@"DatabaseScrambler.Scripts.{fileName}";
            return GetEmbeddedResource(location);
        }

        protected static string GetResource(string fileName)
        {
            var location = $@"DatabaseScrambler.Resources.{fileName}";
            return GetEmbeddedResource(location);
        }

        private static string GetEmbeddedResource(string location)
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
            return type == _scrambleType;
        }
    }
}
