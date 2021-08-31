using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{   /// <summary>
    /// 签到 记录
    /// </summary>
   public class SignRecordViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// 代理商 、 商家、 平台、买家 id 
        /// </summary>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 签到 id 
        /// </summary>
       public virtual long? SignId { get; set; }

        /// <summary>
        /// 签到 时间
        /// </summary>
        public virtual DateTime SignDate { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
    
}
