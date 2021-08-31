using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Utility.Database.Entities;
using Utility.Helpers;

namespace Utility.Database
{ 
    /// <summary>
    /// SimpleDatabase 简单实现
    /// </summary>
    public class SimpleDatabaseFactory
    {
        public static Dictionary<string,object> ParseDbParam(string sql)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();//sql parameter
            ValidateHelper.ValidateArgumentNull("sql", sql);
            MatchCollection match = Regex.Matches(sql, "(\\?),?");
            int length = match != null && match.Count > 0 ? match.Count : 0;
            if (length == 0)
            {
                return values;
            }
            bool has = sql.IndexOf("?") > 0;
            foreach (Match item in match)
            {
                if (has)
                {
                    values.Add($"@{item.Groups[1].Value}", null);
                    int pointIndex = sql.IndexOf("?");
                    if (pointIndex > 0)
                    {
                        sql = sql.Remove(pointIndex, 1);
                        sql = sql.Insert(pointIndex - 1, $"@{item.Groups[1].Value}");
                    }
                }
                else
                {
                    values.Add(item.Groups[2].Value, null);
                }
            }
            return values;
        }
        public static SimpleDatabaseFactory Create()
        {
            return InstanceFactory.simpleDatabaseFactory;
        }
        public static SimpleDatabaseFactory CreateSync()
        {
            return InstanceFactory.simpleDatabaseFactorySync;
        }
        private class InstanceFactory
        {
            public static readonly SimpleDatabaseFactory simpleDatabaseFactory = new SimpleDatabaseFactory();
            public static readonly SimpleDatabaseFactorySync simpleDatabaseFactorySync = new SimpleDatabaseFactorySync();

        }
        public virtual int Insert()
        {
            return 0;
        }
        public virtual int Update()
        {
            return 0;
        }
        public virtual int Delete()
        {
            return 0;
        }
        public virtual int Get()
        {
            return 0;
        }
        public virtual int List()
        {
            return 0;
        }
    }

    /// <summary>
    /// SimpleDatabase 同步简单实现 
    /// </summary>
    public class SimpleDatabaseFactorySync: SimpleDatabaseFactory
    {
        static readonly object objLock = new object();
        public override int Insert()
        {
            lock (objLock)
            {
                return Insert();
            }
        }
        public override int Update()
        {
            lock (objLock)
            {
                return Insert();
            }
        }
        public override int Delete()
        {
            lock (objLock)
            {
                return Insert();
            }
        }
        public override int Get()
        {
            return 0;
        }
        public override int List()
        {
            lock (objLock)
            {
                return Insert();
            }
        }
    }
}
