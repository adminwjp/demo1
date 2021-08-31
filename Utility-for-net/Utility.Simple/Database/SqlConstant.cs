using System.Collections.Generic;
using Utility.Database.Entities;

namespace Utility.Database
{
    internal struct SqlConstant
    {
        public static readonly string[] KeyWords = new string[] { DATABASE.ToLower(), TABLE.ToLower() , PROCEDURE.ToLower()
        ,VIEW.ToLower(),PrimayKey.ToLower(),"column","length"};
        public static readonly List<string> MySqlKeyWords = new List<string>(KeyWords);
        public static readonly List<string> SqlServerKeyWords = new List<string>(KeyWords);
        static SqlConstant()
        {
            MySqlKeyWords.Add("comment");
            SqlServerKeyWords.Add("isnull");
            SqlServerKeyWords.Add("constraint");
            // "name","enable","create","modify","drop","alert","desc"
        }
        public const string DATABASE = "DATABASE";
        public const string TABLE = "TABLE";
        public const string PROCEDURE = "PROCEDURE";
        public const string VIEW = "VIEW";

        public const string PrimayKey = "PRIMARY";
        public const string Index = "INDEX";
        public static string Replace(string key)
        {
            return key.Replace("`", string.Empty).Replace("\"",string.Empty);
        }

        /// <summary>
        /// mysql `table` sqlserver "table" sqlite `table` "table" 'table' [table]
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dialect"></param>
        /// <returns></returns>
        public static string KeyWordReplace(string key, DbFlag dialect = DbFlag.MySql)
        {
            switch (dialect)
            {
                case DbFlag.None:
                    break;
                case DbFlag.Oracle:
                    break;
                case DbFlag.MySql:
                    if (MySqlKeyWords.IndexOf(key.ToLower()) > -1)
                    {
                        return $"`{key}`";
                    }
                    return key;
                case DbFlag.SqlServer:
                case DbFlag.Sqlite:
                    if (SqlServerKeyWords.IndexOf(key.ToLower()) > -1)
                    {
                        return $"\"{key}\"";
                    }
                    return key;
                case DbFlag.Postgre:
                    break;
                case DbFlag.Access:
                    break;
                default:
                    break;
            }
            return key;
        }
    }
}
