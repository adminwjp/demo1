using Config.Application.Cache;
using Config.Application.Services;
using Config.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Application.Services.Dtos;
using Utility.AspNetCore.Controllers;
using Utility.Domain.Entities;

namespace Config.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : BaseController<ServiceResponseApiService, ServiceService, ServiceEntity, string>
    {
        public ServiceController(ServiceResponseApiService apiBLL,IConfiguration configuration)
        {
            this.ApiService = apiBLL; 
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        //ContactIsOwnerAuthorizationHandler ContactIsOwnerAuthorizationHandler;
        [HttpGet("get/{id}")]
        public dynamic Get(int id)
        {
            return id + Configuration.GetSection("ServiceWeb")["xx"];
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <param name="ip">服务ip</param>
        /// <param name="port">服务端口</param>
        [HttpGet("register")]
        public dynamic Register(string name, string ip, int port)
        {
            foreach (var item in ServiceHelper.GetServiceModels())
            {
                //服务 存在
                if (item.Name.Equals(name))
                {
                    if(item.Status!= ServiceStatus.OnLine)
                    {
                        ServiceHelper.OnLineService(name, ip, port);
                    }
                    return new
                    {
                        note = "成功!",
                        code = 200
                    };
                }
            }
            //不存在该服务 注册
            return ServiceHelper.AddService(name, ip, port) > 0 ?
                new
                {
                    note = "成功!",
                    code = 200
                }
                :
                new
                {
                    note = "失败!",
                    code = 400
                };
        }

        /// <summary>
        /// 下线服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        [HttpGet("offline/{name}")]
        public dynamic OffLine(string name)
        {
            foreach (var item in ServiceHelper.GetServiceModels())
            {
                //服务 存在
                if (item.Name.Equals(name))
                {
                    if (item.Status != ServiceStatus.OnLine)
                    {
                        ServiceHelper.OffLineService(name);
                    }
                    return new
                    {
                        note = "成功!",
                        code = 200
                    };
                }
            }
            return new
            {
                note = "系统内部错误!",
                code = 500
            };
        }

        [HttpPost("GetResultModelByPage")]
        public override async Task<ResponseApi<ResultDto<ServiceEntity>>> GetResultByPage([FromBody] ServiceEntity obj, int page, int size)
        {
            //共享作用域不存在 数据没有
            if (Request.ContentType != null)
            {
                //if (Request.ContentType.Contains("application/json"))
                //{
                //    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                //    {
                //        Ref(ref obj, reader.ReadToEnd());
                //    }
                //}
                //else 
                if (Request.ContentType.Contains("text/xml"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                    {
                        Type t = typeof(ServiceEntity);
                        XmlSerializer serializer = new XmlSerializer(t);
                        obj = serializer.Deserialize(reader) as ServiceEntity;
                    }
                }
            }
            var res = await ApiService.FindResultByPageAsync(obj, page, size, GetLanguage());
            return res;
        }
    }
}
