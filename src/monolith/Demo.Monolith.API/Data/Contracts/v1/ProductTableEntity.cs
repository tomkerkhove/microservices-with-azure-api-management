using Microsoft.Azure.Cosmos.Table;

namespace Demo.Monolith.API.Data.Contracts.v1
{
    public class ProductTableEntity : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
