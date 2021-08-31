using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Product.ViewModels
{  
    /// <summary>
    /// 商品参数
    /// </summary>
    public class SpecAttrViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
      /// <summary>
        /// 参数id
        /// </summary>
        //[StringLength(36)]
        public long AttributeId { get; set; }
        /// <summary>
        /// 商品 id
        /// </summary>
        public long ProductId { get; set; }

        public string Value { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
  
}
