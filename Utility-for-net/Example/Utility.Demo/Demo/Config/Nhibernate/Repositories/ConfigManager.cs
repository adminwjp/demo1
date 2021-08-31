//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;
using Config.Domain.Repositories;
using Utility.Domain.Repositories;
using Utility.Nhibernate;

namespace Config.Nhibernate.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigManager : IConfigManager,System.IDisposable
    {
        private IRepository<ServiceEntity, string> _service;//服务  数据访问层
        private IRepository<ConfigEntity, string> _config;//服务  数据访问层
        private readonly SessionProvider _session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public ConfigManager(SessionProvider session)
        {
            this._session = session;
            //this.unitWork = new Utility.Nhibernate.NhibernateUnitWork(session);
        }
        /// <summary> 服务  数据访问层</summary>
        public IRepository<ServiceEntity, string> Service
        {
            get
            {
                if (_service == null)
                {
                    _service = new ServiceRepository(this._session);
                }
                return _service;
            }
        }

        /// <summary> 服务  数据访问层</summary>
        public IRepository<ConfigEntity, string> Config
        {
            get
            {
                if (_config == null)
                {
                    _config = new ConfigRepository(this._session);
                }
                return _config;
            }
        }
       

        /// <summary>
        /// 
        /// </summary>

        public void Dispose()
        {
            //_session.Close();
            //_session.Dispose();
        }
    }
}
#endif