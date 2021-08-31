using NHibernate.Mapping.Attributes;
using System;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 个人信息
    /// </summary>
    [Class(Table = "person_info", NameType = typeof(PersonEntity), Lazy = false)]
    public class PersonEntity:BaseEntity
    {
        private string _qQ;
        private string _email;
        private string _handset;//电话
        private string _telphone;//手机号
        private string _address;
        private string _postlacode;//邮政编码
        private string _secondSchoolAge;//第一次学龄
        private string _secondSpeciaity;//二次专业
        private string _graduateSchool;//大学学校
        private DateTime _graduateDate;//大学就读时间
        private DateTime _partyMemberDate;//入党时间
        private string _computerGrate;//计算机等级
        private string _likes;
        private string _onesStrongSuit;//强项  特长
        private long _userId;

        [Property(Column = "qq",  Length = 15)]
        public string QQ
        {
            get { return this._qQ; }
            set { Set(ref _qQ, value, "QQ"); }
        }
        [Property(Column = "eamil", Length = 20)]
        public string Email
        {
            get { return this._email; }
            set { Set(ref _email, value, "Email"); }
        }
        /// <summary>
        /// 电话
        /// </summary>
        [Property(Column = "handset", Length = 15)]
        public string Handset
        {
            get { return this._handset; }
            set { Set(ref _handset, value, "Handset"); }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        [Property(Column = "telphone", Length = 15)]
        public string Telphone
        {
            get { return this._telphone; }
            set { Set(ref _telphone, value, "Telphone"); }
        }
        [Property(Column = "address", Length = 100)]
        public string Address
        {
            get { return this._address; }
            set { Set(ref _address, value, "Address"); }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [Property(Column = "postlacode", Length = 5)]
        public string Postlacode
        {
            get { return this._postlacode; }
            set { Set(ref _postlacode, value, "Postlacode"); }
        }
        /// <summary>
        /// 第一次学龄
        /// </summary>
        [Property(Column = "second_school_age", Length = 10)]
        public string SecondSchoolAge
        {
            get { return this._secondSchoolAge; }
            set { Set(ref _secondSchoolAge, value, "SecondSchoolAge"); }
        }
        /// <summary>
        /// 二次专业
        /// </summary>
        [Property(Column = "second_speciaity", Length = 40)]
        public string SecondSpeciaity
        {
            get { return this._secondSpeciaity; }
            set { Set(ref _secondSpeciaity, value, "SecondSpeciaity"); }
        }
        /// <summary>
        /// 大学学校
        /// </summary>
        [Property(Column = "graduate_school", Length = 40)]
        public string GraduateSchool
        {
            get { return this._graduateSchool; }
            set { Set(ref _graduateSchool, value, "GraduateSchool"); }
        }
        /// <summary>
        /// 大学就读时间
        /// </summary>
        [Property(Column = "graduate_date")]
        public DateTime GraduateDate
        {
            get { return this._graduateDate; }
            set { Set(ref _graduateDate, value, "GraduateDate"); }
        }
        /// <summary>
        /// 入党时间
        /// </summary>
        [Property(Column = "party_member_date")]
        public DateTime PartyMemberDate
        {
            get { return this._partyMemberDate; }
            set { Set(ref _partyMemberDate, value, "PartyMemberDate"); }
        }
        /// <summary>
        /// 计算机等级
        /// </summary>
        [Property(Column = "computer_grate",Length =10)]
        public string ComputerGrate
        {
            get { return this._computerGrate; }
            set { Set(ref _computerGrate, value, "ComputerGrate"); }
        }
        [Property(Column = "likes", Length = 50)]
        public string Likes
        {
            get { return this._likes; }
            set { Set(ref _likes, value, "Likes"); }
        }
        /// <summary>
        /// 强项  特长
        /// </summary>
        [Property(Column = "ones_strong_suit", Length = 50)]
        public string OnesStrongSuit
        {
            get { return this._onesStrongSuit; }
            set { Set(ref _onesStrongSuit, value, "OnesStrongSuit"); }
        }
        public long UserId
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
    }
}
