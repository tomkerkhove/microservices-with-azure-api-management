namespace Demo.Monolith.API.Contracts.v1
{
    public class OrderConfirmation : Order
    {
        public string ConfirmationId { get; set; }
        public ShipmentInformation ShipmentInformation { get; set; }
    }
}
