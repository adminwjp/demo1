using System;
using System.Collections.Generic;
using System.Text;
using Utility.Database.Attributes;

namespace Utility.Database.Test
{
   [Class(Table ="t_city")]
    public class City
    {
        [Id]
        [Identity]
        public int Id { get; set; }
        public String Code { get; set; }
        public string Name { get; set; }
        [OneToOne(Constraint = "fk_parent_id",Column  = "parent_id")]
        public City Parent { get; set; }
        [OneToMany(Constraint = "fk_parent_id", Column  = "parent_id")]
        public IList<City> Children { get; set; }
        public int? ParentId { get; set; }
    }
}
