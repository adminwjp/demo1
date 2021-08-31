#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Utility.Application.IntegrationEvents;
using Product.Domain.Events;
using Product.Application.Services.IntegrationEvents.Events;
using Product.Application.Services.IntegrationEvents.EventHandling;

namespace Product.Application.Services.DomainEventHandlers
{
    /// <summary>
    /// 单向 订阅 存储 数据库(提交库之前 执行双向 订阅事件) 双向 订阅  存储缓存即更新缓存
    /// </summary>
    public class NumberChangeDomainEventHandler : INotificationHandler<NumberChangeDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IIntegrationEventService integrationEventService;

        public NumberChangeDomainEventHandler(
            ILoggerFactory logger,
            IIntegrationEventService integrationEventService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.integrationEventService = integrationEventService;
        }

        public async Task Handle(NumberChangeDomainEvent buyerStockDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<NumberChangeDomainEvent>()
                .LogTrace("Product with Id: {ProductId} has been successfully updated to stock {Number} ",
                    buyerStockDomainEvent.ProductId, nameof(buyerStockDomainEvent.Number));
            return;// ef 有点坑 需要在具体 类库下迁移比较麻烦 变动来 累 重复操作
            //更新缓存 删除缓存 没有 从库里查询 暂无实现

            //存储集成事件日志 存不储存无所谓 安全日志
            var buyerStockIntegrationEvent = new NumberChangeIntegrationEvent(buyerStockDomainEvent.ProductId, buyerStockDomainEvent.SpceId, buyerStockDomainEvent.Number);
            await this.integrationEventService.AddAndSaveEventAsync(buyerStockIntegrationEvent);
        }
    }
}
#endif