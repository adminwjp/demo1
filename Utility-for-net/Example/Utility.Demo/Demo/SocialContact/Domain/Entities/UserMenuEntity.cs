using System;
using System.Collections.Generic;
using System.Text;
using Utility.Demo.Domain.Entities;

namespace SocialContact.Domain.Entities
{
    public class UserMenuEntity : BaseEntity
    {
        public virtual MenuEntity Menu { get; set; }
        public virtual AdminEntity Admin { get; set; }
        public virtual CatagoryEntity Role { get; set; }
        public virtual bool Add { get; set; } = true;
        public virtual bool Modify { get; set; } = true;
        public virtual bool Delete { get; set; } = true;
        public virtual bool Query { get; set; } = true;
        public virtual bool Enable { get; set; } = true;
        public virtual string Type { get; set; }
        public virtual bool Val { get; set; }
        public virtual void Clear()
        {
            if (this.Role != null)
            {
                this.Role.Parent = null;
                this.Role.Children = null;
            }
            if (this.Menu != null)
            {
                this.Menu.Icon = null;
                this.Menu.Parent = null;
                this.Menu.Children = null;
            }
        }
    }
}
