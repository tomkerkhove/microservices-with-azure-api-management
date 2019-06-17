namespace Demo.Microservices.Shipments.Contracts.v1
{
    public class ShipmentInformation
    {
        public string TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }
    }
}
