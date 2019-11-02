using System;
using Pacco.Services.Deliveries.Core.Exceptions;

namespace Pacco.Services.Deliveries.Application.Exceptions
{
    public class DeliveryAlreadyStartedException : ExceptionBase
    {
        public override string Code => "delivery_already_started";
        
        public DeliveryAlreadyStartedException(Guid orderId) : base($"Delivery for order: {orderId} was already started.")
        {
        }
    }
}