//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using NHibernate.Mapping.Attributes;
using PetaPoco;
using SocialContact.Domain.Entities;
using SocialContact.Domain.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Tunynet.Caching;
using Tunynet.Common.Configuration;
using Tunynet.Settings;
using Utility.Domain.Uow;

namespace Tunynet.Common
{
    /// <summary>
    /// 用户帐号
    /// </summary>
    [TableName("tn_Users")]
    [PrimaryKey("UserId", autoIncrement = false)]
    [CacheSetting(true)]
    [Serializable]
    [Class(Table = "tn_Users")]
    public class User : Entity,IUser, IEntity
    {
        #region 构造器

        /// <summary>
        /// 构造器
        /// </summary>
        public User()
        {
        }

     
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="userGuid">用户Guid</param>
        /// <param name="userName">用户名称</param>
        public static User New(string userGuid, string userName)
        {
            User user = New();
            //user.UserId = userId;
            user.UserName = userName;
            return user;
        }

        /// <summary>
        /// 新建实体时使用
        /// </summary>
        public static User New()
        {
            User user = new User();
            user.DateCreated = DateTime.Now;
            user.LastActivityTime = DateTime.Now;
            user.BanDeadline = DateTime.Now;
            user.Rank = 1;
            //用户创建后 根据配置设置是否管制
            if (SettingManager<UserSettings>.Get().AutomaticModerated)
                user.IsModerated = true;

            return user;
        }

        #endregion 构造器

        #region 需持久化属性

        /// <summary>
        ///UserId
        /// </summary>
        [Id]
        public virtual long UserId { get; set; }

        /// <summary>
        /// 用户GUID/OpenId
        /// </summary>
        [Property]
        public virtual string UserGuid { get; set; } = string.Empty;

        /// <summary>
        ///用户名（昵称）
        /// </summary>
        [Property]
        public virtual string UserName { get; set; } = string.Empty;

        /// <summary>
        ///密码
        /// </summary>
        [Property]
        public virtual string Password { get; set; } = string.Empty;

        /// <summary>
        ///0=Clear（明文）1=标准MD5
        /// </summary>
        [Property]
        public virtual int PasswordFormat { get; set; }

        /// <summary>
        ///帐号邮箱
        /// </summary>
        [Property]
        public virtual string AccountEmail { get; set; } = string.Empty;

        /// <summary>
        ///帐号邮箱是否通过验证
        /// </summary>
        [Property]
        public virtual bool IsEmailVerified { get; set; }
        /// <summary>
        ///手机号码
        /// </summary>
        [Property]
        public virtual string AccountMobile { get; set; } = string.Empty;

        /// <summary>
        ///帐号手机是否通过验证
        /// </summary>
        [Property]
        public virtual bool IsMobileVerified { get; set; }

        /// <summary>
        ///个人姓名 或 企业名称
        /// </summary>
        [Property]
        public virtual string TrueName { get; set; } = string.Empty;

        /// <summary>
        ///是否强制用户登录
        /// </summary>
        [Property]
        public virtual bool ForceLogin { get; set; }

        /// <summary>
        /// 用户账号状态(-1=已删除,1=已激活,0=未激活)
        /// </summary>
        [Property]
        public virtual UserStatus Status { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Property]
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        ///创建用户时的ip
        /// </summary>
        [Property]
        public virtual string IpCreated { get; set; } = string.Empty;

        /// <summary>
        ///用户类别
        /// </summary>
        [Property]
        public virtual int UserType { get; set; }

        /// <summary>
        ///上次活动时间
        /// </summary>
        [Property]
        public virtual DateTime LastActivityTime { get; set; }

        /// <summary>
        ///上次操作
        /// </summary>
        [Property]
        public virtual string LastAction { get; set; } = string.Empty;

        /// <summary>
        ///上次活动时的ip
        /// </summary>
        [Property]
        public virtual string IpLastActivity { get; set; } = string.Empty;

