#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using TakeOutFoot.Domain.Events;
//using Microsoft.Extensions.Logging;
//using Utility.Application.IntegrationEvents;
//using TakeOutFoot.Application.Services.IntegrationEvents.Events;
//using TakeOutFoot.Infrastructure;
//using TakeOutFoot.Activties;

//namespace TakeOutFoot.Application.Services.DomainEventHandlers
//{
//    /// <summary>
//    /// 库存 双向订阅 
//    /// 这样可读性差  类多爆炸
//    /// </summary>
//    public class StockDomainEventHandler : INotificationHandler<StockDomainEvent>
//    {
//        private readonly ILoggerFactory _logger;
//        private readonly IIntegrationEventService integrationEventService;
//       // [Autofac.Annotation.Autowired]
//        //protected TakeOutFootDbContext DbContext { get; set; }
//        public StockDomainEventHandler(
//            ILoggerFactory logger,
//            IIntegrationEventService integrationEventService)
//        {
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            this.integrationEventService = integrationEventService;
//        }
//        public async Task Handle(StockDomainEvent notification, CancellationToken cancellationToken)
//        {
//            _logger.CreateLogger<StockDomainEvent>()
//                  .LogTrace("{Flag} with Id: {Id} has been successfully updated to stock {Number} ",
//                      notification.Flag.ToString(),notification.Id, notification.Number);
//            //订阅中心处理数据 这里做业务处理一般 来说 集成事件处理 
//            //存储集成事件日志 存不储存无所谓 安全日志
//            var stockIntegration = new StockIntegrationEvent(notification.Id, notification.Number, notification.Flag);
//            await this.integrationEventService.AddAndSaveEventAsync(stockIntegration);
//        }
//    }
//}
#endif