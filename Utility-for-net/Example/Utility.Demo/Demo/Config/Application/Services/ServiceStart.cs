//#define Ef

//using Config.DAL;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Utility;
//using Utility.Nhibernate;
//using Utility.Nhibernate.Infrastructure;
//using Utility.Ef;
//using System.Collections;
//using Utility.ObjectMapping;


////#if NETCOREAPP3_1 || NET5_0
//using Microsoft.Extensions.DependencyInjection;
////using Microsoft.AspNetCore.Builder.Internal;
//using Autofac;
//using Utility.Ioc;
//using Config.Ef.DAL;

//namespace Config.Application.Services
//{
//    public partial class ServiceStart
//    {
//        public static readonly IServiceCollection Services = new ServiceCollection();
//        internal static ApplicationBuilder Builder { get; private set; }

//        public enum Flag
//        {
//            None,
//            Ef,
//            Nhibernate,
//            Dapper,
//            Wcf,
//            Remote
//        }
//        public static void AspNetCoreReg(bool cs = false)
//        {
//            AutofacAspNetCore(Services, Flag.Ef);
//            AutofacAspNetCore(Services, Flag.Nhibernate, cs);
//            AutofacAspNetCore(Services, Flag.Dapper);
//            AutofacAspNetCore(Services, Flag.Wcf);
//            Builder = new ApplicationBuilder(Services.BuildServiceProvider());
//        }

//        private static IConfigManager GetConfigManageByAspNetCore(Flag flag = Flag.Ef)
//        {
//            switch (flag)
//            {
//#if Ef
//                case Flag.Ef: return Builder.ApplicationServices.GetRequiredService<ConfigEfManager>();//net frmaework mysql 不支持
//#endif
//#if Nhibernate
//                case Flag.Nhibernate:
//                    return Builder.ApplicationServices.GetService<ConfigNhibernateManager>();//configManagers[1];//0 ef 1 nhibernate 2 dapper

//                //bug session close
//                //using (IServiceScope scope = ConfigHelper.Builder.ApplicationServices.CreateScope())
//                //{
//                //    //_configManager.Dispose(); //session close 相当于这种方式
//                //    // List<IConfigManager> configManagers = scope.ServiceProvider.GetServices<IConfigManager>().ToList();
//                //    _configManager = scope.ServiceProvider.GetService<ConfigNhibernateManager>();//configManagers[1];//0 ef 1 nhibernate 2 dapper
//                //}
//#endif
//#if Dapper
//                case Flag.Dapper: return Builder.ApplicationServices.GetRequiredService<ConfigDapperManager>();
//#endif
//#if Wcf
//                case Flag.Wcf: return Builder.ApplicationServices.GetRequiredService<ConfigWcfManager>();
//#endif
//#if Remote
//                case Flag.Remote: return Builder.ApplicationServices.GetRequiredService<ConfigRemoteManager>();
//#endif
//                case Flag.None:
//                default:
//                    return null;
//            }
//        }
//        private static void AutofacAspNetCore(IServiceCollection services, Flag flag, bool cs = false)
//        {
//            //实现同一个接口多个实例(泛型实例无法确定哪一个不然一个个写得累(多个)) 注入 可能 造成操作失败 
//            switch (flag)
//            {
//#if Ef
//                case Flag.Ef:
//                    {
//                        services.AddSingleton<ConfigDbContext>();
//                        services.AddTransient<IConfigManager, ConfigEfManager>();
//                        services.AddTransient<ConfigEfManager>();
//                    }
//                    break;
//#endif
//#if Nhibernate
//                case Flag.Nhibernate:
//                    {
//                        services.AddSingleton<AppSessionFactory>(it => new AppSessionFactory(NhibernateHelper.InitialConfig));

