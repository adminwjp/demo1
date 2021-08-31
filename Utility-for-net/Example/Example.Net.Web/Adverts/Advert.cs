using System;

namespace Adverts
{
    /// <summary>
    /// 广告
    /// </summary>
    public class Advert : BaseEntity
	{
		public const string TableName = "Advert";
		/// <summary>
		/// 投放时间
		/// </summary>
		public virtual DateTime StartDate { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public virtual string Status { get; set; }
		/// <summary>
		/// Html
		/// </summary>
		public virtual string Html { get; set; }
		/// <summary>
		/// 标题
		/// </summary>
		public virtual string Title { get; set; }
		/// <summary>
		/// 截止时间
		/// </summary>
		public virtual DateTime EndDate { get; set; }
		/// <summary>
		/// 随机素材
		/// </summary>
		public virtual string UseImagesRandom { get; set; }
		/// <summary>
		/// Code
		/// </summary>
		public virtual string Code { get; set; }

	}
}
