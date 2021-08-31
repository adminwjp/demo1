using Config.Application.Cache;
using Config.Application.Services;
using Config.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Config.Application.Services
{
    /// <summary>
    /// 配置常量
    /// </summary>
    public class ConfigConstant
    {

        public const string CONFIG = "config";
        public const string SERVICE = "service";

        /// <summary>
        //win7 sqlite 标识
        /// </summary>
        public const string Win7Sqlite = "Win7Sqlite";

        /// <summary>
        //win7 mysql 标识
        /// </summary>
        public const string Win7MySql = "Win7MySql";

        /// <summary>
        /// win7 docker mysql 标识
        /// </summary>
        public const string Win7DockerMySql = "Win7DockerMySql";

        /// <summary>
        /// win7 docker sqlserver 标识
        /// </summary>
        public const string Win7DockerSqlServer = "Win7DockerSqlServer";

        /// <summary>
        /// win7 虚拟机 liux sqlserver 标识
        /// </summary>
        public const string Win7VMSqlServer = "Win7VMSqlServer";

        /// <summary>
        /// win7 docker oracle 标识
        /// </summary>
        public const string Win7DockerOracle = "Win7DockerOracle";
        /// <summary>
        /// win7 虚拟机 liux oracle 标识
        /// </summary>
        public const string Win7VMOracle = "Win7VMOracle";

        /// <summary>
        /// win7 postgre 标识
        /// </summary>
        public const string Win7Postgre = "Win7Postgre";

        /// <summary>
        /// win7 redis 标识
        /// </summary>
        public const string Win7Redis = "Win7Redis";

        /// <summary>
        /// win7 es 标识
        /// </summary>
        public const string Win7Es = "Win7Es";

        /// <summary>
        /// win7 zookeeper 标识
        /// </summary>
        public const string Win7Zookeeper = "Win7Zookeeper";

        /// <summary>
        /// win7 kafka 标识
        /// </summary>
        public const string Win7Kafka = "Win7Kafka";


        /// <summary>
        /// win7 rabbitmq 标识
        /// </summary>
        public const string Win7Rabbitmq = "Win7Rabbitmq";



        /// <summary>
        /// win7 consul 标识
        /// </summary>
        public const string Win7Consul = "Win7Consul";

        /// <summary>
        /// win7 zipkin 标识
        /// </summary>
        public const string Win7Zipkin = "Win7Zipkin";

        /// <summary>
        /// win7 hbase 标识
        /// </summary>
        public const string Win7Hbase = "Win7Hbase";

        /// <summary>
        /// win7 mong 标识
        /// </summary>
        public const string Win7Mong = "Win7Mong";

        /// <summary>
        /// win7 docker mong 标识
        /// </summary>
        public const string Win7DockerMong = "Win7DockerMong";
    }

}

