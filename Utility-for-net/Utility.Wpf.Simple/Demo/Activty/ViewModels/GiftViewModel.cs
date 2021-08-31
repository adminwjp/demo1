using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Activty.ViewModels
{/// <summary>
 /// 礼物 即 优惠券
 /// </summary>
	public class GiftViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
	{


		/// <summary>
		/// 更新账户
		/// </summary>
	public virtual string UpdateAccount { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		public virtual int Status { get; set; }

		/// <summary>
		/// 素材
		/// </summary>
		public virtual string Picture { get; set; }

		/// <summary>
		/// 创建账户
		/// </summary>
		public virtual string CreateAccount { get; set; }

		/// <summary>
		/// 礼物价格
		/// </summary>
		public virtual decimal Price { get; set; }

		/// <summary>
		/// 礼物名称
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// 出售数量
		/// </summary>
		public virtual int SellCount { get; set; }


		/// <summary>
		/// 当前库存数量
		/// </summary>
		public virtual int Stocks { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
      
    }
	
}
