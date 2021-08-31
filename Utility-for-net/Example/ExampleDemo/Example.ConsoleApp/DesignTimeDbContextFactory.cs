//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Example
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExampleDbContext>
//    {
//        public ExampleDbContext CreateDbContext(string[] args)
//        {
//            var bulder = new DbContextOptionsBuilder<ExampleDbContext>();
//            //bulder.UseSqlite(@"Data Source=E:\work\db\sqlite\test.db;");
//            bulder.UseSqlServer(@"server=192.168.1.4;database=Test;user=sa;pwd=wjp930514.");
//            return new ExampleDbContext(bulder.Options);
//        }
//    }
//}
