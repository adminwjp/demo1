#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeOutFoot.Application.Services.Commands;
using Utility.Application.Services;
using Utility.Domain.Repositories;

namespace TakeOutFoot.Gifts
{
    /// <summary>
    /// 礼物 即 优惠券 服务
    /// </summary>
    [Component(typeof(GiftAppService), AutofacScope = AutofacScope.InstancePerLifetimeScope)]
    public class GiftAppService: CrudAppService<Gift,long>
    {
        //[Autowired]
        IMediator mediator;
        //[Autowired]
        //IRepository<Gift, long> repository;

        public GiftAppService(IMediator mediator, IRepository<Gift, long> repository):base(repository)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// 获取库存
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual List<int> GetStocks(long[] ids)
        {
            return null;
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="giftIds"></param>
        public virtual void UpdateStock(Tuple<long?, int>[] giftIds)
        { 
            //图方便 循环执行多遍
            foreach (var item in giftIds)
            {
                StockCommand command = new StockCommand()
                {
                    Id = item.Item1,
                    Number = item.Item2,
                    Flag = Domain.Events.StockFlag.Gift
                };
                mediator.Send(command);
            }
         
        }
    }
}
#endif