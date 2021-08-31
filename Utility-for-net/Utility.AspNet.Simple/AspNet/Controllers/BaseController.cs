#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;
using Utility.IO;
using Utility.Json;

namespace Utility.AspNet.Controllers
{
    /// <summary>
    /// [FromForm,FromBody] 只支持 FromForm Multiple actions were found that match the request
    /// "Config/api/{controller}/{action}/{id}", 
    /// //webform mvc webapi 混合搭配时 值 接受不到 
    /// </summary>
    /// <typeparam name="ResponseApiBLL"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="BLL"></typeparam>
    [Route("api/[version]/[controller]")]
    
    public abstract class BaseController<ResponseApiService, Service, T, Key> : ApiController
       where ResponseApiService : ResponseApiService<Service, T, Key>
         where Service : CrudAppService<T, Key>, new()
         where T : class, IEntity<Key>
    {
        protected ResponseApiService ApiService { get; set; }
        protected virtual Language GetLanguage()
        {
            return Language.Chinese;
        }
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost()]
        [Route("InsertAsync")]
        public virtual Task<ResponseApi> InsertAsync([FromBody]T obj)
        {
            //共享作用域不存在 数据没有
            if (Request.Content.Headers.ContentType != null)
            {
                if (Request.Content.Headers.ContentType.MediaType.Contains("application/json"))
                {
                    Ref(ref obj, StreamHelper.GetString(Request.Content.ReadAsStreamAsync().Result));
                }
                else if (Request.Content.Headers.ContentType.MediaType.Contains("text/xml"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Content.ReadAsStreamAsync().Result))
                    {
                        Type t = typeof(T);
                        XmlSerializer serializer = new XmlSerializer(t);
                        obj = serializer.Deserialize(reader) as T;
                    }
                }
            }
            var res = ApiService.InsertAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [HttpPost]
        [Route("Insert")]
        public virtual ResponseApi Insert([FromBody] T obj)
        {
           // Ref(ref obj, StreamHelper.GetString(Request.Content.ReadAsStreamAsync().Result));//webform mvc webapi 混合搭配时 值 接受不到 
            var res = ApiService.Insert(obj, GetLanguage());
            return res;
        }


        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost]
        [Route("UpdateAsync")]
        public virtual Task<ResponseApi> UpdateAsync([FromBody] T obj)
        {
            var res = ApiService.UpdateAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [HttpPost]
        [Route("Update")]
        public virtual ResponseApi Update( [FromBody] T obj)
        {
           
            var res = ApiService.Update(obj, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet]
        [Route("DeleteAsync")]
        public virtual Task<ResponseApi> DeleteAsync(Key id)
        {
            var res = ApiService.DeleteAsync(id, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [HttpGet]
        [Route("Delete")]
        public virtual ResponseApi Delete(Key id)
        {
            var res = ApiService.Delete(id, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost]
        [Route("DeleteListAsync")]
        public virtual Task<ResponseApi> DeleteListAsync([ FromBody] DeleteEntity<Key> ids)
        {
            //共享作用域不存在 数据没有
            if (Request.Content.Headers.ContentType != null)
            {
                if (Request.Content.Headers.ContentType.MediaType.Contains("application/json"))
                {
                    Ref(ref ids, StreamHelper.GetString(Request.Content.ReadAsStreamAsync().Result));
                }
                else if (Request.Content.Headers.ContentType.MediaType.Contains("text/xml"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Content.ReadAsStreamAsync().Result))
                    {
                        Type t = typeof(DeleteEntity<Key>);
                        XmlSerializer serializer = new XmlSerializer(t);
                        ids = serializer.Deserialize(reader) as DeleteEntity<Key>;
                    }
                }
            }
            var res = ApiService.DeleteListAsync(ids.Ids, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [HttpPost]
        [Route("DeleteList")]
        public virtual ResponseApi DeleteList([FromBody] DeleteEntity<Key> ids)
        {
            
            var res = ApiService.DeleteList(ids.Ids, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost]
        [Route("FindListAsync")]
        public virtual Task<ResponseApi<List<T>>> FindListAsync([ FromBody] T obj)
        {
            var res = ApiService.FindListAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [HttpPost]
        [Route("FindList")]
        public virtual ResponseApi<List<T>> FindList([FromBody] T obj)
        {
            var res = ApiService.FindList(obj, GetLanguage());
            return res;
        }


        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost]
        [Route("CountAsync")]
        public virtual Task<ResponseApi<long>> CountAsync([FromBody] T obj)
        {
            var res = ApiService.CountAsync(obj, GetLanguage());
            return res;
        }
        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [HttpPost]
        [Route("Count")]
        public virtual ResponseApi<long> Count([FromBody] T obj)
        {
            var res = ApiService.Count(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="sortOrder"></param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost]
        [Route("FindListByPageAsync")]
        public virtual Task<ResponseApi<List<T>>> FindListByPageAsync([FromBody] T obj, int page, int size,string sortOrder)
        {
            var res = ApiService.FindListByPageAsync(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="sortOrder"></param>
        ///<return>返回实体类数据集信息</return>
        [HttpPost]
        [Route("FindListByPage")]
        public virtual ResponseApi<List<T>> FindListByPage([ FromBody] T obj, int page, int size, string sortOrder)
        {
            var res = ApiService.FindListByPage(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="sortOrder"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost]
        [Route("FindResultByPage")]
        public virtual ResponseApi<ResultDto<T>> FindResultByPage([FromBody] T obj, int page, int size, string sortOrder)
        {
            var res = ApiService.FindResultByPage(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="sortOrder"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [HttpPost]
        [Route("FindResultByPageAsync")]
        public virtual Task<ResponseApi<ResultDto<T>>> FindResultByPageAsync([FromBody] T obj, int page, int size, string sortOrder)
        {
            var res = ApiService.FindResultByPageAsync(obj, page, size, GetLanguage());
            return res;
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
    }


}
#endif