//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
#define NHibernate
#define Dapper
#define EF
#define Annotation
using D = Dapper;

namespace Config.Domain.Entities
{
    /// <summary>服务 实体 模型</summary>
    [System.Serializable]
    [System.ComponentModel.DataAnnotations.Schema.Table("Service")]
    [NHibernate.Mapping.Attributes.Class(Table = "Service", Lazy=false)]
    [D.Table("Service")]

    public class ServiceEntity : BaseEntity
    {
        /// <summary>
        /// 服务 实体模型 表名
        /// </summary>
        public const string TableName ="Service";

        #region 私有变量 start.......
        private ServiceStatus _status;//状态
        #endregion 私有变量 end......

        #region 公共变量 start.......

        /// <summary>
        /// 状态
        /// </summary>
        [NHibernate.Mapping.Attributes.Property]
        public ServiceStatus Status { get { return this._status; } set { Set(ref _status, value,"Status"); } }

        #endregion 公共变量 end...... 

    }
}
#endif