#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using TakeOutFoot.Gifts;
using System;
using TakeOutFoot.Domain.Entities;
using TakeOutFoot.Domain.Events;

namespace TakeOutFoot.Activties
{
    /// <summary>
    /// 活动设置
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("t_activty_setting")]
    public class ActivtySetting : BaseEntity
    {
        /// <summary>
        /// 礼物id
        /// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("gift_id")]
        //[System.ComponentModel.DataAnnotations.StringLength(36)]
        //public virtual string GiftId { get; set; }

        /// <summary>
        /// 活动设置 当前参与抽奖物品数
        /// 可以为 0 比如 谢谢光临
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("number")]
        public virtual int Number { get; set; }


        /// <summary>
        /// 活动 id
        /// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("activty_id")]
        //[System.ComponentModel.DataAnnotations.StringLength(36)]
        //public virtual string ActivtyId { get; set; }

        //这个 ef bug  外键列要与 普通属性名称一致 不然会创建多个  GiftId1 ActivtyId1 列
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("activty_id")]
       // [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual Activty Activty { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("gift_id")]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual Gift Gift { get; set; }

      public virtual long? activty_id { get; set; }

      
        public virtual long? gift_id { get; set; }


        /// <summary>
        /// 添加库存
        /// </summary>
        /// <param name="number"></param>
        public virtual void AddStock(int number)
        {
            this.Number += number;
            base.AddDomainEvent(new StockDomainEvent(this.activty_id.Value, number, StockFlag.AcitivtySetting));
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
            base.AddDomainEvent(new StockDomainEvent(this.activty_id.Value, -number, StockFlag.AcitivtySetting));
        }
    }
}
#endif