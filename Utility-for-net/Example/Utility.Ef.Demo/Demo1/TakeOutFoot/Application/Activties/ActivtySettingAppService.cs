#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac.Annotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TakeOutFoot.Application.Services.Commands;

namespace TakeOutFoot.Activties
{
    [Component(typeof(ActivtySettingAppService), AutofacScope = AutofacScope.InstancePerLifetimeScope)]
    public class ActivtySettingAppService
    {
        [Autowired]
        IMediator mediator;
        /// <summary>
        /// 获取当前活动库存
        /// </summary>
        /// <param name="activtyId">活动 id</param>
        /// <returns>返回当前活动 优惠券 对应库存</returns>
       
        public virtual List<Tuple<string,int>> GetStocks(string activtyId)
        {
            return null;
        }

        public virtual int GetStock(string activtyId,string giftId)
        {
            return 0;
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="activtySettingIds"></param>
        public virtual void UpdateStock(Tuple<long?, int>[] activtySettingIds)
        {
            //图方便 循环执行多遍
            foreach (var item in activtySettingIds)
            {
                StockCommand command = new StockCommand()
                {
                    Id = item.Item1,
                    Number = item.Item2,
                    Flag = Domain.Events.StockFlag.AcitivtySetting
                };
                mediator.Send(command);
            }

        }

    }
}
#endif