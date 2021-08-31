using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{
    /// <summary>
    /// 属性映射普通列信息
    /// 注意:msqyl 自增必须主键 (其他版本不知道 5左右版本)
    /// </summary>
    public class PropertyEntity : BaseEntity
    {
        public PropertyEntity()
        {

        }
        public PropertyEntity(PropertyInfo property) : base(property)
        {

        }
        public virtual void UpdateColumn()
        {
            Column = string.IsNullOrEmpty(Column) ? PropertyName : Column;
        }
        /// <summary>
        /// true: 不创建 主键 使用时为主键 , 不创建 外键 使用时为外键 
        /// </summary>
        public bool IgnorePk { get; set; }
        /// <summary>主键列是否自增 </summary>
        public bool Identity { get; set; }
        /// <summary>
        /// 主键名称  主键 时有效(sqlite 才有 给不给 没影响)
        /// 关键字 处理 table 列 mysql:`table` sqlserver:[table] or "table" sqlite :`table` or [table] or "table"  or 'table'  oracle: "table"
        /// </summary>

        public string Constraint { get; set; }
        /// <summary>
        /// column name 设置列名 时 不为null 更新 各种数据库  列名 信息
        /// </summary>
        public string Column { get; set; }
        /// <summary>数据库类型 默认为null 或""  </summary>
        public string DataType{get;set;}
        public DbType DbType { get; set; }
        /// <summary>
        /// 格式化 统一 a =@a
        /// </summary>
        public string ColumnForamat { get; set; }
     


        /// <summary>
        /// column wether is null,default null (true)
        /// </summary>
        public bool IsNull { get; set; } = true;

        /// <summary>数据库列长度 字符串默认255 </summary>
        public long Length { get; set; } = 255;
        /// <summary>数据库列注释 </summary>
        public string Comment { get; set; }



        /// <summary>数据库列默认值 </summary>
        public string Default { get; set; }


        /// <summary>
        /// column check 约束 格式 话暂时 只能单  >  10 
        /// </summary>
        public string Check { get; set; }

        public void ToCheck(StringBuilder builder)
        {
            if (!string.IsNullOrEmpty(Check))
            {
                builder.Append(" CHECK( ").Append(Column).Append(" ").Append(Check).Append(") ");
            }
        }
        public bool ToForeignKey(StringBuilder builder)
        {
            if (FKColumnEntity.OnConflict)
            {
                return false;
            }
            builder.Append(",\r\n");
            if (!string.IsNullOrEmpty(FKColumnEntity.Constraint))
            {
                builder.Append("  CONSTRAINT  ").Append(FKColumnEntity.Constraint);
            }
            //builder.Append("  REFERENCES ")//跟列一起创建时
            builder.Append("  FOREIGN KEY (").Append(Column).Append(") REFERENCES ")//单独一块创建 最好放末尾 不然 包语法错误 最好统一
                .Append(FKColumnEntity.ReferenceTable).Append(" (").Append(FKColumnEntity.ReferenceId).Append(")");
            return true;
        }
        /// <summary>
        /// 默认列 时普通列
        /// </summary>
        public ColumnFlag Flag { get; set; } = ColumnFlag.Column;
        /// <summary>
        /// 是枚举类型?
        /// </summary>
        public bool IsEnum { get; set; }

        public FKColumnEntity FKColumnEntity { get; set; }

        public bool UseForeignKey()
        {
            if (FKColumnEntity != null)
            {
                if (FKColumnEntity.IsParent)
                    return true;
                if (FKColumnEntity.Single != null || FKColumnEntity.Basic != null)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// true 有效 列 . false: 无效 列
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            return Flag == ColumnFlag.PrimaryKey || Flag == ColumnFlag.Column || Flag == ColumnFlag.ForeignKey;
        }

        #region mysql 有效 
        public bool Unsigned { get; set; }
        public bool ZeroFill { get; set; }
        public bool AutoIncrement { get; set; }
        #endregion mysql 有效
    }
}
