using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Deliveries.Application.Commands
{
    [Contract]
    public class AddDeliveryRegistration : ICommand
    {
        public Guid Id { get; }
        public string Description { get; }
        public DateTime DateTime { get; }

        public AddDeliveryRegistration(Guid id, string description, DateTime dateTime)
        {
            Id = id;
            Description = description;
            DateTime = dateTime;
        }
    }
}