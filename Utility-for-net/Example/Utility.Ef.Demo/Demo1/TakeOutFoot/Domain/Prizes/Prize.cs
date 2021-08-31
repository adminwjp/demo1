#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using TakeOutFoot.Domain.Entities;

namespace TakeOutFoot.Prizes
{
    /// <summary>
    /// 奖品 即 优惠券
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("t_prize")]
    public class Prize: BaseEntity
    {

        /// <summary>
        /// 中奖 玩家 ID
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("user_id")]
        [System.ComponentModel.DataAnnotations.StringLength(36)]
        public virtual string UserId { get; set; }

        /// <summary>
        /// 中奖账号
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("account")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public virtual string Account { get; set; }

        /// <summary>
        /// 中奖玩家 手机号
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("phone")]
        [System.ComponentModel.DataAnnotations.StringLength(11)]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 中奖玩家 地址
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("address")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 礼物id
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("gift_id")]
       public virtual long? GiftId { get; set; }

    }
}
#endif