using Abp.Zero.EntityFramework;
using System.Data.Entity;

namespace {#programName}.EntityFramework
{
    public class {#programName}DbContext : AbpDbContext
    {
      

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public {#programName}DbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in EventCloudDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of {#programName}DbContext since ABP automatically handles it.
         */
        public {#programName}DbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public {#programName}DbContext(DbConnection connection)
            : base(connection, true)
        {

        }
     
    }
}
