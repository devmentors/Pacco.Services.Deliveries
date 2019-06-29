using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Deliveries.Application.Events
{
    [Contract]
    public class DeliveryFailed : IEvent
    {
        public Guid Id { get; }
        public Guid OrderId { get; }

        public DeliveryFailed(Guid id, Guid orderId)
        {
            Id = id;
            OrderId = orderId;
        }
    }
}