using System.Threading.Tasks;
using Pacco.Services.Deliveries.Application.Exceptions;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal sealed class FailDeliveryHandler : CommandHandlerBase<FailDelivery>
    {
        private readonly IDeliveriesRepository _repository;

        public FailDeliveryHandler(IDeliveriesRepository repository, IMessageBroker messageBroker, IEventMapper eventMapper) 
            : base(messageBroker, eventMapper)
        {
            _repository = repository;
        }
        
        public override async Task HandleAsync(FailDelivery command)
        {
            var delivery = await _repository.GetAsync(command.Id);

            if (delivery is null)
            {
                throw new DeliveryNotFoundException(command.Id);
            }
            
            delivery.Fail();

            await _repository.UpdateAsync(delivery);
            await PublishEventsAsync(delivery);
        }
    }
}