//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 用户资料设置
    /// </summary>
    [Serializable]
    [CacheSetting(true)]
    public class UserProfileSettings : IEntity
    {
        #region 头像尺寸设置

        private int originalAvatarWidth = 350;

        /// <summary>
        /// 原始头像最大宽度（像素）
        /// </summary>
        public int OriginalAvatarWidth
        {
            get { return this.originalAvatarWidth; }
            set { originalAvatarWidth = value; }
        }

        private int originalAvatarHeight = 350;

        /// <summary>
        /// 原始头像最大高度（像素）
        /// </summary>
        public int OriginalAvatarHeight
        {
            get { return this.originalAvatarHeight; }
            set { originalAvatarHeight = value; }
        }

        private int bigcoverWidth = 373;

        /// <summary>
        /// 大封面图的宽度（像素）
        /// </summary>
        public int BigCoverWidth
        {
            get { return this.bigcoverWidth; }
            set { bigcoverWidth = value; }
        }

        private int bigcoverHeight = 200;

        /// <summary>
        /// 大封面图高度（像素）
        /// </summary>
        public int BigCoverHeight
        {
            get { return bigcoverHeight; }
            set { bigcoverHeight = value; }
        }

        private int coverWidth = 1140;

        /// <summary>
        /// 封面图的宽度（像素）
        /// </summary>
        public int CoverWidth
        {
            get { return this.coverWidth; }
            set { coverWidth = value; }
        }

        private int coverHeight = 200;

        /// <summary>
        /// 封面图高度（像素）
        /// </summary>
        public int CoverHeight
        {
            get { return coverHeight; }
            set { coverHeight = value; }
        }

        private int avatarWidth = 120;

        /// <summary>
        /// 大头像的宽度（像素）
        /// </summary>
        public int AvatarWidth
        {
            get { return this.avatarWidth; }
            set { avatarWidth = value; }
        }

        private int avatarHeight = 120;

        /// <summary>
        /// 大头像高度（像素）
        /// </summary>
        public int AvatarHeight
        {
            get { return avatarHeight; }
            set { avatarHeight = value; }
        }

        private int mediumAvatarWidth = 90;

        /// <summary>
        /// 中头像宽度（像素）
        /// </summary>
        public int MediumAvatarWidth
        {
            get { return mediumAvatarWidth; }
            set { mediumAvatarWidth = value; }
        }

        private int mediumAvatarHeight = 90;

        /// <summary>
        /// 中头像高度（像素）
        /// </summary>
        public int MediumAvatarHeight
        {
            get { return mediumAvatarHeight; }
            set { mediumAvatarHeight = value; }
        }

        private int smallAvatarWidth = 50;

        /// <summary>
        /// 小头像宽度（像素）
        /// </summary>
        public int SmallAvatarWidth
        {
            get { return smallAvatarWidth; }
            set { smallAvatarWidth = value; }
        }

        private int smallAvatarHeight = 50;

        /// <summary>
        /// 小头像高度（像素）
        /// </summary>
        public int SmallAvatarHeight
        {
            get { return smallAvatarHeight; }
            set { smallAvatarHeight = value; }
        }

        private int microAvatarWidth = 30;

        /// <summary>
        /// 微头像宽度（像素）
        /// </summary>
        public int MicroAvatarWidth
        {
            get { return microAvatarWidth; }
            set { microAvatarWidth = value; }
        }

        private int microAvatarHeight = 30;

        /// <summary>
        /// 微头像高度（像素）
        /// </summary>
        public int MicroAvatarHeight
        {
            get { return microAvatarHeight; }
            set { microAvatarHeight = value; }
        }

        #endregion 头像尺寸设置

        private int[] integrityProportions = null;

        /// <summary>
        /// 档案完整度权重集合
        /// </summary>
        public int[] IntegrityProportions
        {
            get
            {
                if (integrityProportions == null)
                {
                    integrityProportions = new int[9];
                    integrityProportions[(int)ProfileIntegrityItems.Avatar] = 20;
                    integrityProportions[(int)ProfileIntegrityItems.Birthday] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.NowArea] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.HomeArea] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.IM] = 10;
                    integrityProportions[(int)ProfileIntegrityItems.Mobile] = 0;
                    integrityProportions[(int)ProfileIntegrityItems.EducationExperience] = 15;
                    integrityProportions[(int)ProfileIntegrityItems.WorkExperience] = 15;
                    integrityProportions[(int)ProfileIntegrityItems.Introduction] = 10;
                }
                return integrityProportions;
            }
            set { integrityProportions = value; }
        }

        #region 最小资料完整度

        private int minIntegrity = 50;

        /// <summary>
        /// 最小资料完整度
        /// </summary>
        public int MinIntegrity
        {
            get { return minIntegrity; }
            set { minIntegrity = value; }
        }

        #endregion 最小资料完整度

        #region IEntity 成员

        object IEntity.EntityId
        {
            get { return typeof(UserProfileSettings).FullName; }
        }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}