        /// <summary>
        ///是否封禁
        /// </summary>
        [Property]
        public virtual bool IsBanned { get; set; }

        /// <summary>
        ///封禁原因
        /// </summary>
        [Property]
        public virtual string BanReason { get; set; } = string.Empty;

        /// <summary>
        ///封禁截止日期
        /// </summary>
        [Property]
        public virtual DateTime BanDeadline { get; set; }

        /// <summary>
        ///用户是否被监管
        /// </summary>
        public virtual bool IsModerated { get; set; }

        /// <summary>
        ///强制用户管制（不会自动解除）
        /// </summary>
        [Property]
        public virtual bool IsForceModerated { get; set; }

        /// <summary>
        /// 头像 是否存在
        /// </summary>
        public virtual int HasAvatar { get; set; }

        /// <summary>
        /// 封面图 是否存在
        /// </summary>
        [Property]
        public virtual int HasCover { get; set; }

        /// <summary>
        ///磁盘配额
        /// </summary>
        [Property]
        public virtual int DatabaseQuota { get; set; }

        /// <summary>
        ///已用磁盘空间
        /// </summary>
        [Property]
        public virtual int DatabaseQuotaUsed { get; set; }

        /// <summary>
        /// 关注用户数
        /// </summary>
        [Property]
        public virtual int FollowedCount { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        [Property]
        public virtual int FollowerCount { get; set; }

        /// <summary>
        /// 经验积分值
        /// </summary>
        [Property]
        public virtual int ExperiencePoints { get; set; }

        /// <summary>
        /// 威望积分值
        /// </summary>
        [Property]
        public virtual int ReputationPoints { get; set; }

        /// <summary>
        /// 交易积分值
        /// </summary>
        [Property]
        public virtual int TradePoints { get; set; }

        /// <summary>
        /// 交易积分值2
        /// </summary>
        [Property]
        public virtual int TradePoints2 { get; set; }

        /// <summary>
        /// 交易积分值3
        /// </summary>
        [Property]
        public virtual int TradePoints3 { get; set; }

        /// <summary>
        /// 交易积分值4
        /// </summary>
        [Property]
        public virtual int TradePoints4 { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        [Property]
        public virtual int Rank { get; set; }

        /// <summary>
        /// 冻结的交易积分
        /// </summary>
        [Property]
        public virtual int FrozenTradePoints { get; set; }

        #endregion 需持久化属性

        #region 显示实现接口

        long IUser.UserId
        {
            get { return UserId; }
        }

        string IUser.UserName
        {
            get { return UserName; }
        }

        int IUser.UserType
        {
            get { return UserType; }
        }

        string IUser.AccountEmail
        {
            get { return AccountEmail; }
        }

        bool IUser.IsEmailVerified
        {
            get { return IsEmailVerified; }
        }

        string IUser.AccountMobile
        {
            get { return AccountMobile; }
        }

        bool IUser.IsMobileVerified
        {
            get { return IsMobileVerified; }
        }

        string IUser.TrueName
        {
            get { return TrueName; }
        }

        string IUser.DisplayName
        {
            get { return DisplayName; }
        }

        bool IUser.ForceLogin
        {
            get { return ForceLogin; }
        }

        UserStatus IUser.Status
        {
            get { return Status; }
        }

        DateTime IUser.DateCreated
        {
            get { return DateCreated; }
        }

        DateTime IUser.LastActivityTime
        {
            get { return LastActivityTime; }
        }

        string IUser.LastAction
        {
            get { return LastAction; }
        }

        string IUser.IpCreated
        {
            get { return IpCreated; }
        }

        string IUser.IpLastActivity
        {
            get { return IpLastActivity; }
        }

        bool IUser.IsBanned
        {
            get { return IsBanned; }
        }

        bool IUser.IsModerated
        {
            get { return IsModerated; }
        }

        int IUser.HasAvatar
        {
            get { return HasAvatar; }
        }

        /// <summary>
        /// 经验积分值
        /// </summary>
        int IUser.ExperiencePoints
        {
            get { return this.ExperiencePoints; }
        }

        /// <summary>
        /// 威望积分值
        /// </summary>
        int IUser.ReputationPoints
        {
            get { return this.ReputationPoints; }
        }

        /// <summary>
        /// 交易积分值
        /// </summary>
        int IUser.TradePoints
        {
            get { return this.TradePoints; }
        }

        /// <summary>
        /// 交易积分值2
        /// </summary>
        int IUser.TradePoints2
        {
            get { return this.TradePoints2; }
        }

        /// <summary>
        /// 交易积分值3
        /// </summary>
        int IUser.TradePoints3
        {
            get { return this.TradePoints3; }
        }

        /// <summary>
        /// 交易积分值4
        /// </summary>
        int IUser.TradePoints4
        {
            get { return this.TradePoints4; }
        }

        /// <summary>
        /// 用户等级
        /// </summary>
        int IUser.Rank
        {
            get { return this.Rank; }
        }

        /// <summary>
        /// 冻结的交易积分
        /// </summary>
        int IUser.FrozenTradePoints
        {
            get { return this.FrozenTradePoints; }
        }

        #endregion 显示实现接口

        #region 扩展属性

        /// <summary>
        /// 对外显示名称
        /// </summary>
        [Ignore]
        public virtual string DisplayName
        {
            get
            {
                DisplayNameType displayNameType = SettingManager<UserSettings>.Get().DisplayNameType;
                if (displayNameType == DisplayNameType.UserNameFirst)
                {
                    return this.UserName;
                }
                else if (displayNameType == DisplayNameType.TrueNameFirst)
                {
                    if (!string.IsNullOrEmpty(this.TrueName))
                        return this.TrueName;
                }

                return this.UserName;
            }
        }

        /// <summary>
        /// 获取对象的一个深复制
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }

            return retval;

            //return this as object;      //引用同一个对象
            //return this.MemberwiseClone(); //浅复制
        }

