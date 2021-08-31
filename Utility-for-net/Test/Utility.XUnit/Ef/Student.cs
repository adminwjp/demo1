/**
 * net framework not support mysql
 设计范式的分类
第一范式的目标是确定每一列的原子性
第二范式的要求每个表描述一件事情(即除了主键以外的其他列,都依赖与该主键,则满足第二范式(2NF))
第三范式的目标是不出现传递依赖
 */
using System;
using System.Data;
using System.Linq.Expressions;
using D=Dapper;
using Microsoft.EntityFrameworkCore.Design;
using Utility.Domain.Entities;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Utility.Infrastructure;
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
#endif
#if NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif

namespace Utility.Test.Ef
{

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    public class StudentDes : IDesignTimeDbContextFactory<StudentDbContext>
    {
        public StudentDbContext CreateDbContext(string[] args)
        {
            var bulder = new DbContextOptionsBuilder<StudentDbContext>();
            bulder.UseMySql("Database=Shop;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;");
            return new StudentDbContext(bulder.Options,new NoMediator());
        }
    }
#endif
    public class StudentDbContext : BaseDbContext<StudentDbContext, DomainEvent<string>>
    {
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        public StudentDbContext(DbContextOptions<StudentDbContext> options, IMediator mediator)
               : base(options,mediator)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SC> SCs { get; set; }
#endif

#if NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public StudentDbContext()
            : base("Student")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in EventCloudDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of EventCloudDbContext since ABP automatically handles it.
         */
        public StudentDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public StudentDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
#endif
    }
  
    /// <summary>
    /// 学生表
    /// </summary>
    public class Student:DomainEvent<string>
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学生年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 学生性别
        /// </summary>
        public string Sex { get; set; }

       
    }
    /// <summary>
    /// 课程表
    /// </summary>
    public class Course : DomainEvent<string>
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 教师编号
        /// </summary>
        public string TeacherId { get; set; }
    }
    /// <summary>
    /// 成绩表
    /// </summary>
    public class SC : DomainEvent<string>
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public decimal Socre { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public string CourseId { get; set; }
        [D.NotMapped]
        public Student Student { get; set; }
        [D.NotMapped]
        public Course Course { get; set; }
    }
    /// <summary>
    /// 教师表
    /// </summary>
    public class Teacher : DomainEvent<string>
    {
        /// <summary>
        /// 课程姓名
        /// </summary>
        public string Name { get; set; }
    }

    public class StudentExample
    {
        // public static readonly string ConnectionString = string.Format(SqliteDbProvider.ConnectionString, "Student.db");
        public static readonly string ConnectionString = "Database=Shop;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";//mysql
        public static IDbConnection Connection { get; set; }
        /// <summary>
        /// 查询001 课程比 002 课程成绩高的所有学生的学号
        /// </summary>
        public static void QueryQuestion1()
        {
            Expression<Func<SC, SC, bool>> expression = (it, it1) => it.CourseId == "001" || it1.CourseId == "002"
            && it.StudentId == it1.StudentId
             && it.Socre > it1.Socre;
            //IList<SC> sCs=OrmUtils.GetList(Connection, OrmUtils.DefaultMapp, expression);
            string sql = @"Select StudentId from SC  sc1  ,
(Select StudentId from SC  where  CourseId = '002')  sc2  
where sc1.CourseId = '001' and sc1.StudentId=sc2.StudentId and sc1.Socre>sc2.Socre";

        }
    }
}
