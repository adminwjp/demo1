#if NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Collections.Generic;
using System.Threading;
#if !(NET20 || NET30 || NET35)
using System.Threading.Tasks;
#endif
using Utility.Mappers;
using Utility.Application.Services.Dtos;
using Utility.Domain.Repositories;
using Utility.Domain.Entities;

namespace Utility.Remote.Repositories
{
    /// <summary>
    /// remote dal 基类
    /// </summary>
	/// <typeparam name="Object">remote 基类实现</typeparam>
	/// <typeparam name="ResultDto"></typeparam>
	/// <typeparam name="ListDto"></typeparam>
	/// <typeparam name="T">模型</typeparam>
	/// <typeparam name="TDto">remote 模型</typeparam>
	/// <typeparam name="Key"></typeparam>
    public class RemoteRepository<Object,ResultDto,ListDto,T,TDto, Key>:BaseRepository<T>, IRepository<T, Key> 
		where Object:IObject<ResultDto, ListDto, TDto, Key >//,new()
		where ResultDto : ResultDto<TDto>, new()
        where ListDto:List<TDto>,new()
		where T : class,IEntity<Key>
		where TDto : class
       
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
            @object = (Object)System.Activator.GetObject(typeof(Object), url) ;
        }


        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public override int Insert(T obj)
        {
            var Dto = ObjectMapper.Map<T, TDto>(obj);
           return @object.Insert(Dto);
        }

         /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public override int Update(T obj)
        {
            var Dto = ObjectMapper.Map<T, TDto>(obj);
            return @object.Update(Dto);
        }

         /// <summary>根据id删除实体类信息</summary>
        /// <param name="id">id</param>
        ///<return>返回删除结果,大于0 返回删除成功,小于等于0 返回删除失败 </return>
        public virtual int Delete(Key id)
        {
            return @object.Delete(id);
        }

        /// <summary>根据id删除实体类信息(多删除)</summary>
        /// <param name="ids">id</param>
        ///<return>返回删除结果(多删除),大于0 返回删除成功(多删除),小于等于0 返回删除失败(多删除) </return>
        public virtual int DeleteList(Key[] ids)
        {
            return @object.DeleteList(ids);
        }

         /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集信息 </return>
        public override List<T> FindListByEntity(T obj)
        {
            var Dto = ObjectMapper.Map<T, TDto>(obj);
            var res= @object.FindList(Dto);
            var data = ObjectMapper.Map<ListDto, List<T>>(res);
            return data;
        }

          /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        ///<return>返回实体类数据集数量信息</return>
        public override long CountByEntity(T obj)
        {
            var Dto = ObjectMapper.Map<T, TDto>(obj);
            return @object.Count(Dto);
        }


        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息</return>
        public override List<T> FindListByEntityAndPage(T obj, int page, int size)
        {
            var Dto = ObjectMapper.Map<T, TDto>(obj);
            var res = @object.FindListByPage(Dto,page,size);
            var data = ObjectMapper.Map<ListDto,List<T>>(res);
            return data;
        }

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public override ResultDto<T> FindResultByEntityAndPage(T obj, int page, int size)
        {
            var Dto = ObjectMapper.Map<T, TDto>(obj);
            var res = @object.FindResultByPage(Dto, page, size);
            var data = ObjectMapper.Map<ResultDto,ResultDto<T>>(res);
            return data;
        }


#if !(NET20 || NET30 || NET35)

        /// <summary>根据条件及分页查询实体类数据集信息和实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息和实体类数据集数量信息</return>
        public virtual Task<ResultDto<T>> FindResultByPageAsync(T obj, int page, int size,CancellationToken cancellationToken = default)
        {
            return new Task<ResultDto<T>>(() => FindResultByEntityAndPage(obj, page, size));
        }


        /// <summary>添加实体类信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回添加结果,大于0 返回添加成功,小于等于0 返回添加失败 </return>
        public override Task<int> InsertAsync(T obj, CancellationToken cancellationToken = default)
        {
            return new Task<int>(() => Insert(obj));
        }
        /// <summary>修改实体类信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回修改结果,大于0 返回修改成功,小于等于0 返回修改失败 </return>
        public override Task<int> UpdateAsync(T obj,CancellationToken cancellationToken = default)
        {
            return new Task<int>(() => Update(obj));
        }


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


        /// <summary>根据条件查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息 </return>
        public override Task<List<T>> FindListByEntityAsync(T obj,CancellationToken cancellationToken = default)
        {
            return new Task<List<T>>(() => FindListByEntity(obj));
        }


        /// <summary>根据条件查询实体类数据集数量信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集数量信息</return>
        public override  Task<long> CountByEntityAsync(T obj, CancellationToken cancellationToken = default)
        {
            return new Task<long>(() => CountByEntity(obj));
        }

        /// <summary>根据条件及分页查询实体类数据集信息</summary>
        /// <param name="obj">实体类</param>
        /// <param name="page">页数</param>
        /// <param name="size">每页记录</param>
        /// <param name="cancellationToken"></param>
        ///<return>返回实体类数据集信息</return>
        public virtual Task<List<T>> FindListByPageAsync(T obj, int page, int size,CancellationToken cancellationToken = default)
        {
            return new Task<List<T>>(() => FindListByEntityAndPage(obj, page, size));
        }

   

#endif
    }
    }
#endif