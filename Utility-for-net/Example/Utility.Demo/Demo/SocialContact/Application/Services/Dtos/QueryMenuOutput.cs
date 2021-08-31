using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Application.Services.Dtos
{
    public class QueryMenuOutput
    {
        public long Id { get; set; }
        public string MenuName { get; set; }
        public IconDto Icon { get; set; }
        public string MenuGroup { get; set; }
        public string Href { get; set; }
        public bool Collpse { get; set; }
       public string Description { get; set; }
        public ISet<QueryMenuDto> Children { get; set; }
        public QueryMenuOutput Parent { get; set; }
        public object Clone()
        {
            return new QueryMenuOutput()
            {
                Id = this.Id,
                MenuName = this.MenuName,
                MenuGroup = this.MenuGroup,
                Href = this.Href,
                Collpse = this.Collpse,
                Description = this.Description
            };
        }
    }
}
