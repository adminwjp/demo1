#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
//using MediatR;
//using Microsoft.Extensions.Logging;
//using Serilog.Context;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TakeOutFoot.Application.Services.Commands;
//using TakeOutFoot.Application.Services.IntegrationEvents.Events;
//using Utility.EventBus.Abstractions;
//using Utility.EventBus.Events;
//using Utility.EventBus.Extensions;

//namespace TakeOutFoot.Application.Services.IntegrationEvents.EventHandlings
//{
//    public class StockIntegrationEventHandler : IIntegrationEventHandlerAsync<StockIntegrationEvent>
//    {
//        private readonly IMediator _mediator;
//        private readonly ILogger<StockIntegrationEventHandler> _logger;

//        public StockIntegrationEventHandler(
//            IMediator mediator,
//            ILogger<StockIntegrationEventHandler> logger)
//        {
//            _mediator = mediator;
//            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
//        }

//        /// <summary>
//        /// Event handler which confirms that the grace period
//        /// has been completed and order will not initially be cancelled.
//        /// Therefore, the order process continues for validation. 
//        /// </summary>
//        /// <param name="event">       
//        /// </param>
//        /// <returns></returns>
//        public async Task HandleAsync(StockIntegrationEvent @event)
//        {
//            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-TakeOutFoot"))
//            {
//                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at TakeOutFoot - ({@IntegrationEvent})", @event.Id, @event);
//                var command = new StockCommand() { Id = @event.Id, Number = @event.Number,Flag=@event.Flag };

//                _logger.LogInformation(
//                    "----- Sending command: {CommandName} - {Id},{Id1}: {Number},{Number1}: {Flag},{Flag1} ({@Command})",
//                    command.GetGenericTypeName(),
//                    nameof(command.Id),
//                    command.Id,
//                    nameof(command.Number),
//                    command.Number,
//                    nameof(command.Flag),
//                    command.Flag,
//                    command);

//                await _mediator.Send(command);
//            }
//        }
//    }
//}
#endif