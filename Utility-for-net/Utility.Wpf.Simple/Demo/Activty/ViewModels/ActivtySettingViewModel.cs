using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{ 
    /// <summary>
  /// 活动设置
  /// </summary>
    public class ActivtySettingViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// 礼物id
        /// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("gift_id")]
        //[System.ComponentModel.DataAnnotations.StringLength(36)]
        //public virtual string GiftId { get; set; }

        /// <summary>
        /// 活动设置 当前参与抽奖物品数
        /// 可以为 0 比如 谢谢光临
        /// </summary>
        public virtual int Number { get; set; }


        /// <summary>
        /// 活动 id
        /// </summary>
       //public virtual string ActivtyId { get; set; }

        //这个 ef bug  外键列要与 普通属性名称一致 不然会创建多个  GiftId1 ActivtyId1 列
        public virtual ActivtyViewModel Activty { get; set; }

        public virtual GiftViewModel Gift { get; set; }

        public virtual long? activty_id { get; set; }


        public virtual long? gift_id { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
  
}
