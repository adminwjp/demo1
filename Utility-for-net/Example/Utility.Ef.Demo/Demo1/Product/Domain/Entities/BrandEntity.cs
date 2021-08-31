#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Product.Domain.Entities
{ 
    [Table("t_brand")]
    public class BrandEntity : IEntity<long>
    {  
        [Column("id")]
     
		public virtual long Id { get; set; }
        [Column("letter")]
        [StringLength(50)]
        public virtual string Letter { get; set; }
         [Column("name")]
        [StringLength(50)]
         public virtual string Name { get; set; }
         [Column("product_catagory_id")]
     
		public virtual long ProductCatagoryId { get; set; }
       
         [Column("orders")]
         public virtual int Orders { get; set; }
         [Column("product_count")]
          public virtual long ProductCount { get; set; }
         [Column("shop_id")]
          public virtual long ShopId { get; set; }
         
         [Column("factory_status")]
        public virtual bool FactoryStatus { get; set; } = true;//品牌制造商
        [Column("if_show")]
        public virtual bool IfShow { get; set; } = true;//是否显示
         [Column("comment_count")]
        public virtual long CommentCount { get; set; }
        [Column("logo")]
         [StringLength(50)]
        public virtual string Logo { get; set; }
         [Column("images")]
         [StringLength(500)]
        public virtual string Images { get; set; }
         [Column("logo_id")]
        public virtual long LogoId { get; set; }
         [Column("image_ids")]
         [StringLength(500)]
        public virtual long ImageIds { get; set; }
       
        [Column("big_pic")]
         [StringLength(50)]
        public virtual string BigPic { get; set; }
        [Column("brand_story")]
         [StringLength(50)]
        public virtual string BrandStory { get; set; }

        [Column("tag")]
         [StringLength(50)]
        public virtual string Tag { get; set; }
        [Column("description")]
         [StringLength(500)]
        public virtual string Description { get; set; }

        public virtual bool IsTransient()
        {
			return true;
        }
    }
}
#endif