#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TakeOutFoot.Activties;
using TakeOutFoot.Domain.Entities;

namespace TakeOutFoot.Signs
{
    /// <summary>
    /// 签到 实体
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("t_sign")]
    public class Sign : BaseEntity
    {

        ///// <summary>
        ///// 活动 id 
        ///// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("activty_id")]
        //[System.ComponentModel.DataAnnotations.StringLength(36)]
        //public virtual string ActivtyId { get; set; }

        /// <summary>
        /// 签到 名称 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("name")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 二维码 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("qr_code")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public virtual string QrCode { get; set; }
       
        
     

        /// <summary>
        /// 签到 开始时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("start_date")]
        public virtual DateTime StartDate { get; set; }


        /// <summary>
        /// 签到 结束时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("end_date")]
        public virtual DateTime EndDate { get; set; }


        /// <summary>
        /// 签到 天数  比如 签到 一个月 才能 获取该礼物
        /// </summary>
        [Column("sign_day")]
        public virtual int SignDay { get; set; }

        /// <summary>
        /// 积分 比如 签到 10000 积分 才能 获取该礼物
        /// </summary>
        [Column("score")]
        public virtual int Score { get; set; }

        /// <summary>
        /// 标识
        /// 0 签到 天数
        /// 1 积分
        /// </summary>
        public virtual int Flag { get; set; }

        /// <summary>
        ///礼物 id 即 奖品 id 
        /// </summary>
        [Column("gift_id")]
        public virtual long? GiftId { get; set; }
    }
}
#endif