#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using AutoMapper;
using Product.Application.Services.Dtos;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services.ProductCatagoryAttributes.Dtos
{
    public class BaseCatagoryAttributeInput
    {
		/// <summary>
		/// 名称
	  /// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		public virtual long CatagoryId { get; set; }
		/// <summary>
		/// 排序  最好不要用 sql  关键词
		/// </summary>
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		public virtual long ParentId { get; set; }

	}
	[AutoMap(typeof(ProductCatagoryAttribueEntity))]
	public class GetCatagoryAttributeInput : BaseCatagoryAttributeInput
	{

	}

	[AutoMap(typeof(ProductCatagoryAttribueEntity))]
	public class GetCatagoryAttributeOutput : BasetOutput
	{
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		public virtual long CatagoryId { get; set; }
		/// <summary>
		/// 排序  最好不要用 sql  关键词
		/// </summary>
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		public virtual long ParentId { get; set; }
	}
	[AutoMap(typeof(ProductCatagoryAttribueEntity))]
	public class CreateCatagoryAttributeInput : BaseCatagoryAttributeInput
	{
	}

	[AutoMap(typeof(ProductCatagoryAttribueEntity))]
	public class UpdateCatagoryAttributeInput : BaseCatagoryAttributeInput
	{
		public virtual string Id { get; set; }
	}
}
#endif