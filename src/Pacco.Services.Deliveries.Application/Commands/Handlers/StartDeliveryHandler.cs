using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Entities;
using Pacco.Services.Deliveries.Core.Repositories;
using Pacco.Services.Deliveries.Core.ValueObjects;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class StartDeliveryHandler : CommandHandlerBase<StartDelivery>
    {
        private readonly IDeliveriesRepository _repository;
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker,
            IEventMapper eventMapper, ILogger<StartDeliveryHandler> logger)
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
            _logger = logger;
        }

        public override async Task HandleAsync(StartDelivery command)
        {
            var delivery = Delivery.Create(command.DeliveryId, command.OrderId, DeliveryStatus.InProgress);
            delivery.AddRegistration(new DeliveryRegistration(command.Description, command.DateTime));
            await _repository.AddAsync(delivery);
            await PublishEventsAsync(delivery);
            _logger.LogInformation($"Started the delivery with id: {command.DeliveryId}.");
        }
    }
}