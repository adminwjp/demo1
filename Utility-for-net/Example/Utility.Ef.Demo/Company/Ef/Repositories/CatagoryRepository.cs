using System;
using System.Collections.Generic;
using System.Text;
using Company.Domain.Entities;
using Utility.Ef;
using Utility.Ef.Repositories;
using Utility.Logs;

namespace Company.Ef.Repositories
{
    public class CatagoryRepository : BaseEfRepository<CompanyDbContext,CompanyCatagoryEntity, long>
    {
        protected ILog<CatagoryRepository> Log;
        public CatagoryRepository(DbContextProvider<CompanyDbContext> context, ILog<CatagoryRepository> log) : base(context)
        {
            this.Log = log;
        }
    }
}
