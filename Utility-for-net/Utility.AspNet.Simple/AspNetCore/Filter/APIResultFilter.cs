#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0  || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.AspNetCore.Extensions;

namespace Utility.AspNetCore.Filter
{
    public class APIResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult)
            {
                BadRequestObjectResult res = (BadRequestObjectResult)context.Result;
                SerializableError obj = res.Value as SerializableError;
                ResponseApi responseApi = ResponseApi.Create(Language.Chinese, Code.ParamError);
                if (obj != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in obj)
                    {
                        var vals = item.Value as string[];
                        if (vals != null)
                        {
                            sb.AppendLine(vals[0]);
                        }
                    }

                    responseApi.Data = obj.Errors();
                }
                else
                {
                    var obj1 = res.Value as ValidationProblemDetails;

                    responseApi.Data = obj1?.Errors;
                }
              
                context.Result = new JsonResult(responseApi) { StatusCode = 400 };
                return;
            }
        }
    }
}
#endif
