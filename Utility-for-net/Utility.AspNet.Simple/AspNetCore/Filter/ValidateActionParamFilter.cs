#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.AspNetCore.Extensions;
using System.Xml.Serialization;
using Utility.Helpers;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Utility.Json;
using Utility.Attributes;
using Utility.Domain;
using Microsoft.Extensions.Logging;
using Utility.AspNetCore.Controllers;
using System.Threading.Tasks;

namespace Utility.AspNetCore.Filter
{
    public class ResultFilter : IResultFilter//,IAsyncResultFilter
    {
        protected ILogger<ResultFilter> Logger;

        public ResultFilter(ILogger<ResultFilter> logger)
        {
            Logger = logger;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //return;
            //提交事务简单 实现 要么 手动实现
            Execute(context);


        }
        void Execute(ResultExecutedContext context)
        {
            var ctrl = context.Controller as BaseController;
            var t = AttributeHelper.Get<TranstationAttribute>(context.Controller.GetType().GetCustomAttributes(false));
            if (t==null)
            {
                return;
            }
           
            //action method 
            //else if (AttributeHelper.Exists<UnitWorkAttribute>(context.ActionDescriptor.ActionConstraints.GetType().GetCustomAttributes(false)))
            {

            }
            var read = true;
            var action = context.RouteData.Values["action"].ToString().ToLower();
            foreach (var item in ValidateActionParamFilter.actionNames)
            {
                if (action.Contains(item))
                {
                    read = false;
                    break;
                }
            }
            if (!t.UseTranstation)
            {
                var unitWork = ctrl.Service?.UnitWork ?? ctrl.UnitWork;
                if(unitWork != null)
                {
                    return;
                }
                if (!unitWork.UseTransaction)
                {
                    unitWork.UseTransaction = true;
                }
                if (!read)
                    unitWork.Save();
                return;
            }
            Commit(ctrl,read);
            return;
            DomainHelper.Execute(context.Controller, read, false);
            Logger.LogInformation("result fitler UnitWorkAttribute");
        }
        void Commit(BaseController ctrl,bool read)
        {
            if (ctrl != null)
            {
                var unitwork = ctrl.Service?.UnitWork ?? ctrl.UnitWork;
                if (unitwork == null)
                {
                    Logger.LogWarning("unitwork is null");
                    return;
                }
                if (read)
                {
                    unitwork.ReadTransaction.Commit();
                    unitwork.ReadTransaction.TaskCount = 0;
                }
                else
                {
                    unitwork.WriteTransaction.Commit();
                    unitwork.WriteTransaction.TaskCount = 0;
                }
            }
        }
        public void OnResultExecuting(ResultExecutingContext context)
        {
          
        }
        void Execute(ResultExecutingContext context)
        {
            var ctrl = context.Controller as BaseController;
            var t = AttributeHelper.Get<TranstationAttribute>(context.Controller.GetType().GetCustomAttributes(false));
            if (t == null)
            {
                return;
            }

            //action method 
            //else if (AttributeHelper.Exists<UnitWorkAttribute>(context.ActionDescriptor.ActionConstraints.GetType().GetCustomAttributes(false)))
            {

            }
            var read = true;
            var action = context.RouteData.Values["action"].ToString().ToLower();
            foreach (var item in ValidateActionParamFilter.actionNames)
            {
                if (action.Contains(item))
                {
                    read = false;
                    break;
                }
            }
            if (!t.UseTranstation)
            {
                var unitWork = ctrl.Service?.UnitWork ?? ctrl.UnitWork;
                if (unitWork == null)
                {
                    return;
                }
                if (!unitWork.UseTransaction)
                {
                    unitWork.UseTransaction = true;
                }
                if (!read)
                    unitWork.Save();
                return;
            }
            Commit(ctrl, read);
            return;
            DomainHelper.Execute(context.Controller, read, false);
            Logger.LogInformation("result fitler UnitWorkAttribute");
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Execute(context);
            await next();
           // Execute(context);
           
        }
    }
    public class ValidateActionParamFilter : IActionFilter//,IAsyncActionFilter
    {
        protected ILogger<ValidateActionParamFilter> Logger;

        public ValidateActionParamFilter(ILogger<ValidateActionParamFilter> logger)
        {
            Logger = logger;
        }
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
          
        }
       
