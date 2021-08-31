using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Entities;
using Utility.Database.Factory;
using Utility.Database.Utils;
using Utility.Helpers;

namespace Utility.Database.Provider
{
    public abstract partial   class AbstractDbProvider
    {
        #region delete dml

        /// <summary>根据主键删除实体信息 </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="id">主键</param>
        /// <param name="classEntry">解析方式 公开的</param>
        /// <returns> </returns>
        public static int Delete(IDbConnection connection, object id, ClassEntity classEntry,IDbTransaction transaction=null)
        {
            if (id == null) return -1;
            if (classEntry.PkQuantity==0) return -1;
            IDbCommand command = DatabaseUtils.CreateCommand(connection, classEntry.SqlEntry.GetCache(SqlEntity.DeleteByWhereId));
            DatabaseUtils.SetCommandParamter(command, classEntry.IdEntities[0].Column, id);
            int res = DatabaseUtils.ExecuteNonQuery(command, transaction);
            return res;
        }
        /// <summary>根据主键删除实体信息 </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="obj">and delete</param>
        /// <param name="classEntry">解析方式 公开的</param>
        /// <returns> </returns>
        public static int Remove(IDbConnection connection, object obj, ClassEntity classEntry, IDbTransaction transaction = null)
        {
            if (obj == null) return -1;
            if (classEntry.PkQuantity == 0) return -1;
            IDbCommand command = DatabaseUtils.CreateCommand(connection, classEntry.SqlEntry.GetCache(SqlEntity.DeleteByWhereId));
            var sql = DbReflectHelper.ExecuteWhere(new DefaultFactory() {  Transaction = transaction, Command = command }, obj, classEntry, true);
            int res = DatabaseUtils.ExecuteNonQuery(command, transaction);
            return res;
        }
        /// <summary>删除单张表数据 TRUNCATE TABLE 删除数据比delete from 快 </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="table">单张表 关键字需要手动转换 
        /// mysql `table` sqlserver "table" sqlite `table` "table" 'table' [table]
        /// </param>
        /// <returns> </returns>
        public static int Truncate(IDbConnection connection, string table, IDbTransaction transaction = null)
        {
            ValidateHelper.ValidateArgumentNull("table", table);
            string sql = $"TRUNCATE TABLE {table};";
            int res = DatabaseUtils.ExecuteNonQuery(connection, sql,CommandType.Text,transaction);
            return res;
        }

        /// <summary>删除多张表数据 TRUNCATE TABLE 删除数据比delete from 快 </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="tables">多张表 关键字需要手动转换 
        /// mysql `table` sqlserver "table" sqlite `table` "table" 'table' [table]
        /// </param>
        /// <returns></returns>
        public static int Truncate(IDbConnection connection, string[] tables, IDbTransaction transaction = null)
        {
            ValidateHelper.ValidateArgumentArrayNull("tables", tables);
            var builder = new StringBuilder(50 * tables.Length);
            foreach (var item in tables)
            {
                builder.Append($"TRUNCATE TABLE {item};");
            }
            string sql = builder.ToString();
            int res = DatabaseUtils.ExecuteNonQuery(connection, sql,CommandType.Text,transaction);
            return res;
        }

        #endregion delete dml
    }
}
