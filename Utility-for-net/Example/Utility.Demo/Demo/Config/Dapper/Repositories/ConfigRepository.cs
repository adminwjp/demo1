//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;
using Utility.Domain.Repositories;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using BaseDapperDAL = Utility.Dapper.Repositories.BaseDapperRepository<Config.Domain.Entities.ConfigEntity, string>;
using Utility.Dapper;

namespace Config.Dapper.Repositories
{
    /// <summary>配置 dapper 数据访问层接口 实现  </summary>
    public    class ConfigRepository: BaseDapperDAL, IRepository<ConfigEntity, string>
    {
        /// <summary> 构造注入数据库连接对象</summary>
        /// <param name="connection">数据库连接对象</param>
        public ConfigRepository(DapperConnectionProvider connection) :base(connection)
        {
        }

        /// <summary>根据id删除用实体类信息</summary>
        /// <param name="id">实体类 id</param>
        ///<return>返回删除实体类信息结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public override int Delete(string id)
        {
            //switch (SqlWayHelper.Way)
            //{
            //    case SqlWay.Procedure:
            //         base.Connection.Execute("ConfigDeleteById", new { id = id }, null, null, System.Data.CommandType.StoredProcedure);
            //         break;
            //    case SqlWay.View:
            //    case SqlWay.None:
            //    default:
            //         base.Delete(id);
            //         break;
            //}
          return  base.Delete(id);
        }


        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public override int DeleteList(string[] ids)
        {
            //switch (SqlWayHelper.Way)
            //{
            //    case SqlWay.Procedure:
            //        return base.Connection.Execute("ConfigDeleteByIds", new { ids = DALHelper.IdSql(ids) }, null, null, System.Data.CommandType.StoredProcedure);
            //         break;
            //    case SqlWay.View:
            //    case SqlWay.None:
            //    default:
            //        return DeleteList(ids);
            //         break;
            //}
          return  base.DeleteList(ids);
        }

     

        /// <summary>查询wehere sql </summary>
        /// <param name="obj">{#comment}信息</param>
        /// <returns></returns>
        protected override string GetWhere(ConfigEntity obj) 
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(obj.AddressTemplate))
            {
                where += " OR AddressTemplate= @AddressTemplate   ";
            }
            if (!string.IsNullOrEmpty(obj.Name))
            {
                where += " OR Name= @Name   ";
            }
            if (!string.IsNullOrEmpty(obj.Flag))
            {
                where += " OR Flag= @Flag   ";
            }
            if (!string.IsNullOrEmpty(obj.User))
            {
                where += " OR User= @User   ";
            }
            if (!string.IsNullOrEmpty(obj.Pwd))
            {
                where += " OR Pwd= @Pwd   ";
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
            return ConfigEntity.TableName;
        }

    }
}
#endif