using AutoMapper;

namespace Adverts.Dto
{
    /// <summary>
    ///修改 广告 实体
    /// </summary>
    [AutoMap(typeof(Advert))]
    public class UpdateAdvertInput : BaseAdvertInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }
    }
}
