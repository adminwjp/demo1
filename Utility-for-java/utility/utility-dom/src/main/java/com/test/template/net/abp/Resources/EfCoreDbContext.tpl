using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace {#programName}.EntityFrameworkCore
{
    public class {#programName}DbContext : AbpDbContext
    {
      

        public {#programName}DbContext(DbContextOptions<{#programName}DbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }
     
    }
}
