using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Microservices.Shipments.API.Contracts.v1;

namespace Demo.Microservices.Shipments.API.Repositories.Interfaces
{
    public interface IShipmentRepository
    {
        Task<ShipmentInformation> CreateAsync(Address address);
        Task<ShipmentInformation> GetAsync(string trackingNumber);
        Task UpdateAsync(ShipmentStatusUpdate shipmentStatusUpdate);
    }
}