//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Web;

namespace Tunynet.Common
{
    /// <summary>
    /// 用于IHttpHandler的RouteHandler
    /// </summary>
    public class HttpHandlerRouteHandler<THttpHandler> : IRouteHandler //where THttpHandler //: IHttpHandler
    {
        //private Func<RequestContext, THttpHandler> _handlerFactory = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="handlerFactory"></param>
        //public HttpHandlerRouteHandler(Func<RequestContext, THttpHandler> handlerFactory)
        //{
        //    _handlerFactory = handlerFactory;
        //}

        /// <summary>
        /// 获取IHttpHandler
        /// </summary>
        /// <param name="requestContext"><see cref="RequestContext"/></param>
        /// <returns></returns>
        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            return null;
            //return _handlerFactory(httpContext);
        }
    }
}