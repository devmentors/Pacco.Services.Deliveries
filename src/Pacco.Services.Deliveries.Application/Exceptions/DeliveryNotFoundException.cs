using System;
using Pacco.Services.Deliveries.Core.Exceptions;

namespace Pacco.Services.Deliveries.Application.Exceptions
{
    public class DeliveryNotFoundException : ExceptionBase
    {
        public override string Code => "delivery_not_found";

        public DeliveryNotFoundException(Guid deliveryId) : base($"Delivery with id: {deliveryId} was not found.")
        {
        }
    }
}