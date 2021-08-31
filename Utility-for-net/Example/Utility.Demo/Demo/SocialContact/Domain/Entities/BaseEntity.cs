using System;
using Utility.Domain.Entities;

namespace SocialContact.Domain.Entities
{
    public abstract class BaseEntity : IEntity<long>
    {
       
        public virtual long Id { get; set; }
        public virtual long CreateDate { get; set; }
        public virtual long UpdateDate { get; set; }
        public virtual DateTime[] CreateDates { get; set; }
        public virtual DateTime[] UpdateDates { get; set; }
    }
}
