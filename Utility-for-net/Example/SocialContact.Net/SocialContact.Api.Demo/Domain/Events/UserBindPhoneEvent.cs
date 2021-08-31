using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Domain.Events
{
    public class UserBindPhoneEvent : INotification
    {
        public UserBindPhoneEvent()
        {

        }

        public UserBindPhoneEvent(long userId, string phone)
        {
            UserId = userId;
            Phone = phone;
        }

        public long UserId { get; set; }

        public string Phone { get; set; }
    }
}
