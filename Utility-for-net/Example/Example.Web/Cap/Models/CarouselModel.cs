using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Cap.Api.Models
{
    [Flags]
    public enum CarouselFlag
    {
        None,
        Pc,
        Mobile
    }
   
    /// <summary>
    /// 轮播图
    /// </summary>
    [Table("t_carousel")]
    public class CarouselModel: BaseModel
    {
        /// <summary>
        /// 素材地址
        /// </summary>
        [Column("image_id")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string ImageId { get; set; }

        /// <summary>
        /// 素材背景图片
        /// </summary>
        [Column("background")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Background { get; set; }

        /// <summary>
        /// 素材地址
        /// </summary>
        [Column("src")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Src { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Column("desc")]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Desc { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("enable")]
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 标识
        /// </summary>
        [Column("flag")]
        public CarouselFlag Flag { get; set; }
        [Column("remark")]
        [StringLength(50)]
        public virtual string Remark { get; set; }
        [Column("title")]
        [StringLength(50)]
        public virtual string Title { get; set; }
        [Column("orders")] public virtual int Orders { get; set; }
        [Column("link")] [StringLength(100)] public virtual string Link { get; set; }
    }


   
}
