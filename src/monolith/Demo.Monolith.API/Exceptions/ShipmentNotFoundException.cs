using System;

namespace Demo.Monolith.API.Exceptions
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
