using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;

namespace Demo.Monolith.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetAsync();
    }
}