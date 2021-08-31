using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Utility.Database.Entities
{

    /// <summary>
    /// 外键信息
    /// </summary>
    public class FKColumnEntity
    {
        /// <summary> 外键表</summary>
        public string ReferenceTable { get; set; }

        /// <summary> 外键实体引用</summary>
        public Type ReferenceType { get; set; }
        /// <summary> 外键名称</summary>
        public string Constraint { get; set; }

        /// <summary>
        /// 外键 表 主键列
        /// </summary>
        public string ReferenceId { get; set; }
        /// <summary>
        /// 关联外键 即 外键类 的主键 属性名称
        /// </summary>
        public string PrimaryKeyPropertyName { get; set; }
        /// <summary>
        /// 关联外键 即 外键类 的主键 属性
        /// </summary>
        public PropertyInfo PrimaryKeyProperty { get; set; }

        /// <summary>
        ///   关联外键 即 外键类 的主键 属性 类型 用于 生成 表 sql 时 会用到 (数据库列类型)
        /// </summary>
        public Type PrimaryKeyPropertyType { get; set; }
        /// <summary>
        /// 关联 外键 实体 类映射
        /// </summary>
        public ClassEntity ForeignKeyClassEntry { get; set; }

        public DbFKFlag Update { get; set; }
        public DbFKFlag Delete { get; set; }

        public void ForeignKeyCascade(string val, bool update, DbFlag dialect)
        {
            if (dialect == DbFlag.Sqlite)
            {
                val = val.ToLower();
                DbFKFlag fKFlag = DbFKFlag.None;
                if (val == "cascade")
                {
                    fKFlag = DbFKFlag.Cascade;
                }
                else if (val == "noaction")
                {
                    fKFlag = DbFKFlag.NoAction;
                }
                else if (val == "restrict")
                {
                    fKFlag = DbFKFlag.Restrict;
                }
                else if (val == "setdefault")
                {
                    fKFlag = DbFKFlag.SetDefault;
                }
                else if (val == "setnull")
                {
                    fKFlag = DbFKFlag.SetNull;
                }
                if (update)
                {
                    Update = fKFlag;
                }
                else
                {
                    Delete = fKFlag;
                }
            }
        }
        public bool Lazy { get; set; }
        /// <summary>
        /// 没用:默认创建表时外键(特殊情况 相互关联时 表没创建创建外键会出错) 给个标识 
        /// </summary>
        public bool Handler { get; set; }

        /// <summary>
        /// 默认true：存在该列,false:不存在该列 但存在关系.
        ///  只不过没有外键列 说明需要关联 添加 更新 或 删除 或 查询时用到
        /// </summary>
        public bool Has { get; set; } = true;
        /// <summary>
        /// <summary>
        /// 相互关联 优先级 默认false
        /// </summary>
        internal bool OnConflict { get; set; }
        /// <summary> 
        /// 集合外键
        /// </summary>
        public BaseEntity Many { get; set; }
        /// <summary> 
        /// 单 外键
        /// </summary>
        public BaseEntity Single { get; set; }
        /// <summary>
        /// 是否是 自关联 
        /// </summary>
        public bool IsParent { get; set; }
        /// <summary> 
        /// 普通外键
        /// </summary>
        public BaseEntity Basic { get; set; }


#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
        /// <summary>
        /// 获取 外键 值 普通 外键 或 单 键 ,子集不获取
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Tuple<object, bool> GetValue(object obj)
        {
            if (this.Basic != null)
            {
                return new Tuple<object, bool>(ReflectHelper.GetValue(obj, Basic.PropertyName), true);
            }
            else if (this.Single != null)
            {
                return new Tuple<object, bool>(ReflectHelper.GetValue(obj, Single.PropertyName), true);
            }
            else
            {
                return new Tuple<object, bool>(null, false);
            }
        }

        /// <summary>
        /// 获取 外键 值 普通 外键 或 单 键 ,子集不获取
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object GetValue1(object obj)
        {
            if (this.Basic != null)
            {
                return ReflectHelper.GetValue(obj, Basic.PropertyName);
            }
            else if (this.Single != null)
            {
                return ReflectHelper.GetValue(obj, Single.PropertyName);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 设置 外键 值 普通 外键 或 单 键 ,子集不设置
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void SetValue(object obj, object val)
        {
            if (this.Basic != null)
            {
                ReflectHelper.SetValue(obj, Basic.PropertyName, val);
            }
            else if (this.Single != null)
            {
                ReflectHelper.SetValue(obj, Single.PropertyName, val);
            }
        }
        public void SetValue1(object obj, object primaryObj)
        {
            if (this.Basic != null)
            {
                object val = ReflectHelper.GetValue(primaryObj, ForeignKeyClassEntry.IdEntities[0].PropertyName);
                ReflectHelper.SetValue(obj, Basic.PropertyName, val);
            }
            else if (this.Single != null)
            {
                object val = ReflectHelper.GetValue(primaryObj, ForeignKeyClassEntry.IdEntities[0].PropertyName);
                ReflectHelper.SetValue(obj, Single.PropertyName, val);
            }
        }
#endif
    }
}
