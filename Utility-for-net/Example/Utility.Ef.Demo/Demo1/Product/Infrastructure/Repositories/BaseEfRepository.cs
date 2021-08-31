#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
#endif
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
#endif
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Utility.Application.Services.Dtos;
using Utility.Ef;
//using Utility.Domain.Entities;
using Utility.Ef.Repositories;
using Utility.Extensions;
using Utility.Helpers;
using Z.EntityFramework.Plus;

namespace Product.Infrastructure.Repositories
{
    public class BaseEfRepository<Entity> : BaseEfRepository<ProductDbContext, Entity, long>, IBaseRepository <Entity>
        where Entity:BaseEntity,new()
    {
        public BaseEfRepository(DbContextProvider<ProductDbContext> dbContext) : base(dbContext)
        {

        }
        public override int Update(Entity entity)
        {
            var old = FindSingle(it => it.Id == entity.Id);
            entity.CreationTime = old.CreationTime;
            entity.DeletionTime = old.DeletionTime;
            entity.IsDeleted = old.IsDeleted;
           return base.Update(entity);
        }
     
     
        public IList<Entity> Find(Entity entity)
        {
            var where = GetWhere(entity);
            //where = where.And(it => it.IsDeleted == false);//删除的数据 不查询
            return Query(where).ToList();
        }

        public IList<Entity> FindByPage(Entity entity, int page = 1, int size = 10)
        {
            var where = GetWhere(entity);
            //where = where.And(it => it.IsDeleted == false);//删除的数据 不查询
            return QueryByPage(where,page,size).ToList();
        }
        public ResultDto<Entity> FindResultDtoByPage(Entity entity, int page = 1, int size = 10)
        {
            var where = GetWhere(entity);
            //where = where.And(it => it.IsDeleted == false);//删除的数据 不查询
            var datas= QueryByPage(where, page, size).ToList();
            var count = Count(where);
            return new ResultDto<Entity>(datas, page, size, count);
        }

        public override long CountByEntity(Entity entity)
        {
            var where = GetWhere(entity);
            //where = where.And(it => it.IsDeleted == false);//删除的数据 不查询
            return Count(where);
        }
    }
   
}
#endif