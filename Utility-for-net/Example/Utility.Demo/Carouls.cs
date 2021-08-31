using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    [Class(Table ="t_carouls")]
    public class Carouls
    {
           [Property]
        public virtual string ad_image_url { get; set; }
        [Property]
        public virtual string ad_link_url { get; set; }
        [Property]
        public virtual string ad_bgcolor { get; set; }
    }
}
