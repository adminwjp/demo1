using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services.Dtos;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Msg.ViewModels
{
    //新品通知
    public class ProductMsgViewModel : AbstractNotifyPropertyChanged,  IIsSelectedViewModel, INotifyPropertyChanged
    {
       public virtual string ProductIds { get; set; }
      
        public virtual string Title { get; set; }
        public virtual string Msg { get; set; }
        public virtual long StartDate { get; set; }
        public virtual long EndDate { get; set; }
        public virtual bool End { get; set; }
        public virtual string Pic { get; set; }
        public virtual long Times { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
}
