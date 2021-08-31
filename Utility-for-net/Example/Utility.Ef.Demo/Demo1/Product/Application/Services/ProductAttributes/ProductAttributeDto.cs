#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using AutoMapper;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Services.ProductAttributes
{
   public class BaseProductAttributeDto
    {
        /// <summary>
      /// 参数id
      /// </summary>
        public long AttributeId { get; set; }
        /// <summary>
        /// 商品 id
        /// </summary>
        public long ProductId { get; set; }

        public string Value { get; set; }
    }

    [AutoMap(typeof(ProductAttribueEntity))]
    public class CreateProductAttributeInput : BaseProductAttributeDto
    {
    }

    [AutoMap(typeof(ProductAttribueEntity))]
    public class GetProductAttributeInput : BaseProductAttributeDto
    {
    }
    [AutoMap(typeof(ProductAttribueEntity))]
    public class GetProductAttributeOutput : BaseProductAttributeDto
    {
        public virtual long Id { get; set; }
    }
    [AutoMap(typeof(ProductAttribueEntity))]
    public class UpdateProductAttributeInput : BaseProductAttributeDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual long Id { get; set; }
    }
}
#endif