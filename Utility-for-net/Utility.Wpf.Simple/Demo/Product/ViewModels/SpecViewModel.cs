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
   /// 产品 规格 
   /// </summary>
	public class SpecViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
	{
		/// <summary>
		/// 产品 规格  库存
		/// </summary>
		public virtual int Stock { get; set; }
		/// <summary>
		/// 产品 规格  出售数量
		/// </summary>
		public virtual int Sales { get; set; }
		/// <summary>
		/// 产品 ID
		/// </summary>
		public virtual long? ProductID { get; set; }
		/// <summary>
		/// 该 产品 规格 价格
		/// </summary>
		public virtual decimal? Price { get; set; }
		/// <summary>
		/// 进价 
		/// </summary>
		public virtual decimal? MakePrice { get; set; }
		/// <summary>
		/// 产品 规格 尺寸
		/// </summary>
		public virtual string Size { get; set; }
		/// <summary>
		/// 产品 规格 状态
		/// </summary>
		public virtual int Status { get; set; }
		/// <summary>
		/// 产品 规格 颜色
		/// </summary>
		public virtual string Color { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
	
}
