using NHibernate.Mapping.Attributes;
using System;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 档案信息
    /// </summary>
    [Class(Table = "record_info", NameType = typeof(RecordEntity), Lazy = false)]
    public class RecordEntity : BaseEntity
    {
        private string _recordNumber;// 档案编号
        private string _name;
        private string _sex;
        private DateTime _birthday;
        private string _cardNo;
        private string _photo;
        private string _maritalStatus;//婚姻状况
        private string _addrees;
        private string _postlacode;//邮政编码
        private string _partyMember;//是否是党员
        private int _schoolAge;
        private string _speciaity;//特长
        private string _foreignLanguage;//外语
        private string _grate;//外语等级
        private string _famousRace;//名族
        private string _nativePlace;//籍贯
        private string _politicalOutlook;//政治面貌
        private string _education;//学历
        private string _major;//专业
        private string _employmentForm;//用工形式
        private long _userId;

        /// <summary>
        /// 档案编号
        /// </summary>
        [Property(Column = "record_number", NotNull = true, Length = 5)]
        public string RecordNumber
        {
            get { return this._recordNumber; }
            set { Set(ref _recordNumber, value, "RecordNumber"); }
        }
        [Property(Column = "name", NotNull = true, Length = 10)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [Property(Column = "sex", NotNull = true, Length = 2)]
        public string Sex
        {
            get { return this._sex; }
            set { Set(ref _sex, value, "Sex"); }
        }
        [Property(Column = "birthday", NotNull = true, TypeType = typeof(DateTime))]
        public DateTime Birthday
        {
            get { return this._birthday; }
            set { Set(ref _birthday, value, "Birthday"); }
        }
        [Property(Column = "card_no", NotNull = true, Length = 18)]
        public string CardNo
        {
            get { return this._cardNo; }
            set { Set(ref _cardNo, value, "CardNo"); }
        }
        [Property(Column = "photo", NotNull = true, Length = 50)]
        public string Photo
        {
            get { return this._photo; }
            set { Set(ref _photo, value, "Photo"); }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        [Property(Column = "marital_status", NotNull = true, Length = 4)]
        public string MaritalStatus
        {
            get { return this._maritalStatus; }
            set { Set(ref _maritalStatus, value, "MaritalStatus"); }
        }
        [Property(Column = "address", NotNull = true, Length = 100)]
        public string Addrees
        {
            get { return this._addrees; }
            set { Set(ref _addrees, value, "Addrees"); }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [Property(Column = "postlacode", NotNull = true, Length = 5)]
        public string Postlacode
        {
            get { return this._postlacode; }
            set { Set(ref _postlacode, value, "Postlacode"); }
        }
        /// <summary>
        /// 是否是党员
        /// </summary>
        [Property(Column = "party_member",Length =2)]
        public string PartyMember
        {
            get { return this._partyMember; }
            set { Set(ref _partyMember, value, "PartyMember"); }
        }
        [Property(Column = "school_age")]
        public int SchoolAge
        {
            get { return this._schoolAge; }
            set { Set(ref _schoolAge, value, "SchoolAge"); }
        }
        /// <summary>
        /// 特长
        /// </summary>
        [Property(Column = "speciaity", NotNull = true, Length = 10)]
        public string Speciaity
        {
            get { return this._speciaity; }
            set { Set(ref _speciaity, value, "Speciaity"); }
        }
        /// <summary>
        /// 外语
        /// </summary>
        [Property(Column = "foreign_language", NotNull = true, Length = 10)]
        public string ForeignLanguage
        {
            get { return this._foreignLanguage; }
            set { Set(ref _foreignLanguage, value, "ForeignLanguage"); }
        }
        /// <summary>
        /// 外语等级
        /// </summary>
        [Property(Column = "grate", NotNull = true, Length = 10)]
        public string Grate
        {
            get { return this._grate; }
            set { Set(ref _grate, value, "Grate"); }
        }
        /// <summary>
        /// 名族
        /// </summary>
        [Property(Column = "famous_race", NotNull = true,  Length = 10)]
        public string FamousRace
        {
            get { return this._famousRace; }
            set { Set(ref _famousRace, value, "FamousRace"); }
        }

        /// <summary>
        /// 籍贯
        /// </summary>
        [Property(Column = "native_place", NotNull = true, Length = 10)]
        public string NativePlace
        {
            get { return this._nativePlace; }
            set { Set(ref _nativePlace, value, "NativePlace"); }
        }

        /// <summary>
        /// 政治面貌
        /// </summary>
        [Property(Column = "political_outlook", NotNull = true,  Length = 10)]
        public string PoliticalOutlook
        {
            get { return this._politicalOutlook; }
            set { Set(ref _politicalOutlook, value, "PoliticalOutlook"); }
        }
        /// <summary>
        /// 学历
        /// </summary>
        [Property(Column = "education", NotNull = true, Length = 50)]
        public string Education { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [Property(Column = "major", NotNull = true,  Length = 100)]
        public string Major
        {
            get { return this._major; }
            set { Set(ref _major, value, "Major"); }
        }
        /// <summary>
        /// 用工形式
        /// </summary>
        [Property(Column = "employment_form",  Length = 100)]
        public string EmploymentForm
        {
            get { return this._employmentForm; }
            set { Set(ref _employmentForm, value, "EmploymentForm"); }
        }
        public long UserId
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
      
    }
}
