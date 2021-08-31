#if  NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using Config.Domain.Repositories;
using Utility.Domain.Repositories;

namespace Config.Wcf.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager : IConfigManager,System.IDisposable
    {
        private IRepository<ServiceEntity, string> _service ;//服务  数据访问层
        private IRepository<ConfigEntity, string> _config;// 配置 数据访问层

        /// <summary>
        /// 
        /// </summary>
        public ConfigManager()
        {
        }
        /// <summary> DAL 服务 数据访问层</summary>
        public IRepository<ServiceEntity, string> Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new ServiceRepository();
                }
                return _service;
            }
        }

        /// <summary> DAL 配置 数据访问层</summary>
        public IRepository<ConfigEntity, string> Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigRepository();
                }
                return _config;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
#endif
