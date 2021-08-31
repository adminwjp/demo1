using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Utility.Redis;
using Utility.Response;
using Utility.Enums;
using Utility.Json;
using Utility.Base64;
using Utility.Page;
using Utility.Randoms;
using Utility.Security.Extensions;
using SocialContact.Domain.Repositories;
using Utility.Domain.Entities;
using SocialContact.Manager;
using SocialContact.Application.Services;

namespace SocialContact.Manager
{
    /// <summary>
    /// 后台基类 [FromForm,FromBody]  FromForm模型绑定优先级最高  绑定失败 ,[FromBody] 可以省略 手动绑定,FromForm 必须存在 不然 FromForm请求通过不了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="G"></typeparam>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    public class BaseController<Service, Repsotiory, Entity, CreateInput, UpdateInput, Input, Output> : ControllerBase
        where Service : BaseAppService<Repsotiory, Entity, CreateInput, UpdateInput, Input, Output>
        where Repsotiory : IRepository<Entity>
        where Entity : SocialContact.Domain.Entities.Entity
        where CreateInput : class
        where UpdateInput : class
        where Input : class
        where Output : class
    {
        protected Service service;
        protected IRedisCache cache;

        public BaseController(Service service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public virtual ResponseApi Add([FromForm, FromBody] CreateInput create)
        {
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref obj, reader.ReadToEnd());
                    Ref(ref create, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(CreateInput);
                XmlSerializer serializer = new XmlSerializer(t);
                create = serializer.Deserialize(reader) as CreateInput;
            }
            //this.ActionParam(HttpContext.Request,ref obj);//无效 作用域可能 绑定模型失败
            //ActionParam(HttpContext.Request,ref obj);
            service.Insert(create);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.AddSuccess);
            return response;
        }
        protected Language GetLanguage()
        {
            return Language.Chinese;
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public virtual ResponseApi Edit([FromForm, FromBody] UpdateInput update)
        {
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref obj, reader.ReadToEnd());
                    Ref(ref update, reader.ReadToEndAsync().GetAwaiter().GetResult());//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(UpdateInput);
                XmlSerializer serializer = new XmlSerializer(t);
                update = serializer.Deserialize(reader) as UpdateInput;
            }
            //this.ActionParam(HttpContext.Request,ref obj);//无效 作用域可能 绑定模型失败
            //ActionParam(HttpContext.Request,ref obj);
            service.Update(update);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ModifySuccess);
            return response;
        }


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delete/{id}")]
        public virtual ResponseApi Delete(int? id)
        {
            if (!id.HasValue)
            {
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ParamNotNull, false);
                response.Message = $"id {response.Message}";
                return response;
            }
            else
            {
                service.Delete(id.Value);
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.DeleteSuccess);
                return response;
            }
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPost("query")]
        public virtual ResponseApi Query([FromForm, FromBody] Input query, int? page, int? size)
        {
            PageHelper.Set(ref page, ref size);
            //Request.EnableBuffering();
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref query, reader.ReadToEnd());
                    Ref(ref query, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(Input);
                XmlSerializer serializer = new XmlSerializer(t);
                query = serializer.Deserialize(reader) as Input;
            }
            //ActionParam(HttpContext.Request,ref query);//无效 作用域可能 绑定模型失败
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = service.FindResultByPage(query, page.Value, size.Value);
            return response;
        }


        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public virtual ResponseApi Delete([/*Bind("ids"),*/ FromForm, FromBody] DeleteEntity<long> deleteEntity)
        {
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref deleteEntry, reader.ReadToEnd());
                    Ref(ref deleteEntity, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(DeleteEntity<int>);
                XmlSerializer serializer = new XmlSerializer(t);
                deleteEntity = serializer.Deserialize(reader) as DeleteEntity<long>;
            }
            //this.ActionParam(HttpContext.Request,ref deleteEntry);//无效 作用域可能 绑定模型失败
            if (!deleteEntity.Ids.Any())
            {
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ParamNotNull, false);
                response.Message = $"id {response.Message}";
                return response;
            }
            else
            {
                service.DeleteList(deleteEntity.Ids);
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.DeleteSuccess);
                return response;
            }
        }
        protected virtual void ActionParam<M>(HttpRequest request, ref M obj) where M : class
        {
            //启用 否则出现异常  除非卸载方法体类 作用域不同导致 Synchronous operations are disallowed. Call ReadAsync or set AllowSynchronousIO to true instead

            if (request.ContentType.Contains("application/json"))
            {
                //Request.EnableBuffering();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(request.Body))
                {
                    Ref(ref obj, reader.ReadToEndAsync().Result);
                }
            }
            else if (request.ContentType.Contains("text/xml"))
            {
                //Request.EnableBuffering();
                using System.IO.StreamReader reader = new System.IO.StreamReader(request.Body);
                Type t = typeof(M);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as M;
            }
        }


        /// <summary>
        /// formform 数据转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str"></param>
        protected void Ref<M>(ref M obj, string str) where M : class
        {
            obj = JsonHelper.ToObject<M>(str, JsonHelper.JsonSerializerSettings);
        }

        [HttpGet("category")]

        public virtual ResponseApi Category()
        {
            return null;
        }

        [HttpGet("operator")]

        public virtual async Task<ResponseApi> Operator()
        {
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            //response.Data = AuthrizeValidator.GetOperatorAuthrize(GetUserId(), this.PageName);
            return await Task.FromResult(response);
        }

    }
}
