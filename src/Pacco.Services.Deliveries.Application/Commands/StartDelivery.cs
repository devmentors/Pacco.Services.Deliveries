using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Deliveries.Application.Commands
{
    [Contract]
    public class StartDelivery : ICommand
    {
        public Guid Id { get; }
        public Guid OrderId { get; }
        public string Message { get; set; }
        public DateTime DateTime { get; }

        public StartDelivery(Guid id, Guid orderId, string message, DateTime dateTime)
        {
            Id = id;
            OrderId = orderId;
            Message = message;
            DateTime = dateTime;
        }
    }
}