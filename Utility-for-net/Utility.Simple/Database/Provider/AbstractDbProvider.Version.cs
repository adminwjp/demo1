using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Entities;
using Utility.Database.Provider;
using Utility.Database.Utils;

namespace Utility.Database.Provider
{
   public abstract partial class AbstractDbProvider
    {
        /// <summary>
        /// 获取 数据库 版本 
        /// 没有 则从 数据库 检索 暂时支持 mysql sqlite sqlserver
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dialect"></param>
        /// <returns></returns>
        public static string FindVersion(DbConnection connection, DbFlag dialect = DbFlag.MySql)
        {
            if(dialect!= DbFlag.Sqlite || dialect!=DbFlag.None){
                DatabaseUtils.Open(connection);//需要打开 sqlite 不需要
            }
            string StringVersion = connection.ServerVersion;
            if (!string.IsNullOrEmpty(StringVersion))
            {
                return StringVersion;
            }

            string versionSql = string.Empty;
            //db version sql
            switch (dialect)
            {
                case DbFlag.MySql:
                    versionSql = MySqlDbProvider.VersionSql;
                    break;
                case DbFlag.SqlServer:
                    versionSql = SqlServerDbProvider.VersionSql;//这个 查询 的 怎么跟 服务器 版本 不一样
                    break;
                case DbFlag.Sqlite:
                    versionSql = SqliteDbProvider.VersionSql;
                    break;

                case DbFlag.Postgre:
                case DbFlag.Oracle:
                case DbFlag.None:
                default:
                    return string.Empty;
            }
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = versionSql;
                using (IDataReader reader = DatabaseUtils.ExecuteReader(command))
                {
                    if (reader.Read())
                    {
                        StringVersion = reader.GetString(0);
                    }
                }
            }
            return StringVersion;
        }
    }
}
