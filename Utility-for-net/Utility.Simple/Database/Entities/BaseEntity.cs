using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace Utility.Database.Entities
{
    #region BaseEntity

    /// <summary>
    /// base abstract class
    /// </summary>
    public class BaseEntity
    {
        public BaseEntity()
        {

        }
        public BaseEntity(PropertyInfo property)
        {
            Property = property;
        }

        public virtual void UpdateProperty()
        {
            if (TypeHelper.IsString(PropertyType) || TypeHelper.IsGuid(PropertyType) || TypeHelper.IsChar(PropertyType))
            {
                DbDataType = DbDataType.String;
            }
            else if (TypeHelper.IsInterger(PropertyType))
            {
                DbDataType = DbDataType.Integer;
            }

            else if (TypeHelper.IsBoolean(PropertyType))
            {
                DbDataType = DbDataType.Bool;
            }
            else if (TypeHelper.IsDecimal(PropertyType))
            {
                DbDataType = DbDataType.SmallNumer;
            }
            else
            {
                DbDataType = DbDataType.String;
            }
        }

        /// <summary>
        /// 对应的数据 类型 用于比较 实际表 中 类型 操作
        /// </summary>
        public DbDataType DbDataType { get; set; }

        public string PropertyName { get;  set; }

        public PropertyInfo Property { get; set; }
        public Type PropertyType { get;  set; }


#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

        public virtual object GetValue(object obj)
        {
            //return Property.GetValue(obj, null);
            return ReflectHelper.GetValue(obj, PropertyName);
        }
        public virtual void SetValue(object obj, object val)
        {
            //Property.SetValue(obj, val, null);
            ReflectHelper.SetValue(obj, PropertyName, val);
        }
#endif
    }

    #endregion BaseEntry
}
