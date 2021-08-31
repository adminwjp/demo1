//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 勋章实体
    /// </summary>
    [TableName("tn_Medals")]
    [PrimaryKey("MedalId", autoIncrement = true)]
    [CacheSetting(true)]
    [Serializable]
    public class Medal : SerializablePropertiesBase, IEntity
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public static Medal New()
        {
            Medal medal = new Medal
            {
                MedalName = string.Empty,
                Description = string.Empty,
                Conditions = string.Empty,
                ConditionValues = string.Empty,
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now
            };
            return medal;
        }

        #region 需持久化属性

        /// <summary>
        /// 勋章Id
        /// </summary>
        public long MedalId { get; set; }

        /// <summary>
        /// 勋章名
        /// </summary>
        public string MedalName { get; set; }

        /// <summary>
        /// 授予状态（可以授予、停止授予）
        /// </summary>
        public AwardStatus AwardStatus { get; set; }

        /// <summary>
        /// 授予方式（自主申请、人工授予）
        /// </summary>
        public AwardType AwardType { get; set; }

        /// <summary>
        /// 勋章标题图ID
        /// </summary>
        public long ImageAttachmentId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public long DisplayOrder { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModified { get; set; }

        #endregion 需持久化属性

        #region 扩展属性/方法

        /// <summary>
        /// 互斥组Id
        /// </summary>
        [Ignore]
        public long GroupId { get; set; }

        /// <summary>
        /// 之前互斥组Id
        /// </summary>
        [Ignore]
        public long GroupIdBefore { get; set; }

        /// <summary>
        /// 申请条件Id
        /// </summary>
        [Ignore]
        public string Conditions { get; set; }

        /// <summary>
        /// 申请条件最小值
        /// </summary>
        [Ignore]
        public string ConditionValues { get; set; }

        /// <summary>
        /// 获取勋章标题图
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl()
        {
            AttachmentService attachmentService = new AttachmentService(TenantTypeIds.Instance().Medal());
            var imageurl = string.Empty;
            var attachment = attachmentService.Get(ImageAttachmentId);
            if (attachment != null)
                imageurl = attachment.GetDirectlyUrl();

            return imageurl;
        }

        /// <summary>
        /// 获取勋章授予的用户数量
        /// </summary>
        /// <returns></returns>
        public int GetMedalToUserNum()
        {
            int medalToUserNum = 0;
            DIContainer.Resolve<IKvStore>().TryGet(KvKeys.Instance().MedaltoUser(MedalId), out medalToUserNum);
            return medalToUserNum;
        }

        /// <summary>
        /// 判断用户是否拥有某个勋章
        /// </summary>
        /// <returns></returns>
        public long HasMedaltoUser(long userId, UserAwardStatus userAwardStatus = UserAwardStatus.AlreadyAward)
        {
            List<string> userIds = new List<string>();
            userIds.Add(userId.ToString());
            var medaltoUser = DIContainer.Resolve<MedalService>().GetsMedalToUser(userIds, MedalId, userAwardStatus);
            return medaltoUser.Any() ? medaltoUser.ElementAtOrDefault(0).Id : 0;
        }

        /// <summary>
        /// 所有的附件Id集合
        /// </summary>
        [Ignore]
        public IEnumerable<long> AttachmentIdsFinal { get; set; } = new List<long>();

        #endregion 扩展属性/方法

        #region IEntity 成员

        object IEntity.EntityId { get { return this.MedalId; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}