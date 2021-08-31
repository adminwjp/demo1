using Microsoft.Extensions.Logging;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;
using NHibernate.Tool.hbm2ddl;
using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;

namespace OA.Domain
{
    public class NhibernateManger
    {
        public static void Initial(Configuration config, ILoggerFactory loggerFactory=null)
        {
            //config = config.Configure();
           // config.Interceptor = new SQLWatcher(loggerFactory);
            //Enable validation(optional)
            HbmSerializer.Default.Validate = true;
            //Here, we serialize all decorated classes(but you can also do it class by class)
         
            AppSessionFactory.DefaultCodeConfig(config, Utility.DbFlag.Sqlite);
            config.Properties["connection.connection_string"] = "Database = oa; Data Source = 127.0.0.1; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;";
            //Data Source=.\\CloudPos.db;Pooling=true;FailIfMissing=false
            config.Properties["connection.connection_string"] = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/oa.db;Pooling=true;FailIfMissing=false;";

            //config.Properties["hbm2ddl.auto"] = "create-drop";

            //config.AddInputStream(HbmSerializer.Default.Serialize(typeof(OAUserEntity).Assembly));
            // HbmSerializer.Default.Serialize(typeof(OAUserEntity).Assembly, "Config/hibernate-oa.cfg.xml");
            config.AddXmlFile("Config/hibernate-oa.cfg.xml");
            //config.SessionFactory().GenerateStatistics();
            //用NHibernate.Tool.hbm2ddl.SchemaExport生成表结构到\sql.sql文件当中
            //SchemaExport export = new SchemaExport(config);
            //export.SetOutputFile(Path.Combine(Directory.GetCurrentDirectory(), "sql.sql")); //设置输出目录
           // export.Drop(true, true);//设置生成表结构存在性判断,并删除
            //export.Create(false, false);//设置是否生成脚本,是否导出来
        }
    }
}
