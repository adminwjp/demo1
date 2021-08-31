#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TakeOutFoot.Domain.Events
{
    /// <summary>
    /// 库存 单向订阅 
    /// 这样可读性差  类多爆炸
    /// </summary>
    public class StockDomainEvent : INotification
    {

        public StockDomainEvent(long id, int number, StockFlag flag)
        {
            Id = id;
            Number = number;
            Flag = flag;
        }

        /// <summary>
        /// 代理商 、 商家、 平台 、买家 等 id
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// 数量 添加 + 移除 -
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// 库存 标识
        /// </summary>
        public StockFlag Flag { get; private set; }
    }

    /// <summary>
    /// 库存 标识
    /// </summary>
   [Serializable]
    public enum StockFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0x0,

        /// <summary>
        /// 代理商
        /// </summary>
        Agent = 0x1,

        /// <summary>
        /// 商家
        /// </summary>
        Business = 0x2,

        /// <summary>
        /// 平台
        /// </summary>
        Platform = 0x3,

        /// <summary>
        /// 礼物 即 优惠券
        /// </summary>
        Gift = 0x4,

        /// <summary>
        /// 活动 即 优惠券 活动
        /// </summary>
        Acitivty = 0x5,

        /// <summary>
        /// 活动设置 即 优惠券 数量 活动 
        /// </summary>
        AcitivtySetting = 0x6,
    }
}
#endif