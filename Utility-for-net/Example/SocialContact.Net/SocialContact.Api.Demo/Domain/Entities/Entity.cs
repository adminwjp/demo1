using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Domain.Entities
{
    public abstract class Entity
    {
        public List<INotification> Notifications = new List<INotification>(20);
    }
}
