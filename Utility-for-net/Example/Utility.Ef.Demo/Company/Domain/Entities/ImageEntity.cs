using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Entities
{
    public class ImageEntity:BaseEntity
    {
        public string Name { get; set; } //素材名称
        public string Href { get; set; }//素材地址别名
        public string Src { get; set; }//素材地址 即物理地址
        public string Type { get; set; }//素材标识
    }
}
