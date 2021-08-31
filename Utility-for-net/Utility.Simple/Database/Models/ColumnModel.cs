using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database.Models
{
    public class ColumnModel
    {
        public virtual string Table { get; set; }
        public virtual long Length { get; set; }
        public virtual string Column { get; set; }
        public virtual string Comment { get; set; }
        public virtual bool Pk { get; set; }
        public virtual bool Identity { get; set; }
        public virtual bool Fk { get; set; }
        public virtual string ReferenceTable { get; set; }
        public virtual string ReferenceColumn { get; set; }
        public virtual string ConstrainName { get; set; }
        public virtual ColumnDefaultValueFlag DefaultValueFlag { get; set; }
    }

    public enum ColumnDefaultValueFlag
    {
        Number,
        String,
        Date
    }
}
