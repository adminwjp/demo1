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
    /// 签到 配置 实体
    /// 天数 签到 
    /// 还是 积分签到
    /// </summary>
    [Table("t_sign_config")]
    public class SignConfig: BaseEntity
    {
        /// <summary>
        /// 签到 id 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("sign_id")]
        public virtual long? SignId { get; set; }

        /// <summary>
        /// 签到 天数  
        /// 签到 10天 才能 获取该礼物
        /// 签到 1个月 才能 获取该礼物
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("sign_day")]
        public virtual int SignDay { get; set; }

        /// <summary>
        /// 积分
        /// 签到 1000 积分 才能 获取该礼物
        /// 签到 10000 积分 才能 获取该礼物
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("score")]
        public virtual int Score { get; set; }



        /// <summary>
        ///礼物 id 即 奖品 id 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("gift_id")]
        public virtual long? GiftId { get; set; }

    }
}
#endif