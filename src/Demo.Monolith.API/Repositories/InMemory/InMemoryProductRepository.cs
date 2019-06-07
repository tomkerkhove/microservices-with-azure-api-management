using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;
using Demo.Monolith.API.Repositories.Interfaces;

namespace Demo.Monolith.API.Repositories.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _availableProducts = new List<Product>
        {
            new Product {Id = 1, Name = "Surface Go", Price = 399},
            new Product {Id = 2, Name = "Surface Pro 6", Price = 799},
            new Product {Id = 3, Name = "Surface Book 2", Price = 1049},
            new Product {Id = 4, Name = "Surface Laptop 2", Price = 1199},
            new Product {Id = 5, Name = "Surface Studio 2", Price = 3499}
        };

        public Task<Product> GetAsync(int id)
        {
            var foundProduct = _availableProducts.SingleOrDefault(product => product.Id == id);
            return Task.FromResult(foundProduct);
        }

        public Task<List<Product>> GetAsync()
        {
            return Task.FromResult(_availableProducts);
        }
    }
}