using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Utility.Database.Driver;
using Utility.Database.Models;
using Utility.Database.Provider;

namespace Utility.Database.Adapater
{
    public class DbAdapater
    {
        public static DbAdapater Default = new DbAdapater();
        //all table and column(pk fk column identity index...)
        //数据库 数据 类型 不同 需要 转成 同一种 类型 好做处理 暂时 未处理 数据库的数据类型
        public virtual List<TableModel> GetTables(DbConnection connection, string db, DbFlag dialect = DbFlag.None)
        {
            if (dialect== DbFlag.None)
            {
                dialect = AbstractReflectDbDriver.GetDialect(connection.GetType());
                if (dialect == DbFlag.None) return null;
            }
            var tabs = AbstractDbProvider.FindTableByDatabase(connection, db, dialect);
            if (tabs != null && tabs.Count > 0)
            {
                List<TableModel> tables = new List<TableModel>();
                foreach (var tab in tabs)
                {
                    TableModel table = new TableModel() {Database=db, Comment = tab.Comment, Table = tab.Table };
                    tables.Add(table);
                    table.Columns = new List<ColumnModel>();
                    foreach (var pro in tab.PropertyEntities)
                    {
                        ColumnModel column = new ColumnModel() { Column=pro.Column,Comment=pro.Comment,Length=pro.Length,Table=tab.Table};
                        table.Columns.Add(column);
                        switch (pro.Flag)
                        {
                           case Entities.ColumnFlag.Column:
                                break;
                            case Entities.ColumnFlag.PrimaryKey:
                                column.Pk = true;
                                column.Identity = pro.Identity;
                                break;
                            case Entities.ColumnFlag.ForeignKey:
                                column.Fk = true;
                                column.ReferenceColumn = pro.FKColumnEntity.ReferenceId;
                                column.ReferenceTable = pro.FKColumnEntity.ReferenceTable;
                                column.ConstrainName = pro.FKColumnEntity.Constraint;
                                break;
                            case Entities.ColumnFlag.Ignore:
                                break;
                              case Entities.ColumnFlag.None:
                            default:
                                break;
                        }
                    }
                }
                return tables;
            }
            return null;

        }
    }
}
