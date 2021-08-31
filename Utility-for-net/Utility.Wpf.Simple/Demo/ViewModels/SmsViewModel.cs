using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Demo.Domain.Entities;

namespace Utility.Wpf.Demo.ViewModels
{
    public class SmsViewModel: SmsViewModel<long>
    {

    }
    public class SmsViewModel<Key> : SmsEntity<Key>
    {
    }
}
