//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Newtonsoft.Json;
using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 用户举报实体
    /// </summary>
    [TableName("tn_ImpeachReports")]
    [PrimaryKey("Id", autoIncrement = true)]
    [CacheSetting(true)]
    [Serializable]
    public class Report : IEntity
    {
        #region 构造器

        /// <summary>
        /// 新建实体时使用
        /// </summary>
        /// <returns>用户举报实体</returns>
        public static Report New()
        {
            Report impeachReport = new Report()
            {
                Reason = ImpeachReasonEnum.Other,
                TenantTypeId = string.Empty,
                Reporter = string.Empty,
                Title = string.Empty,
                Description = string.Empty,
                DateCreated = DateTime.Now,
                Status = false,
            };
            return impeachReport;
        }

        #endregion 构造器

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///租户Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Reporter { get; set; }

        /// <summary>
        ///举报原因
        /// </summary>
        public ImpeachReasonEnum Reason { get; set; }

        /// <summary>
        ///举报对象标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///附加说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///被举报相关对象Id
        /// </summary>
        public long ReportObjectId { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///Status
        /// </summary>
        public bool Status { get; set; }

        #endregion 需持久化属性

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员

        /// <summary>
        /// 举报对象详情
        /// </summary>
        [Ignore]
        [JsonIgnore]
        public ImpeachObject ImpeachObject
        {
            get
            {
                var impeachUrlGetter = ImpeachUrlGetterFactory.Get(this.TenantTypeId);
                return impeachUrlGetter?.GetImpeachObject(this.ReportObjectId);
            }
        }
    }
}