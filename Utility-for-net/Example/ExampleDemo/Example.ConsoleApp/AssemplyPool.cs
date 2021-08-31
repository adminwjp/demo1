using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility
{
    public class AssemplyPool
    {
        private AssemplyPool()
        {

        }
        private readonly object _obj = new object();
        private readonly IDictionary<string, object> _objectPool = new ConcurrentDictionary<string, object>();
        protected int Total { get; set; } = 100;
        private bool IgnoreName { get; set; } = false;
        public void Scan(string package = "")
        {
            Assembly assembly;
            if (!string.Empty.Equals(package))
            {
                assembly = Assembly.Load(package);
            }
            else
            {
                //assembly = Assembly.GetEntryAssembly();
                assembly = Assembly.GetCallingAssembly();
                // assembly = Assembly.GetExecutingAssembly();
            }
            if (assembly != null)
            {
                using (IEnumerator<Module> modules = assembly.Modules.GetEnumerator())
                {
                    while (modules.MoveNext())
                    {
                        Module module = modules.Current;
                        foreach (Type type in module.GetTypes())
                        {
                            foreach (object item in type.GetCustomAttributes(false))
                            {
                                if (item is ServiceAttribute service)
                                {
                                    if (_objectPool.Count > this.Total)
                                    {
                                        throw new ArgumentOutOfRangeException(nameof(this.Total));
                                    }
                                    string name = service.Name.Equals(string.Empty) ? type.Name : service.Name;
                                    if (!_objectPool.ContainsKey(name))
                                    {
                                        _objectPool.Add(name, Activator.CreateInstance(type));
                                    }
                                    else
                                    {
                                        if (this.IgnoreName)
                                        {
                                            _objectPool[name] = Activator.CreateInstance(type);
                                        }
                                        else
                                        {
                                            throw new ArgumentException(nameof(name));
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public object this[string key]
        {
            get
            {
                return this._objectPool[key];
            }
        }
        public T Get<T>(string key) where T : class
        {
            return (T)this._objectPool[key];
        }
        public static AssemplyPool Instance
        {
            get
            {
                return ObjectInstance.objectPool;
            }
        }
        private class ObjectInstance
        {
            public static readonly AssemplyPool objectPool = new AssemplyPool();
        }
    }


    /// <summary>
    /// 资源特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class ResourceAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

    }
    /// <summary>
    /// 服务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ServiceAttribute : Attribute
    {
        public string Name { get; set; } = string.Empty;
    }
    /// <summary>
    /// 自动注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class AutowiredAttribute : Attribute
    {

    }
    /// <summary>
    /// 编码类型特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ContentTypeAttribute : Attribute
    {
        public string Name { get; set; } = string.Empty;
    }
}
