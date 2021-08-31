#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using TakeOutFoot.Domain.Entities;
using TakeOutFoot.Domain.Events;

namespace TakeOutFoot.Activties
{
    /// <summary>
    /// 活动 标识
    /// </summary>
    [Flags]
    public enum ActivtyFlag
    {
        /// <summary>
        ///无
        /// </summary>
        None=0x0,

        /// <summary>
        /// 规定时间 做活动
        /// </summary>
        Date=0x1,

        /// <summary>
        /// 礼物发送完则结束
        /// </summary>
        GiftFree=0x2
    }

    /// <summary>
    /// 活动 用户 标识
    /// </summary>
    public enum ActivtySellerFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None=0x0,

        /// <summary>
        /// 代理商
        /// </summary>
        Agent=0x1,

        /// <summary>
        /// 商家
        /// </summary>
        Business=0x2,

        /// <summary>
        /// 平台
        /// </summary>
        Platform=0x3
    }
    /// <summary>
    /// 活动 实体
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("t_activty")]
    public class Activty : BaseEntity
    {

        /// <summary>
        /// 代理商 、 商家、 平台 id 
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("seller_id")]
        
        public virtual long? SellerId { get; set; }

        /// <summary>
        /// 活动 用户 标识
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("seller_flag")]
        public virtual ActivtySellerFlag SellerFlag { get; set; }

        /// <summary>
		/// 代理商 、 商家、 平台 账户
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("account")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public virtual string Account { get; set; }

        /// <summary>
        /// 活动 参与抽奖物品数 即 优惠券
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("number")]
        public virtual int Number { get; set; }

        /// <summary>
        /// 活动 开始时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("start_date")]
        public virtual DateTime StartDate { get; set; }


        /// <summary>
        /// 活动 结束时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("end_date")]
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// 活动 标识
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("flag")]
        public virtual ActivtyFlag Flag { get; set; }

        /// <summary>
        /// 活动 类型
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("activty_type")]
        public virtual int ActivtyType { get; set; }


        /// <summary>
        /// 后台 通知活动 提前结束 (奖品已抽中)
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("end")]
        public virtual bool End { get; set; }

        ///// <summary>
        /////活动 中奖 玩家 ID 最多 20 个玩家(买家)
        ///// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("buyer_ids")]
        //[System.ComponentModel.DataAnnotations.StringLength(750)]
        //public virtual string BuyerIds { get; set; }

        /// <summary>
        /// 后台 通知活动 提前结束 原因
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("reason")]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public virtual string Reason { get; set; }

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual  List<ActivtySetting> ActivtySettings { get; set; }

        /// <summary>
        /// 添加库存
        /// </summary>
        /// <param name="number"></param>
        public virtual void AddStock(int number)
        {
            this.Number += number;
            base.AddDomainEvent(new StockDomainEvent(this.SellerId.Value,number, StockFlag.Acitivty));
        }

        /// <summary>
        /// 移除库存
        /// </summary>
        /// <param name="number"></param>
        public virtual void RemoveStock(int number)
        {
            if (this.Number < number)
            {
                throw new Exception("活动库存不足!");
            }
            this.Number -= number;
            base.AddDomainEvent(new StockDomainEvent(this.SellerId.Value, -number, StockFlag.Acitivty));
        }
    }
}
#endif