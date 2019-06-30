using System.Threading.Tasks;
using Pacco.Services.Deliveries.Application.Exceptions;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;
using Pacco.Services.Deliveries.Core.ValueObjects;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class AddDeliveryRegistrationHandler : CommandHandlerBase<AddDeliveryRegistration>
    {
        private readonly IDeliveriesRepository _repository;
        public AddDeliveryRegistrationHandler(IDeliveriesRepository repository, IMessageBroker messageBroker, IEventMapper eventMapper) 
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
        }

        public override async Task HandleAsync(AddDeliveryRegistration command)
        {
            var delivery = await  _repository.GetAsync(command.Id);

            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.Id);
            }
            
            delivery.AddRegistration(new DeliveryRegistration(command.Description, command.DateTime));

            if (delivery.HasChanged)
            {
                await _repository.UpdateAsync(delivery);
                await PublishEventsAsync(delivery);
            }
        }
    }
}