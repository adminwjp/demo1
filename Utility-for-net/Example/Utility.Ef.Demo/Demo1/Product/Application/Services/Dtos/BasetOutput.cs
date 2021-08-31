using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Services.Dtos
{
    public class BasetOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 创建 时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 修改 时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }
    }
}
