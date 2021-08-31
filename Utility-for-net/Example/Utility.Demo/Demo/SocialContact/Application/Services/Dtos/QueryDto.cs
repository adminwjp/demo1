using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Application.Services.Dtos
{
    public class QueryDto
    {
        public long? Id { get; set; }
       [Newtonsoft.Json.JsonProperty(  "create_date")]
        public DateTime[] CreateDates { get; set; }
        [Newtonsoft.Json.JsonProperty( "update_date")]
        public DateTime[] UpdateDates { get; set; }
    }
}
