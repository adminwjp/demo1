using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Application.Services.Dtos
{
    public class QueryAdminDto: QueryDto
    {
        public string Account { get; set; }
       // [BindProperty(Name = "nick_name")]
        public string NickName { get; set; }
       // [BindProperty(Name = "login_date")]
        public DateTime[] LoginDate { get; set; }
       // [BindProperty(Name = "real_name")]
        public string RealName { get; set; }
       // [BindProperty(Name = "birthday_date")]
        public DateTime[] BirthdayDate { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
       // [BindProperty(Name = "role_id")]
        public int? RoleId { get; set; }
        public string Token { get; set; }
    }
}
