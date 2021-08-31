using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{

    #region IdentityEntry
    /// <summary>自增缓存 </summary>
    internal class IdentityEntity
    {
        /// <summary>自增表 </summary>
        public string Table { get; set; }
        /// <summary>自增id </summary>
        public long Id { get; internal set; } = -1;

        /// <summary>自增id 自增 </summary>
        public void Increment()
        {
            lock (this)
            {
                long id = this.Id;
                Interlocked.Increment(ref id);
                this.Id = id;
            }
        }
    }

    #endregion IdentityEntry
}
