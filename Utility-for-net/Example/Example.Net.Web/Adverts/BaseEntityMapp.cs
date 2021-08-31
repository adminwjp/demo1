using NHibernate.Mapping.ByCode.Conformist;

namespace Adverts
{
    /// <summary>
    /// 基类 实体 nhibernate mapp
    /// </summary>
    public abstract class BaseEntityMapp<T> : ClassMapping<T> where T : BaseEntity
    {
        public BaseEntityMapp(string tableName)
        {
            Table(tableName);
            Id(x => x.Id,it=> { it.Length(36); });//主键
            this.Set();
            Property(it=>it.CreationTime);
            Property(it => it.LastModificationTime);
            Property(it => it.DeletionTime);
            Property(it => it.IsDeleted);
        }
        protected abstract void Set();
    }
}
