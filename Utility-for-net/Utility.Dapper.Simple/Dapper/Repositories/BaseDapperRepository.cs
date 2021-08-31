#if !(NET10 || NET11 || NET20 || NET30 || NET35  || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
//#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System.Data;
using Utility.Domain.Entities;
using Utility.Domain.Repositories;
using Utility.Dapper.Uow;
using System;
using System.Threading.Tasks;
using System.Threading;
using Utility.Attributes;

namespace Utility.Dapper.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    [Transtation]
    public class BaseDapperRepository<T, Key> : BaseDapperRepository<T>, IRepository<T, Key>, IRepository<T> where T : class, IEntity<Key>
    {
        /// <summary> 构造注册数据库连接对象</summary>
        /// <param name="connection">数据库连接对象</param>
        public BaseDapperRepository(DapperConnectionProvider connection) : base(connection)
        {

        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeConnection"> 写库</param>
        /// <param name="readConnection">读库</param>
        public BaseDapperRepository(Lazy<DapperConnectionProvider> writeConnection, Lazy<DapperConnectionProvider> readConnection) : base(writeConnection, readConnection)
        {
   
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete(Key id)
        {
           return base.UnitWork.Delete<T>(id);
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList(Key[] ids)
        {
            int res = 0;
            foreach (var id in ids)
            {
                res+=base.UnitWork.Delete<T>(id);
            }
            return res;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult( Delete(id) );
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual Task<int> DeleteListAsync(Key[] ids, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(DeleteList(ids));
        }
    }



    /// <summary>dapper linq 不支持 需要自己转换 </summary>
    [Transtation]
    public class BaseDapperRepository<T> : BaseRepository<T>, IRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public new DapperUnitWork UnitWork { get;protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public BaseDapperRepository(DapperConnectionProvider connection) 
        {
            UnitWork = new DapperUnitWork(connection);
            UnitWork.UseTransaction = false;
            base.UnitWork = UnitWork;
        }

        /// <summary>
        ///  读写分离
        /// </summary>
        /// <param name="writeConnection"> 写库</param>
        /// <param name="readConnection">读库</param>
        public BaseDapperRepository(Lazy<DapperConnectionProvider> writeConnection, Lazy<DapperConnectionProvider> readConnection)
        {
            UnitWork = new DapperUnitWork(writeConnection,readConnection);
            base.UnitWork = UnitWork;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTable()
        {
            return null;
        }

        /// <summary>查询wehere sql </summary>
        /// <param name="obj">信息</param>
        /// <returns></returns>
        protected virtual string GetWhere(T obj)
        {
            return string.Empty;
        }
    }
}
#endif