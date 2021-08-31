//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 搜索热词实体
    /// </summary>
    [TableName("tn_SearchWords")]
    [PrimaryKey("Id", autoIncrement = true)]
    [CacheSetting(true)]
    [Serializable]
    public class SearchWord 
    {

        /// <summary>
        ///搜索词Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 搜索词
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// 搜索词类型
        /// </summary>
        public string SearchTypeCode { get; set; }

        /// <summary>
        /// 是否由管理员添加
        /// </summary>
        public int IsAddedByAdministrator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 最后使用时间
        /// </summary>
        public DateTime LastModified { get; set; }

    }
}