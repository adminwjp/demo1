//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;
using Utility.Application.Services;

namespace Config.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public  class ConfigResponseApiService: ResponseApiService<ConfigService,ConfigEntity, string>
    {

        //public ConfigResponseApiBLL(Config.DAL.IConfigManager manager):base(new ConfigBLL(manager.ConfigDAL))
        //{
        //}

        public ConfigResponseApiService(ConfigService bLL) : base(bLL)
        {

        }
    }
}
#endif
