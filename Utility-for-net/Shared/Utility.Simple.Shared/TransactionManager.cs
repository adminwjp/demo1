using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif

namespace Utility
{
    public class TransactionManager : IDisposable
    {
        public virtual bool IsCompleted { get ; protected set; }
        /// <summary>
        /// true 可以 提交 否则 不可以提交
        /// </summary>
        public virtual bool TaskCommit { get; set; }
        /// <summary>
        /// 执行的任务数
        /// </summary>
        public virtual int TaskCount { get; set; }
        /// <summary>
        /// 使用时 标识 true 
        /// </summary>
        protected virtual bool Used { get; set; }

        public virtual string Id { get; set; }
        public virtual bool HasTransaction()
        {
            return false;
        }
        public virtual void Begin()
        {

        }
        public virtual void Commit()
        {

        }
        public virtual void RollBack()
        {

        }
        public virtual void Dispose()
        {

        }
#if !(NET20 || NET30 || NET35)
        public virtual Task BeginAsync(CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }
        public virtual Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }
        public virtual Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

       
#endif
    }
}
