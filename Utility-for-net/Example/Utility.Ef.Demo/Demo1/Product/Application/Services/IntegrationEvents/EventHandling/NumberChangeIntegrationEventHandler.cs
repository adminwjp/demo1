#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Product.Application.Services.Commands;
using Product.Application.Services.IntegrationEvents.Events;
using System.Threading;
using System.Threading.Tasks;
using Utility.EventBus.Abstractions;
using Utility.EventBus.Extensions;

namespace Product.Application.Services.IntegrationEvents.EventHandling
{
    public  class NumberChangeIntegrationEventHandler : IIntegrationEventHandlerAsync<NumberChangeIntegrationEvent>
       
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NumberChangeIntegrationEventHandler> _logger;

        public NumberChangeIntegrationEventHandler(
            IMediator mediator,
            ILogger<NumberChangeIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Event handler which confirms that the grace period
        /// has been completed and order will not initially be cancelled.
        /// Therefore, the order process continues for validation. 
        /// </summary>
        /// <param name="event">       
        /// </param>
        /// <returns></returns>
        public async Task HandleAsync(NumberChangeIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{ApplicationConstant.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, ApplicationConstant.AppName, @event);
               // var command = new Command(@event.ProductId, @event.SpceId);
                var command = new NumberChanngeCommand() { ProductId=@event.ProductId,SpceId=@event.SpceId};

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty},{IdProperty1}: {CommandId},{CommandId1} ({@Command})",
                    command.GetGenericTypeName(),
                    nameof(command.ProductId),
                    command.ProductId,
                    nameof(command.SpceId),
                    command.SpceId,
                    command);

                await _mediator.Send(command);
            }
        }
    }
}
#endif