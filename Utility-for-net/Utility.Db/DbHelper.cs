using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;

namespace Utility
{
    /// <summary>
    /// db helper
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// handler get db connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbConnection GetConnection(string connectionString,DbFlag flag= DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlConnection(connectionString);
                case DbFlag.MySql:
                    return new MySqlConnection(connectionString);
                case DbFlag.Sqlite:
                    return new SQLiteConnection(connectionString);
                case DbFlag.Oracle:
                    return new OracleConnection(connectionString);
                case DbFlag.Postgre:
                    return new NpgsqlConnection(connectionString);
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }


        /// <summary>
        /// handler get db command
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbCommand GetCommand(DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlCommand();
                case DbFlag.MySql:
                    return new MySqlCommand();
                case DbFlag.Sqlite:
                    return new SQLiteCommand();
                case DbFlag.Oracle:
                    return new OracleCommand();
                case DbFlag.Postgre:
                    return new NpgsqlCommand();
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// handler get db dataAdapter
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbDataAdapter GetDataAdapter(DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlDataAdapter();
                case DbFlag.MySql:
                    return new MySqlDataAdapter();
                case DbFlag.Sqlite:
                    return new SQLiteDataAdapter();
                case DbFlag.Oracle:
                    return new OracleDataAdapter();
                case DbFlag.Postgre:
                    return new NpgsqlDataAdapter();
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }


        /// <summary>
        /// handler get db dataAdapter
        /// </summary>
        /// <param name="command"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DbDataAdapter GetDataAdapter(DbCommand command,DbFlag flag = DbFlag.None)
        {
            switch (flag)
            {
                case DbFlag.SqlServer:
                    return new SqlDataAdapter((SqlCommand)command);
                case DbFlag.MySql:
                    return new MySqlDataAdapter((MySqlCommand)command);
                case DbFlag.Sqlite:
                    return new SQLiteDataAdapter((SQLiteCommand)command);
                case DbFlag.Oracle:
                    return new OracleDataAdapter((OracleCommand)command);
                case DbFlag.Postgre:
                    return new NpgsqlDataAdapter((NpgsqlCommand)command);
                case DbFlag.None:
                default:
                    break;
            }
            return null;
        }
    }
}
