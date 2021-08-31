using AutoMapper;
using System.Collections.Generic;

namespace Adverts
{
    /// <summary>
    /// 订单航运 服务 
    /// </summary>
    public class BaseAppService<Repository,CreateInput,UpdateInput,GetAllDto,RequestDto,Entity>
        where Repository :IBaseRepository<Entity>
        where CreateInput:class
        where UpdateInput : class
        where GetAllDto : class
        where RequestDto : class
        where Entity :BaseEntity 
    {
        private IMapper mapper;
        protected Repository repository;
        public BaseAppService(Repository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 添加 
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        public int Insert(CreateInput create)
        {
            Entity order = mapper.Map<Entity>(create);
            repository.Insert(order);
            return 1;
        }
        /// <summary>
        /// 修改 订单航运
        /// </summary>
        /// <param name="create"></param>
        /// <returns></returns>
        public int Update(UpdateInput update)
        {
            Entity order = mapper.Map<Entity>(update);
            //已删除 的不能 修改
            repository.Update(order);
            return 1;
        }
        public void Delete(string id)
        {
            repository.Delete(id);
        }
        public void Delete(string[] ids)
        {
            repository.Delete(ids);
        }
        public IList<GetAllDto> Find(RequestDto entity)
        {
            Entity order = mapper.Map<Entity>(entity);
            var res = repository.Find(order);
            var result = mapper.Map<IList<GetAllDto>>(res);
            return result;
        }

        public IList<GetAllDto> FindByPage(RequestDto entity, int page = 1, int size = 10)
        {
            Entity order = mapper.Map<Entity>(entity);
            var res = repository.FindByPage(order, page, size);
            var result = mapper.Map<IList<GetAllDto>>(res);
            return result;
        }

        public int Count(RequestDto entity)
        {
            Entity order = mapper.Map<Entity>(entity);
            var res = repository.Count(order);
            return res;
        }
    }
}
