using System;
using System.Collections.Generic;
using System.Text;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Entities;

namespace Utility.Demo.Application.Services.Dtos
{
     /// <summary>用户 </summary>
    public class UserDto : UserDto<UserDto, long>
    {

    }
    public class AdminDto : BaseUserDto<AdminDto, long>
    {
        public virtual long RoleId { get; set; }
    }
    public abstract class BaseUserDto<User, Key> : Entity<Key>
        where User : BaseUserDto<User, Key>
    {
        private string account;
        private string email;
        private string phone;
        private string pwd;
        private string nickName;
        private string realName;
        private Sex sex;
        private DateTime? birthday;
        private string headPic;
        private Key headPicId;
        private UserStatus status;
        private DateTime? loginDate;
        private long registerIp;
        private long loginIp;
        private string description;
        private int? failCount;
        private Key parentId;
        private User parent;
        private ISet<User> children;
        private DateTime? registerDate;

        /// <summary>用户账号 </summary>
        public virtual string Account { get => account; set { Set(ref account, value, "Account"); } }
        /// <summary>用户 邮箱</summary>
        public virtual string Email { get => email; set { Set(ref email, value, "Email"); } }
        /// <summary>用户手机号 </summary>
        public virtual string Phone { get => phone; set { Set(ref phone, value, "Phone"); } }
        /// <summary>用户密码 </summary>
        public virtual string Pwd { get => pwd; set { Set(ref pwd, value, "Pwd"); } }
        /// <summary>用户 昵称</summary>
        public virtual string NickName { get => nickName; set { Set(ref nickName, value, "NickName"); } }
        /// <summary>用户真实姓名 </summary>
        public virtual string RealName { get => realName; set { Set(ref realName, value, "RealName"); } }
        /// <summary>用户性别 </summary>
        public virtual Sex Sex { get => sex; set { Set(ref sex, value, "Sex"); } }
        /// <summary>用户 出生日期</summary>
        public virtual DateTime? Birthday { get => birthday; set { Set(ref birthday, value, "Birthday"); } }

        /// <summary>用户 头像 </summary>
        public virtual string HeadPic { get => headPic; set { Set(ref headPic, value, "HeadPic"); } }
        /// <summary>用户头像id </summary>
        public virtual Key HeadPicId { get => headPicId; set { Set(ref headPicId, value, "HeadPicId"); } }
        /// <summary>用户 状态</summary>
        public virtual UserStatus Status { get => status; set { Set(ref status, value, "Status"); } }
        /// <summary>用户注册时间 </summary>
        public virtual DateTime? RegisterDate { get => registerDate; set { Set(ref registerDate, value, "RegisterDate"); } }
        /// <summary>用户登录时间 </summary>
        public virtual DateTime? LoginDate { get => loginDate; set { Set(ref loginDate, value, "LoginDate"); } }
        public virtual DateTime[] LoginDates { get; set; }

        /// <summary>用户注册ip </summary>
        public virtual long RegisterIp { get => registerIp; set { Set(ref registerIp, value, "RegisterIp"); } }
        /// <summary>用户登录ip </summary>
        public virtual long LoginIp { get => loginIp; set { Set(ref loginIp, value, "LoginIp"); } }
        /// <summary>用户描述 </summary>
        public virtual string Description { get => description; set { Set(ref description, value, "Description"); } }

        /// <summary>用户登录失败次数 </summary>
        public virtual int? FailCount { get => failCount; set { Set(ref failCount, value, "FailCount"); } }

        /// <summary>父用户 id(推荐人/创建人) </summary>
        public virtual Key ParentId { get => parentId; set { Set(ref parentId, value, "ParentId"); } }

        /// <summary>父用户 (推荐人/创建人) </summary>
        public virtual User Parent { get => parent; set => parent = value; }
        /// <summary> 子集 (推荐人/创建人) </summary>
        public virtual ISet<User> Children { get => children; set => children = value; }
    }

