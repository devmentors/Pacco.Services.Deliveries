using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pacco.Services.Deliveries.Application.Exceptions;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;
using Pacco.Services.Deliveries.Core.ValueObjects;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class AddDeliveryRegistrationHandler : CommandHandlerBase<AddDeliveryRegistration>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly ILogger<AddDeliveryRegistrationHandler> _logger;

        public AddDeliveryRegistrationHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper, ILogger<AddDeliveryRegistrationHandler> logger) 
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
            _logger = logger;
        }

        public override async Task HandleAsync(AddDeliveryRegistration command)
        {
            var delivery = await  _repository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.DeliveryId);
            }
            
            delivery.AddRegistration(new DeliveryRegistration(command.Description, command.DateTime));
            if (delivery.HasChanged)
            {
                await _repository.UpdateAsync(delivery);
                await PublishEventsAsync(delivery);
                _logger.LogInformation($"Added a registration for the delivery with id: {command.DeliveryId}.");
            }
        }
    }
}