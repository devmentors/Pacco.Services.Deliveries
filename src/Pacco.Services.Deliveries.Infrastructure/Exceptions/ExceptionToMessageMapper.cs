using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers.RabbitMQ;
using Pacco.Services.Deliveries.Application.Commands;
using Pacco.Services.Deliveries.Application.Events.Rejected;
using Pacco.Services.Deliveries.Application.Exceptions;
using Pacco.Services.Deliveries.Core.Exceptions;

namespace Pacco.Services.Deliveries.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
            => exception switch
            {
                CannotAddDeliveryRegistrationException ex => message switch
                {
                    StartDelivery command => new StartDeliveryRejected(command.DeliveryId, command.OrderId, ex.Message, ex.Code),
                    _ => null,
                },
                CannotChangeDeliveryStateException ex => message switch
                {
                    StartDelivery command => (IRejectedEvent) new StartDeliveryRejected(command.DeliveryId, command.OrderId, ex.Message, ex.Code),
                    CompleteDelivery command => new CompleteDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    FailDelivery command => new FailDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    _ => null
                },
                DeliveryNotFoundException ex => message switch
                {
                    CompleteDelivery command => (IRejectedEvent) new CompleteDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    FailDelivery command => new FailDeliveryRejected(command.DeliveryId, ex.Message, ex.Code),
                    _ => null
                },
                _ => null
            };
    }
}