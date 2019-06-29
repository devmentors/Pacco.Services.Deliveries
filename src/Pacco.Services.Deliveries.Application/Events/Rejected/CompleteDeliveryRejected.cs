using System;
using Convey.CQRS.Events;

namespace Pacco.Services.Deliveries.Application.Events.Rejected
{
    public class CompleteDeliveryRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public Guid OrderId { get; }
        public string Reason { get; }
        public string Code { get; }

        public CompleteDeliveryRejected(Guid id, Guid orderId, string reason, string code)
        {
            Id = id;
            OrderId = orderId;
            Reason = reason;
            Code = code;
        }
    }
}