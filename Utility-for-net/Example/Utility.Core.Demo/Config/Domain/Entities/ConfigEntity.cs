//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
#define NHibernate
#define Dapper
#define EF
#define Annotation
using D = Dapper;


namespace Config.Domain.Entities
{
    /// <summary>配置 实体 模型</summary>
#if Annotation
    [System.Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("Config")]
    [NHibernate.Mapping.Attributes.Class(Table = "Config", Lazy=false)]
    [D.Table("Config")]
#endif
    public class ConfigEntity : BaseEntity
    {
        public const int AddressTemplateMaxLength = 100;
        public const int FlagMaxLength = 100;
        public const int UserMaxLength = 10;
        public const int PwdMaxLength = 20;
        /// <summary>
        /// 配置 实体模型 表名
        /// </summary>
        public const string TableName ="Config";

        #region 私有变量 start.......
        private string _addressTemplate;//地址 模板
        private string _flag;//标识
        private ConfigStatus _status;//状态
        private string _user;//用户名
        private string _pwd;//密码
        private bool _isPwd;//是否需要密码



        #endregion 私有变量 end......

        #region 公共变量 start.......
        /// <summary>
        /// 地址 模板
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(AddressTemplateMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length= AddressTemplateMaxLength)]
        public string AddressTemplate { get { return this._addressTemplate; } set { Set(ref _addressTemplate, value, "AddressTemplate"); } }


        /// <summary>
        /// 标识
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(FlagMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length = FlagMaxLength)]
        public string Flag { get { return this._flag; } set { Set(ref _flag, value,"Flag"); } }

        /// <summary>
        /// 状态
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public ConfigStatus Status { get { return this._status; } set { Set(ref _status, value, "Status"); } }

        /// <summary>
        /// 用户名
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(UserMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length= UserMaxLength)]
        public string User { get { return this._user; } set { Set(ref _user, value,"User"); } }

        /// <summary>
        /// 密码
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(PwdMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length= PwdMaxLength)]
        public string Pwd { get { return this._pwd; } set { Set(ref _pwd, value,"Pwd"); } }

        /// <summary>
        /// 是否需要密码
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public bool IsPwd { get { return this._isPwd; } set { Set(ref _isPwd, value, "IsPwd"); } }

        #endregion 公共变量 end...... 

    }
}
#endif