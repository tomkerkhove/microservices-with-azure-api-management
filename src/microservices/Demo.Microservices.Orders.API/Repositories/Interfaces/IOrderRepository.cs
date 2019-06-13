using System.Threading.Tasks;
using Demo.Microservices.Orders.API.Contracts.v1;

namespace Demo.Microservices.Orders.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(string confirmationId);
        Task<string> CreateAsync(Order createdOrder);
    }
}