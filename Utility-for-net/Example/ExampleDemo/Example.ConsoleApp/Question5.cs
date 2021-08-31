//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utility.Json;

//namespace Example
//{
//    public class Question5
//    {
//        public static void InitData()
//        {

//        }

//        // 使用标准sql 嵌套 查询 课程名称为 税收基础 , 学员学号 和 学员姓名
//        public static void FindByCourse(string name="税收基础")
//        {
//            var dbContext = Question2.DbContexts.Get();
//            var conn = dbContext.Database.GetDbConnection();
//            var cmd = conn.CreateCommand();
//            cmd.CommandText = "select Id,Name from Students where  Id in(select s.StudentId from  Course c,Score s where s.CourseId=c.Id and c.Name=@Name) ;";
//            var param = cmd.CreateParameter();
//            cmd.Parameters.Add(param);
//            param.ParameterName = "@Name";
//            param.Value = name;
//            var reader = cmd.ExecuteReader();
//            List<dynamic> datas = new List<dynamic>();
//            while (reader.Read())
//            {
//                datas.Add(new {
//                    Id=reader.GetInt64(0),
//                    Name = reader.GetString(1),
//                });
//            }
//            reader.Close();
//            cmd.Dispose();
//            Console.WriteLine(JsonHelper.ToJson(datas));
//            Question2.DbContexts.Release(dbContext);
//        }

//        // 使用标准sql 嵌套 查询 课程编程为 2 , 学员姓名 和 学员所属单位
//        public static void FindByCourse(long id=2)
//        {
//            var dbContext = Question2.DbContexts.Get();
//            var conn = dbContext.Database.GetDbConnection();
//            var cmd = conn.CreateCommand();
//            cmd.CommandText = "select Unit,Name from Students where  Id in(select s.StudentId from  Course c,Score s where s.CourseId=c.Id and c.Id=@Id) ;";
//            var param = cmd.CreateParameter();
//            cmd.Parameters.Add(param);
//            param.ParameterName = "@Id";
//            param.Value = id;
//            var reader = cmd.ExecuteReader();
//            List<dynamic> datas = new List<dynamic>();
//            while (reader.Read())
//            {
//                datas.Add(new
//                {
//                    Unit = reader.GetString(0),
//                    Name = reader.GetString(1),
//                });
//            }
//            reader.Close();
//            cmd.Dispose();
//            Console.WriteLine(JsonHelper.ToJson(datas));
//            Question2.DbContexts.Release(dbContext);
//        }

//    }
//}
