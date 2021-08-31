#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1  || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac;
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
#endif
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Utility.Ioc
{
    /// <summary>
    ///<see cref="Container"/> be based on Autofac implement ioc
    /// </summary>
    public class AutofacIocManager:IIocManager
    {
      
        public static readonly ThreadLocal<IDictionary<Type, object>> AopWrapperType = new ThreadLocal<IDictionary<Type, object>>
            (()=>new ConcurrentDictionary<Type,object>(1,1000));
        /// <summary>
        /// inner class
        /// </summary>
        class Inner
        {

            /// <summary>
            /// create instance
            /// </summary>
            public static readonly AutofacIocManager Empty = new AutofacIocManager();
        }

        /// <summary>
        /// single instance 
        /// </summary>
        public static AutofacIocManager Instance { get { return Inner.Empty; } }

        /// <summary>
        /// no param constractor
        /// </summary>
        public AutofacIocManager()
        {

        }

        /// <summary>
        /// has  param constractor
        /// </summary>
        /// <param name="action"> injection operator</param>
        public AutofacIocManager(Action<ContainerBuilder> action)
        {
            Register(action);
        }
        public virtual void AddTransient<T, ImplT>(string name = null) where ImplT : class
        {
            var register = Builder.RegisterType<ImplT>().As<T>();
            if(name!=null){
                register = register.Named<T>(name);
            }
            register.InstancePerDependency();
        }
        public virtual void AddScoped<T, ImplT>(string name = null) where ImplT : class
        {
           var register= Builder.RegisterType<ImplT>().As<T>();
            if (name != null)
            {
                register = register.Named<T>(name);
            }
            register.InstancePerLifetimeScope();
        }

        public virtual void SingleInstance<T, ImplT>(string name = null) where ImplT : class
        {
            var register = Builder.RegisterType<ImplT>().As<T>();
            if (name != null)
            {
                register = register.Named<T>(name);
            }
            register.SingleInstance();
        }
        public virtual T Get<T>(string name=null)
        {
            return Resolver<T>(name);
        }
        private ContainerBuilder _builder; // declare container builder

        private IContainer _container;// declare container

        /// <summary> 
        /// initial container builder
        /// </summary>
        public virtual ContainerBuilder Builder
        {
            get
            {
                if (_builder == null)
                {
                    _builder = new ContainerBuilder();//instance
                }
                return _builder;
            }
            set { _builder = value; }
        }


        /// <summary>
        /// injection operator
        /// </summary>
        /// <param name="action">injection operator</param>
        public virtual void Register(Action<ContainerBuilder> action)
        {
            action?.Invoke(Builder);
        }

#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
        /// <summary>
        /// annotation injection operator
        /// </summary>
        /// <param name="assemblyies">Assembly</param>
        /// <param name="allowCircularDependencies">loop?</param>
        public virtual void Register(Assembly[] assemblyies,bool allowCircularDependencies =false)
        {
            foreach(Assembly assembly in assemblyies)
            {
                if(allowCircularDependencies){
                     Builder.RegisterModule(new AutofacAnnotationModule(assembly).SetAllowCircularDependencies(true));
                }else{
                    Builder.RegisterModule(new AutofacAnnotationModule(assembly));
                }
            }
        }
#endif

        /// <summary>
        ///initial container 
        ///asp.net core 组合
        /// </summary>
        public virtual IContainer Container 
        {
            get
            {
                if (_container == null)
                {
                    _container = Builder.Build();//instnbce
                }
                return _container;
            }
            set{ _container = value; } 
        }

        /// <summary>
        /// according name or type get dependency class
        /// </summary>
        /// <typeparam name="T">dependency class generic </typeparam>
        /// <param name="name">dependency name,can is null </param>
        /// <returns></returns>
        public virtual T Resolver<T>(string name = null)
        {
            T t; 
            var type = typeof(T);
            if (name == null)
            {
                if (!EmptyIocManager.IsSupportGeneric)
                {
                    t = Container.Resolve<T>();
                }
                else
                {
                    var obj = Container.Resolve(type);
                    //error as T not support as int pass
                    //system.runtime
                    t = (T)Convert.ChangeType(obj, type);
                    if(obj is T)
                    {
                        return (T)obj;
                    }
                    t = (T)Container.Resolve(type);//aop 
                }
            }
            else
            {
                if (!EmptyIocManager.IsSupportGeneric)
                {
                    t = Container.ResolveNamed<T>(name);
                }
                else
                {
                    t = (T)Container.ResolveNamed(name, type); //aop 
                }
            }
            return t;
        }

        /// <summary>
        /// according name or type get dependency class
        /// </summary>
        /// <typeparam name="type">dependency class generic </typeparam>
        /// <param name="name">dependency name,can is null </param>
        /// <returns></returns>
        public virtual object ResolverType(Type type,string name = null)
        {
            if (name == null)
            {
                return Container.Resolve(type);
            }
            else
            {
                return  Container.ResolveNamed(name, type); 
            }
        }
        public IScopeIocManager CreateScope()
        {
            return new AutofacScopeIocManager(this);
        }
        internal class AutofacScopeIocManager : IScopeIocManager,IDisposable
        {
            ILifetimeScope lifetimeScope;

            public AutofacScopeIocManager(AutofacIocManager iocManager)
            {
                this.lifetimeScope = iocManager.Container.BeginLifetimeScope();
               
            }

            public void Dispose()
            {
                lifetimeScope?.Dispose();
            }

            public T Get<T>(string name = null)
            {
                T t;
                if (name == null)
                {
                    t = lifetimeScope.Resolve<T>();
                }
                else
                {
                    t = lifetimeScope.ResolveNamed<T>(name);
                }
                return t;
            }
        }
    }
}
#endif