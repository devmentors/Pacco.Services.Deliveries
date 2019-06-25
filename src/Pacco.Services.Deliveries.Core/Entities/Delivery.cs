using System;
using System.Collections.Generic;
using System.ComponentModel;
using Pacco.Services.Vehicles.Core.Entities;

namespace Pacco.Services.Deliveries.Core.Entities
{
    public class Delivery
    {
        public AggregateId Id { get; protected set; }
        public Guid OrderId { get; protected set; }



    }
}