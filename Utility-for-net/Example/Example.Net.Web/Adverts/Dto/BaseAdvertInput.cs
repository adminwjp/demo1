using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverts.Dto
{
    public class BaseAdvertInput
	{
		/// <summary>
		/// 更新账户
		/// </summary>
		public virtual string UpdateAccount { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public virtual string Status { get; set; }
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
		public virtual decimal? GiftPrice { get; set; }
		/// <summary>
		/// 礼物名称
		/// </summary>
		public virtual string GiftName { get; set; }
	}
}
