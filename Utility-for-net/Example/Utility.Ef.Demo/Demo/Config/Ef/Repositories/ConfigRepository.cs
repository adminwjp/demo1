//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System.Linq.Expressions;
using System;
using Utility.Extensions;
using Utility.Domain.Uow;
using Config.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Ef;

namespace Config.Ef.Repositories
{
    /// <summary>配置 ef 数据访问层接口 实现  </summary>
    public    class ConfigRepository : BaseRepository<ConfigEntity>, IRepository<ConfigEntity,string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public ConfigRepository(DbContextProvider<ConfigDbContext> content):base(content) 
        {
        }
        /// <summary>查询wehere sql </summary>
        /// <param name="obj">服务信息</param>
        /// <returns></returns>
        protected override Expression<Func<ConfigEntity, bool>> GetWhere(ConfigEntity obj) 
        {
            Expression<Func<ConfigEntity, bool>> where=base.GetWhere(obj);
            if(!string.IsNullOrEmpty(obj.AddressTemplate))
            {
               where = where.Or(it=>it.AddressTemplate == obj.AddressTemplate);
            }
            if (!string.IsNullOrEmpty(obj.User))
            {
                where = where.Or(it => it.User == obj.User);
            }
            if (!string.IsNullOrEmpty(obj.Pwd))
            {
                where = where.Or(it => it.Pwd == obj.Pwd);
            }
            if (!string.IsNullOrEmpty(obj.Flag))
            {
               where = where.Or(it=>it.Flag == obj.Flag);
            }
            return where;
        }

       
    }
}
#endif