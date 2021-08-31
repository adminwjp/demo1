using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Web.Middlewares
{
    public class HtmlMiddleware
    {
        private readonly RequestDelegate _next;

        public HtmlMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("RequestSetOptionsMiddleware.Invoke");

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
