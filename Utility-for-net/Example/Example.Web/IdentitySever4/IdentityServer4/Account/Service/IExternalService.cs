using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Utility.IdentityServer4.Service
{

    public interface IExternalService
    {
        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        Task<ResponseApi> Callback();
    }
}
