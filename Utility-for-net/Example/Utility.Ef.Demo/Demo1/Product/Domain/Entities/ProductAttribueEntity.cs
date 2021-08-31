#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//using Utility.Domain.Entities;

namespace Product.Domain.Entities
{
    /// <summary>
    /// 商品参数
    /// </summary>
    [Table("t_product_attribute")]
    public  class ProductAttribueEntity : BaseEntity
    {       
        /// <summary>
        /// 表名 
        /// </summary>
        public const string TableName = "t_product_attribute";
        /// <summary>
        /// 参数id
        /// </summary>
        [Column("attribute_id")]
        //[StringLength(36)]
        public long AttributeId { get; set; }
        /// <summary>
        /// 商品 id
        /// </summary>
        [Column("product_id")]
        //[StringLength(36)]
        public long ProductId { get; set; }
        [Column("value")]
        [StringLength(500)]
        public string Value { get; set; }
    }
}
#endif