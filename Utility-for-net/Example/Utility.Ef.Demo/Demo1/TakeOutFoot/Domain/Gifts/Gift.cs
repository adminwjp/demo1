#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using TakeOutFoot.Domain.Entities;
using TakeOutFoot.Domain.Events;

namespace TakeOutFoot.Gifts
{
    /// <summary>
    /// 礼物 即 优惠券
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("t_gift")]
	public class Gift : BaseEntity
	{

		public const string TableName = "t_gift";

		/// <summary>
		/// 更新账户
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("update_account")]
		[System.ComponentModel.DataAnnotations.StringLength(20)]
		public virtual string UpdateAccount { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("status")]
		public virtual int Status { get; set; }

		/// <summary>
		/// 素材
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("picture")]
		[System.ComponentModel.DataAnnotations.StringLength(50)]
		public virtual string Picture { get; set; }

		/// <summary>
		/// 创建账户
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("careate_account")]
		[System.ComponentModel.DataAnnotations.StringLength(20)]
		public virtual string CreateAccount { get; set; }

		/// <summary>
		/// 礼物价格
		/// </summary>
		public virtual decimal Price { get; set; }

		/// <summary>
		/// 礼物名称
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("name")]
		[System.ComponentModel.DataAnnotations.StringLength(20)]
		public virtual string Name { get; set; }

		/// <summary>
		/// 出售数量
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("sell_count")]
		public virtual int SellCount { get; set; }


		/// <summary>
		/// 当前库存数量
		/// </summary>
		[System.ComponentModel.DataAnnotations.Schema.Column("stocks")]
		public virtual int Stocks { get; set; }

		/// <summary>
		/// 添加库存
		/// </summary>
		/// <param name="number"></param>
		public virtual void AddStock(int number)
		{
			this.Stocks += number;
			base.AddDomainEvent(new StockDomainEvent(this.Id, number,StockFlag.Gift));
		}

		/// <summary>
		/// 移除库存
		/// </summary>
		/// <param name="number"></param>
		public virtual void RemoveStock(int number)
		{
			if (this.Stocks < number)
			{
				throw new Exception("优惠券库存不足!");
			}
			this.Stocks -= number;
			base.AddDomainEvent(new StockDomainEvent(this.Id, -number, StockFlag.Gift));
		}

	}
}
#endif