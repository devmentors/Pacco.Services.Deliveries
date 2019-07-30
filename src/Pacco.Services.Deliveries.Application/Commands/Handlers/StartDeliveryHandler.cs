using System.Threading.Tasks;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Entities;
using Pacco.Services.Deliveries.Core.Repositories;
using Pacco.Services.Deliveries.Core.ValueObjects;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class StartDeliveryHandler : CommandHandlerBase<StartDelivery>
    {
        private readonly IDeliveriesRepository _repository;

        public StartDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker, IEventMapper eventMapper) 
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
        }
        
        public override async Task HandleAsync(StartDelivery command)
        {
            var delivery = Delivery.Create(command.DeliveryId, command.OrderId, DeliveryStatus.InProgress);
            delivery.AddRegistration(new DeliveryRegistration(command.Description, command.DateTime));

            await _repository.AddAsync(delivery);
            await PublishEventsAsync(delivery);
        }
    }
}