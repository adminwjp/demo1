//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

using System.Linq.Expressions;
using System;
using Utility.Extensions;
using Config.Domain.Entities;
using Utility.Ef;

namespace Config.Ef.Repositories
{
    /// <summary>基类 ef 数据访问层接口 实现  </summary>
    public    class BaseRepository<Entity> : Utility.Ef.Repositories.BaseEfRepository<ConfigDbContext,Entity, string>
        where Entity:BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public BaseRepository(DbContextProvider<ConfigDbContext> content):base(content) 
        {
        }
        /// <summary>查询wehere sql </summary>
        /// <param name="obj">服务信息</param>
        /// <returns></returns>
        protected override Expression<Func<Entity, bool>> GetWhere(Entity obj)
        {
            Expression<Func<Entity, bool>> where = null;
            if (!string.IsNullOrEmpty(obj.Id))
            {
                where = where.Or(it => it.Id == obj.Id);
            }
            if (!string.IsNullOrEmpty(obj.Name))
            {
                where = where.Or(it => it.Name == obj.Name);
            }
            if (!string.IsNullOrEmpty(obj.Ip))
            {
                where = where.Or(it => it.Ip == obj.Ip);
            }
            if (obj.Port > 0)
            {
                where = where.Or(it => it.Port == obj.Port);
            }
            if (obj.CreateDate.HasValue)
            {
                where = where.Or(it => it.CreateDate == obj.CreateDate);
            }
            if (obj.LastDate.HasValue)
            {
                where = where.Or(it => it.LastDate == obj.LastDate);
            }
            return where;
        }
       
    }
}
#endif