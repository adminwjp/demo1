#if NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif
using System.Threading;
using System.Collections.Generic;
using System;
using Utility.Json;
using Utility.Mappers;
using Utility.Domain.Repositories;
using Utility.Application.Services.Dtos;
using System.Linq;
using Utility.Domain.Entities;

namespace Utility.Remote
{
    /// <summary>
    /// remote  基类
    /// </summary>
	/// <typeparam name="RepositoryImpl"></typeparam>
	/// <typeparam name="ResultDto"></typeparam>
	/// <typeparam name="ListDto"></typeparam>
	/// <typeparam name="Entity">remote 模型</typeparam>
    /// <typeparam name="EntityDto">模型</typeparam>
    /// <typeparam name="Key"></typeparam>
    public class Object<RepositoryImpl, ResultDto, ListDto, Entity, EntityDto, Key> : System.MarshalByRefObject, IObject<ResultDto, ListDto, Entity, Key> 
		where RepositoryImpl : IRepository<EntityDto, Key>
		where ResultDto : Utility.Application.Services.Dtos.ResultDto<Entity>, new()
        where ListDto : System.Collections.Generic.List<Entity>,new()
		where Entity : class
        where EntityDto : class,IEntity<Key>
    {
        /// <summary>
        /// 
        /// </summary>
        protected RepositoryImpl Repository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected IMapper ObjectMapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Object()
        {
            //注意只能无参构造函数 传参数不支持怎么传 支持注册类型 这玩意服务器端怎么绑定了(比较麻烦参数结果都需要序列化 引用类型都需要指出序列化)
            //手动调用
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public Object(RepositoryImpl repository)
        {
            Repository = repository;
        }
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        public virtual int Insert(Entity obj)
        {
            var entity=ObjectMapper.Map<Entity, EntityDto>(obj);
            return Repository.Insert(entity);
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        public virtual int Update(Entity obj)
        {
            var entity = ObjectMapper.Map<Entity, EntityDto>(obj);
            return Repository.Update(entity);
        }

        /// <summary>根据id删除实体类信息 </summary>
        /// <param name="id">id</param>
        public virtual int Delete(Key id)
        {
            return Repository.Delete(id);
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        public virtual int DeleteList(Key[] ids)
        {
            return Repository.DeleteList(ids);
        }

        /// <summary>根据条件查询实体类数据集信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        public virtual ListDto FindList(Entity obj)
        {
            var entity = ObjectMapper.Map<Entity, EntityDto>(obj);
            var res= Repository.FindListByEntity(entity).ToList();
            var data=ObjectMapper.Map <System.Collections.Generic.List<EntityDto> , ListDto>(res);
            return data;
        }

        /// <summary>根据条件查询实体类数据集数量信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual long Count(Entity obj)
        {
            var entity = ObjectMapper.Map<Entity, EntityDto>(obj);
            return Repository.CountByEntity(entity);
        }

        /// <summary>根据条件及分页查询实体类数据集信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        public virtual ListDto FindListByPage(Entity obj, int page, int size)
        {
            var entity = ObjectMapper.Map<Entity, EntityDto>(obj);
            var res = Repository.FindListByEntityAndPage(entity,page,size).ToList();
            var data = ObjectMapper.Map<System.Collections.Generic.List<EntityDto>, ListDto>(res);
            return data;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual ResultDto FindResultByPage(Entity obj, int page, int size)
        {
            var entity = ObjectMapper.Map<Entity, EntityDto>(obj);
            var data = Repository.FindResultByEntityAndPage(entity, page, size);
            var res = ObjectMapper.Map<ResultDto<EntityDto>, ResultDto>(data);
            return res;
        }
    }


    /// <summary>
    /// Remote 实现
    /// </summary>
    /// <typeparam name="RepositoryImpl">基于 <see cref="IRepository{Model, Key}"/> 实现</typeparam>
    /// <typeparam name="EntityDto">实体模型</typeparam>
    /// <typeparam name="Key">主键类型</typeparam>
    public class Object<RepositoryImpl,EntityDto, Key> : System.MarshalByRefObject,IObject
		where RepositoryImpl : IRepository<EntityDto, Key>
		where EntityDto : class,IEntity<Key>
    {
        /// <summary>
        /// 基于 <see cref="IRepository{Model, Key}"/> 实现
        /// </summary>
        protected RepositoryImpl Repository { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected IMapper ObjectMapper { get; set; }
        /// <summary>
        /// Remote 实现
        /// </summary>
        public Object()
        {
            //注意只能无参构造函数 传参数不支持怎么传 支持注册类型 这玩意服务器端怎么绑定了(比较麻烦参数结果都需要序列化 引用类型都需要指出序列化)
            //手动调用
        }

        /// <summary>
        /// Remote 实现
        /// </summary>
        /// <param name="repository">基于 <see cref="IRepository{Model, Key}"/> 实现</param>
        public Object(RepositoryImpl repository)
        {
            Repository = repository;
        }
        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public virtual int Insert(string obj)
        {
            var entity = JsonHelper.ToObject<EntityDto>(obj);
            return Repository.Insert(entity);
        }

        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public virtual int Update(string obj)
        {
            var entity = JsonHelper.ToObject<EntityDto>(obj);
            return Repository.Update(entity);
        }

        /// <summary>根据id删除实体类信息 </summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete(string id)
        {
            return Repository.Delete((Key)Convert.ChangeType(id,typeof(Key)));
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList(string ids)
        {
            var entity = JsonHelper.ToObject<Key[]>(ids);
            return Repository.DeleteList(entity);
        }

        /// <summary>根据条件查询实体类数据集信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        public virtual string FindList(string obj)
        {
            var entity = JsonHelper.ToObject<EntityDto>(obj);
            var data = Repository.FindListByEntity(entity).ToList();
            var json =JsonHelper.ToJson(data);
            return json;
        }

        /// <summary>根据条件查询实体类数据集数量信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        public virtual long Count(string obj)
        {
            var entity = JsonHelper.ToObject<EntityDto>(obj);
            return Repository.CountByEntity(entity);
        }

        /// <summary>根据条件及分页查询实体类数据集信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        public virtual string FindListByPage(string obj, int page, int size)
        {
            var entity = JsonHelper.ToObject<EntityDto>(obj);
            var data = Repository.FindListByEntityAndPage(entity,page,size).ToList();
            var json = JsonHelper.ToJson(data);
            return json;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息 不支持 会出现异常 序列化失败</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public  virtual string FindResultByPage(string obj, int page, int size)
        {
            var entity = JsonHelper.ToObject<EntityDto>(obj);
            var data = Repository.FindResultByEntityAndPage(entity,page,size);
            var json = JsonHelper.ToJson(data);
            return json;
        }
    }
	
	/// <summary>
    /// remote Repository 基类
    /// </summary>
	/// <typeparam name="Object">remote 基类实现</typeparam>
	/// <typeparam name="T"> 模型</typeparam>
	/// <typeparam name="Key">主键类型</typeparam>
    public class RemoteRepository<Object, T, Key>:BaseRepository<T>, IRepository<T, Key> 
		where Object:IObject,new()
		where T : class,IEntity<Key>
       
    {
        /// <summary>
        /// 
        /// </summary>
        protected IMapper ObjectMapper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Object @object;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public RemoteRepository(string url)
        {
            System.Runtime.Remoting.RemotingConfiguration.RegisterActivatedClientType(typeof(Object), url);//http 不支持 TCP://localhost:20001/test
            @object = new Object();
        }


        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public override int Insert(T obj)
        {
            return @object.Insert(JsonHelper.ToJson(obj));
        }

         /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public override int Update(T obj)
        {
            return @object.Update(JsonHelper.ToJson(obj));
        }

         /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete(Key id)
        {
            return @object.Delete(id.ToString());
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList(Key[] ids)
        {
            return @object.DeleteList(JsonHelper.ToJson(ids));
        }

         /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        public override List<T> FindListByEntity(T obj)
        {
            var res= @object.FindList(JsonHelper.ToJson(obj));
            var data = JsonHelper.ToObject< List<T>>(res);
            return data;
        }

          /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        public override long CountByEntity(T obj)
        {
            return @object.Count(JsonHelper.ToJson(obj));
        }


        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        public override List<T> FindListByEntityAndPage(T obj, int page, int size)
        {
            var res = @object.FindListByPage(JsonHelper.ToJson(obj),page,size);
            var data = JsonHelper.ToObject<List<T>>(res);
            return data;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public override ResultDto<T> FindResultByEntityAndPage(T obj, int page, int size)
        {
            var res = @object.FindResultByPage(JsonHelper.ToJson(obj), page, size);
            var data = JsonHelper.ToObject<ResultDto<T>>(res);
            return data;
        }


#if !(NET20 || NET30 || NET35)

        /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual Task<int> DeleteAsync(Key id, CancellationToken cancellationToken = default)
        {
            return new Task<int>(() => Delete(id));
        }


        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual Task<int> DeleteListAsync(Key[] ids,CancellationToken cancellationToken = default)
        {
            return new Task<int>(() => DeleteList(ids));
        }





#endif
        }
}
#endif