namespace Config.Application.Services
{
    public class ConnectionHelper
    {
        public const string SqliteConnectionString = "Data Source={0}.db;";
        public const string MySqlConnectionString = "Database={0};Data Source={1};User Id={2};Password={3};Old Guids=True;charset=utf8;";
        public const string SqlServerConnectionString = "Server={0};database={1};user={2};pwd={3}";
        public const string OracleConnectionString = "DATA SOURCE={0};USER ID={1};PASSWORD={2};";
        public const string PostgreConnectionString = "User ID={0};Password={1};Host={2};Port={3};Database={4};Pooling=true;";
        public const string ElasticsearchConnectionString = "http://{0}:{1}/";
        public const string RedisConnectionString = "{0}:{1},password={2},connectTimeout=1000,connectRetry=1,syncTimeout=10000";
        public const string ZipkinConnectionString = "http://{0}:{1}";
        public const string ConsulConnectionString = "http://{0}:{1}";
        public const string ZookeeperConnectionString = "{0}:{1}";
        public const string KafkaConnectionString = "http://{0}:{1}";
        public const string HbaseConnectionString = "http://{0}:{1}";
        public const string MongByPwdConnectionString = "mongodb://{0}:{1}@{2}:{3}";
        public const string MongConnectionString = "mongodb://@{0}:{1}";
    }
    public class DataManager
    {
        public readonly static List<ConfigEntity> Configs = new List<ConfigEntity>();
        public const string LocalIp = "192.168.1.3";
        public const string LocalWin7DockerIp = "192.168.99.100";
        public const string LocalWin7VMLinux1 = "192.168.1.4";
        public const string LocalWin7VMLinux2 = "192.168.1.7";
        /// <summary>
        /// 初始化所有 地址信息 网络 ip 有时 变动 麻烦
        /// </summary>
        static DataManager()
        {
            var sqlit = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.SqliteConnectionString,
                Name = ConfigConstant.Win7Sqlite,
                Ip = LocalIp,
                Port = 27017,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(sqlit);

            InitMySql();
            InitSqlServer();
            InitPostgre();
            InitOracle();
            InitRedis();
            InitZipkin();
            InitConsul();
            InitZookeeper();
            InitKafka();
            InitRabbitmq();
            InitHbase();
            InitMong();
            foreach (var item in Configs)
            {
                item.Flag = item.Name;
            }
            /**
             ConnectionStrings": {
    "SqliteConnectionString": "Data Source=Config.db;",
    "MySqlConnectionString": "Database=Config;Data Source=192.168.2.110;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;",
    "SqlServerConnectionString": "server=192.168.99.101;database=Config;user=sa;pwd=wjp930514.",
    "OracleConnectionString": "DATA SOURCE=192.168.99.101:1521/orcl;USER ID=Config;PASSWORD=123456;",
    "PostgreConnectionString": "User ID=postgres;Password=wjp930514.;Host=192.168.2.110;Port=5432;Database=Config;Pooling=true;",
    "ElasticsearchConnectionString": "http://192.168.2.110:9200/",
    "RedisConnectionString": "192.168.2.110:6379,password=wjp930514.,connectTimeout=1000,connectRetry=1,syncTimeout=10000"
             */

        }
        static void InitMong()
        {
            var mong = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.MongConnectionString,
                Name = ConfigConstant.Win7Mong,
                Ip = LocalIp,
                Port = 27017,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(mong);

            mong = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.MongConnectionString,
                Name = ConfigConstant.Win7DockerMong,
                Ip = LocalWin7DockerIp,
                Port = 27017,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(mong);
        }
        static void InitHbase()
        {
            var hbase = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.HbaseConnectionString,
                Name = ConfigConstant.Win7Hbase,
                Ip = LocalIp,
                Port = 16010,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(hbase);
        }
        static void InitRabbitmq()
        {
            var kafka = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = ConfigConstant.Win7Rabbitmq,
                Ip = LocalIp,
                User="guest",
                Pwd="guest",
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(kafka);
        }
        static void InitKafka()
        {
            var kafka = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.KafkaConnectionString,
                Name = ConfigConstant.Win7Kafka,
                Ip = LocalIp,
                Port = 9092,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(kafka);
        }
        static void InitZookeeper()
        {
            var consul = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.ConsulConnectionString,
                Name = ConfigConstant.Win7Zookeeper,
                Ip = LocalIp,
                Port = 2181,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(consul);
        }
        static void InitConsul()
        {
            var consul = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.ConsulConnectionString,
                Name = ConfigConstant.Win7Consul,
                Ip = LocalIp,
                Port = 8500,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(consul);
        }
        static void InitZipkin()
        {
            var zipkin = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.ZipkinConnectionString,
                Name = ConfigConstant.Win7Zipkin,
                Ip = LocalIp,
                Port = 9411,
                CreateDate = DateTime.Now,
                Status = ConfigStatus.Stop
            };
            Configs.Add(zipkin);
        }

        static void InitRedis()
        {
            var redis = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.RedisConnectionString,
                Name = ConfigConstant.Win7Redis,
                Ip = LocalIp,
                Port = 6379,
                CreateDate = DateTime.Now,
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(redis);
        }
        static void InitOracle()
        {
            var oracle = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.OracleConnectionString,
                Name = ConfigConstant.Win7DockerOracle,
                Ip = LocalWin7DockerIp,
                Port = 1521,
                CreateDate = DateTime.Now,
                User = "orcl",
                Pwd = "123456",
                Status = ConfigStatus.Stop
            };
            Configs.Add(oracle);