    /// <summary>用户 </summary>
    public class UserDto<User, Key> : BaseUserDto<User, Key>, IEntity<Key>
        where User : BaseUserDto<User, Key>
    {
        private string edution;
        private string school;
        private string jobCompany;
        private string job;
        private string likes;
        private MaritalStatusFlag marital;
        private string cardId;
        private string cardPhoto1;
        private string cardPhoto2;
        private string handCardPhoto1;
        private string handCardPhoto2;
        private bool cardPhotoStatus;
        private string cardPhotoId1;
        private string cardPhotoId2;
        private string handCardPhotoId1;
        private string handCardPhotoId2;
        private int level;
        private string bankId;

        /// <summary>用户 学历</summary>
        public virtual string Edution { get => edution; set { Set(ref edution, value, "Edution"); } }
        /// <summary>用户学校 </summary>
        public virtual string School { get => school; set { Set(ref school, value, "School"); } }
        /// <summary>用户职位公司 </summary>
        public virtual string JobCompany { get => jobCompany; set { Set(ref jobCompany, value, "JobCompany"); } }
        /// <summary>用户 职位</summary>
        public virtual string Job { get => job; set { Set(ref job, value, "Job"); } }
        /// <summary>用户 爱好</summary>
        public virtual string Likes { get => likes; set { Set(ref likes, value, "Likes"); } }
        /// <summary>用户婚姻状态 </summary>
        public virtual MaritalStatusFlag Marital { get => marital; set { Set(ref marital, value, "Marital"); } }

        /// <summary>用户身份证 </summary>
        public virtual string CardId { get => cardId; set { Set(ref cardId, value, "CardId"); } }
        /// <summary>用户身份证正面 图片 </summary>
        public virtual string CardPhoto1 { get => cardPhoto1; set { Set(ref cardPhoto1, value, "CardPhoto1"); } }
        /// <summary>用户身份证反面 图片 </summary>
        public virtual string CardPhoto2 { get => cardPhoto2; set { Set(ref cardPhoto2, value, "CardPhoto2"); } }

        /// <summary>用户手持本人身份证正面 图片 </summary>
        public virtual string HandCardPhoto1 { get => handCardPhoto1; set { Set(ref handCardPhoto1, value, "HandCardPhoto1"); } }
        /// <summary>用户手持本人身份证反面图片 </summary>
        public virtual string HandCardPhoto2 { get => handCardPhoto2; set { Set(ref handCardPhoto2, value, "HandCardPhoto2"); } }
        /// <summary>用户身份证 状态(有效/无效) </summary>
        public virtual bool CardPhotoStatus { get => cardPhotoStatus; set { Set(ref cardPhotoStatus, value, "CardPhotoStatus"); } }

        /// <summary>用户身份证正面 图片  id</summary>
        public virtual string CardPhotoId1 { get => cardPhotoId1; set { Set(ref cardPhotoId1, value, "CardPhotoId1"); } }
        /// <summary>用户身份证反面 图片 id</summary>
        public virtual string CardPhotoId2 { get => cardPhotoId2; set { Set(ref cardPhotoId2, value, "cardPhotoId2"); } }

        /// <summary>用户手持本人身份证正面 图片id </summary>
        public virtual string HandCardPhotoId1 { get => handCardPhotoId1; set { Set(ref handCardPhotoId1, value, "HandCardPhotoId1"); } }
        /// <summary>用户手持本人身份证反面图片 id </summary>
        public virtual string HandCardPhotoId2 { get => handCardPhotoId2; set { Set(ref handCardPhotoId2, value, "HandCardPhotoId2"); } }

        /// <summary>用户等级 </summary>
        public virtual int Level { get => level; set { Set(ref level, value, "Level"); } }
        /// <summary>用户银行卡 卡号 </summary>
        public virtual string BankId { get => bankId; set { Set(ref bankId, value, "BankId"); } }

    }
}
