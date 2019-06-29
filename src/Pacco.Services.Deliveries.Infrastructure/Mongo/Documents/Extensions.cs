using System.Linq;
using Pacco.Services.Deliveries.Core.Entities;
using Pacco.Services.Deliveries.Core.ValueObjects;

namespace Pacco.Services.Deliveries.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static DeliveryDocument AsDocument(this Delivery delivery)
            => new DeliveryDocument
            {
                Id = delivery.Id,
                OrderId = delivery.OrderId,
                Status = delivery.Status,
                Registrations = delivery.Registrations.Select(r => new DeliveryRegistrationDocument
                {
                    Description = r.Description,
                    DateTime = r.DateTime
                })
            };

        public static Delivery AsEntity(this DeliveryDocument document)
            => new Delivery(document.Id, document.OrderId, document.Status, 
                document.Registrations.Select(r => new DeliveryRegistration(r.Description, r.DateTime)));
    }
}