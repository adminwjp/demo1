using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Test.SocialContact.Application.Services.Dtos
{
    public class QueryIconOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string ShowStyle => !string.IsNullOrEmpty(Style) ? $" <i class=\"{Style}\"></i>" : string.Empty;
        public string Description { get; set; }
       
    }
}
