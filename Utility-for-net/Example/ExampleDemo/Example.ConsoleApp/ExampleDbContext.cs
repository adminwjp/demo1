#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 ||  NET5_0 || NET6_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif


namespace Example
{
    public class ExampleDbContext : DbContext 
    {
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 ||  NET5_0 || NET6_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        public static readonly ILoggerFactory MyLoggerFactory  = LoggerFactory.Create(builder =>
        {
            //builder.AddConsole();
        });
        bool Migeration = false;
         public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {
            if (Migeration)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }
        //#endif

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
        public ExampleDbContext() : base("Database = Example; Data Source = localhost; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;")
        {
            
        }
        public ExampleDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {

        }
#endif


        public DbSet<TypeInfo> Types { get; set; }

        public DbSet<PivotTable> PivotTables { get; set; }


        public DbSet<Test> Tests { get; set; }

#if NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
       
        }
        //#endif


#elif  NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 ||  NET5_0 || NET6_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            //optionsBuilder.UseMySql("Database = Example; Data Source = localhost; User Id = root; Password = wjp930514.; Old Guids = True; charset = utf8;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
#endif

    }


}
