using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;

namespace Demo.Monolith.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(string confirmationId);
        Task<string> CreateAsync(Order createdOrder);
    }
}