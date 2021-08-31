using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database.Provider
{
    /// <summary>
    /// sqlserver 数据库 驱动操作
    /// </summary>
    public class SqlServerDbProvider : AbstractDbProvider
    { 
        //https://www.cnblogs.com/selene/p/4456789.html
        public const string CreateSqlServerDBByDropIfNotExistsSql = "go use master go if DB_ID('#database') is  null  create database #database";
        public const string CreateSqlServerDBByDropIfExistsSql = "go use master go if DB_ID('#database') is not null  drop database #database go  create database #database";
        public const string DropSqlServerDBByIfExistsSql = "go use master go if DB_ID('#database') is not null  drop database #database ";
        public const string CreateSqlServerDBByPathSql = "go use master go if DB_ID('#database') is not null  drop database #database go  create database #database on primary(name='#database',fileName='#path#database.mdf',size=5120kb,MaxSize=UNLIMITED ,FILEGROWTH=1024kb)log on(name='#database_log',fileName= '#path#database.ldf',SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%);";
        public const string SuffixSql = "on primary(name='#database',fileName='#path#database.mdf',size=5120kb,MaxSize=UNLIMITED ,FILEGROWTH=1024kb)log on(name='#database_log',fileName= '#path#database.ldf',SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)";
        public const string QuerySqlServerDBSql = "select count(1) from sys.databases where name=@name;";
        public const string DropSqlServerDBSql = "drop database #database;";
        public const string QueryDbVersionSql = "select    DATABASEPROPERTYEX('master','version');";

        //https://www.cnblogs.com/sharing1986687846/p/10265925.html

        public const string VersionSql = "select    DATABASEPROPERTYEX('master','version');";
        public const string IdentitySql = "select  AUTO_INCREMENT from information_schema.tables where table_name=@table";

        public const string IdentityPrefixSql = "select  AUTO_INCREMENT from information_schema.tables where ";

        public const string IdentitySufixSql = "table_name=@table";
        public static readonly SqlServerDbProvider Default = new SqlServerDbProvider();

    }
}
