using System;
using System.Collections.Generic;
using System.Text;
using Utility.Database.Entities;

namespace Utility.Database.Provider
{
    /// <summary>
    /// mysql 数据库 驱动操作
    /// </summary>
    public  class MySqlDbProvider : AbstractDbProvider
    {
        public string Engine { get; set; } = "INNODB";
        public string Charset { get; set; } = "utf8";

        public const string ShowDbSql = "SHOW DATABASES";

        public const string ExistsDbSql = "select count(1) from mysql.db where Db=@name";

        public const string VersionSql = "SELECT version();";

        public const string IdentitySql = "select  AUTO_INCREMENT from information_schema.tables where table_name=@table";

        public const string IdentityPrefixSql = "select  AUTO_INCREMENT from information_schema.tables where ";

        public const string IdentitySufixSql = "table_name=@table";

        public const string MySqlColumnSql = "SELECT* from information_schema.`COLUMNS`  WHERE TABLE_SCHEMA = @databaseName";

        public const string KeySql = "SELECT * from information_schema.KEY_COLUMN_USAGE  WHERE TABLE_SCHEMA=@databaseName";

        public static readonly MySqlDbProvider Default = new MySqlDbProvider();
        public MySqlDbProvider()
        {
            base.Dialect = DbFlag.MySql;
            base.Quot = "`";
        }

        
    }
}
