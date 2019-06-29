using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Deliveries.Application.Events
{
    [Contract]
    public class RegistrationAddedToDelivery : IEvent
    {
        public Guid Id { get; }
        public Guid OrderId { get; }
        public string Message { get; }

        public RegistrationAddedToDelivery(Guid id, Guid orderId, string message)
        {
            Id = id;
            OrderId = orderId;
            Message = message;
        }
    }
}