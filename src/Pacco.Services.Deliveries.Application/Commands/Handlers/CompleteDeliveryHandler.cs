using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pacco.Services.Deliveries.Application.Exceptions;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class CompleteDeliveryHandler : CommandHandlerBase<CompleteDelivery>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly ILogger<CompleteDeliveryHandler> _logger;

        public CompleteDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper, ILogger<CompleteDeliveryHandler> logger) 
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
            _logger = logger;
        }

        public override async Task HandleAsync(CompleteDelivery command)
        {
            var delivery = await _repository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.DeliveryId);
            }
            
            delivery.Complete();
            await _repository.UpdateAsync(delivery);
            await PublishEventsAsync(delivery);
            _logger.LogInformation($"Completed the delivery with id: {command.DeliveryId}.");
        }
    }
}