//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 实体
    /// </summary>
    [TableName("tn_PointRechargeOrders")]
    [PrimaryKey("Id", autoIncrement = false)]
    [CacheSetting(true)]
    public class PointRechargeOrder : IEntity
    {
        /// <summary>
        /// 新建实体时使用
        /// </summary>
        public static PointRechargeOrder New()
        {
            PointRechargeOrder pointRechargeOrder = new PointRechargeOrder()
            {
                Id = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss") + BuildRandomStr(3)),
                Description = string.Empty,
                TradingAccount = string.Empty,
                TradeNo = string.Empty,
                DateCreated = DateTime.Now
            };
            return pointRechargeOrder;
        }

        #region 需持久化属性

        /// <summary>
        ///订单号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///积分
        /// </summary>
        public int TradePoints { get; set; }

        /// <summary>
        ///金额
        /// </summary>
        public float TotalPrice { get; set; }

        /// <summary>
        ///支付方式
        /// </summary>
        public Buyway Buyway { get; set; }

        /// <summary>
        ///支付媒介类型
        /// </summary>
        public PayMediaType PayMediaType { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///交易账号
        /// </summary>
        public string TradingAccount { get; set; }

        /// <summary>
        ///订单状态
        /// </summary>
        public RechargeOrdeStatus Status { get; set; }

        /// <summary>
        ///流水账号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        #endregion 需持久化属性

        #region 扩展方法

        /// <summary>
        /// 生成订单取随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }
            return str;
        }

        #endregion 扩展方法

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}