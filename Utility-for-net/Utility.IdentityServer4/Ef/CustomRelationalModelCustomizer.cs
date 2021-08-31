//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Utility.Ef
//{
//    public class CustomRelationalModelCustomizer : RelationalModelCustomizer
//    {
//        public CustomRelationalModelCustomizer(ModelCustomizerDependencies dependencies)
//            : base(dependencies) { }

//        public override void Customize(ModelBuilder modelBuilder, DbContext dbContext)
//        {
//            base.Customize(modelBuilder, dbContext);
//            var sp = (IInfrastructure<IServiceProvider>)dbContext;
//            var dbOptions = sp.Instance.GetServices<DbContextOptions>();
//            foreach (var item in dbOptions)
//            {
//                if (item.ContextType == dbContext.GetType())
//                    ConfigureDbContextEntityService.Configure(modelBuilder, item, dbContext);
//            }
//        }
//    }
//}
