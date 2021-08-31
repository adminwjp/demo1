using Abp.Domain.Entities;
using Abp.NHibernate;
using Abp.NHibernate.Repositories;

namespace {#programName}
{
    public abstract class {#programName}RepositoryBase<TEntity, TPrimaryKey> : NhRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected {#programName}RepositoryBase(ISessionProvider sessionProvider) : base(sessionProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class {#programName}RepositoryBase<TEntity> : {#programName}RepositoryBase<TEntity, string>
        where TEntity : class, IEntity<int>
    {
        protected {#programName}RepositoryBase(ISessionProvider sessionProvider) : base(sessionProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