//                        if (cs)
//                        {
//                            //identifier of an instance of 
//                            //AppSessionFactory sessionFactory = new AppSessionFactory(NhibernateHelper.InitialConfig);
//                            // SessionManager.BindSession(sessionFactory.SessionFactory);                      
//                            // AddTransient 只能使用这 使用其他 再次获取对象没创建
//                            services.AddTransient<NHibernate.ISession>(c => c.GetService<AppSessionFactory>().OpenSession());//每次重新创建对象不然出错   identifier of an instance of   //AddSingleton  AddScoped无效
//                            services.AddTransient<IConfigManager, ConfigNhibernateManager>();
//                            services.AddTransient<ConfigNhibernateManager>();

//                        }
//                        else
//                        {
//                            //nhibernate identifier of an instance of was altered from 16 to 1
//                            services.AddScoped<NHibernate.ISession>(c => c.GetService<AppSessionFactory>().OpenSession());
//                            services.AddScoped<IConfigManager, ConfigNhibernateManager>();
//                            services.AddScoped<ConfigNhibernateManager>();//再次获取 没有创建 cs 
//                        }
//                    }
//                    break;
//#endif
//#if Dapper
//                case Flag.Dapper:
//                    {
//                        services.AddSingleton<System.Data.IDbConnection>((System.Data.IDbConnection)new MySql.Data.MySqlClient.MySqlConnection(ServiceDbContext.ConnectionString));
//                        services.AddTransient<IConfigManager, ConfigDapperManager>();
//                        services.AddTransient<ConfigDapperManager>();
//                    }
//                    break;
//#endif
//#if Wcf
//                case Flag.Wcf:
//                    {
//                                            services.AddTransient<IConfigManager, ConfigWcfManager>();
//                                            services.AddTransient<ConfigWcfManager>();
//                    }
//                    break;
//#endif
//#if Remote
//                case Flag.Remote:
//                    {
//                                            services.AddTransient<IConfigManager, ConfigRemoteManager>();
//                                            services.AddTransient<ConfigRemoteManager>();
//                    }
//                    break;
//#endif
//                case Flag.None:
//                default:
//                    break;
//            }
//        }
//    }
//}

////#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48


//namespace Service.BLL
//{
//    public partial class ServiceStart
//    {
//        public static readonly AutofacIocManager IocManager = new AutofacIocManager();
//        public enum Flag
//        {
//            None,
//            Ef,
//            Nhibernate,
//            Dapper,
//            Wcf,
//            Remote,
//            EnterpriseLibrary
//        }


//        [Obsolete]
//        private static void AutofacReg(bool cs = false)
//        {
//            IocManager.Register(it => {
//                AutofacReg(it, Flag.Ef);
//                AutofacReg(it, Flag.Nhibernate, cs);
//                AutofacReg(it, Flag.Dapper);
//                AutofacReg(it, Flag.Wcf);      
//                AutofacReg(it, Flag.Remote);    
//                AutofacReg(it, Flag.EnterpriseLibrary);
//            });

//        }

//        private static IConfigManager GetConfigManagerByAutofac(Flag flag = Flag.Ef)
//        {
//            switch (flag)
//            {
//#if Ef
//                case Flag.Ef: return IocManager.Resolver<IConfigManager>("ConfigEfManager");
//#endif
//#if Nhibernate
//                case Flag.Nhibernate:
//                    //该方式还是创建一次 不是每次创建  OwnedByLifetimeScope 每次创建  内部实现不懂 其他作用域同样错误
//                    return IocManager.Resolve<IConfigManager>("ConfigNhibernateManager");//ISession 没关闭导致 异常 nhibernate identifier of an instance of was altered from 16 to 1
//                case Flag.Dapper: return IocManager.Resolve<IConfigManager>("ConfigDapperManager");
//#endif
//#if Wcf
//                case Flag.Wcf:
//                    return IocManager.Resolve<IConfigManager>("ConfigWcfManager");
//#endif
//#if Remote
//                case Flag.Remote:
//                    return IocManager.Resolve<IConfigManager>("ConfigRemoteManager");
//#endif
//#if EnterpriseLibrary
//                case Flag.EnterpriseLibrary:
//                    return IocManager.Resolve<IConfigManager>("ConfigEnterpriseLibraryManager");
//#endif
//                case Flag.None:
//                default: return null;
//            }
//        }


