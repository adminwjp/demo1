using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Product.ViewModels
{
    public class BrandViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {

        public virtual long Id { get; set; }
        public virtual string Letter { get; set; }
        public virtual string Name { get; set; }
        public virtual long ProductCatagoryId { get; set; }
        public virtual int Orders { get; set; }
        public virtual long ProductCount { get; set; }
        public virtual long ShopId { get; set; }
        public virtual bool FactoryStatus { get; set; } = true;//品牌制造商
     
        public virtual bool IfShow { get; set; } = true;//是否显示
      
        public virtual long CommentCount { get; set; }
        public virtual string Logo { get; set; }
        public virtual string Images { get; set; }
        public virtual long LogoId { get; set; }
        public virtual long ImageIds { get; set; }

        public virtual string BigPic { get; set; }
        public virtual string BrandStory { get; set; }

        public virtual string Tag { get; set; }
        public virtual string Description { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
     
    }
  
}
