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
	/// 产品 实体
	/// </summary>
    [Table("t_product")]
	public class ProductEntity:BaseEntity
    {
		/// <summary>
		/// 表名 
		/// </summary>
		public const string TableName = "t_product";
		/// <summary>
		/// 目录分类 id
		/// </summary>
		[Column("catagory_id")]
		//[StringLength(36)]
		public virtual long CatagoryId { get; set; }
		/// <summary>
		/// 出售数量
		/// </summary>
		[Column("sales")]
		public virtual int Sales { get; set; }
		/// <summary>
		/// 库存
		/// </summary>
		[Column("stock")]
		public virtual int Stock { get; set; }
		/// <summary>
		/// 关键字
		/// </summary>
		[Column("keywords")]
		[StringLength(100)]
		public virtual string Keywords { get; set; }
		/// <summary>
		/// 评分
		/// </summary>
		[Column("score")]
		public virtual int Score { get; set; }
		/// <summary>
		/// 创建账户
		/// </summary>
		[Column("create_account")]
		[StringLength(20)]
		public virtual string CreateAccount { get; set; }
		/// <summary>
		/// 热销 数量
		/// </summary>
		[Column("hit")]
		public virtual int Hit { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		[Column("title")]
		[StringLength(50)]
		public virtual string Title { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		[Column("price")]
		public virtual decimal? Price { get; set; }

		/// <summary>
		/// 当前价格
		/// </summary>
		[Column("now_price")]
		public virtual decimal? NowPrice { get; set; }

		/// <summary>
		/// 更新账户
		/// </summary>
		[Column("update_account")]
		[StringLength(20)]
		public virtual string UpdateAccount { get; set; }
		
		
		/// <summary>
		/// 活动 id
		/// </summary>
		[Column("activity_id")]
		//[StringLength(36)]
		public virtual long ActivityId { get; set; }
		/// <summary>
		/// 状态 
		/// </summary>
		[Column("status")]
		public virtual int Status { get; set; }
		/// <summary>
		/// 产品 显示 html
		/// </summary>
		[Column("product_html")]
		[StringLength(int.MaxValue)]
		public virtual string ProductHTML { get; set; }
		/// <summary>
		/// 是否 显示 新闻
		/// </summary>
		[Column("is_new")]
		//[StringLength(1)]
		public virtual bool IsNew { get; set; }
		/// <summary>
		/// 介绍
		/// </summary>
		[Column("introduce")]
		[StringLength(int.MaxValue)]
		public virtual string Introduce { get; set; }
		/// <summary>
		/// 搜索关键词
		/// </summary>
		[Column("search_key")]
		[StringLength(500)]
		public virtual string SearchKey { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		[Column("images")]
		[StringLength(1000)]
		public virtual string Images { get; set; }
		/// <summary>
		/// 高清素材
		/// </summary>
		[Column("max_picture")]
		[StringLength(100)]
		public virtual string MaxPicture { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		[Column("description")]
		[StringLength(int.MaxValue)]
		public virtual string Description { get; set; }
		/// <summary>
		/// 单价 $ r
		/// </summary>
		[Column("unit")]
		[StringLength(5)]
		public virtual string Unit { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		[Column("picture")]
		[StringLength(100)]
		public virtual string Picture { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		[Column("name")]
		[StringLength(50)]
		public virtual string Name { get; set; }
		/// <summary>
		/// 是否出售
		/// </summary>
		[Column("sale")]
		public virtual bool Sale { get; set; }
		/// <summary>
		/// 礼物 id
		/// </summary>
		[Column("gift_id")]
		//[StringLength(36)]
		public virtual long GiftID { get; set; }

		/// <summary>
		/// 不使用外键
		/// </summary>
		[NotMapped]
		public virtual List<SpecEntity> Specs { get; set; }

		public virtual void AddStock(int num)
        {
			base.AddDomainEvent(new NumberChangeDomainEvent(Id, 0, num) { Flag= NumberEventFlag.Stock});
			this.Stock+=num;
        }

		public virtual void RemoveStock(int num)
		{
			base.AddDomainEvent(new NumberChangeDomainEvent(Id, 0, -num) { Flag= NumberEventFlag.Stock});
			this.Stock -= num;
		}

		public virtual void UpdateSales(int num)
		{
			base.AddDomainEvent(new NumberChangeDomainEvent(Id, 0, num) { Flag= NumberEventFlag.SellCount});
			this.Sales += num;
		}

	}
}
#endif