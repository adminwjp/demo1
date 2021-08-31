using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SocialContact.Domain.Entities
{
    public class RelationEntity : BaseEntity
    {
        public virtual long RelationId { get; set; }
        public virtual long UserId { get; set; }
        public virtual long CatagoryId { get; set; }
        public virtual CatalogFlag Flag { get; set; }
    }
}