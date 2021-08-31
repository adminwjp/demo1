using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility.Ioc
{
    public enum Scope
    {
        SingleInstnace,
        Scope,
        Transient
    }
    internal class IocEntity
    {
        public virtual Type Type { get; set; }
        public virtual Type ImplType { get; set; }
        public virtual Scope Scope { get; set; } = Scope.SingleInstnace;
        public virtual string Name { get; set; }
        public virtual object Instance { get; set; }
       public virtual Func<EmptyIocManager, object> GetInstance { get; set; }
        public virtual ConstructorInfo[] Constructors { get; set; }
        public virtual List<IocConstructorEntity> ConstructorParams { get; set; } = new List<IocConstructorEntity>(10);

        public virtual bool UnkowConstructorParamType { get; set; }
    }
    internal class IocConstructorEntity
    {
        public virtual ConstructorInfo Constructor { get; set; }
        public List<IocConstructorParamEntity> Params { get; set; }
        public virtual bool Unkow { get; set; }
    }
    internal class IocConstructorParamEntity
    {
        public virtual Type UnkowType { get; set; }
        public virtual IocEntity IocEntity { get; set; }
        public virtual bool Unkow { get; set; }
    }
    /// <summary>
    /// default ioc 只 支持 构造函数
    /// </summary>
    public class EmptyIocManager : IIocManager
    {
        public static bool IsSupportGeneric { get; set; } = false;
        internal List<IocEntity> IocEntities = new List<IocEntity>(10000);
        /// <summary>
        /// not implement
        /// </summary>
        public static readonly EmptyIocManager Empty = new EmptyIocManager();
        readonly object ObjLock = new object();
        bool update;
        /// <summary>
        /// 
        /// </summary>
        protected EmptyIocManager()
        {

        }
        protected virtual int Exists(Type type,Type ImplType,string name)
        {
            for (int i = 0; i < IocEntities.Count; i++)
            {
                var ioc = IocEntities[i];
                if(ioc.Type == ImplType && ioc.Type == type)
                {
                    if (ioc.Name.Equals(name))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public virtual void Add<T, ImplT>(string name = null, Scope scope = Scope.SingleInstnace) where ImplT : class
        {
            Type impl = typeof(ImplT);
            var type  = typeof(T);
            name = name ?? impl.Name;
            var index = -1;
            if ((index = Exists(type,impl, name)) < 0)
            {
                IocEntities.Add(new IocEntity() { ImplType = impl, Type = type, Name = name, Scope = scope,Constructors=impl.GetConstructors() });
            }
            else
            {
                //var ioc = IocEntities[index];
            }
        }
        public virtual void Add<T, ImplT>(Func<EmptyIocManager,T> instance,string name = null,Scope scope=Scope.SingleInstnace) where ImplT : class
        {
            Type impl = typeof(ImplT);
            var type = typeof(T);
            name = name ?? impl.Name;
            var index = -1;
            if ((index = Exists(type,impl, name)) < 0)
            {
                IocEntities.Add(new IocEntity() { ImplType = impl, Type = type, GetInstance=(iocManager)=>instance(iocManager), Name = name, Scope = scope });
            }
            else
            {
                //var ioc = IocEntities[index];
                IocEntities.RemoveAt(index);
                IocEntities.Insert(index,new IocEntity() { ImplType = impl, Type = type, GetInstance = (iocManager) => instance(iocManager), Name = name, Scope = scope });
            }
        }
        /// <summary>
        /// add transitent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ImplT"></typeparam>
        public virtual  void AddTransient<T, ImplT>(string name = null) where ImplT : class
        {
            Add<T, ImplT>(name, Scope.Transient);
        }

        /// <summary>
        /// add scope
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ImplT"></typeparam>
        public virtual void AddScoped<T, ImplT>(string name = null) where ImplT : class
        {
            Add<T, ImplT>(name, Scope.Scope);
        }

        /// <summary>
        /// add single instanc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ImplT"></typeparam>
        public virtual  void SingleInstance<T, ImplT>(string name = null) where ImplT : class
        {
            Add<T, ImplT>(name, Scope.SingleInstnace);
        }

        /// <summary>
        /// get object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T Get<T>(string name=null)
        {
            return (T)Get(typeof(T),name);
        }

        public virtual object Get(Type type, string name = null)
        {
            if (!update)
            {
                lock (ObjLock)
                {
                    if (!update)
                    {
                        Update();
                        update = true;
                    }
                }
            }
            IocEntity iocEntity = null;
            for (int i = 0; i < IocEntities.Count; i++)
            {
                var ioc = IocEntities[i];
                if (ioc.Type == type)
                {
                    if (name != null && ioc.Name.Equals(name))
                    {
                        iocEntity = ioc;
                        break;
                    }
                    else
                    {
                        iocEntity = ioc;
                        break;
                    }
                }
            }
            if (iocEntity != null)
            {
                if (iocEntity.GetInstance != null)
                {
                    if (iocEntity.Scope == Scope.SingleInstnace)
                    {
                        if (iocEntity.Instance != null)
                        {
                            return iocEntity.Instance;
                        }
                        lock (ObjLock)
                        {
                            if (iocEntity.Instance != null)
                            {
                                return iocEntity.Instance;
                            }
                            var obj= iocEntity.GetInstance(this);
                            iocEntity.Instance = obj;
                            return obj;
                        }
                    }
                    else
                    {
                        if (iocEntity.Scope == Scope.Scope && iocEntity.Instance != null)
                        {
                            return iocEntity.Instance;
                        }
                        var obj = iocEntity.GetInstance(this);
                        iocEntity.Instance = obj;
                        return obj;
                    }
                    
                }
                else
                {
                    if (iocEntity.Scope == Scope.SingleInstnace)
                    {
                        if (iocEntity.Instance != null)
                        {
                            return iocEntity.Instance;
                        }
                        lock (ObjLock)
                        {
                            if (iocEntity.Instance != null)
                            {
                                return iocEntity.Instance;
                            }
                            return Resolver(iocEntity);
                        }
                    }
                    else
                    {
                        if (iocEntity.Scope == Scope.Scope && iocEntity.Instance != null)
                        {
                            return iocEntity.Instance;
                        }
                        return Resolver(iocEntity);
                    }
                }
            }
            return null;
        }
        internal virtual void Update()
        {
            for (int i = 0; i < IocEntities.Count; i++)
            {
                var ioc = IocEntities[i];
                if (ioc.GetInstance != null)
                {
                    continue;
                }
                if (ioc.Constructors!=null&&ioc.Constructors.Length > 0)
                {
                    int unkowCount = 0;
                    ioc.ConstructorParams = new List<IocConstructorEntity>(ioc.Constructors.Length);
                    foreach (var constructor in ioc.Constructors)
                    {
                        List<IocConstructorParamEntity> paramTypes = new List<IocConstructorParamEntity>(ioc.Constructors.Length);
                        var con = new IocConstructorEntity() { Params=paramTypes};
                        ioc.ConstructorParams.Add(con);
                        int unkowParamCount = 0;
                        foreach (var param in constructor.GetParameters())
                        {
                            bool found = false;
                            for (int j = 0; j < IocEntities.Count; j++)
                            {
                                var nioc = IocEntities[j];
                                if(param.ParameterType==nioc.Type|| param.ParameterType == nioc.ImplType)
                                {
                                    paramTypes.Add(new IocConstructorParamEntity { IocEntity=nioc});
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                paramTypes.Add(new IocConstructorParamEntity { UnkowType = param.ParameterType,Unkow=true});
                                unkowParamCount++;
                            }
                        }
                        if (unkowParamCount == constructor.GetParameters().Length)
                        {
                            con.Unkow = true;
                            unkowCount++;
                        }

                    }
                    if(unkowCount== ioc.Constructors.Length)
                    {
                        ioc.UnkowConstructorParamType = true;
                    }
                }
            }
            //顺序 情况下 打乱 情况 下 需要 整理下 是否(构造函数)参数 存在
        }
         internal virtual object Resolver(IocEntity iocEntity)
        {
            if (iocEntity.UnkowConstructorParamType)
            {
                return  null;
            }
            var constructors = iocEntity.ConstructorParams;
            foreach (var constructor in constructors)
            {
                var parames = constructor.Params;
                if (parames == null || parames.Count == 0)
                {
                    var obj = Activator.CreateInstance(iocEntity.ImplType);
                    iocEntity.Instance = obj;
                    return obj;
                }
                else
                {
                    if (constructor.Unkow)
                    {
                        continue;
                    }
                    List<object> objs = new List<object>(parames.Count);
                    foreach (var p in parames)
                    {
                        objs.Add(Get(p.IocEntity.Type,p.IocEntity.Name));
                    }
                    var obj = Activator.CreateInstance(iocEntity.ImplType,objs.ToArray());
                    iocEntity.Instance = obj;
                    return obj;
                }
            }
            return null;
        }

        public IScopeIocManager CreateScope()
        {
            return new EmptyScopeIocManager();
        }

        internal class EmptyScopeIocManager : IScopeIocManager
        {
            public void Dispose()
            {
                
            }

            public T Get<T>(string name = null)
            {
                return default(T);
            }
        }
    }
}
