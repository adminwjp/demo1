//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )

using Config.Dapper.Repositories;
using Config.Domain.Entities;
using Config.Domain.Repositories;
using System.Data.Common;
using Utility.Dapper;
using Utility.Domain.Repositories;

namespace Config.Dapper.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager : IConfigManager,System.IDisposable
    {
        private IRepository<ServiceEntity,string> _service;//服务
        private IRepository<ConfigEntity, string> _config;//配置
        DapperConnectionProvider connectionProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public ConfigManager(System.Data.IDbConnection connection)
        {
            connectionProvider = new DapperConnectionProvider((DbConnection)connection);
        }

        /// <summary>
        /// 服务 Dapper 数据访问层
        /// </summary>
        public IRepository<ServiceEntity, string> Service
        {            get
            {
                if (_service == null)
                {
                    this._service = new ServiceRepository(connectionProvider);
                }
               return _service;
            }
        }
        /// <summary>
        /// 配置 Dapper 数据访问层
        /// </summary>
        public IRepository<ConfigEntity, string> Config
        {            get
            {
                if (_config == null)
                {
                    this._config = new ConfigRepository(connectionProvider);
                }
               return _config;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //this._connection.Close();
            //this._connection.Dispose();
        }
    }

}
#endif