//        [Obsolete]
//        private static void AutofacReg(Autofac.ContainerBuilder it, Flag flag, bool cs = false)
//        {
//            //实现同一个接口多个实例(泛型实例无法确定哪一个不然一个个写得累(多个)) 注入 可能 造成操作失败 
//            switch (flag)
//            {
//#if Ef
//                case Flag.Ef:
//                    {
//                        it.Register<ConfigDbContext>(c => new ConfigDbContext()).As<ConfigDbContext>();
//                        it.RegisterType<ConfigEfManager>().As<IConfigManager>().InstancePerLifetimeScope()
//                            .Named("ConfigEfManager", typeof(IConfigManager));

//                        it.RegisterType<ConfigEfManager>().InstancePerLifetimeScope();
//                    }
//                    break;
//#endif
//#if Nhibernate
//                case Flag.Nhibernate:
//                    {
//                        if (cs)
//                        {
//                            // identifier of an instance of 
//                            //AppSessionFactory sessionFactory = new AppSessionFactory(NhibernateHelper.InitialConfig);
//                            //SessionManager.BindSession(sessionFactory.SessionFactory);
//                            //it.Register<NHibernate.ISession>(c => SessionManager.GetCurrentSession(sessionFactory.SessionFactory));
//                            //OwnedByLifetimeScope 只能使用这 使用其他 再次获取对象没创建
//                            it.Register<AppSessionFactory>(c => new AppSessionFactory(NhibernateHelper.InitialConfig)).SingleInstance();
//                            it.Register<NHibernate.ISession>(c => c.Resolve<AppSessionFactory>().OpenSession())
//                          .OwnedByLifetimeScope();
//                            it.RegisterType<ConfigNhibernateManager>().As<IConfigManager>()
//                      .OwnedByLifetimeScope()
//                      .Named("ConfigNhibernateManager", typeof(IConfigManager));

//                         it.RegisterType<ConfigNhibernateManager>().InstancePerLifetimeScope();

//                        }
//                        else
//                        {
//                            //nhibernate identifier of an instance of was altered from 16 to 1
//                            it.Register<AppSessionFactory>(c => new AppSessionFactory(NhibernateHelper.InitialConfig)).SingleInstance();
//                            it.Register<NHibernate.ISession>(c => c.Resolve<AppSessionFactory>().OpenSession());

//                            it.RegisterType<ConfigNhibernateManager>().As<IConfigManager>()
//                                .InstancePerLifetimeScope()
//                                .Named("ConfigNhibernateManager", typeof(IConfigManager));

//                                it.RegisterType<ConfigNhibernateManager>();
//                        }

//                    }
//                    break;
//#endif
//#if Dapper
//                case Flag.Dapper:
//                    {
//                        it.RegisterInstance<System.Data.IDbConnection>((System.Data.IDbConnection)new MySql.Data.MySqlClient.MySqlConnection(ServiceDbContext.ConnectionString));
//                        it.RegisterType<ConfigDapperManager>().As<IConfigManager>().InstancePerLifetimeScope()
//                            .Named("ConfigDapperManager", typeof(IConfigManager));
//                        it.RegisterType<ConfigDapperManager>().InstancePerLifetimeScope();
//                    }
//                    break;
//#endif
//#if Wcf
//                case Flag.Wcf:
//                    {
//                        it.RegisterType<ConfigWcfManager>().As<IConfigManager>().InstancePerLifetimeScope().Named("ConfigWcfManager", typeof(IConfigManager));;
//                        it.RegisterType<ConfigWcfManager>();
//                    }
//                    break;
//#endif
//#if Remote
//                case Flag.Remote:
//                    {
//                        it.RegisterType<ConfigRemoteManager>().As<IConfigManager>().InstancePerLifetimeScope().Named("ConfigRemoteManager", typeof(IConfigManager));;
//                       it.RegisterType<ConfigRemoteManager>().InstancePerLifetimeScope();
//                    }
//                    break;
//                    case Flag.EnterpriseLibrary:
//                     {
//                        it.RegisterType<ConfigEnterpriseLibraryManager>().As<IConfigManager>().InstancePerLifetimeScope().Named("ConfigEnterpriseLibraryManager", typeof(IConfigManager));;
//                       it.RegisterType<ConfigEnterpriseLibraryManager>().InstancePerLifetimeScope();
//                    }
//                    break;
//#endif
//                case Flag.None:
//                default:
//                    break;
//            }
//        }
//    }
//}
////#endif

