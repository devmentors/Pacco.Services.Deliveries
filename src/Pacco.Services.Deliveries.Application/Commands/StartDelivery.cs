using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Deliveries.Application.Commands
{
    [Contract]
    public class StartDelivery : ICommand
    {
        public Guid Id { get; }
        public Guid OrderId { get; }
        public string Description { get; set; }
        public DateTime DateTime { get; }

        public StartDelivery(Guid id, Guid orderId, string description, DateTime dateTime)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            OrderId = orderId;
            Description = description;
            DateTime = dateTime;
        }
    }
}