using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Database.Mapping;

namespace Utility.Test
{
    //example 

    #region example
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
    public class DbEntity : BaseEntity
    {

        public string Database { get; set; }
        public string ProgramName { get; set; }
        public ICollection<TableEntity> Tables { get; set; }
    }
    public class TableEntity : BaseEntity
    {
        public string Table { get; set; }
        public string ClassName { get; set; }

        public DbEntity Db { get; set; }

        public Guid DbId { get; set; }
        public ICollection<ColumnEntity> Columns { get; set; }
    }

    public class ColumnEntity : BaseEntity
    {

        private string columnName;

        public string ColumnName { get => !string.IsNullOrEmpty(columnName) ? PropertName : columnName; set => columnName = value; }
        public string PropertName { get; set; }

        public Type PropertyType { get; set; }
        public string MySqlValue { get; set; }
        public string SqlServerValue { get; set; }
        public string OracleValue { get; set; }
        public string SqliteValue { get; set; }
        public string PostgreValue { get; set; }
        public string Remark { get; set; }
        public string DefaultValue { get; set; }
        public string CheckValue { get; set; }
        public TableEntity Table { get; set; }
        public Guid TableId { get; set; }

        public string ReferenceId { get; set; }
        public string ReferenceTable { get; set; }
        public string ConstraintName { get; set; }
    }

    public abstract class BaseEntityMapp<Entity> : MappClass<Entity> where Entity : BaseEntity
    {
        public BaseEntityMapp(string tableName)
        {
            Table(tableName);
#if !(NET20 || NET30 || NET35)
            Id(it => it.Id).
#else
            Id().   
#endif
            Constraint($"PK_{tableName}_ID").Property("Id").Comment("主键");

        }
    }

    public class ColumnEntityMapp : MappClass<ColumnEntity>
    {
        public ColumnEntityMapp()
        {
            Table("T_Column");

#if !(NET20 || NET30 || NET35)
            Id(it => it.Id)
#else
        Id("Id")    
#endif
        .Constraint("PK_T_Column_ID").Comment("主键");

#if !(NET20 || NET30 || NET35)
            Property(it => it.PropertName).
#endif
        Property("PropertName").Comment("属性名称");


#if !(NET20 || NET30 || NET35)
            Property(it => it.ColumnName)
#else
           Property("ColumnName")
#endif
            .Comment("列名");



#if !(NET20 || NET30 || NET35)
            Property(it => it.MySqlValue).
#endif
        Property("MySqlValue").Comment("mysql 数据库数据类型");


#if !(NET20 || NET30 || NET35)
            Property(it => it.SqliteValue)
#else
            Property("SqliteValue")
#endif
       .Comment("sqlite 数据库数据类型");


#if !(NET20 || NET30 || NET35)
            Property(it => it.OracleValue)
#else
           Property("OracleValue")
#endif
        .Comment("oracle 数据库数据类型");

            Property("SqlServerValue")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.SqlServerValue)
#endif
                .Comment("sqlserver 数据库数据类型");

            Property("Remark")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.Remark)
#endif
                .Comment("备注");

            Property("DefaultValue")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.DefaultValue)
#endif                
                .Comment("默认值");

            Property("CheckValue")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.CheckValue)
#endif
                .Comment("Check 约束");

            OneOne("Table")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.Table)
#endif
                .Comment("表 外键 id 约束");

            Property("TableId")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.TableId)
#endif
                .Comment("表 外键 id 约束");


            Property("ReferenceId")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.ReferenceId)
#endif
                .Comment("外键 表 id 列");

            Property("ReferenceTable")
#if !(NET20 || NET30 || NET35)
                .Property(it => it.ReferenceTable)
                #endif
                .Comment("外键 表 ");

            Property("ConstraintName")
#if !(NET20 || NET30 || NET35)
           .Property(it => it.ConstraintName)
#endif
           .Comment("外键 名称 ");



        }
    }

    public class TableEntityMapp : BaseEntityMapp<TableEntity>
    {
        public TableEntityMapp() : base("T_Table")
        {
#if !(NET20 || NET30 || NET35)
            Property(it => it.Table).
#endif
        Property("Table").Comment("表名称 ");



#if !(NET20 || NET30 || NET35)
            OneMany(it => it.Columns)
#else
        OneMany("Columns")
#endif
        .ReferenceId("Id").Table("T_Column").Column("TableId").Comment("表名称 ");


#if !(NET20 || NET30 || NET35)
            OneMany(it => it.Db)
#else
            OneMany("Db")
#endif
            .ReferenceId("Id").Table("T_Db").Column("DbId").Comment("数据库外键  ");

#if !(NET20 || NET30 || NET35)
            Property(it => it.DbId)
#else
           Property("DbId")
#endif
.Comment("数据库外键 id ");
        }
    }
    public class DbEntityMapp : BaseEntityMapp<DbEntity>
    {
        public DbEntityMapp() : base("T_Db")
        {
#if !(NET20 || NET30 || NET35)
            Property(it => it.Database)
#else
            Property("Database")
#endif
                .Comment("表名称 ");



#if !(NET20 || NET30 || NET35)
            OneMany(it => it.Tables)
#else
            OneMany("Tables")
#endif
            .ReferenceId("Id").Table("T_Table").Column("TableId").Comment("外键表 集合 ");


        }
    }

    #endregion example
}
