using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;

namespace Demo.Monolith.API.Repositories.Interfaces
{
    public interface IShipmentRepository
    {
        Task<ShipmentInformation> CreateAsync(Order order);
        Task<ShipmentInformation> GetAsync(string trackingNumber);
        Task UpdateAsync(ShipmentStatusUpdate shipmentStatusUpdate);
    }
}