using System;

namespace Demo.Microservices.Shipments.Data.Exceptions
{
    public class ShipmentNotFoundException : Exception
    {
        public ShipmentNotFoundException(string trackingNumber)
        {
            TrackingNumber = trackingNumber;
        }

        public string TrackingNumber { get; }
    }
}
