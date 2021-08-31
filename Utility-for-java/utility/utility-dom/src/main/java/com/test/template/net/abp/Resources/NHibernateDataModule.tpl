using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.NHibernate;
using FluentNHibernate.Cfg.Db;
using System.Configuration;
using System.Reflection;


namespace {#programName}
{
    /// <summary>
    /// 组态要开始使用NHibernate，必须在模块的PreInitialize方法中对其进行配置
    /// 该AbpNHibernateModule模块提供基本功能和适配器，以与ASP.NET样板NHibernate的工作
    /// <![CDATA[http://aspnetboilerplate.com/Pages/Documents/NHibernate-Integration]]>
    /// </summary>
    [DependsOn(typeof(AbpNHibernateModule), typeof({#programName}CoreModule))]
    public class {#programName}DataModule : AbpModule
    {
        public override void PreInitialize()
        {
            var connStr = ConfigurationManager.AppSettings["Default"];

            Configuration.Modules.AbpNHibernate().FluentConfiguration
                .Database(MySQLConfiguration.Standard.ConnectionString(connStr).ShowSql().FormatSql().Raw("hbm2ddl.auto","update"))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
