using System;
using System.Collections.Generic;
using Utility.Demo.Domain.Entities;

namespace SocialContact.Domain.Entities
{
    public class UserEntity : UserEntity<UserEntity, long>
    {
        public virtual ISet<WorkEntity> Works { get; set; }
        public virtual ISet<EdutionEntity> Edutions { get; set; }
        //public virtual ISet<LikeInfo>  Likes { get; set; }
        //public virtual ISet<SkillInfo> Skills { get; set; }
        //public virtual ISet<UserTagInfo>  UserTags { get; set; }
    }
}