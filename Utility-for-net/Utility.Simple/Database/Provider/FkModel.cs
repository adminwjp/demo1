using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Provider
{

    internal class FkModel
    {
        public string Table { get; set; }
        public string Column { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceId { get; set; }
        public string Constraint { get; set; }
    }
}
