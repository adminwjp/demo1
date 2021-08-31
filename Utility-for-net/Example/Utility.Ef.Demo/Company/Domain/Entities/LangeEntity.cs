using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain.Entities
{
    public class LangeEntity: BaseEntity
    {
        public const string Table = "t_c_lange";
        public virtual string Val1 { get; set; }
        public virtual string Val2 { get; set; }
        public virtual string Val3 { get; set; }
        public virtual string Val4 { get; set; }
        public virtual string Val5 { get; set; }
        public virtual string Val6 { get; set; }
        public virtual string Val7 { get; set; }
        public virtual string Description { get; set; }
        public virtual long RelationId { get; set; }
        public virtual string RelationTable { get; set; }
        public virtual CompanyCatagoryFlag Flag { get; set; }
    }
}
