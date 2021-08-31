using Autofac;
using Utility.Nhibernate.Infrastructure;
using Utility.Nhibernate;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Utility.Nhibernate.Uow;
using Utility.Domain.Uow;
using NHibernate;
using Utility;

namespace SocialContact.Domain
{
    public class DomainManagerModule : Autofac.Module
    {
        public static DbFlag Flag { 
            get {
                if (flag == DbFlag.None)
                {
                    if(DbConfig.Flag== DbFlag.None)
                    {
                        return DbFlag.Sqlite;
                    }
                    flag = DbConfig.Flag;
                }
                return flag;
            }
            set => flag = value;
        }
        private static DbFlag flag = DbFlag.None;

        //private readonly Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;


        //public DomainManagerModule(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        //{
        //    _loggerFactory = loggerFactory;
        //}

        protected override void Load(ContainerBuilder builder)
        {
            string filename = "sql";
            builder.Register(it => new AppSessionFactory(config =>
            {
                switch (Flag)
                {
                    case DbFlag.SqlServer:
                        config = config.Configure("Config/hibernate-sqlserver.cfg.xml");
                        filename = "sqlserver";
                        break;
                    case DbFlag.MySql:
                        config = config.Configure("Config/hibernate.cfg.xml");
                        filename = "mysql";
                        break;
                    case DbFlag.Sqlite:
                        config = config.Configure("Config/hibernate-sqlite.cfg.xml");
                        filename = "sqlite";
                        break;
                    case DbFlag.Oracle:
                        config = config.Configure("Config/hibernate-oracle.cfg.xml");
                        filename = "oracle";
                        break;
                    case DbFlag.Postgre:
                        config = config.Configure("Config/hibernate-postgre.cfg.xml");
                        filename = "postgre";
                        break;
                    default:
                        break;
                }

                //config.AddXmlFile("Config/hbm/social_contact.hbm.xml");
              //  config.Interceptor = new SQLWatcher(_loggerFactory);
                foreach (var item in Directory.GetFiles("Config/hbm", "*.hbm.xml"))
                {
                    config.AddXmlFile(item);
                }
                SchemaExport export = new SchemaExport(config);
                if(!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), $"sql")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), $"sql"));
                }
                export.SetOutputFile(Path.Combine(Directory.GetCurrentDirectory(), $"sql/sql-{filename}.sql")); //设置输出目录
                // export.Drop(true, true);//设置生成表结构存在性判断,并删除
                export.Create(false, false);//设置是否生成脚本,是否导出来
            })).As<AppSessionFactory>().SingleInstance();
            builder.Register(it => it.Resolve<AppSessionFactory>().OpenSession()).As<ISession>().OwnedByLifetimeScope();
            builder.Register(it => new NhibernateUnitWork(it.Resolve<ISession>())).As<IUnitWork>().OwnedByLifetimeScope();
            base.Load(builder);
        }
    }
}
