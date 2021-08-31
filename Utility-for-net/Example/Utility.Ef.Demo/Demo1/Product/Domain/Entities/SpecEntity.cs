#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Product.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    /// <summary>
    /// 产品 规格 
    /// </summary>
    [Table("t_spec")]
	public class SpecEntity:BaseEntity
    {
		/// <summary>
		/// 表名 
		/// </summary>
		public const string TableName = "t_spec";
		/// <summary>
		/// 产品 规格  库存
		/// </summary>
		[Column("stock")]
		public virtual int Stock { get; set; }
		/// <summary>
		/// 产品 规格  出售数量
		/// </summary>
		[Column("sales")]
		public virtual int Sales { get; set; }
		/// <summary>
		/// 产品 ID
		/// </summary>
		[Column("product_id")]
		public virtual long? ProductId { get; set; }
		/// <summary>
		///  进价 
		/// </summary>
		[Column("price")]
		public virtual decimal? Price { get; set; }
		/// <summary>
		/// 该 产品 规格 价格
		/// </summary>
		[Column("now_price")]
		public virtual decimal? NowPrice { get; set; }
		/// <summary>
		/// 产品 规格 尺寸
		/// </summary>
		[Column("size")]
		[StringLength(20)]
		public virtual string Size { get; set; }
		/// <summary>
		/// 产品 规格 状态
		/// </summary>
		[Column("status")]
		//[StringLength(1)]
		public virtual int Status { get; set; }
		/// <summary>
		/// 产品 规格 颜色
		/// </summary>
		[Column("color")]
		[StringLength(50)]
		public virtual string Color { get; set; }

		public virtual void AddStock(int num)
		{
			base.AddDomainEvent(new NumberChangeDomainEvent(Id, 0, num) { Flag = NumberEventFlag.Stock });
			this.Stock += num;
		}

		public virtual void RemoveStock(int num)
		{
			base.AddDomainEvent(new NumberChangeDomainEvent(Id, 0, -num) { Flag = NumberEventFlag.Stock });
			this.Stock -= num;
		}

		public virtual void UpdateSales(int num)
		{
			base.AddDomainEvent(new NumberChangeDomainEvent(Id, 0, num) { Flag = NumberEventFlag.SellCount });
			this.Sales += num;
		}
	}
}
#endif