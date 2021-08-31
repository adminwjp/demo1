using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace {#programName}.EntityFramework.Repositories
{
    public abstract class {#programName}RepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<{#programName}DbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected {#programName}RepositoryBase(IDbContextProvider<{#programName}DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class {#programName}RepositoryBase<TEntity> : {#programName}RepositoryBase<TEntity, string>
        where TEntity : class, IEntity<int>
    {
        protected {#programName}RepositoryBase(IDbContextProvider<{#programName}DbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
