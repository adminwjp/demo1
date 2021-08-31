#if Test
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialContact.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Ioc;
using Autofac;
using SocialContact.Nhibernate;
using Dapper;
using System.Data.Common;
using SocialContact.Domain.Entities;
using Utility.Domain.Uow;

namespace SocialContact.Test
{
    [TestClass]
    public class StartTest
    {
        string ConnectionString = string.Empty;
        [TestInitialize]
        public void Init()
        {
            Utility.DbConfig.Flag = Utility.DbFlag.Sqlite;
            ConnectionString = "Database=socialcontact;Data Source=192.168.1.3;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";
            AutoHelper.Load();
            AutofacIocManager iocManager = AutofacIocManager.Instance;
            iocManager.Builder.RegisterModule(new Domain.DomainManagerModule());
            iocManager.Builder.RegisterModule(new NhibernateManagerModule());
            iocManager.Builder.RegisterType<Utility.Nhibernate.Uow.NhibernateUnitWork>().As<Utility.Domain.Uow.IUnitWork>().OwnedByLifetimeScope();
            Console.WriteLine("初始化 ioc 容器 成功");
        }

        //迁移数据
        [TestMethod]
        public void TestMethod1()
        {
            string sqlByIcon = "select name, style, description from icon_info";
            //冲突 类库 哪里 来的
            var type = Type.GetType("MySql.Data.MySqlClient.MySqlConnection,MySql.Data");
            var conn = Activator.CreateInstance(type,new object[] { ConnectionString}) as DbConnection;
            var iconResults = conn.Query(sqlByIcon).ToList();
            List<IconEntity> icons = new List<IconEntity>(iconResults.Count);
            var unitWork = AutofacIocManager.Instance.Resolver<IUnitWork>();
            foreach (var item in iconResults)
            {
                IconEntity icon = new IconEntity();
                icons.Add(icon);
                icon.Create();
                icon.Name = item.name;
                icon.Style = item.style;
                icon.Description = item.description;
            }
            unitWork.BatchInsert(icons.ToArray());
            Console.WriteLine("添加 图标 成功");
        }
    }
}
#endif