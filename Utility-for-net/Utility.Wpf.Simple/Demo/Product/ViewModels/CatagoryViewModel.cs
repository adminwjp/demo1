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
	/// 分类
	/// </summary>
	public class CatagoryViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
	{
	
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 编码
		/// </summary>
		public virtual string Code { get; set; }
		/// <summary>
		/// 排序 
		/// </summary>
		public virtual int Orders { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
		public virtual long ParentId { get; set; }
		public virtual long ShopId { get; set; }
		/// <summary>
		/// 底部导航 链接
		/// </summary>
		public virtual string Link { get; set; }
		/// <summary>
		/// 底部导航 链接 跳转方式
		/// </summary>
		public virtual string Target { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		public virtual string Description { get; set; }


		/// <summary>
		///  1 菜单 (显示导航)  2 二级 菜单 3 底部导航
		/// </summary>
		public virtual int Flag { get; set; }
		/// <summary>
		/// 父 编号
		/// </summary>
	
		public virtual long ImageId { get; set; }

		/// <summary>
		/// 不使用外键
		/// </summary>
		
		public virtual List<CatagoryViewModel> Catagorys { get; set; }
		/// <summary>
		/// 不使用外键
		/// </summary>
		public virtual List<CatagoryAttrViewModel> CatagoryAttribues { get; set; }
		/// <summary>
		/// 不使用外键
		/// </summary>
		public virtual List<ProductViewModel> Products { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
	
}
