using MySql.Data.MySqlClient;
using Spring.Context.Support;
using Spring.Data.Common;
using Spring.Data.NHibernate;
using Spring.Objects;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Utility.Ioc;
using S = Spring;

namespace Example.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IIocManager IocManager { get; set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}

namespace Example.Web
{
    public class WebApiApplication : SpringMvcApplication
    //System.Web.HttpApplication
    {
        protected override void ConfigureApplicationContext()
        {
            base.ConfigureApplicationContext();
            Application_Start();
        }

        protected void Application_Start()
        {
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.Clear();//clear xml  use json
        }
    }
}


namespace Utility.Spring
{

    /// <summary>
    /// xml 老是 报错 没法 进行下去 了
    /// </summary>
    public class StartHelper
    {

        public static void Start()
        {
            //XmlApplicationContext xmlApplicationContext = new XmlApplicationContext();
            //S.Context.Support.StaticApplicationContext staticApplication = new StaticApplicationContext();
            //S.Context.Support.GenericApplicationContext genericApplicationContext = new GenericApplicationContext();
            S.Context.Support.MvcApplicationContext mvcApplicationContext = new MvcApplicationContext();

            IConfigurableListableObjectFactory objectDefinitionFactory = mvcApplicationContext.ObjectFactory;

            objectDefinitionFactory.RegisterObjectDefinition("MySqlDbMetadata", new RootObjectDefinition(typeof(MySqlDbMetadata)));

            var dbProvider = new RootObjectDefinition(typeof(DbProvider));
            dbProvider.ConstructorArgumentValues.AddIndexedArgumentValue(0, objectDefinitionFactory.GetObject<IDbMetadata>(), typeof(IDbMetadata).FullName);
            objectDefinitionFactory.RegisterObjectDefinition("DbProvider", dbProvider);


            var mySessionFactoryMutablePropertyValues = new MutablePropertyValues();
            var mySessionFactory = new RootObjectDefinition(typeof(LocalSessionFactoryObject), mySessionFactoryMutablePropertyValues);
            mySessionFactoryMutablePropertyValues.Add("DbProvider", objectDefinitionFactory.GetObject<IDbProvider>());//scope 怎么 使用了 
            mySessionFactoryMutablePropertyValues.Add("MappingAssemblies", new List<string>() { "Shop.Spring.Advert.Api" });

            objectDefinitionFactory.RegisterObjectDefinition("MySessionFactory", mySessionFactory);
        }
    }

    public class MySqlDbMetadata : IDbMetadata
    {
        public string ProductName => null;

        public Type ConnectionType => typeof(MySqlConnection);

        public Type CommandType => typeof(MySqlCommand);

        public Type ParameterType => typeof(MySqlParameter);

        public Type DataAdapterType => typeof(MySqlDataAdapter);

        public Type CommandBuilderType => typeof(MySqlCommandBuilder);

        public Type ExceptionType => null;

        public string ErrorCodeExceptionExpression => null;

        public MethodInfo CommandBuilderDeriveParametersMethod => null;

        public string ParameterNamePrefix => string.Empty;

        public bool UseParameterNamePrefixInParameterCollection => false;

        public bool UseParameterPrefixInSql => false;

        public bool BindByName { get; set; }

        public Type ParameterDbType => typeof(MySqlDbType);

        public PropertyInfo ParameterDbTypeProperty => typeof(MySqlParameter).GetProperty("SetMySqlDbType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);

        public PropertyInfo ParameterIsNullableProperty => null;

        public ErrorCodes ErrorCodes => null;
    }
}
