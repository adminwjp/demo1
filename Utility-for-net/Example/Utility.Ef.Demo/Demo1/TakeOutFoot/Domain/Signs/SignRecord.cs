#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TakeOutFoot.Domain.Entities;

namespace TakeOutFoot.Signs
{

    /// <summary>
    /// 签到 记录
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("t_sign_record")]
    public class SignRecord: BaseEntity
    {
        /// <summary>
        /// 代理商 、 商家、 平台、买家 id 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("user_id")]
       public virtual long? UserId { get; set; }

        /// <summary>
        /// 签到 id 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("sign_id")]
        public virtual long? SignId { get; set; }

        /// <summary>
        /// 签到 时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("sign_date")]
        public virtual DateTime SignDate { get; set; }

    }
}
#endif