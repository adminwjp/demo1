using Abp.NHibernate.EntityMappings;
using System;
using {#programName};

namespace {#programName}
{
    public abstract class BaseEntityMapp : EntityMap<T,string> where T:BaseEntity
    {
        public BaseEntityNhibernateMapp(string tableName)
               : base(tableName)
        {
            Id(x => x.Id).Not.Nullable().Length(36);//主键
            this.Set();
            this.MapCreationTime();
            this.MapLastModificationTime();
            this.MapIsDeleted();
            Map(x => x.DeletionTime).Nullable();
        }
        protected abstract void Set();
    }
}
