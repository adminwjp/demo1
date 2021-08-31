#if true
//#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482
using Utility.Application.Services.Dtos;
using Utility.Domain.Repositories;
using Utility.Domain.Entities;
using System.Linq;

namespace Utility.Wcf
{
    /// <summary>
    /// wcf 实体信息  契约接口
    /// (前提必须要有System.ServiceDto.ServiceContract注解),注意方法名不能相同 不然 
    /// 提示签名有多个这样的错误(要么在注解上改名称) 
    /// </summary>
    //[System.ServiceDto.ServiceBehavior]
    public class ServiceApi<RepositoryImpl,Entity,Key> : IServiceApi<Entity,Key>  
		where RepositoryImpl:IRepository<Entity,Key> 
		where Entity:class,IEntity<Key>
    {
        /// <summary>
        /// 
        /// </summary>
        protected RepositoryImpl  Repository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ServiceApi()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public ServiceApi(RepositoryImpl repository)
        {
            Repository = repository;
        }

        /// <summary>添加实体类信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="obj">实体类</param>
        /// <param name="language"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public virtual ResponseApi Insert(Entity obj,Language language= Language.Chinese)
        {
            Repository.Insert(obj);
            return ResponseApi.Create(language,Code.AddSuccess);
        }

        /// <summary>修改实体类信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="obj">实体类</param>
        /// <param name="language"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public virtual ResponseApi Update(Entity obj,Language language= Language.Chinese)
        {
            Repository.Update(obj);
            return ResponseApi.Create(language,Code.ModifySuccess);
        }

        /// <summary>根据id删除实体类信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="id">id</param>
        /// <param name="language"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public  virtual ResponseApi Delete(Key id,Language language= Language.Chinese)
        {
            Repository.Delete(id);
            return ResponseApi.Create(language,Code.DeleteSuccess);
        }

        /// <summary>根据id删除实体类信息(多删除) (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="ids">id</param>
        /// <param name="language"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual ResponseApi DeleteList(Key[] ids,Language language= Language.Chinese)
        {
            Repository.DeleteList(ids);
            return ResponseApi.Create(language,Code.DeleteSuccess);
        }

        /// <summary>根据条件查询实体类数据集信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="obj">实体类</param>
        /// <param name="language"></param>
        ///<return>返回实体类数据集信息 </return>
        public virtual ResponseApi<System.Collections.Generic.List<Entity>> FindList(Entity obj,Language language= Language.Chinese)
        {
            var res=Repository.FindListByEntity(obj).ToList();
            return ResponseApi<System.Collections.Generic.List<Entity>>.Create(language,Code.QuerySuccess).SetData(res);
        }

        /// <summary>根据条件查询实体类数据集数量信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="obj">实体类</param>
        /// <param name="language"></param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual ResponseApi<long> Count(Entity obj,Language language= Language.Chinese)
        {
            var res=Repository.CountByEntity(obj);
            return ResponseApi<long>.Create(language,Code.QuerySuccess).SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="language"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual ResponseApi<System.Collections.Generic.List<Entity>> FindListByPage(Entity obj,int page,int size,Language language= Language.Chinese)
        {
            var res=Repository.FindListByEntityAndPage(obj,page,size).ToList();
            return ResponseApi<System.Collections.Generic.List<Entity>>.Create(language,Code.QuerySuccess).SetData(res);
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息 (契约接口方法必须要有System.ServiceDto.OperationContract注解 支持web 必须要有 System.ServiceDto.Web.WebGet：httpget System.ServiceDto.Web.WebInvoke:httppost)</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="language"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResponseApi<ResultDto<Entity>> FindResultByPage(Entity obj,int page,int size,Language language= Language.Chinese)
        {
            var res=Repository.FindResultByEntityAndPage(obj,page,size);
            return ResponseApi<ResultDto<Entity>>.Create(language,Code.QuerySuccess).SetData(res);
        }

    }

}
#endif