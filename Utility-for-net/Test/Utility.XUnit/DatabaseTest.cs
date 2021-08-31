////using MySql.Data.MySqlClient;
//using NPOI.HPSF;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utility.Database;

//namespace Utility.XUnit
//{
//    public class DatabaseTest
//    {
//        //mysql.data 驱动支持 
//        //mysqlconnector 驱动不支持
//        List<A> As = new List<A>();
//        static Configuration Configuration = new Configuration() { Dialect= Dialect.MySql};
//        public static void Test()
//        {  
//            var classEntity= EmptyResolver.Empty.Resolver(typeof(A));
//            var classEntity1 = EmptyResolver.Empty.Resolver(typeof(Utility.Temaplate.DbEntity));
//            var classEntity2 = EmptyResolver.Empty.Resolver(typeof(Utility.Temaplate.TableEntity));
//            var classEntity3 = EmptyResolver.Empty.Resolver(typeof(Utility.Temaplate.ColumnEntity));
//            ClassEntity[] classEntity5 = { classEntity, classEntity1, classEntity2, classEntity3 };//强类型 支持 该 语法 不然 var  报语法错
//            DbHelper.UpdateForeignKey(classEntity5);
//            DbHelper.UpdateInformation(classEntity5);

//            DbConnection connection = AbstractDatabaseProvider.CreateDatabaseTypeFactory(Configuration.Dialect).CreateConnection("server=127.0.0.1;port=3306;database=Test1;user=root;password=wjp930514.;Allow User Variables=true;");
//            DbHelper.InitialConnectionString(connection);

//            DbHelper.TableOperator(connection, classEntity5, OperatorFlag.CreateIfNotExists, Configuration.Dialect);
//            //mysqlconnector 驱动不支持
//            //using (var connection1 = (MySqlConnection)connection)
//            //{
//            //    using (var tx = connection1.BeginTransaction())
//            //    {
//            //         using (var cmd = new MySqlCommand(connection1, tx))
//            //       // using (var cmd = connection.CreateCommand())
//            //        {
//            //            cmd.Transaction = tx;
//            //            cmd.CommandText = "insert into a(id,name) values(@id,@name)";
//            //            //pass
//            //            //cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString("N"));
//            //            //cmd.Parameters.AddWithValue("@name", Guid.NewGuid().ToString("N"));
//            //            //这 语法不支持 
//            //            //cmd.CommandText = "insert into a(id,name) values(?id,?name)";//语法 报错 ?
//            //            //error
//            //            //@ 语法 报 找不到 不能 为null
//            //            //DatabaseUtils.SetCommandParamter(cmd, "@id", Guid.NewGuid().ToString("N"));
//            //            //DatabaseUtils.SetCommandParamter(cmd, "@name", Guid.NewGuid().ToString("N"));
//            //            //这驱动 只能通过反射 参数格式化 实现 否则显示 实现 
//            //            //pass
//            //            cmd.Parameters.Add(new MySqlParameter("@id", Guid.NewGuid().ToString("N")));
//            //            cmd.Parameters.Add(new MySqlParameter("@name", Guid.NewGuid().ToString("N")));
//            //            int res = cmd.ExecuteNonQuery();
//            //            tx.Commit();
//            //        }
//            //    }
//            //}
//            //什么玩意驱动 
//            DbHelper.Insert(connection, classEntity1,Utility.Temaplate.TemplateHelper.DbEntity, false, Configuration.Dialect);
//           // Example.ConfigExample.Init();
//           // DbHelper.Insert(connection, classEntity1, Example.ConfigExample.GetDbEntity(Example.ConfigExample.classModels), false, Configuration.Dialect);
//            DbHelper.Insert(connection,classEntity,new A() { Name="2"},true, Configuration.Dialect);
//            DbHelper.Update(connection,classEntity,new A {Name="223",Id="1" },Configuration.Dialect,true);
//            DbHelper.Update(connection, classEntity, new A (){ Name = "224", Id = "1" }, Configuration.Dialect, true);
//            //var a= OrmUtils.Get<A>(connection,it=>classEntity,"1");
//            // Console.WriteLine(a);
//        }
//    }

//    public class A
//    {
//        public A()
//        {
//            Id = Guid.NewGuid().ToString("N");
//        }
//        public string Id { get; set; }
//        public string Name { get; set; }

//        public string B { get; set; }
//    }
//    public class B:A
//    {
//        public string Desc { get; set; }
//    }
//}
