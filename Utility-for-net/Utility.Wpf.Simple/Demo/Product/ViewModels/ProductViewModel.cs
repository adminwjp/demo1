using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Demo.Product.ViewModels
{   /// <summary>
	/// 产品 实体
	/// </summary>
	public class ProductViewModel : AbstractNotifyPropertyChanged, IIsSelectedViewModel, INotifyPropertyChanged
	{
		/// <summary>
		/// 目录分类 id
		/// </summary>
		public virtual long CatagoryID { get; set; }
		/// <summary>
		/// 出售数量
		/// </summary>
		public virtual int Sales { get; set; }
		/// <summary>
		/// 库存
		/// </summary>
		public virtual int Stock { get; set; }
		/// <summary>
		/// 关键字
		/// </summary>
		public virtual string Keywords { get; set; }
		/// <summary>
		/// 评分
		/// </summary>
		public virtual int Score { get; set; }
		/// <summary>
		/// 创建账户
		/// </summary>
		public virtual string CreateAccount { get; set; }
		/// <summary>
		/// 热销 数量
		/// </summary>
		public virtual int Hit { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public virtual string Title { get; set; }
		/// <summary>
		/// 价格
		/// </summary>
		public virtual decimal? Price { get; set; }
		/// <summary>
		/// 更新账户
		/// </summary>
		public virtual string UpdateAccount { get; set; }
		/// <summary>
		/// 活动 id
		/// </summary>
		//[StringLength(36)]
		public virtual long ActivityID { get; set; }
		/// <summary>
		/// 状态 
		/// </summary>
		public virtual int Status { get; set; }
		/// <summary>
		/// 产品 显示 html
		/// </summary>
		public virtual string ProductHTML { get; set; }
		/// <summary>
		/// 是否 显示 新闻
		/// </summary>
		public virtual bool IsNew { get; set; }
		/// <summary>
		/// 介绍
		/// </summary>
		public virtual string Introduce { get; set; }
		/// <summary>
		/// 搜索关键词
		/// </summary>
		public virtual string SearchKey { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		public virtual string Images { get; set; }
		/// <summary>
		/// 高清素材
		/// </summary>
		public virtual string MaxPicture { get; set; }
		/// <summary>
		/// 描述
		/// </summary>
		public virtual string Description { get; set; }
		/// <summary>
		/// 单价 $ r
		/// </summary>
		public virtual string Unit { get; set; }
		/// <summary>
		/// 素材
		/// </summary>
		public virtual string Picture { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// 是否出售
		/// </summary>
		public virtual bool Sale { get; set; }
		/// <summary>
		/// 礼物 id
		/// </summary>
		public virtual long GiftID { get; set; }

		/// <summary>
		/// 不使用外键
		/// </summary>
		public virtual List<SpecViewModel> Specs { get; set; }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action<IIsSelectedViewModel> AllSelectEvent;
    }
	
}
