using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{    /// <summary>
     /// 奖品 即 优惠券
     /// </summary>
    public class PrizeViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {

        /// <summary>
        /// 中奖 玩家 ID
        /// </summary>
       public virtual string UserId { get; set; }

        /// <summary>
        /// 中奖账号
        /// </summary>
       public virtual string Account { get; set; }

        /// <summary>
        /// 中奖玩家 手机号
        /// </summary>
       public virtual string Phone { get; set; }

        /// <summary>
        /// 中奖玩家 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 礼物id
        /// </summary>
        public virtual long? GiftId { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
       
    }
    
}
