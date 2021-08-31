using System;

namespace Utility
{
    public enum OrmFlag
    {
        None,
        Annotation,
        Map,
        Xml
    }
    /// <summary>
    /// 配置
    /// </summary>
    public partial class ConfigHelper
    {
        public static bool AnnationIoc = false;
        public static OrmFlag OrmFlag = OrmFlag.None;
        /// <summary>
        /// 连接地址
        /// </summary>
        public static string ConnectionString { get; set; }
        /// <summary>
        /// 服务存储标识
        /// </summary>
        public static ServiceFlag ServiceFlag { get; set; }
        /// <summary>
        /// 是否 是 abp 框架 配置不同造成其他错误影响
        /// </summary>
        public static bool IsAbpEf { get; set; }
        /// <summary>
        /// key 名称
        /// </summary>
        public static string Key { get; set; }

        /// <summary>
        /// 数据库标识
        /// </summary>
        public static DbFlag DbFlag { get; set; } = DbFlag.SqlServer;
        /// <summary>
        /// 数据库版本 5.5.0 
        /// </summary>
        public static string Version { get; set; }
    }
    /// <summary>
    /// 数据库标识
    /// </summary>
    [Flags]
    public enum DbFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        Access,
        /// <summary>SqlServer  数据库/ </summary>
        SqlServer,
        /// <summary>MySql  数据库 5.5/ </summary>
        MySql,
        /// <summary>Sqlite  数据库/ </summary>
        Sqlite,
        /// <summary>Oracle  数据库/ </summary>
        Oracle,
        /// <summary>Postgre  数据库/ </summary>
        Postgre
    }

    /// <summary>
    /// 服务存储标识
    /// </summary>
    [Flags]
    public enum ServiceFlag
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Eureka
        /// </summary>
        Eureka = 0x1,
        /// <summary>
        /// Consul
        /// </summary>
        Consul = 0x2,
        /// <summary>
        /// Zookeeper
        /// </summary>
        Zookeeper = 0x3,
        /// <summary>
        /// ServiceFabric
        /// </summary>
        ServiceFabric = 0x4,
        /// <summary>
        /// Redis
        /// </summary>
        Redis = 0x5,
        /// <summary>
        /// Rabbitmq
        /// </summary>
        Rabbitmq = 0x6,
        /// <summary>
        /// Kubernetes
        /// </summary>
        Kubernetes = 0x7,
        /// <summary>
        /// Etcd
        /// </summary>
        Etcd = 0x8
    }

}
