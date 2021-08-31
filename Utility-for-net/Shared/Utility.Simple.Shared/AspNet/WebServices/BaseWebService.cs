#if NET35 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET472 || NET48
#if !NET35
using System.Threading.Tasks;
#endif
using System.Web.Services;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Domain.Entities;

namespace Utility.AspNet.WebServices
{
    /// <summary>
    /// BaseService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]

    public abstract class BaseWebService<ResponseApiServiceImpl, ServiceImpl, T, Key> : System.Web.Services.WebService
         where ResponseApiServiceImpl : IResponseApiService<T, Key>
        where ServiceImpl : CrudAppService<T, Key>
        where T : class, IEntity<Key>
    {
        /// <summary>
        /// 
        /// </summary>
        protected ResponseApiServiceImpl ApiService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual Language GetLanguage()
        {
            return Language.Chinese;
        }
        

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [WebMethod()]
        public virtual ResponseApi Insert(T obj)
        {
            var res = ApiService.Insert(obj, GetLanguage());
            return res;
        }



        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [WebMethod()]
        public virtual ResponseApi Update(T obj)
        {

            var res = ApiService.Update(obj, GetLanguage());
            return res;
        }

       

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [WebMethod()]
        public virtual ResponseApi Delete(Key id)
        {
            var res = ApiService.Delete(id, GetLanguage());
            return res;
        }
       

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [WebMethod()]
        public virtual ResponseApi DeleteList(DeleteEntity<Key> ids)
        {

            var res = ApiService.DeleteList(ids.Ids, GetLanguage());
            return res;
        }

      

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [WebMethod()]
        public virtual  ResponseApi<System.Collections.Generic.List<T>> FindList( T obj)
        {
            var res = ApiService.FindList(obj, GetLanguage());
            return res;
        }


     
        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [WebMethod()]
        public virtual ResponseApi<long> Count( T obj)
        {
            var res = ApiService.Count(obj, GetLanguage());
            return res;
        }


        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [WebMethod()]
        public  virtual ResponseApi<System.Collections.Generic.List<T>> FindListByPage(T obj, int page, int size)
        {
            var res = ApiService.FindListByPage(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [WebMethod()]
        public virtual ResponseApi<ResultDto<T>> FindResultModelByPage(T obj, int page, int size)
        {
            var res = ApiService.FindResultByPage(obj, page, size, GetLanguage());
            return res;
        }
#if !NET35

        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        [WebMethod()]
        public virtual Task<ResponseApi> InsertAsync(T obj)
        {
            var res = ApiService.InsertAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        [WebMethod()]
        public virtual System.Threading.Tasks.Task<ResponseApi> UpdateAsync(T obj)
        {
            var res = ApiService.UpdateAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        [WebMethod()]
        public virtual System.Threading.Tasks.Task<ResponseApi> DeleteAsync(Key id)
        {
            var res = ApiService.DeleteAsync(id, GetLanguage());
            return res;
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        [WebMethod()]
        public virtual System.Threading.Tasks.Task<ResponseApi> DeleteListAsync(DeleteEntity<Key> ids)
        {
            var res = ApiService.DeleteListAsync(ids.Ids, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        [WebMethod()]
        public virtual System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<T>>> FindListAsync(T obj)
        {
            var res = ApiService.FindListAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        [WebMethod()]
        public  virtual System.Threading.Tasks.Task<ResponseApi<long>> CountAsync(T obj)
        {
            var res = ApiService.CountAsync(obj, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        [WebMethod()]
        public virtual System.Threading.Tasks.Task<ResponseApi<System.Collections.Generic.List<T>>> FindListByPageAsync(T obj, int page, int size)
        {
            var res = ApiService.FindListByPageAsync(obj, page, size, GetLanguage());
            return res;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        [WebMethod()]
        public virtual System.Threading.Tasks.Task<ResponseApi<ResultDto<T>>> FindResultModelByPageAsync(T obj, int page, int size)
        {
            var res = ApiService.FindResultByPageAsync(obj, page, size, GetLanguage());
            return res;
        }

#endif
    }
}
#endif