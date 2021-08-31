#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
//using Utility.Domain.Entities;
using Utility.Domain.Repositories;

namespace Product.Domain.Repositories
{
    public interface IBaseRepository<Entity> : IRepository<Entity,long> where Entity:BaseEntity
    {

        IList<Entity> Find(Entity entity);

        IList<Entity> FindByPage(Entity entity, int page = 1, int size = 10);

        ResultDto<Entity> FindResultDtoByPage(Entity entity, int page = 1, int size = 10);

        //int Count(Entity entity);


    }
}
#endif