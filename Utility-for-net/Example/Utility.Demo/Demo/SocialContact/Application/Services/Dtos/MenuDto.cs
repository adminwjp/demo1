using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Application.Services.Dtos
{
    public class MenuDto
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public string Style { get; set; }
        public string Href { get; set; }
        public string Group { get; set; }
        public ISet<MenuDto> Children { get; set; }
    }
}
