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
                                            , _random.Next(30000));     //4

            using (var sqlCommand = new SqlCommand(sql, connection, transaction))
            {
                sqlCommand.CommandTimeout = 0;
                sqlCommand.ExecuteNonQuery();
            }
        }

        public virtual void AddScrambleData(SqlConnection connection, SqlTransaction transaction)
        {
            var scrambleData = GetScrambleData();

            for (var i = 0; i < scrambleData.Count; i++)
            {
                var query = "insert into #ScrambleData ([Type], Value, ValueIndex) values (@type, @value, @valueIndex)";
                using (var command = new SqlCommand(query, connection, transaction))
                {
                    command.CommandTimeout = 0;

                    var typeParameter = new SqlParameter("@type", SqlDbType.NVarChar);
                    typeParameter.Value = ScrambleType.ToString();
                    var valueParameter = new SqlParameter("@value", SqlDbType.NVarChar);
                    valueParameter.Value = scrambleData[i];
                    var valueIndexParameter = new SqlParameter("@valueIndex", SqlDbType.Int);
                    valueIndexParameter.Value = i;

                    command.Parameters.Add(typeParameter);
                    command.Parameters.Add(valueParameter);
                    command.Parameters.Add(valueIndexParameter);
                    command.ExecuteNonQuery();
                }
            }
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
