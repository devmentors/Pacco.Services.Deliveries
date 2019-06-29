using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Deliveries.Application.Commands
{
    [Contract]
    public class CompleteDelivery : ICommand
    {
        public Guid Id { get; }

        public CompleteDelivery(Guid id)
            => Id = id;
    }
}