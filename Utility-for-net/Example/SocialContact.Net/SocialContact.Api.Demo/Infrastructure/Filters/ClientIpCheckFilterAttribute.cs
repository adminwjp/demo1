using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Infrastructure.Filters
{
    /// <summary>
    /// 检测 客户端 非法 访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ClientIpCheckFilterAttribute : ActionFilterAttribute
    {
    }
}
