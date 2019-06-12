using Microsoft.Azure.Cosmos.Table;

namespace Demo.Monolith.API.Data.Contracts.v1
{
    public class ShipmentTableEntity : TableEntity
    {
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
