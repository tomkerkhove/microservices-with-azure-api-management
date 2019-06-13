using System;

namespace Demo.Microservices.Shipments.API.Exceptions
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
