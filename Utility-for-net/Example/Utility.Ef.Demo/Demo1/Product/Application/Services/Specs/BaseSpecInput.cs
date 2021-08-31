#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using AutoMapper;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Services.Specs
{
    /// <summary>
    /// 产品 规格 
    /// </summary>
    public class BaseSpecInput
    {
		/// <summary>
		/// 产品 规格  库存
		/// </summary>
		public virtual long Stock { get; set; }
		/// <summary>
		/// 产品 ID
		/// </summary>
		public virtual long ProductID { get; set; }
		/// <summary>
		/// 该 产品 规格 价格
		/// </summary>
		public virtual decimal? Price { get; set; }
		/// <summary>
		/// 产品 规格 尺寸
		/// </summary>
		public virtual string Size { get; set; }
		/// <summary>
		/// 产品 规格 状态
		/// </summary>
		public virtual int Status { get; set; }
		/// <summary>
		/// 产品 规格 颜色
		/// </summary>
		public virtual string Color { get; set; }
	}

	/// <summary>
	///添加 产品 规格 
	/// </summary>
	[AutoMap(typeof(SpecEntity))]
	public class CreateSpecInput : BaseSpecInput
	{
	}
	/// <summary>
	 /// 查询 产品 规格 
	 /// </summary>
	[AutoMap(typeof(SpecEntity))]
    public class GetSpecInput : BaseSpecInput
	{

	}  
	/// <summary>
	   ///查询 产品 规格  返回 实体
	   /// </summary>
	[AutoMap(typeof(SpecEntity))]
	public class GetSpecOutput : BaseSpecInput
	{
		public virtual long Id { get; set; }
	}

	/// <summary>
	///修改 产品 规格 
	/// </summary>
	[AutoMap(typeof(SpecEntity))]
	public class UpdateSpecInput : BaseSpecInput
	{
		/// <summary>
		/// 主键
		/// </summary>
		public virtual string Id { get; set; }
	}
}
#endif