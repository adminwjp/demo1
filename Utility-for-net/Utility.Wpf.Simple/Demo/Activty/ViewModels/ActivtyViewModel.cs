using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{ /// <summary>
  /// 活动 标识
  /// </summary>
    [Flags]
    public enum ActivtyFlag
    {
        /// <summary>
        ///无
        /// </summary>
        None = 0x0,

        /// <summary>
        /// 规定时间 做活动
        /// </summary>
        Date = 0x1,

        /// <summary>
        /// 礼物发送完则结束
        /// </summary>
        GiftFree = 0x2
    }

    /// <summary>
    /// 活动 用户 标识
    /// </summary>
    public enum ActivtySellerFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0x0,

        /// <summary>
        /// 代理商
        /// </summary>
        Agent = 0x1,

        /// <summary>
        /// 商家
        /// </summary>
        Business = 0x2,

        /// <summary>
        /// 平台
        /// </summary>
        Platform = 0x3
    }
    /// <summary>
    /// 活动 实体
    /// </summary>
    public class ActivtyViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {

        /// <summary>
        /// 代理商 、 商家、 平台 id 
        /// </summary>
       public virtual long? SellerId { get; set; }

        /// <summary>
        /// 活动 用户 标识
        /// </summary>
       public virtual ActivtySellerFlag SellerFlag { get; set; }

        /// <summary>
		/// 代理商 、 商家、 平台 账户
		/// </summary>
		 public virtual string Account { get; set; }

        /// <summary>
        /// 活动 参与抽奖物品数 即 优惠券
        /// </summary>
        public virtual int Number { get; set; }

        /// <summary>
        /// 活动 开始时间
        /// </summary>
        public virtual DateTime StartDate { get; set; }


        /// <summary>
        /// 活动 结束时间
        /// </summary>
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// 活动 标识
        /// </summary>
        public virtual ActivtyFlag Flag { get; set; }

        /// <summary>
        /// 活动 类型
        /// </summary>
       public virtual int ActivtyType { get; set; }


        /// <summary>
        /// 后台 通知活动 提前结束 (奖品已抽中)
        /// </summary>
        public virtual bool End { get; set; }

        ///// <summary>
        /////活动 中奖 玩家 ID 最多 20 个玩家(买家)
        ///// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("buyer_ids")]
        //[System.ComponentModel.DataAnnotations.StringLength(750)]
        //public virtual string BuyerIds { get; set; }

        /// <summary>
        /// 后台 通知活动 提前结束 原因
        /// </summary>
       public virtual string Reason { get; set; }

       public virtual List<ActivtySettingViewModel> ActivtySettings { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
        
    }
   
}
