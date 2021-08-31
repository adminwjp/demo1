using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Demo.Domain.Entities
{
    public interface IIsDefault: IIsDefault<long>
    {

    }
    public interface IIsDefault<Key>
    {
        bool IsDefault { get; set; }
        /// <summary>用户id </summary>
         Key UserId { get; set; }
    }

}
