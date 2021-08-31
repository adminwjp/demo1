using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain.Repositories;

namespace Product.Domain.Repositories
{
    public interface IBrandRepository : IRepository<BrandEntity,long>
    {
    }
}
