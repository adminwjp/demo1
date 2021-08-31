using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace SocialContact.Application.Services.Dtos
{
   public class CatagoryDto
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public ISet<CatagoryDto> Children { get; set; }
    }
}
