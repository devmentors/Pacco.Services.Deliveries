using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Deliveries.Application.Events
{
    [Contract]
    public class DeliveryStarted : IEvent
    {
        public Guid Id { get; }
        public Guid OrderId { get; }

        public DeliveryStarted(Guid id, Guid orderId)
        {
            Id = id;
            OrderId = orderId;
        }
    }
}