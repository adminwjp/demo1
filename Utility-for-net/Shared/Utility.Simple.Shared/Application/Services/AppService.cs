using Utility.Attributes;
using Utility.Domain.Services;

namespace Utility.Application.Services
{
    /// <summary>
    /// application service interface implement
    /// </summary>
    [Transtation]
    public  class AppService:DomainService,IAppService
    {
        /// <summary>
        /// no param application service  constractor 
        /// </summary>
        public AppService() :base(){ }
    }

}
