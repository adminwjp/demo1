//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utility.Ef;
//using Utility.Json;
//using Utility.Pool;

//namespace Example
//{
//    /**
//     Id Type Num Des
//     1  A    9   a1
//     2  B    5   b1
//     3  C    7   c1
//     4  A    6   a2
//     5  B    8   b2
//     6  B    4   b3
//    使用 一条 语句 查询 出 该表 中 每个 type 类型 Num 值 最大的那条 信息
//     id 1 3 5
//     */
//    /// <summary>
//    /// 问题
//    /// </summary>
//    public class Question2
//    {
//        public static IObjectPool<ExampleDbContext> DbContexts { get; set; }
//        static string Table = "Types";
//        public static void InitConnect(){
//            DbContexts = ConnectionHelper.GetDbContexts<ExampleDbContext>(Question1.ConnectionString, Question1.Flag);
//        }
//        public static void InitData()
//        {
//            var dbContext = DbContexts.Get();
//            TypeInfo[] list = { 
//                new TypeInfo() { Id=Question1.userCache.GetId(Table),Type="A",Num=9,Des="a1"},
//                new TypeInfo() { Id=Question1.userCache.GetId(Table),Type="B",Num=5,Des="b1"},
//                new TypeInfo() { Id=Question1.userCache.GetId(Table),Type="C",Num=7,Des="c1"},
//                new TypeInfo() { Id=Question1.userCache.GetId(Table),Type="A",Num=6,Des="a2"},
//                new TypeInfo() { Id=Question1.userCache.GetId(Table),Type="B",Num=8,Des="b2"},
//                new TypeInfo() { Id=Question1.userCache.GetId(Table),Type="B",Num=4,Des="b3"},
//            };
//           //dbContext.Database.GetDbConnection();
//            int res= dbContext.Database.ExecuteSqlRaw(string.Format("delete from {0}",Table));
//            if (res > 0)
//            {
//                Console.WriteLine("{0} has data, delete all data success",Table);
//            }
//            else
//            {
//                Console.WriteLine("{0} has data, delete all data fail", Table);
//            }
//            dbContext.Types.AddRange(list);
//            res= dbContext.SaveChanges();
//            if (res > 0)
//            {
//                Console.WriteLine("{0} init data (add) success", Table);
//            }
//            else
//            {
//                Console.WriteLine("{0} init data (add) fail", Table);
//            }
//            DbContexts.Release(dbContext);
//        }

//        public static void ListByGroupByTypeAndMaxNum()
//        {
           
//            //GroupBy not support 
//            //var data = dbContext.Types.AsQueryable().OrderBy(it => it.Type).OrderByDescending(it => it.Num).GroupBy(it => it.Type).ToList();
//            //var res= new List<TypeInfo>(data.Count);
//            //foreach (var item in data)
//            //{
//            //    var temp = item.ToList();
//            //    if (temp!=null&&temp.Count>0)
//            //        res.Add(temp[0]);
//            //}
//            //DbContexts.Release(dbContext);
//            //Console.WriteLine(JsonHelper.ToJson(res));
//            //return;
//            //sqlserver 
//            //分组 查询 必须 查询的列 都要分组 ORDER BY 子句在视图、内联函数、派生表、子查询和公用表表达式中无效
//            string sql = "select t1.Id,t1.Type,t1.Num,t1.Des from types  t1 where not exists( select t1.Id,t1.Type,t1.Num,t1.Des from types t2 where  t1.Type=t2.Type and t1.Num<t2.Num  ) ";
//            sql = string.Format(sql,Table);
//            //if(Question1.Flag== Utility.DbFlag.Sqlite) { return; }
//            var dbContext = DbContexts.Get();
//            var conn= dbContext.Database.GetDbConnection();
//            var cmd = conn.CreateCommand();
//            cmd.CommandText = sql;
//            //ef sqlite not support dataAdapter
//            //using Microsoft.Data.SqlClient;
//            //using System.Data.SqlClient; //ex 
//            //Unhandled exception. System.IO.FileNotFoundException: Could not load file or ass
//            //embly 'System.Data.SqlClient, Version=4.4.0.0, Culture=neutral, PublicKeyToken=b
//            //03f5f7f11d50a3a'. 系统找不到指定的文件。
//            bool adap = false;
//            if (!adap)
//            {
//                var reader = cmd.ExecuteReader();
//                List<TypeInfo> types1;
//                types1 = new List<TypeInfo>();
//                while (reader.Read())
//                {
//                    TypeInfo type = new TypeInfo
//                    {
//                        Id = reader.GetInt64(0),
//                        Type = reader.GetString(1),
//                        Num = reader.GetInt32(2),
//                        Des = reader.GetString(3),
//                    };
//                    types1.Add(type);
//                }
//                reader.Close();
//                cmd.Dispose();
//                Console.WriteLine(JsonHelper.ToJson(types1));
//                DbContexts.Release(dbContext);
//                return;
//            }
//            var ada = ConnectionHelper.GetDataAdapter(cmd, Question1.Flag);
//            DataSet ds = new DataSet();
//            ada.Fill(ds);
//            DataTable dt = ds.Tables[0];
//            List<TypeInfo> types;
//            if (dt != null && dt.Rows.Count > 0)
//            {
//                types = new List<TypeInfo>(dt.Rows.Count);
//                foreach (DataRow row in dt.Rows)
//                {
//                    TypeInfo type = new TypeInfo { 
//                        Id=Convert.ToInt64(row["Id"]),
//                        Type = row["Type"].ToString(),
//                        Num = Convert.ToInt32(row["Num"]),
//                        Des = row["Des"].ToString()
//                    };
//                    types.Add(type);
//                }
//                Console.WriteLine(JsonHelper.ToJson(types));
//            }
//            cmd.Dispose();
//            DbContexts.Release(dbContext);
//        }
//    }
//}
