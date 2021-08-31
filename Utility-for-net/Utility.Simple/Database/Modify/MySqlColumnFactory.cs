using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database
{
    public enum ModifyColumnFlag
    {
        TableRename,
        AddColumn,
        ModifyColumn,
        DropColumn,
        ColumnRename,
        AddForeignKey,
        DropForeignKey,
        AddPrimaryKey,
        DropPrimaryKey,
        /// <summary>
        /// 添加唯一键
        /// </summary>
        AddUnique,
        DropUnique,
        AddIndex,
        DropIndex,
    }
    public class MySqlColumnFactory
    {
        public static readonly MySqlColumnFactory Empty = new MySqlColumnFactory();
        /// <summary>
        /// 修改表名
        /// <para>mysql:alter table oldTableName rename newTableName;</para>
        /// </summary>
        /// <returns></returns>
        public string TableRename(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.TableRename);
        }
        /// <summary>
        /// 表中修改字段
        /// <para>mysql:自增就是主键 不能有多个主键 不能修改列名</para>
        /// <para>ALTER TABLE tableName  modify COLUMN  columnName  dataType [CHARACTER SET utf8 COLLATE utf8_general_ci] [NOT NULL] default defaultValue [COMMENT '分类id']  [auto_increment |  , DROP PRIMARY KEY ]  ;</para>
        /// </summary>
        /// <returns></returns>
        public string ModifyColumn(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.AddColumn);
        }
        /// <summary>
        /// 表中修改字段
        /// <para>mysql:自增就是主键 不能有多个主键 只能修改列名</para>
        /// <para>ALTER TABLE tableName  change COLUMN  oldColumnName  newColumnName dataType [CHARACTER SET utf8 COLLATE utf8_general_ci]  [NOT NULL] default defaultValue [auto_increment] ;</para>
        /// </summary>
        /// <returns></returns>
        public string ChangeColumn(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.ColumnRename);
        }
        public string ToSqlByColumnOperator(ModifyColumnModel modifyColumnModel, ModifyColumnFlag flag)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ").Append(modifyColumnModel.Table);
            switch (flag)
            {
                case ModifyColumnFlag.TableRename:
                    builder.Append(" RENAME ").Append(modifyColumnModel.NewTable).Append(";");
                    return builder.ToString();
                case ModifyColumnFlag.AddColumn:
                    builder.Append(" ADD COLUMN ").Append(modifyColumnModel.Column);
                    break;
                case ModifyColumnFlag.ModifyColumn:
                    builder.Append(" MODIFY COLUMN ").Append(modifyColumnModel.Column);
                    break;
                case ModifyColumnFlag.DropColumn:
                    builder.Append(" DROP COLUMN ").Append(modifyColumnModel.Column).Append(";");
                    return builder.ToString();
                case ModifyColumnFlag.ColumnRename:
                    builder.Append(" CHANGE COLUMN ").Append(modifyColumnModel.Column).Append(" ").Append(modifyColumnModel.NewColumn);
                    break;
                case ModifyColumnFlag.AddForeignKey:
                    builder.Append(" ADD CONSTRAINT ").Append(modifyColumnModel.ConstraintName).Append("  FOREIGN KEY  (")
                        .Append(modifyColumnModel.Column).Append(") REFERENCES ")
                        .Append(modifyColumnModel.ReferenceTable).Append(" (").Append(modifyColumnModel.ReferenceColumn).Append(")").Append(";");
                    return builder.ToString();
                case ModifyColumnFlag.DropForeignKey:
                    builder.Append(" DROP   FOREIGN KEY ").Append(modifyColumnModel.ConstraintName).Append(";");
                    return builder.ToString();
                case ModifyColumnFlag.AddPrimaryKey:
                    //builder.Append(" DROP PRIMARY KEY,ADD PRIMARY KEY (`").Append(modifyColumnModel.Column).Append("`);");
                    builder.Append(" ADD PRIMARY KEY (`").Append(modifyColumnModel.Column).Append("`);");
                    return builder.ToString();
                case ModifyColumnFlag.DropPrimaryKey:
                    builder.Append(" DROP PRIMARY KEY;");
                    return builder.ToString();
                case ModifyColumnFlag.AddUnique:
                    {
                        var id = $"(`{modifyColumnModel.Column}`)";
                        builder.Append(" DROP UNIQUE  ").Append(id).Append(", ADD UNIQUE  ").Append(id).Append(";");
                        return builder.ToString();
                    }
                case ModifyColumnFlag.DropUnique:
                    {
                        var id = $"(`{modifyColumnModel.Column}`)";
                        builder.Append(" DROP UNIQUE  ").Append(id).Append(";");
                        return builder.ToString();
                    }
                case ModifyColumnFlag.AddIndex:
                    {
                        var id = $"(`{modifyColumnModel.Column}`)";
                        //builder.Append(" DROP INDEX ").Append(id).Append(", ADD INDEX ").Append(id).Append(";");
                        builder.Append(" ADD INDEX ").Append(id).Append(";");
                        return builder.ToString();
                    }
                case ModifyColumnFlag.DropIndex:
                    {
                        var id = $"(`{modifyColumnModel.Column}`)";
                        builder.Append(" DROP INDEX ").Append(id).Append(";");
                        return builder.ToString();
                    }
                default:
                    throw new NotSupportedException();
            }
            builder.Append(" ").Append(modifyColumnModel.DataType)
                .Append(" ").Append(modifyColumnModel.IsNotNull ? "NOT NULL " : " NULL ")
                .Append(string.IsNullOrEmpty(modifyColumnModel.DefaultValue) ? string.Empty : $"default {modifyColumnModel.DefaultValue} ");
            if (!string.IsNullOrEmpty(modifyColumnModel.Comment))
            {
                builder.Append(" COMMENT '").Append(modifyColumnModel.Comment).Append("' ");
            }
            builder.Append(modifyColumnModel.AutoIncreMent ? "auto_increment" : string.Empty);
            builder.Append(";");
            return builder.ToString();
        }
        /// <summary>
        /// 表中添加字段
        /// <para>mysql:自增就是主键 不能有多个主键</para>
        /// <para>ALTER TABLE tableName  add COLUMN  columnName  dataType [NOT NULL] default defaultValue [auto_increment] ;</para>
        /// </summary>
        /// <returns></returns>
        public string AddColumn(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.AddColumn);
        }
        /// <summary>
        /// 删除表中字段
        /// <para>mysql:ALTER TABLE tableName  drop COLUMN columnName ;</para>
        /// </summary>
        /// <returns></returns>
        public string DropColumn(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.DropColumn);
        }
        /// <summary>
        /// 添加外键
        /// <para>mysql:ALTER TABLE tableName ADD [CONSTRAINT constraintName]  FOREIGN KEY (columnName) REFERENCES referenceTabelName(referenceColumnName);</para>
        /// </summary>
        /// <returns></returns>
        public string AddForeignKey(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.AddForeignKey);
        }
        /// <summary>
        /// 删除外键
        /// <para>mysql:ALTER TABLE tableName drop  FOREIGN KEY columnName ;</para>
        /// </summary>
        /// <returns></returns>
        public string DropForeignKey(ModifyColumnModel modifyColumnModel)
        {
            return ToSqlByColumnOperator(modifyColumnModel, ModifyColumnFlag.DropForeignKey);
        }
    }
}
