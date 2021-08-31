using System.Collections.Generic;

namespace Adverts
{
    public interface IBaseRepository<Entity>
        where Entity:BaseEntity
    {
        Entity Insert(Entity entity);
        Entity Update(Entity entity);
        void Delete(string id);
        void Delete(string[] ids);
         IList<Entity> Find(Entity entity);
         IList<Entity> FindByPage(Entity entity, int page, int size);
         int Count(Entity entity);
    }
}