        protected virtual  void ActionExecut(ActionExecutingContext context)
        {
            var Request = context.HttpContext.Request;
            
            if (Request.Method.ToLower() == "post")
            {
                object obj = null;
                Type type = null;
                ParameterDescriptor parameterDescriptor = null;
                for (int i = 0; i < context.ActionDescriptor.Parameters.Count; i++)
                {
                    parameterDescriptor = context.ActionDescriptor.Parameters[i];
                    if (!TypeHelper.IsCommonType(parameterDescriptor.ParameterType)
                        &&(
                        AttributeHelper.Exists<FromFormAttribute>(parameterDescriptor.ParameterType.GetCustomAttributes(false))
                        ||
                        AttributeHelper.Exists<FromBodyAttribute>(parameterDescriptor.ParameterType.GetCustomAttributes(false))
                        ))
                    {
                        type = parameterDescriptor.ParameterType;
                        obj = context.ActionArguments[parameterDescriptor.Name];
                        break;
                    }
                }
                if (type != null)
                {
                    if (Request.ContentType != null)
                    {
                        if (Request.ContentType.Contains("form")
                            //&&!Request.ContentType.Contains("data")
                            )
                        {
                            try
                            {
                                if (context.Controller is ControllerBase controller)
                                {
                                    obj = JsonHelper.ToObject(Request.Form[parameterDescriptor.Name].ToString(), type, JsonHelper.JsonSerializerSettings);
                                    context.ActionArguments[parameterDescriptor.Name] = obj;
                                   // controller.TryUpdateModelAsync(obj, type, null);
                                }
                                else if (context.Controller is Controller controller1)
                                {
                                    //controller1.TryUpdateModelAsync(obj, type, null);
                                }
                            }
                            catch (Exception)
                            {
    
                            }
                        }
                         else   if (Request.ContentType.Contains("application/json"))
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                            {
                                obj = JsonHelper.ToObject(reader.ReadToEndAsync().Result,type, JsonHelper.JsonSerializerSettings);
                                //Ref(ref obj, reader.ReadToEnd());
                            }
                            context.ActionArguments[parameterDescriptor.Name] = obj;
                        }
                        else if (Request.ContentType.Contains("text/xml"))
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                            {
                                XmlSerializer serializer = new XmlSerializer(type);
                                obj = serializer.Deserialize(reader);
                            }
                            context.ActionArguments[parameterDescriptor.Name] = obj;
                        }
                    }
                }
            }
        }
        public static readonly List<string> actionNames = new List<string>(10) { 
        "insert","add","update","modify","edit","remove","delete"
        };
        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            ActionExecut(context);//绑定数据
            //asp.net core api  需要手动调用
            if (!context.ModelState.IsValid)
            {
                ResponseApi responseApi = ResponseApi.Create(Language.Chinese, Code.ParamError);
                responseApi.Data = context.ModelState.Errors();
                context.Result = new JsonResult(responseApi) { StatusCode = 400 };
            }
            //提交事务简单 实现 要么 手动实现
            // return;
            Execute(context);


        }
        void Execute(ActionExecutingContext context)
        {
            var ctrl = context.Controller as BaseController;
            if (ctrl == null) { return; }
            if (!AttributeHelper.Exists<TranstationAttribute>(context.Controller.GetType().GetCustomAttributes(false)))
            {
                return;
            }
            //action method 
            //else if (AttributeHelper.Exists<UnitWorkAttribute>(context.ActionDescriptor.ActionConstraints.GetType().GetCustomAttributes(false)))
            {

            }
            var read = true;
            var action = context.RouteData.Values["action"].ToString().ToLower();
            foreach (var item in actionNames)
            {
                if (action.Contains(item))
                {
                    read = false;
                    break;
                }
            }
            if (ctrl != null)
            {
                var unitwork = ctrl.Service?.UnitWork ?? ctrl.UnitWork;
               if (unitwork == null)
                {
                    Logger.LogWarning("unitwork is null");
                    return;
                }
               //用时 提交 事务 没用到 则 不做任何操作
                if (read)
                {
                    //unitwork.ReadTranstion.TaskCommit = true;
                    unitwork.ReadTransaction.Begin();
                    unitwork.ReadTransaction.TaskCount++;
                }
                else
                {
                   // unitwork.ReadTranstion.TaskCommit = true;
                    unitwork.WriteTransaction.Begin();
                    unitwork.WriteTransaction.TaskCount++;
                }
            }
            return;
            DomainHelper.Execute(context.Controller, read, true);
            Logger.LogInformation("action fitler UnitWorkAttribute");
        }
        /// <summary>
        /// formbody 数据转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str"></param>
        protected virtual void Ref<M>(ref M obj, string str) where M : class
        {
            obj = JsonHelper.ToObject<M>(str, JsonHelper.JsonSerializerSettings);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            Execute(context);
        }
    }
}
#endif