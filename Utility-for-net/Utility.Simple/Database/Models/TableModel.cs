using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Database.Models
{
    public class TableModel
    {
        public virtual string Table { get; set; }
        public virtual string Comment { get; set; }
        public virtual string Database { get; set; }
        public virtual List<ColumnModel> Columns { get; set; }

    }
}
