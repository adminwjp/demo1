//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utility.Ef;
//using Utility.Json;

//namespace Example
//{
//    public class Question3
//    {
//        static string Table = "PivotTables";
//        public static void InitData()
//        {
//            var dbContext = Question2.DbContexts.Get();
//            PivotTable[] list = { 
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1990,Quarter=1,Amount=1.0m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1990,Quarter=1,Amount=1.1m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1990,Quarter=2,Amount=1.2m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1990,Quarter=2,Amount=1.2m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1990,Quarter=3,Amount=1.3m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1990,Quarter=4,Amount=1.4m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1991,Quarter=1,Amount=2.1m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1991,Quarter=2,Amount=2.2m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1991,Quarter=3,Amount=2.3m},
//             new PivotTable(){Id=Question1.userCache.GetId(Table),Year=1991,Quarter=4,Amount=2.4m}
//            };
//           // dbContext.Database.GetDbConnection();
//            int res = dbContext.Database.ExecuteSqlRaw(string.Format("delete from {0}", Table));
//            if (res > 0)
//            {
//                Console.WriteLine("{0} has data, delete all data success", Table);
//            }
//            else
//            {
//                Console.WriteLine("{0} has data, delete all data fail", Table);
//            }
//            dbContext.PivotTables.AddRange(list);
//            res = dbContext.SaveChanges();
//            if (res > 0)
//            {
//                Console.WriteLine("{0} init data (add) success", Table);
//            }
//            else
//            {
//                Console.WriteLine("{0} init data (add) fail", Table);
//            }
//            Question2.DbContexts.Release(dbContext);
//        }

//        /**
//         Year Q1 Q2 Q3 Q4
//        1990 2.1 2.4 1.3 1.4
//        1991 2.1 2.2 2.3 2.4
//         */
//        public static void ListByYearAndQuarterBy1Or2Or3Or4()
//        {
//            string sql = @"select Year,
//sum(case Quarter when 1 then Amount else 0 end )  Q1,
//sum(case Quarter when 2 then Amount else 0 end )  Q2,
//sum(case Quarter when 3 then Amount else 0 end )  Q3,
//sum(case Quarter when 4 then Amount else 0 end )  Q4 
//from PivotTables group by Year ";
//            //if (Question1.Flag == Utility.DbFlag.Sqlite) { return; }
//            var dbContext = Question2.DbContexts.Get();
//            var conn = dbContext.Database.GetDbConnection();
//            var cmd = conn.CreateCommand();
//            cmd.CommandText = sql;
//            bool adap = false;
//            if (!adap)
//            {
               
//                List<dynamic> datas1=new List<dynamic>();
//                var reader = cmd.ExecuteReader();
//                while (reader.Read())
//                {
//                    datas1.Add(new
//                    {
//                        Year = reader.GetString(0),
//                        Q1 = reader.GetString(1),
//                        Q2 = reader.GetString(2),
//                        Q3 = reader.GetString(3),
//                        Q4 = reader.GetString(4)
//                    });
//                }
//                reader.Close();
//                cmd.Dispose();
//                Console.WriteLine(JsonHelper.ToJson(datas1));
//                Question2.DbContexts.Release(dbContext);
//                return;
//            }
//            var ada = ConnectionHelper.GetDataAdapter(cmd, Question1.Flag);
//            DataSet ds = new DataSet();
//            ada.Fill(ds);
//            DataTable dt = ds.Tables[0];
//            List<dynamic> datas;
//            if (dt != null && dt.Rows.Count > 0)
//            {
//                datas = new List<dynamic>(dt.Rows.Count);
//                foreach (DataRow row in dt.Rows)
//                {
//                    datas.Add(new {
//                        Year = row["Year"].ToString(),
//                        Q1 = row["Q1"].ToString(),
//                        Q2 = row["Q2"].ToString(),
//                        Q3 = row["Q3"].ToString(),
//                        Q4 = row["Q4"].ToString()
//                    }); 
//                }
//                Console.WriteLine(JsonHelper.ToJson(datas));
//            }
//            cmd.Dispose();
//            Question2.DbContexts.Release(dbContext);
//        }
//    }
//}
