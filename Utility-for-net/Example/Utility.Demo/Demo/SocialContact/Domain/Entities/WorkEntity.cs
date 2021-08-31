
using System;

namespace SocialContact.Domain.Entities
{
    public class WorkEntity:BaseEntity
    {
        
        public virtual string CompanyName { get; set; }
        public virtual string Job { get; set; }
        public virtual CatagoryEntity Catagory { get; set; }
        public virtual long? CatagoryId { get; set; }
        public virtual string Description { get; set; }
        public virtual long StartDate { get; set; }
        public virtual long? EndDate { get; set; }

        public virtual DateTime[] StartDates { get; set; }
        public virtual DateTime[] EndDates { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual long? UserId { get; set; }

    }
}
