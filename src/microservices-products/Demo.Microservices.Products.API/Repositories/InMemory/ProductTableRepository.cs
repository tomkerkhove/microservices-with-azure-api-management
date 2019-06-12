using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Microservices.Products.API.Contracts.v1;
using Demo.Microservices.Products.API.Data.Contracts.v1;
using Demo.Microservices.Products.API.Data.Providers;
using Demo.Microservices.Products.API.Repositories.Interfaces;

namespace Demo.Microservices.Products.API.Repositories.InMemory
{
    public class ProductTableRepository : IProductRepository
    {
        private readonly TableStorageAccessor _tableStorageAccessor;
        private const string TableName = "products";

        public ProductTableRepository(TableStorageAccessor tableStorageAccessor)
        {
            _tableStorageAccessor = tableStorageAccessor;
        }

        public async Task<List<Product>> GetAsync()
        {
            var products = await _tableStorageAccessor.GetAsync<ProductTableEntity>(TableName, "products");
            return products.Select(MapToContract).ToList();
        }

        public async Task<Product> GetAsync(int id)
        {
            var persistedProduct = await _tableStorageAccessor.GetAsync<ProductTableEntity>(TableName, "products", id.ToString());

            Product order = null;
            if (persistedProduct != null)
            {
                order = MapToContract(persistedProduct);
            }

            return order;
        }

        private Product MapToContract(ProductTableEntity productTableEntity)
        {
            return new Product
            {
                Id = productTableEntity.Id,
                Name = productTableEntity.Name,
                Price = productTableEntity.Price,
            };
        }
    }
}