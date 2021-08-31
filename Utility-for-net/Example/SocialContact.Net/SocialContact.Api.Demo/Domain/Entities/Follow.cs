
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialContact.Domain.Entities
{
    /// <summary>关注用户实体类</summary>
    [Serializable]
    [Table("t_follow")]
    public class FollowEntity
    {
        /// <summary>Id </summary>
        public long Id { get; set; }

        /// <summary>关注用户Id </summary>
        public long UserId { get; set; }

        /// <summary>被关注用户Id </summary>
        public long FollowedUserId { get; set; }

        /// <summary>备注名称 </summary>
        public string NoteName { get; set; }

        public bool IsDelete { get; set; }

        /// <summary>
        ///是否为互相关注
        /// </summary>
        public bool IsMutual { get; set; }

        /// <summary>关注时间 </summary>
        public long FollowDate { get; set; }

    }
}