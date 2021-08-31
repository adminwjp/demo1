#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//using Utility.Domain.Entities;

namespace Product.Domain.Entities
{
	/// <summary>
	/// 分类
	/// </summary>
    [Table("t_product_catagory")]
    public class ProductCatagoryEntity:BaseEntity
    {
		/// <summary>
		/// 表名 
		/// </summary>
		public const string TableName = "t_product_catagory";
		/// <summary>
		/// 名称
		/// </summary>
		[Column( "name")]
		[StringLength(20)]
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		[Column("code")]
		[StringLength(50)]
		public virtual string Code { get; set; }
		/// <summary>
		/// 排序 
		/// </summary>
		[Column("orders")]
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		[Column("parent_id")]
		//[StringLength(36)]
		public virtual long ParentId { get; set; }
		[Column("shop_id")]
		public virtual long ShopId { get; set; }
		/// <summary>
		/// 底部导航 链接
		/// </summary>
		[Column("link")]
		[StringLength(100)]
		public virtual string Link { get; set; }
		/// <summary>
		/// 底部导航 链接 跳转方式
		/// </summary>
		[Column("target")]
		[StringLength(10)]
		public virtual string Target { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		[Column("description")]
		[StringLength(500)]
		public virtual string Description { get; set; }


		/// <summary>
		///  1 菜单 (显示导航)  2 二级 菜单 3 底部导航
		/// </summary>
		[Column("flag")]
		public virtual ProductCatagoryFlag Flag { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		[Column("image_id")]
		//[StringLength(36)]
		public virtual long ImageId { get; set; }

		/// <summary>
		/// 不使用外键
		/// </summary>
		[NotMapped]
		public virtual List<ProductCatagoryEntity> Catagorys { get; set; }
		/// <summary>
		/// 不使用外键
		/// </summary>
		[NotMapped]
		public virtual List<ProductCatagoryAttribueEntity> CatagoryAttribues { get; set; }
		/// <summary>
		/// 不使用外键
		/// </summary>
		[NotMapped]
		public virtual List<ProductEntity> Products { get; set; }

		public virtual void Update(ProductCatagoryEntity CatagoryEntity)
		{
			base.Update(CatagoryEntity);
			this.Name = CatagoryEntity.Name;
			this.Code = CatagoryEntity.Code;
			this.Orders = CatagoryEntity.Orders;
			this.ParentId = CatagoryEntity.ParentId;
			this.Flag = CatagoryEntity.Flag;
		}
	}
}
#endif