using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Entities
{
    public class RelationEntity:BaseEntity
    {
        public const string Table = "t_c_relation";
        public long Fk1 { get; set; }
        public long Fk2 { get; set; }
        public string Flag { get; set; }
    }
}
