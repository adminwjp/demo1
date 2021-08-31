//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;
using Config.Domain.Repositories;
using Utility.Domain.Repositories;
using Utility.Domain.Uow;
using Utility.Ef;
using Utility.Ef.Uow;

namespace Config.Ef.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager : IConfigManager,System.IDisposable
    {
        private IRepository<ServiceEntity, string> _service;//服务  数据访问层
        private IRepository<ConfigEntity, string> _config;//配置  数据访问层
        private readonly DbContextProvider<ConfigDbContext> _dbContext;//数据库上下文
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public ConfigManager(DbContextProvider<ConfigDbContext> dbContext)
        {
            this._dbContext = dbContext;
        }
        /// <summary> 服务  数据访问层</summary>
        public IRepository<ServiceEntity, string> Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new ServiceRepository(_dbContext);
                }
                return _service;
            }
        }
        /// <summary> 配置  数据访问层</summary>
        public IRepository<ConfigEntity, string> Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigRepository(_dbContext);
                }
                return _config;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //this._dbContext.Dispose();
        }
    }

}
#endif