using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Deliveries.Application.Commands
{
    [Contract]
    public class AddDeliveryRegistration : ICommand
    {
        public Guid Id { get; }
        public string Message { get; }
        public DateTime DateTime { get; }

        public AddDeliveryRegistration(Guid id, string message, DateTime dateTime)
        {
            Id = id;
            Message = message;
            DateTime = dateTime;
        }
    }
}