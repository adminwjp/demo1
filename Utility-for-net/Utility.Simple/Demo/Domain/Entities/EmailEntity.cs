using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{
    public class EmailEntity: EmailEntity<long>
    {

    }
    public class EmailEntity<Key> : Entity<Key>
    {
        public virtual Key UserId { get; set; }
        public virtual string Account { get; set; }
        public virtual string Email { get; set; }
        public virtual string Scrept { get; set; }
        public virtual bool IsDefault { get; set; }
    }
}
