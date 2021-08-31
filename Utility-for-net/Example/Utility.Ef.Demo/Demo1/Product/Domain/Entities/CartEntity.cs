using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain.Entities;

namespace Product.Domain.Entities
{
    [Table("t_cart_detail")]
    public class CartDetailEntity : BaseEntity
    {
        [Column("product_id")]
        public virtual long ProductId { get; set; }
        [Column("number")] public virtual long Number { get; set; }
        //buyer buyer_id
        [Column("user_id")] public virtual long? UserId { get; set; }
        [Column("stock")] public virtual long Stock { get; set; }
        [Column("image")] public virtual long Image { get; set; }
        [Column("title")] public virtual string Title { get; set; }
        [Column("price")] public virtual decimal? Price { get; set; }
        [Column("attr_val")] public virtual string AttrVal { get; set; }

       
    }
    [Table("t_cart")]
    public class CartEntity : BaseEntity
    {
        [Column("product_ids")]
        [StringLength(500)]
        //1,2,3
        public virtual string ProductIds { get; set; }
        //buyer buyer_id
        [Column("user_id")] 
        public virtual long? UserId { get; set; }

      
    }
}
