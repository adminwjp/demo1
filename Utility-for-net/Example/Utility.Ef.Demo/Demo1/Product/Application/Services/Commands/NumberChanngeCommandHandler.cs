#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using Product.Domain.Entities;
using Product.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using Utility.Domain.Entities;

namespace Product.Application.Services.Commands
{
    public  class NumberChanngeCommandHandler : IRequestHandler<NumberChanngeCommand, bool>
    {
        [Autofac.Annotation.Autowired]
        protected ProductDbContext ProductDbContext { get; set; }

        public NumberChanngeCommandHandler(ProductDbContext productDbContext)
        {
            ProductDbContext = productDbContext;
        }

        /// <summary>
        /// Handler which processes the command when
        /// customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual async Task<bool> Handle(NumberChanngeCommand command, CancellationToken cancellationToken)
        {
            if(command.SpceId >0)
            {
                return await HandleProcess<SpecEntity>(command,cancellationToken);
            }
            else
            {
                return await HandleProcess<ProductEntity>(command, cancellationToken);
            }
        }
        public virtual async Task<bool> HandleProcess<Entity>(NumberChanngeCommand command, CancellationToken cancellationToken)
            where Entity:BaseEntity
        {
            var itemToUpdate = await ProductDbContext.Set<Entity>().FindAsync(command.SpceId>0? command.SpceId: command.ProductId);
            if (itemToUpdate == null)
            {
                return false;
            }
           await  Update(command,itemToUpdate,cancellationToken);
            return await ProductDbContext.SaveEntitiesAsync(cancellationToken);
        }
        protected virtual async Task<bool> Update<Entity>(NumberChanngeCommand command, Entity entity, CancellationToken cancellationToken)
        {
            if (command.SpceId > 0)
            {
                var itemToUpdate = await ProductDbContext.Set<SpecEntity>().FindAsync(command.SpceId);
                if (itemToUpdate == null)
                {
                    return false;
                }
                itemToUpdate.UpdateSales(command.Number);//更新规格 出售数量

            }
            var productToUpdate = await ProductDbContext.Set<ProductEntity>().FindAsync(command.ProductId);
            if (productToUpdate == null)
            {
                return false;
            }
            switch (command.Flag)
            {
                case Domain.Events.NumberEventFlag.None:
                    break;
                case Domain.Events.NumberEventFlag.Stock:
                    productToUpdate.AddStock(command.Number);//更新规格对应产品 库存数量
                    break;
                case Domain.Events.NumberEventFlag.SellCount:
                    productToUpdate.UpdateSales(command.Number);//更新规格对应产品 出售数量
                    break;
                default:
                    break;
            }

            return await ProductDbContext.SaveEntitiesAsync(cancellationToken);//提交库里
        }
    }
}
#endif