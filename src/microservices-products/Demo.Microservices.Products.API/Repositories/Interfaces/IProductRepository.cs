using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Microservices.Products.API.Contracts.v1;

namespace Demo.Microservices.Products.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetAsync();
    }
}