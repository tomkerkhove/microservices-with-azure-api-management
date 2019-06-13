using Microsoft.Azure.Cosmos.Table;

namespace Demo.Monolith.API.Data.Contracts.v1
{
    public class OrderTableEntity : TableEntity
    {
        public string ConfirmationId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string Basket { get; set; }
    }
}
