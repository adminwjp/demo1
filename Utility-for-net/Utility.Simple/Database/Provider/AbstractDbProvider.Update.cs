using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Entities;
using Utility.Database.Utils;

namespace Utility.Database.Provider
{
    public abstract partial class AbstractDbProvider
    {
        #region update dml


        /// <summary>
        /// 根据主键修改
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntry">实体</param>
        /// <param name="obj">实体对象</param>
        /// <param name="skipNull">非空修改标识 默认false</param>
        /// <returns></returns>
        public static int Update(IDbConnection connection, ClassEntity classEntry, object obj,
            bool skipNull = false,IDbTransaction transaction=null)
        {
            if (obj == null) return -1;
            if (classEntry.PkQuantity == 0) return -1;
            if (skipNull)
            {
                return UpdateSkipNull(connection, classEntry, obj);
            }

            IDbCommand command = DatabaseUtils.CreateCommand(connection, classEntry.SqlEntry.GetCache(SqlEntity.UpdateByWhereId));
            foreach (PropertyEntity item in classEntry.PropertyEntities)
            {
                if (!item.Valid())
                {
                    continue;
                }
                if (!(item.Flag == ColumnFlag.Column || (item.Flag == ColumnFlag.ForeignKey && item.FKColumnEntity.Has)))
                {
                    continue;
                }
                object val;
                if (item.Flag == ColumnFlag.ForeignKey)
                {
                    val = item.FKColumnEntity.GetValue1(obj);
                }
                else
                {
                    val = item.GetValue(obj);
                }
                DatabaseUtils.SetCommandParamter(command, item.ColumnForamat, val);
            }
            int res = DatabaseUtils.ExecuteNonQuery(command,transaction);
            return res;
        }

        /// <summary>
        /// 根据主键修改  空 则不更新 
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="classEntry">实体</param>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        private static int UpdateSkipNull(IDbConnection connection, ClassEntity classEntry, object obj,
            IDbTransaction transaction = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE ").Append(classEntry.Table).Append(" SET ");
            IDbCommand command = connection.CreateCommand();
            string where = string.Format(" Where {0}={1}", classEntry.IdEntities[0].Column, classEntry.IdEntities[0].ColumnForamat);
            DatabaseUtils.SetCommandParamter(command, classEntry.IdEntities[0].ColumnForamat, classEntry.IdEntities[0].GetValue(obj));
            bool has = false;
            foreach (PropertyEntity item in classEntry.PropertyEntities)
            {
                if (!item.Valid())
                {
                    continue;
                }
                if (!(item.Flag == ColumnFlag.Column || (item.Flag == ColumnFlag.ForeignKey && item.FKColumnEntity.Has)))
                {
                    continue;
                }
                object val = null;
                if (item.Flag == ColumnFlag.ForeignKey)
                {
                    val = item.FKColumnEntity.GetValue1(obj);
                }
                else
                {
                    val = item.GetValue(obj);
                }
                //值是否非空 处理
                if (val == null)
                {
                    continue;
                }
                if (has)
                {
                    builder.Append(" , ");
                    has = false;
                }
                string column = item.Column;
                builder.Append(column).Append(" = ").Append(item.ColumnForamat);
                DatabaseUtils.SetCommandParamter(command, item.ColumnForamat, val);
                has = true;
            }
            command.CommandText = $"{builder.ToString()} {where}";
            int res = DatabaseUtils.ExecuteNonQuery(command,transaction);
            return res;
        }


        #endregion update dml
    }
}
