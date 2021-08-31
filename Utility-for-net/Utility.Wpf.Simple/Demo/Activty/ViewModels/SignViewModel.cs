using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{/// <summary>
 /// 签到 实体
 /// </summary>
   public class SignViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {

        ///// <summary>
        ///// 活动 id 
        ///// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("activty_id")]
        //[System.ComponentModel.DataAnnotations.StringLength(36)]
        //public virtual string ActivtyId { get; set; }

        /// <summary>
        /// 签到 名称 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 二维码 
        /// </summary>
      public virtual string QrCode { get; set; }




        /// <summary>
        /// 签到 开始时间
        /// </summary>
        public virtual DateTime StartDate { get; set; }


        /// <summary>
        /// 签到 结束时间
        /// </summary>
       public virtual DateTime EndDate { get; set; }


        /// <summary>
        /// 签到 天数  比如 签到 一个月 才能 获取该礼物
        /// </summary>
       public virtual int SignDay { get; set; }

        /// <summary>
        /// 积分 比如 签到 10000 积分 才能 获取该礼物
        /// </summary>
        public virtual int Score { get; set; }

        /// <summary>
        /// 标识
        /// 0 签到 天数
        /// 1 积分
        /// </summary>
        public virtual int Flag { get; set; }

        /// <summary>
        ///礼物 id 即 奖品 id 
        /// </summary>
        public virtual long? GiftId { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

       public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
   
}
