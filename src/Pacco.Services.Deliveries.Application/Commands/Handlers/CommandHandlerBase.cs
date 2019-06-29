using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Entities;

namespace Pacco.Services.Deliveries.Application.Commands.Handlers
{
    internal abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IEventMapper _eventMapper;
        
        protected CommandHandlerBase(IMessageBroker messageBroker, IEventMapper eventMapper)
        {
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
        }

        public virtual Task HandleAsync(TCommand command)
            => Task.CompletedTask;

        protected async Task PublishEventsAsync(AggregateRoot aggregateRoot)
        {
            var events = _eventMapper.MapAll(aggregateRoot.Events);
            await _messageBroker.PublishAsync(events.ToArray());
        }
    }
}