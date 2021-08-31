using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Database.Entities
{
    internal class WhereCollectionEntity
    {
        public readonly List<WhereEntity> Ands = new List<WhereEntity>();
        public readonly List<WhereEntity> Ors = new List<WhereEntity>();

    }
}
