using System;
using Convey.CQRS.Commands;

namespace Pacco.Services.Deliveries.Application.Commands
{
    [Contract]
    public class FailDelivery : ICommand
    {
        public Guid Id { get; }
        public string Reason { get; }

        public FailDelivery(Guid id, string reason)
        {
            Id = id;
            Reason = reason;
        }
    }
}