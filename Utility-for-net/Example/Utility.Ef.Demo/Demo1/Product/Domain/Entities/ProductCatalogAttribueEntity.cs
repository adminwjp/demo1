#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//using Utility.Domain.Entities;

namespace Product.Domain.Entities
{
	/// <summary>
	/// 分类 属性
	/// </summary>
	[Table("t_catagory_attribue")]
	public class ProductCatagoryAttribueEntity : BaseEntity
    {
		/// <summary>
		/// 表名 
		/// </summary>
		public const string TableName = "t_catagory_attribue";
		/// <summary>
		/// 名称
		/// </summary>
		[Column("name")]
		[StringLength(20)]
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		[Column("catagory_id")]
		//[StringLength(36)]
		public virtual long CatagoryId { get; set; }
		/// <summary>
		/// 排序  最好不要用 sql  关键词
		/// </summary>
		[Column("orders")]
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		[Column("parent_id")]
		//[StringLength(36)]
		public virtual long ParentId { get; set; }

		/// <summary>
		/// 不使用外键
		/// </summary>
		[NotMapped]
		public virtual List<ProductAttribueEntity> ProductAttribues { get; set; }
	}
}
#endif