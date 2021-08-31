using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Attributes;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Ioc;

namespace Utility.Demo.Application.Services
{
    public interface IIsDefaultAppService<Entity>: ICrudAppService<Entity, long>
         where Entity : class, IEntity<long>, IIsDefault, new()
    {
      
        int SetDefault(long id);
    }
    [Transtation]
    public class IsDefaultAppService<Entity>: CrudAppService<Entity, long>, IIsDefaultAppService<Entity>
        where Entity: class, IEntity<long>,IIsDefault,new()

    {
        public IsDefaultAppService(//IIocManager iocManager
            IRepository<Entity, long> repository
            ) : base(repository)
        {
            //IocManager = iocManager;
            //Repository = IocManager.Get<IRepository<Entity, long>>("DemoNhibernateRepository");
        }
        public override int Insert(Entity entity)
        {
            if (!UnitWork.IsExist<UserEntity>(entity.UserId))
            {
                return -1;
            }
            if (entity.IsDefault)
            {
                Repository.Update(it => it.UserId == entity.UserId, it => new Entity()
                {
                    IsDefault=false
                });
            }
            return base.Insert(entity);
        }
        public override int Update(Entity entity)
        {
            var old = Repository.FindSingle(it => it.Id == entity.Id && entity.UserId == it.UserId);
            if (old == null)
            {
                return -1;
            }
            if (!UnitWork.IsExist<UserEntity>(entity.UserId))
            {
                return -1;
            }
            if (old.IsDefault&&!entity.IsDefault)
            {
                //if (Repository.Count(it => it.UserId == entity.UserId) > 1)
                {
                    Repository.Update(it => it.UserId == entity.UserId && it.Id != entity.Id, it => new Entity()
                    {
                        IsDefault = false
                    });
                }
            }
            return base.Update(entity);
        }
        public virtual int SetDefault(long id)
        {
            var adrr = Repository.FindSingle(id);
            if (adrr == null)
            {
                return -1;
            }
            if (adrr.IsDefault)
            {
                return 1;
            }
            Repository.Update(it => it.UserId == adrr.UserId&&it.Id!=id, it => new Entity()
            {
                IsDefault = false
            });
            adrr.IsDefault = true;
            return Repository.Update(adrr);
        }
        public override int Delete(long id)
        {
            var old = Repository.FindSingle(it => it.Id == id);
            if (old == null)
            {
                return -1;
            }
            if (old.IsDefault)
            {
                var first = Repository.Query(it => it.UserId == old.UserId).OrderBy(it => it.Id)
                     .FirstOrDefault();
                if (first != null)
                {
                    first.IsDefault = true;
                    return Repository.Update(first);
                }
                return 1;
            }
            return base.Delete(id);
        }
    }
}
