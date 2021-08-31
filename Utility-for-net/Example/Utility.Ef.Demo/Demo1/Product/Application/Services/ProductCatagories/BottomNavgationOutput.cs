#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using AutoMapper;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Product.Application.Services.ProductCatagories
{
    public class BottomNavgationOutput
    {
        /// <summary>
        /// 主键 
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public virtual long Id { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
		/// 底部导航 链接
		/// </summary>
		public virtual string Link { get; set; }
        /// <summary>
        /// 底部导航 链接 跳转方式
        /// </summary>
        public virtual string Target { get; set; }
    }

    public class BottomOutput : BottomOutput<BottomOutput>
    {

    }
    public class BottomOutput<T> : NavgationOutput where T : BottomOutput<T>
    {
        [Newtonsoft.Json.JsonIgnore]
        public virtual List<long> Cids { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public virtual List<T> Children { get; set; }
    }

	[AutoMap(typeof(ProductCatagoryEntity))]
	public class CreateProductCatagoryInput//: IValidatableObject
	{
		/// <summary>
		/// 名称
		/// </summary>
		[Range(2, 20, ErrorMessageResourceName = "Catagory.name")]
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		[Range(2, 50, ErrorMessageResourceName = "Catagory.code")]
		public virtual string Code { get; set; }
		/// <summary>
		/// 排序 
		/// </summary>
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		public virtual string ParentId { get; set; }
		/// <summary>
		///  1 菜单 (显示导航)  2 二级 菜单 3 底部导航
		/// </summary>
		public virtual ProductCatagoryFlag Flag { get; set; }


		/// <summary>
		/// 底部导航 链接
		/// </summary>
		public virtual string Link { get; set; }
		/// <summary>
		/// 底部导航 链接 跳转方式
		/// </summary>
		public virtual string Target { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public virtual string Description { get; set; }

		//public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
		//{
		//    throw new NotImplementedException();
		//}
	}
	[AutoMap(typeof(ProductCatagoryEntity))]
	public class GetProductCatagoryInput: CreateProductCatagoryInput
	{
		
	}
	[AutoMap(typeof(ProductCatagoryEntity))]
	public class GetProductCatagoryOutput : CreateProductCatagoryInput
	{
		public virtual long Id { get; set; }
	
	}

	public class NavgationOutput
	{
		/// <summary>
		/// 主键 
		/// </summary>
		[Newtonsoft.Json.JsonIgnore]
		public virtual long Id { get; set; }


		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		public virtual string Code { get; set; }
	}

	/// <summary>
	/// 用户界面 查询 菜单 
	/// </summary>
	//[AutoMap(typeof(CatagoryEntity))]
	public class ProductCatagoryOutput : BottomOutput<ProductCatagoryOutput>
	{
		/// <summary>
		/// 商品信息
		/// </summary>
		public virtual List<ProductOutput> Products { get; set; }


	}/// <summary>
	 /// 用户界面 查询 菜单  热销 前 3 商品
	 /// </summary>
	//[AutoMap(typeof(ProductEntity))]
	public class ProductOutput
	{
		/// <summary>
		/// 主键 
		/// </summary>
		public virtual long Id { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
	}
	[AutoMap(typeof(ProductCatagoryEntity))]
	public class UpdateProductCatagoryInput : CreateProductCatagoryInput
	{
		public virtual string Id { get; set; }
	}
}
#endif