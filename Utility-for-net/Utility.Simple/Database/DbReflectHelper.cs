using System;
using System.Collections.Generic;
using System.Text;
using Utility.Database.Entities;
using Utility.Database.Factory;
using Utility.Database.Provider;
using Utility.Helpers;

namespace Utility.Database
{
    public class DbReflectHelper
    {
        public static string ExecuteWhere(DbFactory dbFactory,object obj,ClassEntity classEntity,bool and=false)
        {
            var keyword = and ? " and " : " or ";
            StringBuilder builder = new StringBuilder(1000);
            foreach (var item in classEntity.PropertyEntities)
            {
                if (item.Flag == ColumnFlag.PrimaryKey|| item.Flag == ColumnFlag.Column)
                {
                    var val = ReflectHelper.GetValue(obj,item.Property.Name);
                    bool where = IfIsDefaultValue(val,item.Property.PropertyType);
                    if (where)
                    {
                        builder.Append(keyword).Append(item.Column).Append("=").Append(item.ColumnForamat);
                         dbFactory.CreateParameter(item.ColumnForamat,val);
                    }
                } 
            }
            var str = builder.ToString();
            if (str.Length > 0)
            {
                str = str.TrimStart(" and".ToCharArray());
            }
            return str;
        }
        public static bool IfIsDefaultValue(object val,Type propertyType)
        {
            if (TypeHelper.IsInterger(propertyType))
            {
                if (propertyType.IsAssignableFrom(typeof(Nullable<>)))
                {
                    long? val1 = (long?)Convert.ChangeType(val, typeof(long?));
                    if (val1.HasValue && val1 != 0)
                    {
                        return true;
                    }
                }
                else
                {
                    long val1 = (long)Convert.ChangeType(val, typeof(long));
                    if (val1 != 0)
                    {
                        return true;
                    }
                }
            }
            else if (TypeHelper.IsDecimal(propertyType))
            {
                if (propertyType.IsAssignableFrom(typeof(Nullable<>)))
                {
                    decimal? val1 = (decimal?)Convert.ChangeType(val, typeof(decimal?));
                    if (val1.HasValue && val1 != 0)
                    {
                        return true;
                    }
                }
                else
                {
                    decimal val1 = (decimal)Convert.ChangeType(val, typeof(decimal));
                    if (val1 !=0)
                    {
                        return true;
                    }
                }
            }
            else if (TypeHelper.IsString(propertyType))
            {
                if (!string.IsNullOrEmpty(val as string))
                {
                    return true;

                }
            }
            else
            {
                //do nothing
                return false;
            }
            return false;
        }

        public static long ExecuteInsert(DbFactory dbFactory, object obj, ClassEntity classEntity, bool and = false,Func<PropertyEntity,bool> func=null)
        {

            StringBuilder builder = new StringBuilder(1000);
            StringBuilder builder1 = new StringBuilder(1000);
            foreach (var item in classEntity.PropertyEntities)
            {
                bool skip = true;
                if (func == null)
                {
                    if (func(item))
                    {
                        skip = false;
                    }
                }
                if (item.Flag == ColumnFlag.PrimaryKey || item.Flag == ColumnFlag.Column)
                {
                    if (item.Identity)
                    {
                        continue;
                    }
                    skip = false;
                    
                }
                if (!skip)
                {
                    var val = ReflectHelper.GetValue(obj, item.Property.Name);
                    bool where = IfIsDefaultValue(val, item.Property.PropertyType);
                    if (where)
                    {
                        builder.Append(item.Column).Append(",");
                        builder1.Append(item.ColumnForamat).Append(",");
                        dbFactory.CreateParameter(item.ColumnForamat, val);
                    }
                }
            }
            var str = builder.ToString();
            if (str.Length > 0)
            {
                var str1 = builder1.ToString();
                str = str.TrimEnd(',');
                str1 = str1.TrimEnd(',');
                string sql = $"insert into {classEntity.Table}({str}) values({str1});";
                sql = AbstractDbProvider.ToIdentityInsertSql(dbFactory.Dialect, sql,classEntity.IsIdentity);
                dbFactory.Command.CommandText = sql;
                return AbstractDbProvider.ExecuteInsert(classEntity,dbFactory.Transaction, dbFactory.Command,obj,classEntity.IsIdentity,false);
            }
            return 0;
        }
    }
}
