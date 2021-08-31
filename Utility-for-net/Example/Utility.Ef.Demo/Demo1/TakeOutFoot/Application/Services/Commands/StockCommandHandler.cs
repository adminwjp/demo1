#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using TakeOutFoot.Gifts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TakeOutFoot.Activties;
using TakeOutFoot.Infrastructure;

namespace TakeOutFoot.Application.Services.Commands
{
    public class StockCommandHandler : IRequestHandler<StockCommand, bool>
    {
        protected TakeOutFootDbContext DbContext { get; set; }

        public StockCommandHandler(TakeOutFootDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// Handler which processes the command when
        /// customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> Handle(StockCommand request, CancellationToken cancellationToken)
        {
            //怎么一直进来 
            switch (request.Flag)
            {
                case Domain.Events.StockFlag.None:
                case Domain.Events.StockFlag.Agent:
                case Domain.Events.StockFlag.Business:
                case Domain.Events.StockFlag.Platform:
                    break;
                case Domain.Events.StockFlag.Acitivty:
                    {
                        var activty = DbContext.Set<Activty>().Where(it => it.Id == request.Id && !it.IsDeleted).FirstOrDefault();
                        if (request.Number > 0)
                        {
                            activty.AddStock(request.Number);
                        }
                        else
                        {
                            activty.RemoveStock(-request.Number);
                        }
                        return await DbContext.SaveEntitiesAsync(cancellationToken);//提交库里
                    }
                case Domain.Events.StockFlag.AcitivtySetting:
                    {

                        //多个 物品 怎么传 累 这 玩意拆得累
                        var activtySetting = DbContext.Set<ActivtySetting>().Where(it => it.Id == request.Id && !it.IsDeleted).FirstOrDefault();
                        if (request.Number > 0)
                        {
                            activtySetting.AddStock(request.Number);
                        }
                        else
                        {
                            activtySetting.RemoveStock(-request.Number);
                        }
                        return await DbContext.SaveEntitiesAsync(cancellationToken);//提交库里
                    }
                case Domain.Events.StockFlag.Gift:
                    {
                        var gift = DbContext.Set<Gift>().Where(it => it.Id == request.Id && !it.IsDeleted).FirstOrDefault();
                        if (request.Number > 0)
                        {
                            gift.AddStock(request.Number);
                        }
                        else
                        {
                            gift.RemoveStock(-request.Number);
                            gift.SellCount += -request.Number;
                        }
                        return await DbContext.SaveEntitiesAsync(cancellationToken);//提交库里
                    }
                default:
                    break;
            }
            return false;
        }
    }
}
#endif