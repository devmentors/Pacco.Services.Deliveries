using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pacco.Services.Deliveries.Application.Exceptions;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class FailDeliveryHandler : CommandHandlerBase<FailDelivery>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly ILogger<FailDeliveryHandler> _logger;

        public FailDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper, ILogger<FailDeliveryHandler> logger) 
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public override async Task HandleAsync(FailDelivery command)
        {
            var delivery = await _repository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.DeliveryId);
            }

            delivery.Fail(command.Reason);
            await _repository.UpdateAsync(delivery);
            await PublishEventsAsync(delivery);
            _logger.LogInformation($"Failed the delivery with id: {command.DeliveryId}, reason: {command.Reason}");
        }
    }
}