//namespace Service.BLL
//{
//    public partial class ServiceStart
//    {
//        public static readonly AutoMapperObjectMapper MapperManager = new AutoMapperObjectMapper();


//        public enum AutoWay
//        {
//            None,
//            Autofac,
//            AspNetCore
//        }
//        public static void Reg(AutoWay way, bool cs = false)
//        {
//            Dapper.SimpleCRUD.SetDialect(Dapper.SimpleCRUD.Dialect.MySQL);
//            switch (way)
//            {
//                case AutoWay.Autofac:
//#if !NETCOREAPP3_1
//                    AutofacReg(cs);
//#endif
//                    break;
//                case AutoWay.AspNetCore:
//#if NETCOREAPP3_1
//                    AspNetCoreReg(cs);
//#endif
//                    break;
//                case AutoWay.None:
//                default:
//                    break;
//            }
//        }

//        public static IConfigManager GetConfigManager(AutoWay way, Flag flag)
//        {
//            switch (way)
//            {
//                case AutoWay.Autofac:
//#if !NETCOREAPP3_1
//                    return GetConfigManagerByAutofac(flag);
//#endif
//#if NETCOREAPP3_1
//                case AutoWay.AspNetCore:
//                    return GetConfigManageByAspNetCore(flag);
//#endif
//                case AutoWay.None:
//                default: return null;
//            }
//        }
//    }

//    //public class ServiceConfig
//    //{
//    //    public static readonly List<ListModel> ListModels = new List<ListModel>();
//    //    public static void Init()
//    //    {
//    //        ListModel listModel = new ListModel() { Title = "服务信息", Id = "Id", IdType = typeof(string) };
//    //        ListModels.Add(listModel);
//    //        ListModel.ColumnModel column = new ListModel.ColumnModel() { Name = "Id", ColumnType = ListModel.ColumnType.None, Flag = ListModel.ColumnEditFlag.Hiddern, Header = "服务Id" };
//    //        listModel.Columns.Add(column);
//    //        column = new ListModel.ColumnModel() { Name = "Name", ColumnType = ListModel.ColumnType.TextBox, Header = "服务名称" };
//    //        listModel.Columns.Add(column);
//    //        column = new ListModel.ColumnModel() { Name = "Ip", ColumnType = ListModel.ColumnType.TextBox, Header = "ip地址" };
//    //        listModel.Columns.Add(column);
//    //        column = new ListModel.ColumnModel() { Name = "Port", ColumnType = ListModel.ColumnType.TextBox, Header = "端口" };
//    //        listModel.Columns.Add(column);
//    //        column = new ListModel.ColumnModel() { Name = "Status", ColumnType = ListModel.ColumnType.TextBox, Header = "状态" };
//    //        listModel.Columns.Add(column);
//    //        column = new ListModel.ColumnModel() { Name = "CreateDate", ColumnType = ListModel.ColumnType.TextBox, Flag = ListModel.ColumnEditFlag.Disabled, Header = "创建时间" };
//    //        listModel.Columns.Add(column);
//    //        column = new ListModel.ColumnModel() { Name = "LastDate", ColumnType = ListModel.ColumnType.TextBox, Flag = ListModel.ColumnEditFlag.Disabled, Header = "修改时间" };
//    //        listModel.Columns.Add(column);

//    //    }
//    //}
//}

