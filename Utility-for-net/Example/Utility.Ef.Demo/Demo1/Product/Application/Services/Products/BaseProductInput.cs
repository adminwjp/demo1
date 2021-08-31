#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using AutoMapper;
using Product.Application.Services.Specs;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Services.Products
{
    
	/// <summary>
	///  产品 实体
	/// </summary>
	public class BaseProductInput
	{
		/// <summary>
		/// 目录分类 id
		/// </summary>
		public virtual long CatagoryID { get; set; }
		/// <summary>
		/// 出售数量
		/// </summary>
		public virtual int Sales { get; set; }
		/// <summary>
		/// 库存
		/// </summary>
		public virtual int Stock { get; set; }
		/// <summary>
		/// 关键字
		/// </summary>
		public virtual string Keywords { get; set; }
		/// <summary>
		/// 评分
		/// </summary>
		public virtual int Score { get; set; }
		/// <summary>
		/// 热销 数量
		/// </summary>
		public virtual int Hit { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public virtual string Title { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public virtual decimal? Price { get; set; }
		/// <summary>
		/// 活动 id
		/// </summary>
		public virtual string ActivityID { get; set; }
		/// <summary>
		/// 状态 
		/// </summary>
		public virtual int Status { get; set; }
		/// <summary>
		/// 产品 显示 html
		/// </summary>
		public virtual string ProductHTML { get; set; }
		/// <summary>
		/// 是否 显示 新闻
		/// </summary>
		public virtual bool IsNew { get; set; }
		/// <summary>
		/// 介绍
		/// </summary>
		public virtual string Introduce { get; set; }
		/// <summary>
		/// 搜索关键词
		/// </summary>
		public virtual string SearchKey { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		public virtual string Images { get; set; }
		/// <summary>
		/// 当前价格
		/// </summary>
		public virtual decimal? MakePrice { get; set; }
		/// <summary>
		/// 最大价格
		/// </summary>
		public virtual string MaxPicture { get; set; }
		/// <summary>
		/// 单价 $ r
		/// </summary>
		public virtual string Unit { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		public virtual string Picture { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 是否出售
		/// </summary>
		public virtual bool Sale { get; set; }
		/// <summary>
		/// 礼物 id
		/// </summary>
		public virtual long GiftID { get; set; }
	}

	/// <summary>
	/// 添加 产品 实体
	/// </summary>
	[AutoMap(typeof(ProductEntity))]
	public class CreateProductInput : BaseProductInput
	{
		/// <summary>
		/// 创建账户
		/// </summary>
		public virtual string CreateAccount { get; set; }
	}

	public class LeftHotProductOutput
	{
		public virtual long Id { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public virtual decimal? Price { get; set; }
		/// <summary>
		/// 当前价格
		/// </summary>
		public virtual decimal? NowPrice { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		public virtual string Picture { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
	}
	
	/// <summary>
	///查询 产品   返回 实体
	/// </summary>
	[AutoMap(typeof(ProductEntity))]
	public class ProductAndSpecDto : ProductDto
	{
		public virtual long Id { get; set; }
		public IList<GetSpecOutput> Specs { get; set; }
	}


	/// <summary>
	///查询 产品   返回 实体
	/// </summary>
	[AutoMap(typeof(ProductEntity))]
	public class ProductDto : BaseProductInput
	{
		
	}
	/// <summary>
	/// 查询 产品  
	/// </summary>
	[AutoMap(typeof(ProductEntity))]
	public class GetProductInput : BaseProductInput
	{
		public virtual string Id { get; set; }
	}
	/// <summary>
	///查询 产品   返回 实体
	/// </summary>
	[AutoMap(typeof(ProductEntity))]
	public class GetProductOutput : BaseProductInput
	{

	}    /// <summary>
		 /// 修改 产品 实体
		 /// </summary>
	[AutoMap(typeof(ProductEntity))]
	public class UpdateProductInput : BaseProductInput
	{
		/// <summary>
		/// 主键
		/// </summary>
		public virtual string Id { get; set; }
		/// <summary>
		/// 更新账户
		/// </summary>
		public virtual string UpdateAccount { get; set; }
	}
}
#endif