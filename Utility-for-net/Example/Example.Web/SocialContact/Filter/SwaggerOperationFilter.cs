using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SocialContact.Filter
{
    /// <summary>
    /// 使用过滤器单独对某些API接口实施认证
    /// </summary>
    public class SwaggerOperationFilter : IOperationFilter,IDocumentFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controller = context.ApiDescription.ActionDescriptor.RouteValues["controller"];
            var action = context.ApiDescription.ActionDescriptor.RouteValues["action"];
            
            if (controller.Equals("weatherforecast"))
            {
                return;
            }
            if (((controller.Equals("admin") || controller.Equals("user")) && action.Equals("login")) ||
                controller.Equals("img") || (controller.Equals("file") && action.Equals("get")))
            {
                return;
            }
            operation.Parameters ??= new List<OpenApiParameter>();
            var info = context.MethodInfo;
            context.ApiDescription.TryGetMethodInfo(out info);
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                @In = ParameterLocation.Header,
                Description = "token",
                Required = true
            });
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            
        }
    }
}
