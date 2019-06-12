using Microsoft.Azure.Cosmos.Table;

namespace Demo.Microservices.Products.API.Data.Contracts.v1
{
    public class ProductTableEntity : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
