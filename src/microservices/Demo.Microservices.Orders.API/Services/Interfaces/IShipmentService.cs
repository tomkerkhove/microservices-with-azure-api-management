using System.Threading.Tasks;
using Demo.Microservices.Orders.API.Contracts.v1;

namespace Demo.Microservices.Orders.API.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<ShipmentInformation> CreateAsync(Address address);
    }
}
