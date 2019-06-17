using System.Threading.Tasks;
using Demo.Microservices.Shipments.Contracts.v1;

namespace Demo.Microservices.Shipments.Data.Repositories.Interfaces
{
    public interface IShipmentRepository
    {
        Task<ShipmentInformation> CreateAsync(Address address);
        Task<ShipmentInformation> GetAsync(string trackingNumber);
        Task UpdateAsync(ShipmentStatusUpdate shipmentStatusUpdate);
    }
}