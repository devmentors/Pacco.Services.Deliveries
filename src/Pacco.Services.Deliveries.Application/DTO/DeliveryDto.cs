using System;
using System.Collections.Generic;
using Pacco.Services.Deliveries.Core.Entities;

namespace Pacco.Services.Deliveries.Application.DTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DeliveryStatus Status { get; set; }
        public DateTime? LastUpdate { get; set; }
        public IEnumerable<(string, DateTime)> Registrations { get; set; }
    }
}