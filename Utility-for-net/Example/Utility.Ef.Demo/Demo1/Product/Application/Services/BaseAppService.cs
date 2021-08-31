#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System;
using System.Collections.Generic;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Cache;
using Utility.Mappers;
using Utility.Helpers;

namespace Product.Application.Services
{
    /// <summary>
    /// (增删改查)基础 服务
    /// </summary>
    //[Component(typeof(BaseAppService<,,,,>), AutofacScope = AutofacScope.InstancePerLifetimeScope)]
    public class BaseAppService<Entity,CreateInput, UpdateInput, GetInput, GetOutput> : AppService
        where Entity:BaseEntity,new()
        where CreateInput :class
        where UpdateInput : class
        where GetInput : class
        where GetOutput : class
    {
        private IBaseRepository<Entity> repository;

        public BaseAppService(IBaseRepository<Entity> repository, IMapper objectMapper, ICacheContent cache)
        {
            this.repository = repository;
            this.Mapper = objectMapper;
            this.Cache = cache;
        }


        /// <summary>
        /// 添加
        /// 1.如果放入缓存 也要 更新 该信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        public virtual int Insert(CreateInput create)
        {
            Entity entity = Mapper.Map<Entity>(create);
            repository.Insert(entity);
            return 1;
        }

        /// <summary>
        /// 修改 
        /// 1.如果放入缓存 也要 更新 该信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        public virtual int Update(UpdateInput update)
        {
            Entity entity = Mapper.Map<Entity>(update);
            repository.Update(entity);
            return 1;
        }

        /// <summary>
        /// 删除
        /// 1.如果放入缓存 也要 更新 该信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(long id)
        {
            repository.Delete(id);
        }

        /// <summary>
        /// 删除
        /// 1.如果放入缓存 也要 更新 该信息
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="ids"></param>
        public virtual int Delete(long[] ids)
        {
          return  repository.DeleteList(ids);
        }

        /// <summary>
        /// 根据条件查询 
        /// 1.如果放入缓存 缓存查询 
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual IList<GetOutput> Find(GetInput input)
        {
            Entity entity = Mapper.Map<Entity>(input);
            var res = repository.Find(entity);
            var result = Mapper.Map<IList<GetOutput>>(res);
            return result;
        }

        /// <summary>
        /// 根据条件查询 
        /// 1.如果放入缓存 缓存查询 
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual IList<GetOutput> FindByPage(GetInput input, int page = 1, int size = 10)
        {
            PageHelper.Set(ref page, ref size);
            Entity entity = Mapper.Map<Entity>(input);
            var res = repository.FindByPage(entity, page, size);
            var result = Mapper.Map<IList<GetOutput>>(res);
            return result;
        }

        /// <summary>
        /// 根据条件查询 
        /// 1.如果放入缓存 缓存查询 
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public virtual ResultDto<GetOutput> FindResultDtoByPage(GetInput input, int page = 1, int size = 10)
        {
            PageHelper.Set(ref page, ref size);
            Entity entity = Mapper.Map<Entity>(input);
            var res = repository.FindResultDtoByPage(entity, page, size);
            //var result = ObjectMapper.Map<ResultDto<IList<GetOutput>>>(res); //error
            var data = Mapper.Map<List<GetOutput>>(res.Data);
            var result = new ResultDto<GetOutput>() { Data=data,Result=res.Result};
            return result;
        }

        /// <summary>
        /// 根据条件查询 
        /// 1.如果放入缓存 缓存查询 
        /// 2.不放人缓存 直接数据库操作 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual long Count(GetInput input)
        {
            Entity entity = Mapper.Map<Entity>(input);
            var res = repository.CountByEntity(entity);
            return res;
        }

    }
}
#endif