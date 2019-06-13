namespace Demo.Microservices.Shipments.API.Contracts.v1
{
    public class ShipmentStatusUpdate
    {
        public string TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
    }
}
