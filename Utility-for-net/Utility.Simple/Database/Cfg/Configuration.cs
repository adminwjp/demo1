using System;
using System.Collections.Generic;
using Utility.Helpers;
using Utility.Collections;
using Utility.Database.Mapping;
using Utility.Database.Entities;

namespace Utility.Database
{
    public class Configuration
    {
        protected readonly Utility.Collections.Array<MappClass> MappClasses = new Utility.Collections.Array<MappClass>();
        protected readonly Utility.Collections.Array<ClassEntity> tableEntries = new Utility.Collections.Array<ClassEntity>();
        public DbFlag Dialect { get; set; }

       
        public void AddMapp(Type type)
        {
#if !(NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
            if (type == null || !type.IsAssignableFrom(typeof(MappClass)))
            {
                throw new Exception("type is  not null ,but type must  MappClass inherit");
            }
#endif
            MappClasses.Add((MappClass)Activator.CreateInstance(type));
        }
        public void AddMapps(Type[] types)
        {
            ValidateHelper.ValidateArgumentArrayNull("types", types);
            for (int i = 0; i < types.Length; i++)
            {
                AddMapp(types[i]);
            }
        }

        public void AddMapp(MappClass mappClass)
        {
            if (mappClass==null)
            {
                throw new Exception("mappClass is  not null");
            }
            MappClasses.Add(mappClass);
        }
        public void AddMapps(MappClass[] mappClasses)
        {
            ValidateHelper.ValidateArgumentArrayNull("mappClasses", mappClasses);
            for (int i = 0; i < mappClasses.Length; i++)
            {
                AddMapp(mappClasses[i]);
            }
        }
        public void Builder()
        {
            //过滤处理 相同 类型 或 相同表 
            if (MappClasses.Count > 0)
            {
                var temps=CollectionHelper.Distinct(MappClasses.ToArray(), ClassCompare.compare);
                MappClasses.Clear();
                MappClasses.InsertRange(temps);
            }
            //列信息 没有去重 或错误 验证
            for (int i = 0; i < MappClasses.Count; i++)
            {

            }
        }


        private class ClassCompare : IComparer<MappClass>
        {
            public static readonly ClassCompare compare = new ClassCompare();
            public int Compare(MappClass x, MappClass y)
            {
                if (x.TableEntry.ClassType == y.TableEntry.ClassType || x.TableEntry.Table.Equals(y.TableEntry.Table))
                {
                    return 0;
                }
                return -1;
            }
        }
    }

}
