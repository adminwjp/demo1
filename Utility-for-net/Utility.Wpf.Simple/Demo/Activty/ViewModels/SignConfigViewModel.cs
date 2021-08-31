using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{ /// <summary>
  /// 签到 配置 实体
  /// 天数 签到 
  /// 还是 积分签到
  /// </summary>
    public class SignConfigViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// 签到 id 
        /// </summary>
       public virtual long? SignId { get; set; }

        /// <summary>
        /// 签到 天数  
        /// 签到 10天 才能 获取该礼物
        /// 签到 1个月 才能 获取该礼物
        /// </summary>
       public virtual int SignDay { get; set; }

        /// <summary>
        /// 积分
        /// 签到 1000 积分 才能 获取该礼物
        /// 签到 10000 积分 才能 获取该礼物
        /// </summary>
       public virtual int Score { get; set; }



        /// <summary>
        ///礼物 id 即 奖品 id 
        /// </summary>
        public virtual long? GiftId { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
   
}