            oracle = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.OracleConnectionString,
                Name = ConfigConstant.Win7VMOracle,
                Ip = LocalWin7VMLinux1,
                Port = 1521,
                CreateDate = DateTime.Now,
                User = "orcl",//orcl 用户 
                Pwd = "123456",
                Status = ConfigStatus.Stop
            };
            Configs.Add(oracle);

            oracle = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.OracleConnectionString,
                Name = ConfigConstant.Win7VMOracle,
                Ip = LocalWin7VMLinux2,
                Port = 1521,
                CreateDate = DateTime.Now,
                User = "orcl",
                Pwd = "123456",
                Status = ConfigStatus.Stop
            };
            Configs.Add(oracle);
        }

        static void InitPostgre()
        {
            var postgre = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.PostgreConnectionString,
                Name = ConfigConstant.Win7Postgre,
                Ip = LocalIp,
                Port= 5432,
                CreateDate = DateTime.Now,
                User = "postgres",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(postgre);
        }

        static void InitSqlServer()
        {
            var sqlserver = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.SqlServerConnectionString,
                Name = ConfigConstant.Win7DockerSqlServer,
                Ip = LocalWin7DockerIp,
                CreateDate = DateTime.Now,
                User = "sa",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(sqlserver);

            sqlserver = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.SqlServerConnectionString,
                Name = ConfigConstant.Win7VMSqlServer,
                Ip = LocalWin7VMLinux1,
                CreateDate = DateTime.Now,
                User = "sa",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(sqlserver);

            sqlserver = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.SqlServerConnectionString,
                Name = ConfigConstant.Win7VMSqlServer,
                Ip = LocalWin7VMLinux2,
                CreateDate = DateTime.Now,
                User = "sa",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(sqlserver);

        }
        static void InitMySql()
        {
            //win7 mysql
            var mysql = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate= ConnectionHelper.MySqlConnectionString,
                Name = ConfigConstant.Win7MySql,
                Ip = LocalIp,
                CreateDate = DateTime.Now,
                Port = 3306,
                User = "root",
                Pwd = "wjp930514."
            };
            Configs.Add(mysql);

            //win7 docker mysql 集群
            //win7 docker mysql1
             mysql = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                 AddressTemplate = ConnectionHelper.MySqlConnectionString,
                 Name = ConfigConstant.Win7DockerMySql,
                Ip = LocalWin7DockerIp,
                CreateDate = DateTime.Now,
                Port = 3306,
                User = "root",
                Pwd = "wjp930514.",
                 Status = ConfigStatus.Stop
             };
            Configs.Add(mysql);

            //win7 docker mysql2
            mysql = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.MySqlConnectionString,
                Name = ConfigConstant.Win7DockerMySql,
                Ip = LocalWin7DockerIp,
                CreateDate = DateTime.Now,
                Port = 3307,
                User = "root",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(mysql);

            //win7 docker mysql3
            mysql = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.MySqlConnectionString,
                Name = ConfigConstant.Win7DockerMySql,
                Ip = LocalWin7DockerIp,
                CreateDate = DateTime.Now,
                Port = 3308,
                User = "root",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(mysql);

            //win7 docker mysql4
            mysql = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.MySqlConnectionString,
                Name = ConfigConstant.Win7DockerMySql,
                Ip = LocalWin7DockerIp,
                CreateDate = DateTime.Now,
                Port = 3309,
                User = "root",
                Pwd = "wjp930514.",
                Status = ConfigStatus.Stop
            };
            Configs.Add(mysql);

            //win7 docker mysql5
            mysql = new ConfigEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AddressTemplate = ConnectionHelper.MySqlConnectionString,
                Name = ConfigConstant.Win7DockerMySql,
                Ip = LocalWin7DockerIp,
                CreateDate = DateTime.Now,
                Port = 3310,
                User = "root",
                Pwd = "wjp930514.",
                Status= ConfigStatus.Stop
            };
            Configs.Add(mysql);
        }
    }
}
