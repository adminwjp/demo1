//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utility.Json;

//namespace Example
//{
//    public class Question4
//    {
//        static string Table = "Tests";
//        public static void InitData()
//        {
//            var dbContext = Question2.DbContexts.Get();
//            Test[] list = {
//                new Test(){Id=Question1.userCache.GetId(Table),A="a1",B="b1"},
//                new Test(){Id=Question1.userCache.GetId(Table),A="a1",B="b1"},
//                new Test(){Id=Question1.userCache.GetId(Table),A="a2",B="b2"},
//                new Test(){Id=Question1.userCache.GetId(Table),A="a1",B="b1"},
//                new Test(){Id=Question1.userCache.GetId(Table),A="a2",B="b2"},
//                new Test(){Id=Question1.userCache.GetId(Table),A="a2",B="b2"}
//            };
//            ///dbContext.Database.GetDbConnection();
//            int res = dbContext.Database.ExecuteSqlRaw(string.Format("delete from {0}", Table));
//            if (res > 0)
//            {
//                Console.WriteLine("{0} has data, delete all data success", Table);
//            }
//            else
//            {
//                Console.WriteLine("{0} has data, delete all data fail", Table);
//            }
//            dbContext.Tests.AddRange(list);
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

//        //删除 重复 a,b 数据
//        public static void DeleteRepeat()
//        {
//            var dbContext = Question2.DbContexts.Get();
//            //int res = dbContext.Database.ExecuteSqlRaw("delete from Tests where not in id (select min(id) from Tests groupby a,b);");
//            int res = dbContext.Database.ExecuteSqlRaw("delete from Tests where exists (select * from Tests t where Tests.id>t.id and t.a=Tests.a and t.b=Tests.a);");
//            if (res > 0)
//            {
//                Console.WriteLine("{0} delete repeat data  success", Table);
//            }
//            else
//            {
//                Console.WriteLine("{0} delete repeat data fail", Table);
//            }
//            var list = dbContext.Tests.AsNoTracking().ToList();
//            Console.WriteLine(JsonHelper.ToJson(list));
//            Question2.DbContexts.Release(dbContext);
//        }
//    }
//}
