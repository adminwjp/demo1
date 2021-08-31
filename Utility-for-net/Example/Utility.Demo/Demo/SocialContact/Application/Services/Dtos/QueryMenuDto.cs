using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Application.Services.Dtos
{
   public class QueryMenuDto:QueryDto
    {
       // [BindProperty(Name = "menu_name")]
        public string MenuName { get; set; }
       // [BindProperty(Name = "menu_group")]
        public string MenuGroup { get; set; }
       // [BindProperty(Name = "parent_id")]
        public int? ParentId { get; set; }
    }
}
