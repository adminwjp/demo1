//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using NHibernate.Criterion;
using System.Collections.Generic;
using NHibernate;
using Utility.Domain.Repositories;
using Config.Domain.Entities;
using Utility.Nhibernate;

namespace Config.Nhibernate.Repositories
{
    /// <summary>配置 nhibernate 数据访问层接口 实现   </summary>
    public    class ConfigRepository : Utility.Nhibernate.Repositories.BaseNhibernateRepository<ConfigEntity, string>, IRepository<ConfigEntity, string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public ConfigRepository(SessionProvider session): base(session) 
        {
        }
        /// <summary>
        /// 模糊查询 通用查询 默认实现
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override void QueryFilterByOr(List<NHibernate.Criterion.AbstractCriterion> criterias, ConfigEntity obj)
        {
            if (!string.IsNullOrEmpty(obj.Id))
            {
                criterias.Add(Expression.Eq("Id", obj.Id));
            }
            if (!string.IsNullOrEmpty(obj.Name))
            {
                criterias.Add(Expression.Eq("Name", obj.Name));
            }
            if (!string.IsNullOrEmpty(obj.AddressTemplate))
            {
                criterias.Add(Expression.Eq("AddressTemplate", obj.AddressTemplate));
            }
            if (!string.IsNullOrEmpty(obj.User))
            {
                criterias.Add(Expression.Eq("User", obj.User));
            }
            if (!string.IsNullOrEmpty(obj.Pwd))
            {
                criterias.Add(Expression.Eq("Pwd", obj.Pwd));
            }
            if (!string.IsNullOrEmpty(obj.Flag))
            {
                criterias.Add(Expression.Eq("Status", obj.Flag));
            }
            if (obj.CreateDate.HasValue)
            {
                criterias.Add(Expression.Eq("CreateDate", obj.CreateDate));
            }
            if (obj.LastDate.HasValue)
            {
                criterias.Add(Expression.Eq("LastDate", obj.LastDate));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string GetTable()
        {
            return ConfigEntity.TableName;
        }
        
    }
}
#endif