using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Deliveries.Application.Events
{
    [Contract]
    public class DeliveryCompleted : IEvent
    {
        public Guid Id { get; }
        public Guid OrderId { get; }

        public DeliveryCompleted(Guid id, Guid orderId)
        {
            Id = id;
            OrderId = orderId;
        }
    }
}