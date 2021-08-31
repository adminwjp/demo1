#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeOutFoot.Application.Services.Commands;
using Utility.Application.Services;
using Utility.Application.Services.Dtos;
using Utility.Domain.Repositories;

namespace TakeOutFoot.Activties
{
    /// <summary>
    /// 活动 服务
    /// </summary>
    [Component(typeof(ActivtyAppService), AutofacScope = AutofacScope.InstancePerLifetimeScope)]

    public class ActivtyAppService : CrudAppService<Activty, long>
    {
        [Autowired]
        IMediator mediator;
        public ActivtyAppService(IRepository<Activty, long> repository):base(repository)
        {

        }
        /// <summary>
        /// 获取活动信息
        /// 一般实际操作根据 条件获取 活动信息
        /// </summary>
        /// <returns></returns>
        public virtual ResultDto<ActivtyOutput> GetActivties()
        {
            return null;
        }

        /// <summary>
        /// 该活动 是否结束
        /// </summary>
        /// <param name="activtyId">活动 id </param>
        /// <returns></returns>
        public virtual bool IsEnd(long? activtyId)
        {
            return false;
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="activtyId"></param>
        /// <param name="number"></param>
        public virtual void UpdateStock(long? activtyId,  int number)
        {
            StockCommand command = new StockCommand()
            {
                Id = activtyId,
                Number = number,
                Flag = Domain.Events.StockFlag.Acitivty
            };
            mediator.Send(command);
        }
    }
}
#endif