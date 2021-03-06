//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Tunynet.Caching;
using Tunynet.Repositories;

namespace Tunynet.Common
{
    /// <summary>
    /// 积分设置
    /// </summary>
    [Serializable]
    [CacheSetting(true)]
    public class PointSettings : IEntity
    {
        private float experiencePointsCoefficient = 1;

        /// <summary>
        /// 经验系数
        /// </summary>
        public float ExperiencePointsCoefficient
        {
            get { return experiencePointsCoefficient; }
            set { experiencePointsCoefficient = value; }
        }

        private float reputationPointsCoefficient = 2;

        /// <summary>
        /// 威望系数
        /// </summary>
        public float ReputationPointsCoefficient
        {
            get { return reputationPointsCoefficient; }
            set { reputationPointsCoefficient = value; }
        }

        private int transactionTax = 0;

        /// <summary>
        /// 交易税（0-100）
        /// </summary>
        public int TransactionTax
        {
            get { return transactionTax; }
            set { transactionTax = value; }
        }

        private string userIntegratedPointRuleText = string.Empty;

        /// <summary>
        /// 显示用户综合积分规则文字描述
        /// </summary>
        /// <example>经验*1 + 威望*2</example>
        public string UserIntegratedPointRuleText
        {
            get
            {
                if (string.IsNullOrEmpty(userIntegratedPointRuleText))
                {
                    var pointCategoryRepository = new Repository<PointCategory>();
                    PointCategory experiencePointsCategory = pointCategoryRepository.Get(PointCategoryKeys.Instance().ExperiencePoints());
                    PointCategory reputationPointsCategory = pointCategoryRepository.Get(PointCategoryKeys.Instance().ReputationPoints());

                    userIntegratedPointRuleText = string.Format("{0}*{1} + {2}*{3}", experiencePointsCategory.CategoryName, experiencePointsCoefficient, reputationPointsCategory.CategoryName, reputationPointsCoefficient);
                }
                return userIntegratedPointRuleText;
            }
        }

        /// <summary>
        /// 计算用户综合积分
        /// </summary>
        /// <param name="experiencePoints">经验</param>
        /// <param name="reputationPoints">威望</param>
        /// <returns>计算后的综合积分</returns>
        public int CalculateIntegratedPoint(int experiencePoints, int reputationPoints)
        {
            //按照UserIntegratedPointRule计算综合积分(注意性能不要每次都解析)
            return (int)(experiencePoints * experiencePointsCoefficient + reputationPoints * reputationPointsCoefficient);
        }

        #region IEntity 成员

        object IEntity.EntityId
        {
            get { return typeof(PointSettings).FullName; }
        }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}