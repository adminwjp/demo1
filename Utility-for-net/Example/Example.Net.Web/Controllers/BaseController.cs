using System.Collections.Generic;
using System.Web.Http;
using Utility.Domain.Entities;
using Utility;
using Adverts;

namespace Example.Web.Controllers
{
    public abstract class BaseController<Service, Repository, CreateInput, UpdateInput, GetAllDto, RequestDto, Entity> : ApiController
        where Service: BaseAppService<Repository, CreateInput, UpdateInput, GetAllDto, RequestDto, Entity>
         where Repository : IBaseRepository<Entity>
        where CreateInput : class
        where UpdateInput : class
        where GetAllDto : class
        where RequestDto : class
        where Entity : BaseEntity
    {
        protected Service service;
        public BaseController(Service service)
        {
            this.service = service;
        }
        /// <summary>
        /// 添加 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseApi Insert([FromBody] CreateInput create)
        {
            int res = service.Insert(create);
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.AddSuccess : Code.AddFail);
        }
        /// <summary>
        /// 修改 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseApi Update([FromBody] UpdateInput update)
        {
            int res = service.Update(update);
            return ResponseApi.Create(Language.Chinese, res > 0 ? Code.ModifySuccess : Code.ModifyFail);
        }
        [HttpGet]
        public IResponseApi Delete(string id)
        {
            service.Delete(id);
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }
        [HttpPost]
        public IResponseApi DeleteList([FromBody] DeleteEntity<string> ids)
        {
            service.Delete(ids.Ids);
            return ResponseApi.Create(Language.Chinese, Code.DeleteSuccess);
        }
        [HttpPost]
        public ResponseApi<IList<GetAllDto>> Find([FromBody] RequestDto entity)
        {
            //Abp.WebApi.ExceptionHandling.AbpApiExceptionFilterAttribute pass 为何报错 
            var res = service.Find(entity);
            return ResponseApi<IList<GetAllDto>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        }
        [HttpPost]
        public ResponseApi<IList<GetAllDto>> FindByPage([FromBody] RequestDto entity, int page = 1, int size = 10)
        {
            var res = service.FindByPage(entity, page, size);
            return ResponseApi<IList<GetAllDto>>.Create(Language.Chinese, Code.QuerySuccess).SetData(res);
        }
        [HttpPost]
        public int Count([FromBody] RequestDto entity)
        {
            int res = service.Count(entity);
            return res;
        }
    }
}
