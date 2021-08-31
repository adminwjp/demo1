//using Autofac;
//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Text;
//using Utility.Ioc;
//using Utility.Nhibernate.Infrastructure;
//using Utility.ObjectMapping;
//using Utility.Wpf.Attributes;
//using Config.Nhibernate;
//using Config.DAL;
//using Config.Nhibernate.DAL;
//using NHibernate;
//using System.Configuration;

//namespace Wpf
//{
//    public class StartManager
//    {

//        public static IIocManager IocManager { get;protected set; }
//        static StartManager()
//        {
//            //Initial();
//            //ConfigManager.Load();//数据没加载 进去 拿不到 
//        }

//       public static void Initial()
//        {
//            AutofacIocManager autofacIocManager = new AutofacIocManager();
//            IocManager = autofacIocManager;
//            AutoMapperObjectMapper autoMapperObjectMapper = new AutoMapperObjectMapper();
//            autoMapperObjectMapper.Init(it =>
//            {
//                foreach (var module in typeof(Wpf.Config.ConfigViewModel).Assembly.Modules)
//                {
//                    foreach (var type in module.GetTypes())
//                    {
//                        var mappTypeAttribute = type.GetCustomAttribute<MappTypeAttribute>(false);
//                        if (mappTypeAttribute != null)
//                        {
//                            it.CreateMap(type, mappTypeAttribute.Type);
//                            it.CreateMap(mappTypeAttribute.Type, type);
//                        }
//                    }
//                }
//            });
//            autofacIocManager.Builder.Register<AutoMapperObjectMapper>(it => autoMapperObjectMapper).As<IObjectMapper>().SingleInstance();
//            autofacIocManager.Builder.Register<AutofacIocManager>(it => autofacIocManager).As<IIocManager>().SingleInstance();

//            //config
//            string address = ConfigurationManager.AppSettings["Mysql"];
//            address = string.Format(address, "Example");
//            var appSessionFactory = NhibernateHelper.GetAppSessionFactory(address);
//            autofacIocManager.Builder.Register<AppSessionFactory>(it => appSessionFactory).SingleInstance();
//            autofacIocManager.Builder.Register<ISession>(it => it.Resolve<AppSessionFactory>().OpenSession()).OwnedByLifetimeScope();
//            autofacIocManager.AddScoped<IConfigDAL,ConfigNhibernateDAL>();


//        }

        
//    }
//}
