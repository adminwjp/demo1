//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Autofac;
using Autofac.Extras.DynamicProxy;
using Config.Domain.Repositories;
using Config.Nhibernate.EntityMappings;
using Config.Nhibernate.Repositories;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.IO;
using Utility.Demo;
using Utility.Interceptors;
using Utility.Net.Http;
using Utility.Nhibernate.Infrastructure;

namespace Config.Nhibernate
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfignHibernateModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigManager>().As<IConfigManager>().OwnedByLifetimeScope()
                 .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();
            base.Load(builder);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string SqlPath="e:";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static AppSessionFactory GetAppSessionFactory(string connectionString)
        {
            return new AppSessionFactory(it=> InitialConfig(it,connectionString));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="connectionString"></param>
        public static void InitialConfig(Configuration config,string connectionString)
        {    //1、 xml配置  数据库
             //config = config.Configure("Config//hibernate.cfg.xml");
             //Enable validation(optional)
             // NHibernate.Mapping.Attributes.HbmSerializer.Default.Validate = true;
             //Here, we serialize all decorated classes(but you can also do it class by class)
             //2、硬编码配置 数据库
            AppSessionFactory.DefaultCodeConfig(config);

            config.Properties["connection.connection_string"] = connectionString;
            //这一步注解 映射 但 只能程序集 映射 指定类映射了? 
            //using (MemoryStream ms=new MemoryStream())
            //{
            //    Type type1 = typeof(Domain.AboutInfo);
            //    foreach (var module in type1.Assembly.Modules)
            //    {
            //        foreach (var type in module.GetTypes())
            //        {
            //            if(type.GetCustomAttribute(typeof(NHibernate.Mapping.Attributes.ClassAttribute))!=null)
            //            {
            //                if (type.Module.Name == type1.Module.Name)
            //                {
            //                    using (MemoryStream memoryStream = NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(type))
            //                    {
            //                        byte[] buffer = memoryStream.ToArray();
            //                        ms.Write(buffer, 0, buffer.Length);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    config.AddInputStream(ms);// Could not compile the mapping document: (unknown) 未知问题 暂放弃
            //}
            //1.注解 映射 //如果该程序集有其他的了 这种方式不适用了 
            //asp.net web 出现问题   NHibernate.MappingException: No type name specified
            //config.AddInputStream(NHibernate.Mapping.Attributes.HbmSerializer.Default.Serialize(Assembly.Load("Service.Model")));

            // HbmSerializer.Default.Serialize(Assembly.Load("Service.Model"), "Config/hibernate-service.cfg.xml");
            //  config.AddXmlFile("Config/hibernate-service.cfg.xml");
            //2、xml 映射
            //asp.net web 跑c盘去了
            //foreach (var item in Directory.GetFiles(@"E:\work\csharp\src\Service\Service.Web\bin\Config/hbm", "*.hbm.xml"))
            //{
            //    config.AddXmlFile(item);
            //}
            //3. mapp
            var mapper = new ModelMapper();
            mapper.AddMappings(new Type[] { typeof(ServiceNhibernateMapp),typeof(ConfigNhibernateMapp) });
            var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddMapping(domainMapping);
            config.SessionFactory().GenerateStatistics();
            //用NHibernate.Tool.hbm2ddl.SchemaExport生成表结构到\sql.sql文件当中
            SchemaExport export = new SchemaExport(config);
            export.SetOutputFile(Path.Combine(/*Directory.GetCurrentDirectory()*/SqlPath, "config.sql")); //设置输出目录
            // export.Drop(true, true);//设置生成表结构存在性判断,并删除
            export.Create(false, false);//设置是否生成脚本,是否导出来
        }
    }
}
#endif