        #endregion 扩展属性

        /// <summary>
        /// 总浏览数
        /// </summary>
        [Ignore]
        public virtual int HitTimes
        {
            get
            {
                return CountService.Get(CountTypes.Instance().HitTimes(), this.UserId);
            }
        }

        /// <summary>
        /// 内容数
        /// </summary>
        //[Ignore]
        [Property]
        public virtual long ContentCount
        {
            get
            {
                //IKvStore IKvStore = DIContainer.Resolve<IKvStore>();
                //int Value;
                //if (IKvStore.TryGet<int>(KvKeys.Instance().UserContributeCount(this.UserId), out Value))
                //{
                //    return Value;
                //}

                return 0;
            }
        }

        /// <summary>
        /// 最近七天的威望数
        /// </summary>
        [Ignore]
        public virtual int PreWeekReputationPointsCount
        {
            get
            {
                int count = CountService.GetStageCount(CountTypes.Instance().ReputationPointsCounts(), 7, this.UserId);
                if (count < 0)
                    return 0;
                return count;
            }
        }

        /// <summary>
        /// 最近七天浏览数
        /// </summary>
        [Ignore]
        public virtual int PreWeekHitTimes
        {
            get
            {
                int count = CountService.GetStageCount(CountTypes.Instance().HitTimes(), 7, this.UserId);
                if (count < 0)
                    return 0;
                return count;
            }
        }

        #region IEntity 成员

        object IEntity.EntityId { get { return this.UserId; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员

        #region 拓展方法

        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Role> GetUserRoles()
        {
            var userRoles = RoleService.GetRoleIdsOfUser(UserId);
            return RoleService.GetRoles(userRoles);
        }


        public virtual void BindPhone(string phone)
        {
            this.AccountMobile = phone;
            this.IsMobileVerified = true;
            Notifications.Add(new UserBindPhoneEvent(UserId, phone));
        }

       
        public virtual void Handler()
        {
            foreach (var item in Notifications)
            {

            }
        }
        #endregion 拓展方法
    }
}