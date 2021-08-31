//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )

using Config.Domain.Entities;
using Utility.Dapper;
using Utility.Domain.Repositories;
using BaseDapperDAL = Utility.Dapper.Repositories.BaseDapperRepository<Config.Domain.Entities.ServiceEntity, string>;

namespace Config.Dapper.Repositories
{
    /// <summary>服务 dapper 数据访问层接口 实现  </summary>
    public    class ServiceRepository: BaseDapperDAL, IRepository<ServiceEntity, string>
    {
        /// <summary> 构造注入数据库连接对象</summary>
        /// <param name="connection">数据库连接对象</param>
        public ServiceRepository(DapperConnectionProvider connection) :base(connection)
        {
        }

        /// <summary>查询wehere sql </summary>
        /// <param name="obj">{#comment}信息</param>
        /// <returns></returns>
        protected override string GetWhere(ServiceEntity obj) 
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(obj.Name))
            {
                where += " OR Name= @Name   ";
            }
            if (!string.IsNullOrEmpty(obj.Ip))
            {
                where += " OR Ip= @Ip   ";
            }
            if (obj.Port != 0)
            {
                where += " OR Port= @Port   ";
            }
            if (obj.Status != 0)
            {
                where += " OR Status= @Status   ";
            }
            where = where.Length > 0 ? where.Substring(4) : where;
            return where;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        protected override string GetTable()
        {
            return ServiceEntity.TableName;
        }

    }
}
#endif