using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Product.ViewModels
{   /// <summary>
	/// 分类 属性
	/// </summary>
	public class CatagoryAttrViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
	{
	/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		//[StringLength(36)]
		public virtual long CatagoryId { get; set; }
		/// <summary>
		/// 排序  最好不要用 sql  关键词
		/// </summary>
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		public virtual long ParentId { get; set; }

		/// <summary>
		/// 不使用外键
		/// </summary>
		
		public virtual List<SpecAttrViewModel> ProductAttribues { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }

}
