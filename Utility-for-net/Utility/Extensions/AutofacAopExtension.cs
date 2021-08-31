using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.OpenGenerics;
using Autofac.Features.Scanning;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Utility.Extensions
{
    public static class AutofacAopExtension
    {
        private const string InterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName";

        private const string AttributeInterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName";

        private static readonly IEnumerable<Service> EmptyServices = Enumerable.Empty<Service>();

        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

      
        public static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> 
            EnableClassInterceptors1<TLimit, TRegistrationStyle>(this IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration)
        {
            return registration.EnableClassInterceptors(ProxyGenerationOptions.Default);
        }

        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> 
            EnableClassInterceptors1<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration) where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {
            return registration.EnableClassInterceptors(ProxyGenerationOptions.Default);
        }

        public static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> 
            EnableClassInterceptors1<TLimit, TRegistrationStyle>(this IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration, ProxyGenerationOptions options, params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            registration.ActivatorData.ConfigurationActions.Add(delegate (Type t, IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> rb)
            {
                rb.EnableClassInterceptors(options, additionalInterfaces);
            });
            return registration;
        }

       
        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
            EnableClassInterceptors1<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(this 
            IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options, params Type[] additionalInterfaces) 
            where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            registration.ActivatorData.ImplementationType = ProxyGenerator.ProxyBuilder.CreateClassProxyType(registration.ActivatorData.ImplementationType, additionalInterfaces ?? Type.EmptyTypes, options);
            IEnumerable<Service> interceptorServicesFromAttributes = GetInterceptorServicesFromAttributes(registration.ActivatorData.ImplementationType);
            AddInterceptorServicesToMetadata(registration, interceptorServicesFromAttributes, "Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName");
            registration.OnPreparing(delegate (PreparingEventArgs e)
            {
                List<Parameter> list = new List<Parameter>();
                int position = 0;
                if (options.HasMixins)
                {
                    foreach (object mixin in options.MixinData.Mixins)
                    {
                        list.Add(new PositionalParameter(position++, mixin));
                    }
                }

                list.Add(new PositionalParameter(position++, (from s in GetInterceptorServices(e.Component, registration.ActivatorData.ImplementationType)
                                                              select e.Context.ResolveService(s)).Cast<IInterceptor>().ToArray()));
                if (options.Selector != null)
                {
                    list.Add(new PositionalParameter(position, options.Selector));
                }

                e.Parameters = list.Concat<Parameter>(e.Parameters).ToArray();
            });
            return registration;
        }
        public static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle>
            EnableClassInterceptorsByGeneric<TLimit, TRegistrationStyle>(this 
            IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration)
        {
            return registration.EnableClassInterceptorsByGeneric(ProxyGenerationOptions.Default);
        }

        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
            EnableClassInterceptorsByGeneric<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
            (this IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> 
            registration) where TConcreteReflectionActivatorData : ReflectionActivatorData
        {
            return registration.EnableClassInterceptorsByGeneric(ProxyGenerationOptions.Default);
        }

        public static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle>
            EnableClassInterceptorsByGeneric<TLimit, TRegistrationStyle>(this 
            IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options, params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            registration.ActivatorData.ConfigurationActions.Add(delegate (Type t, IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> rb)
            {
                rb.EnableClassInterceptorsByGeneric(options, additionalInterfaces);
            });
            return registration;
        }
        
        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
        EnableClassInterceptorsByGeneric<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(this
        IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration,
        ProxyGenerationOptions options, params Type[] additionalInterfaces)
        //where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        where TConcreteReflectionActivatorData : ReflectionActivatorData
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }
            registration.ConfigurePipeline(delegate (IResolvePipelineBuilder p)
            {
                p.Use(PipelinePhase.Activation, MiddlewareInsertionMode.StartOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                   
                    //interface
                    //EnsureInterfaceInterceptionApplies(ctxt.Registration);
                    //Type[] source = ctxt.Instance?.GetType().GetInterfaces().Where(ProxyUtil.IsAccessible)
                    //    .ToArray();
                    //if (source.Any())
                    //{
                    //    Type interfaceToProxy = source.First();
                    //    Type[] additionalInterfacesToProxy = source.Skip(1).ToArray();

                    //}
                    //包装 后 怎么 返回 原来 对象
                    Type interfaceToProxy = ctxt.Instance?.GetType();
                    ctxt.Instance = ProxyGenerator.ProxyBuilder.CreateClassProxyType(interfaceToProxy, additionalInterfaces ?? Type.EmptyTypes, options);
                    //if (!Ioc.AutofacIocManager.AopWrapperType.Value.ContainsKey(interfaceToProxy))
                    //{
                    //    Ioc.AutofacIocManager.AopWrapperType.Value.Add(interfaceToProxy, ctxt.Instance);
                   // }

                    //泛型 类型 不知道 没法 创建
                    //class
                    //registration.ActivatorData.ImplementationType = ProxyGenerator.ProxyBuilder.CreateClassProxyType(registration.ActivatorData.ImplementationType, additionalInterfaces ?? Type.EmptyTypes, options);
                    //IEnumerable<Service> interceptorServicesFromAttributes = GetInterceptorServicesFromAttributes(registration.ActivatorData.ImplementationType);
                    //AddInterceptorServicesToMetadata(registration, interceptorServicesFromAttributes, "Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName");
                    //registration.OnPreparing(delegate (PreparingEventArgs e)
                    //{
                    //    List<Parameter> list = new List<Parameter>();
                    //    int position = 0;
                    //    if (options.HasMixins)
                    //    {
                    //        foreach (object mixin in options.MixinData.Mixins)
                    //        {
                    //            list.Add(new PositionalParameter(position++, mixin));
                    //        }
                    //    }

                    //    list.Add(new PositionalParameter(position++, (from s in GetInterceptorServices(e.Component, registration.ActivatorData.ImplementationType)
                    //                                                  select e.Context.ResolveService(s)).Cast<IInterceptor>().ToArray()));
                    //    if (options.Selector != null)
                    //    {
                    //        list.Add(new PositionalParameter(position, options.Selector));
                    //    }

                    //    e.Parameters = list.Concat<Parameter>(e.Parameters).ToArray();
                    //});
                });
                //System.InvalidOperationException:“Cannot add service middleware in phase 'ResolveRequestStart' to a registration pipeline. Valid service middleware phases: 
                //[ResolveRequestStart, ScopeSelection, Decoration, Sharing, ServicePipelineEnd]”
                //ServicePipelineEnd StartOfPhase EndOfPhase ex
                p.Use(PipelinePhase.RegistrationPipelineStart, MiddlewareInsertionMode.EndOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                    
                    
                });


            });
         
            return registration;
        }


        public static IRegistrationBuilder<TLimit, OpenGenericDelegateActivatorData, TRegistrationStyle>
       EnableClassInterceptorsByOpenGeneric<TLimit, TRegistrationStyle>(this
       IRegistrationBuilder<TLimit, OpenGenericDelegateActivatorData, TRegistrationStyle> registration)
        {
            return registration.EnableClassInterceptorsByOpenGeneric(ProxyGenerationOptions.Default);
        }

        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
            EnableClassInterceptorsByOpenGeneric<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
            (this IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
            registration) where TConcreteReflectionActivatorData : OpenGenericDelegateActivatorData
        {
            return registration.EnableClassInterceptorsByOpenGeneric(ProxyGenerationOptions.Default);
        }

        public static IRegistrationBuilder<TLimit, OpenGenericDelegateActivatorData, TRegistrationStyle>
            EnableClassInterceptorsByOpenGeneric<TLimit, TRegistrationStyle>(this
            IRegistrationBuilder<TLimit, OpenGenericDelegateActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options, params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }
          
            //registration.ActivatorData.Factory.ConfigurationActions.Add(delegate (Type t, IRegistrationBuilder<object, OpenGenericDelegateActivatorData, DynamicRegistrationStyle> rb)
            //{
            //    rb.EnableClassInterceptorsByOpenGeneric(options, additionalInterfaces);
            //});
            return registration;
        }

        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>
      EnableClassInterceptorsByOpenGeneric<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(this
      IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration,
      ProxyGenerationOptions options, params Type[] additionalInterfaces)
      //where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
      where TConcreteReflectionActivatorData : OpenGenericDelegateActivatorData
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }
            registration.ConfigurePipeline(delegate (IResolvePipelineBuilder p)
            {
                p.Use(PipelinePhase.Activation, MiddlewareInsertionMode.StartOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                   
                    //包装 后 怎么 返回 原来 对象
                    Type interfaceToProxy = ctxt.Instance?.GetType();
                    ctxt.Instance = ProxyGenerator.ProxyBuilder.CreateClassProxyType(interfaceToProxy, additionalInterfaces ?? Type.EmptyTypes, options);
                    
                });
                p.Use(PipelinePhase.RegistrationPipelineStart, MiddlewareInsertionMode.EndOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                });
            });

            return registration;
        }
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
           EnableInterfaceInterceptorsAsync<TLimit, TActivatorData, TSingleRegistrationStyle,TI>(this
           IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration)
        {
            return EnableInterfaceInterceptorsAsync(registration,  typeof(TI));
        }

        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
      EnableInterfaceInterceptorsAsync<TLimit, TActivatorData, TSingleRegistrationStyle>(this
      IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration
     , params Type[] additionalInterfaces)
        {
            return EnableInterfaceInterceptorsAsync(registration, ProxyGenerationOptions.Default, additionalInterfaces);
        }


        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
          EnableInterfaceInterceptorsAsync<TLimit, TActivatorData, TSingleRegistrationStyle>(this
          IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration,
          ProxyGenerationOptions options = null, params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            registration.ConfigurePipeline(delegate (IResolvePipelineBuilder p)
            {
                p.Use(PipelinePhase.Activation, MiddlewareInsertionMode.StartOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                    EnsureInterfaceInterceptionApplies(ctxt.Registration);
                    //Type[] source = ctxt.Instance?.GetType().GetInterfaces().Where(ProxyUtil.IsAccessible)
                                        // .ToArray();
                   // if (source.Any())
                    {
                        Type interfaceToProxy = additionalInterfaces[0]; //source.First();
                        //Type[] additionalInterfacesToProxy = additionalInterfaces;// new Type[] { interfaceToProxy};
                        // IAsyncInterceptor[]
                        IInterceptor[]
                        interceptors = (from s in GetInterceptorServices(ctxt.Registration, ctxt.Instance?.GetType())
                                                       select ctxt.ResolveService(s))
                                                      // .Cast<IAsyncInterceptor>()
                                                      .Cast<IInterceptor>()
                                                       .ToArray();
                        ctxt.Instance = ((options == null) ? ProxyGenerator.CreateInterfaceProxyWithTarget(interfaceToProxy, additionalInterfaces, ctxt.Instance, interceptors) : ProxyGenerator.CreateInterfaceProxyWithTarget(interfaceToProxy, additionalInterfaces, ctxt.Instance, options, interceptors));
                    }

                });
            });
            return registration;
        }

        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
        EnableInterfaceInterceptors1<TLimit, TActivatorData, TSingleRegistrationStyle>(this
        IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration,
         params Type[] additionalInterfaces)
        {
            return EnableInterfaceInterceptors1(registration,ProxyGenerationOptions.Default,additionalInterfaces);
        }


        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
          EnableInterfaceInterceptors1<TLimit, TActivatorData, TSingleRegistrationStyle>(this
          IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration,
          ProxyGenerationOptions options = null, params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            registration.ConfigurePipeline(delegate (IResolvePipelineBuilder p)
            {
                p.Use(PipelinePhase.Activation, MiddlewareInsertionMode.StartOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                    EnsureInterfaceInterceptionApplies(ctxt.Registration);
                    
                        IInterceptor[] interceptors = (from s in GetInterceptorServices(ctxt.Registration, ctxt.Instance?.GetType())
                                                       select ctxt.ResolveService(s)).Cast<IInterceptor>().ToArray();
                        ctxt.Instance = ((options == null) ? ProxyGenerator.CreateInterfaceProxyWithTarget(additionalInterfaces[0], additionalInterfaces, ctxt.Instance, interceptors) : ProxyGenerator.CreateInterfaceProxyWithTarget(additionalInterfaces[0], additionalInterfaces, ctxt.Instance, options, interceptors));
                  
                });
            });
            return registration;
        }


        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle>
            EnableInterfaceInterceptors1<TLimit, TActivatorData, TSingleRegistrationStyle>(this 
            IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration,
            ProxyGenerationOptions options = null)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            registration.ConfigurePipeline(delegate (IResolvePipelineBuilder p)
            {
                p.Use(PipelinePhase.Activation, MiddlewareInsertionMode.StartOfPhase, delegate (ResolveRequestContext ctxt, Action<ResolveRequestContext> next)
                {
                    next(ctxt);
                    EnsureInterfaceInterceptionApplies(ctxt.Registration);
                    Type[] source = ctxt.Instance?.GetType().GetInterfaces().Where(ProxyUtil.IsAccessible)
                        .ToArray();
                    if (source.Any())
                    {
                        Type interfaceToProxy = source.First();
                        Type[] additionalInterfacesToProxy = source.Skip(1).ToArray();
                        IInterceptor[] interceptors = (from s in GetInterceptorServices(ctxt.Registration, ctxt.Instance?.GetType())
                                                       select ctxt.ResolveService(s)).Cast<IInterceptor>().ToArray();
                        ctxt.Instance = ((options == null) ? ProxyGenerator.CreateInterfaceProxyWithTarget(interfaceToProxy, additionalInterfacesToProxy, ctxt.Instance, interceptors) : ProxyGenerator.CreateInterfaceProxyWithTarget(interfaceToProxy, additionalInterfacesToProxy, ctxt.Instance, options, interceptors));
                    }
                });
            });
            return registration;
        }

        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy1<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder, params Service[] interceptorServices)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            if (interceptorServices == null || interceptorServices.Any((Service s) => s == null))
            {
                throw new ArgumentNullException("interceptorServices");
            }

            AddInterceptorServicesToMetadata(builder, interceptorServices, "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName");
            return builder;
        }

      
   
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy1<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder, params string[] interceptorServiceNames)
        {
            if (interceptorServiceNames == null || interceptorServiceNames.Any((string n) => n == null))
            {
                throw new ArgumentNullException("interceptorServiceNames");
            }

            Service[] interceptorServices = interceptorServiceNames.Select((string n) => new KeyedService(n, typeof(IInterceptor))).ToArray();
            return builder.InterceptedBy(interceptorServices);
        }

        
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy1<TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder, params Type[] interceptorServiceTypes)
        {
            if (interceptorServiceTypes == null || interceptorServiceTypes.Any((Type t) => t == null))
            {
                throw new ArgumentNullException("interceptorServiceTypes");
            }

            Service[] interceptorServices = interceptorServiceTypes.Select((Type t) => new TypedService(t)).ToArray();
            return builder.InterceptedBy(interceptorServices);
        }

        private static void EnsureInterfaceInterceptionApplies(IComponentRegistration componentRegistration)
        {
            if ((from s in componentRegistration.Services.OfType<IServiceWithType>()
                 select new Tuple<Type, TypeInfo>(s.ServiceType, s.ServiceType.GetTypeInfo())).Any((Tuple<Type, TypeInfo> s) => !s.Item2.IsInterface || !ProxyUtil.IsAccessible(s.Item1)))
            {
                throw new InvalidOperationException("EnsureInterfaceInterceptionApplies");
            }
        }

        private static void AddInterceptorServicesToMetadata<TLimit, TActivatorData, TStyle>(IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder, IEnumerable<Service> interceptorServices, string metadataKey)
        {
            if (builder.RegistrationData.Metadata.TryGetValue(metadataKey, out object value))
            {
                builder.RegistrationData.Metadata[metadataKey] = ((IEnumerable<Service>)value).Concat(interceptorServices).Distinct();
            }
            else
            {
                builder.RegistrationData.Metadata.Add(metadataKey, interceptorServices);
            }
        }

        private static IEnumerable<Service> GetInterceptorServices(IComponentRegistration registration, Type implType)
        {
            if (registration == null)
            {
                throw new ArgumentNullException("registration");
            }

            if (implType == null)
            {
                throw new ArgumentNullException("implType");
            }

            IEnumerable<Service> first = EmptyServices;
            if (registration.Metadata.TryGetValue("Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName", out object value))
            {
                first = first.Concat((IEnumerable<Service>)value);
            }

            if (!registration.Metadata.TryGetValue("Autofac.Extras.DynamicProxy.RegistrationExtensions.AttributeInterceptorsPropertyName", out value))
            {
                return first.Concat(GetInterceptorServicesFromAttributes(implType));
            }

            return first.Concat((IEnumerable<Service>)value);
        }

        private static IEnumerable<Service> GetInterceptorServicesFromAttributes(Type implType)
        {
            TypeInfo typeInfo = implType.GetTypeInfo();
            if (!typeInfo.IsClass)
            {
                return Enumerable.Empty<Service>();
            }

            IEnumerable<Service> first = from InterceptAttribute att in typeInfo.GetCustomAttributes(typeof(InterceptAttribute), inherit: true)
                                         select att.InterceptorService;
            IEnumerable<Service> second = from InterceptAttribute att in implType.GetInterfaces().SelectMany((Type i) => i.GetTypeInfo().GetCustomAttributes(typeof(InterceptAttribute), inherit: true))
                                          select att.InterceptorService;
            return first.Concat(second);
        }
    }
}
