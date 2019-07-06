using System;
using Convey.MessageBrokers.RabbitMQ;

namespace Pacco.Services.Deliveries.Infrastructure.Exceptions
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
        {
            return null;
        }
    }
}