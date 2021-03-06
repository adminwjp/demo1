using AutoMapper;

namespace Adverts.Dto
{
	/// <summary>
	///查询 广告 实体 
	/// </summary>
	[AutoMap(typeof(Advert))]
    public class RequestAdvertDto
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
