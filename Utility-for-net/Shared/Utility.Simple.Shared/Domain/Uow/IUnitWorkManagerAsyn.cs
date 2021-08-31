#if !(NET20 || NET30 || NET35 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Domain.Uow
{
    /// <summary>
    /// 统一 管理
    /// </summary>
    public interface IUnitWorkManagerAsyn
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task BeginAsyn(CancellationToken cancellationToken=default(CancellationToken));

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task CommitAsyn(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task RollbakAsyn(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task InsertAsyn<Entity>(Entity entity, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;


        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        Task InsertBatchAsyn<Entity>(Entity[] entities, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;

        /// <summary>
        /// 编辑
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsyn<Entity>(Entity entity, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;

        /// <summary>
        /// 编辑
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="where"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsyn<Entity>(Expression<Func<Entity,bool>> where, Expression<Func<Entity, Entity>> update, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;

        /// <summary>
        /// 编辑
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateBatchAsyn<Entity>(Entity[] entities, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsyn<Entity, Key>(Key id, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="ids"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteBatchAsyn<Entity, Key>(Key[] ids, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteAsyn<Entity>(Entity entity, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        Task DeleteBatchAsyn<Entity>(Entity[] entities, CancellationToken cancellationToken = default(CancellationToken)) where Entity : class;
    }
}
#endif