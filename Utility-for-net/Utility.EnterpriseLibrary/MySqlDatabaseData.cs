//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utility.EnterpriseLibrary
//{
//    //
//    // 摘要:
//    //     Describes a EntLibContrib.Data.MySql.MySqlDatabase instance, aggregating information
//    //     from a System.Configuration.ConnectionStringSettings.
//    public class MySqlDatabaseData : DatabaseData
//    {
//        //
//        // 摘要:
//        //     Initializes a new instance of the EntLibContrib.Data.MySql.MySqlDatabase class
//        //     with a connection string and a configuration source.
//        //
//        // 参数:
//        //   connectionStringSettings:
//        //     The System.Configuration.ConnectionStringSettings for the represented database.
//        //
//        //   configurationSource:
//        //     The Microsoft.Practices.EnterpriseLibrary.Common.Configuration.IConfigurationSource
//        //     from which additional information can be retrieved if necessary.
//        public MySqlDatabaseData(ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
//            : base(connectionStringSettings, configurationSource)
//        {
//        }

//        //
//        // 摘要:
//        //     Creates a Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.TypeRegistration
//        //     instance describing the database represented by this configuration object.
//        //
//        // 返回结果:
//        //     A Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.TypeRegistration
//        //     instance describing a database.
//        public override IEnumerable<TypeRegistration> GetRegistrations()
//        {
//            TypeRegistration<Database> val = new TypeRegistration<Database>((Expression<Func<Database>>)(() => new MySqlDatabase(base.ConnectionString)));
//            ((TypeRegistration)val).set_Name(base.Name);
//            ((TypeRegistration)val).set_Lifetime((TypeRegistrationLifetime)1);
//            yield return (TypeRegistration)(object)val;
//        }
//    }
//}
