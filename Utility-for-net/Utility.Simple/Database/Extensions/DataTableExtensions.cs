using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Utility.Database.Extensions
{

    public static  class DataTableExtensions
    {
        public static void Bind<T>(this DataTable dataTable, List<T> objs)where T:class,new()
        {
            if (dataTable.Rows.Count > 0)
            {
                Type type = typeof(T);
                foreach (DataRow row in dataTable.Rows)
                {
                    T obj = new T();
                    foreach (PropertyInfo pro in type.GetProperties())
                    {
                        if (row[pro.Name] != null)
                        {
                            pro.SetValue(obj, Convert.ChangeType(row[pro.Name], pro.PropertyType, CultureInfo.InvariantCulture));
                        }
                    }
                    objs.Add(obj);
                }
            }
        }
    }
}
