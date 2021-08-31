using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database.Provider
{

    /// <summary>
    /// oracle 数据库 驱动操作
    /// select {[distinct | all] columns|*} [into table_name] from [tables | views | other select]
    /// [where conditions] [group by columns] [having conditions] [order by columns]
    /// </summary>
    public class OracleDbProvider : AbstractDbProvider
    {
        //select* from v$version;或select banner from sys.v_$version;
        public const string VersionSql = "select* from v$version;";
        public static readonly OracleDbProvider Default = new OracleDbProvider();
        /// <summary>
        /// 查询表空间(需要一定权限)
        /// </summary>
        public const string QueryTableSpaceSql = "select * from v$tablespace;";
        /// <summary>
        /// 查询当前数据库中所有表名
        /// </summary>
        public const string QueryAllTableSql = "select * from user_tables;";
        /// <summary>
        /// 查询指定表中的所有字段信息
        /// </summary>
        public const string QueryColumnByTableSql = "select column_name from user_tab_columns where table_name = @tableName;";


        public static string GetPrefix(OperatorTypeFlag operatorTypeFlag, OperatorFlag flag)
        {
            string name = SqlConstant.PROCEDURE;
            switch (operatorTypeFlag)
            {
                case OperatorTypeFlag.Database:
                    name = SqlConstant.DATABASE;
                    break;
                case OperatorTypeFlag.Table:
                    name = SqlConstant.TABLE;
                    break;
                case OperatorTypeFlag.View:
                    name = SqlConstant.VIEW;
                    break;
                case OperatorTypeFlag.Procedure:
                default:
                    break;
            }
            switch (flag)
            {
                case OperatorFlag.Drop:
                case OperatorFlag.DropIfExists:
                    return $"DROP {name}  ";
                case OperatorFlag.CreateDropIfExists:
                case OperatorFlag.CreateIfNotExists:
                    return $"CREATE OR REPLACE {name} ";
                case OperatorFlag.Create:
                default: return $"CREATE {name}  ";
            }
        }
    }
}
