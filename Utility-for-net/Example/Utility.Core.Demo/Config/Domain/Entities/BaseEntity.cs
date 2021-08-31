//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
#define NHibernate
#define Dapper
#define EF
#define Annotation
using Utility.Domain.Entities;
using D = Dapper;
using ISelectedModel = Utility.Domain.Entities.ISelectedEntity<string>;


namespace Config.Domain.Entities
{
    /// <summary>基类</summary>
    public class BaseEntity : IEntity<string>,ISelectedModel
    {
        public const int IdMaxLength = 36;
        public const int NameMaxLength = 10;
        public const int IpMaxLength = 15;
        public const int DescriptionMaxLength = 500;
        /// <summary>
        /// 是否选中
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [D.NotMapped]
        public  virtual bool IsSelected { get; set; }

        #region 私有变量 start.......
        private string _id;//Id
        private string _name;//名称
        private string _ip;//ip地址
        private int _port;//端口
        private string _description;//描述
        private System.DateTime? _createDate;//创建时间
        private System.DateTime? _lastDate;//修改时间
        #endregion 私有变量 end......


        #region 公共变量 start.......

        /// <summary>Id</summary>
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.StringLength(IdMaxLength)]
        [NHibernate.Mapping.Attributes.Id(Length = IdMaxLength)]
        [D.Key]
        public virtual string Id { get { return this._id; } set { Set(ref _id, value, "Id"); } }

        /// <summary>
        /// 名称
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(NameMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length = NameMaxLength)]
        public virtual string Name { get { return this._name; } set { Set(ref _name, value, "Name"); } }

        /// <summary>
        /// ip地址
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(IpMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length = IpMaxLength)]
        public virtual string Ip { get { return this._ip; } set { Set(ref _ip, value, "Ip"); } }

        /// <summary>
        /// 端口
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public virtual int Port { get { return this._port; } set { Set(ref _port, value, "Port"); } }

        /// <summary>
        /// 描述
        /// </summary>
        [System.ComponentModel.DataAnnotations.StringLength(DescriptionMaxLength)]
        [NHibernate.Mapping.Attributes.Property(Length = DescriptionMaxLength)]
        public virtual string Description { get { return this._description; } set { Set(ref _description, value, "Description"); } }

        /// <summary>
        ///创建时间
        ///</summary>
        [NHibernate.Mapping.Attributes.Property()]
        [D.IgnoreUpdate]
        public virtual System.DateTime? CreateDate { get { return this._createDate; } set { Set(ref _createDate, value, "CreateDate"); } }

        /// <summary>
        ///修改时间
        ///</summary>
        [NHibernate.Mapping.Attributes.Property()]
        [D.IgnoreInsert]
        public virtual System.DateTime? LastDate { get { return this._lastDate; } set { Set(ref _lastDate, value, "LastDate"); } }

        #endregion 公共变量 end...... 

        /// <summary>
        /// 设置属性值 wpf 使用时直接继承 viewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <param name="propertyName">属性名称 wpf 有效</param>

        protected virtual void Set<T>(ref T oldValue, T newValue, string propertyName)
        {
            oldValue = newValue;
        }
    }
}
#endif