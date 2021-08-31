using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Product.ViewModels
{
    public class CartDetailViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        public virtual long ProductId { get; set; }
      public virtual long Number { get; set; }
        //buyer buyer_id
       public virtual long? UserId { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
    public class CartViewModel 
    {
        //1,2,3
        public virtual string ProductIds { get; set; }
      
        public virtual long? UserId { get; set; }


    }
 
}
