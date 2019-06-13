
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Microservices.Shipments.API.Contracts.v1;
using Demo.Microservices.Shipments.API.Exceptions;
using Demo.Microservices.Shipments.API.Repositories.Interfaces;

namespace Demo.Microservices.Shipments.API.Repositories.InMemory
{
    public class InMemoryShipmentRepository : IShipmentRepository
    {
        private readonly Dictionary<string, ShipmentInformation> _shipments = new Dictionary<string, ShipmentInformation>();

        public Task<ShipmentInformation> CreateAsync(Address address)
        {
            var trackingNumber = Guid.NewGuid().ToString();

            var shipmentInformation = new ShipmentInformation
            {
                TrackingNumber = trackingNumber,
                DeliveryAddress = address,
                Status = ShipmentStatus.AwaitingPickup
            };

            _shipments.Add(trackingNumber, shipmentInformation);

            return Task.FromResult(shipmentInformation);
        }

        public Task<ShipmentInformation> GetAsync(string trackingNumber)
        {
            if (_shipments.TryGetValue(trackingNumber, out ShipmentInformation shipment))
            {
                return Task.FromResult(shipment);
            }

            return Task.FromResult<ShipmentInformation>(null);
        }

        public Task UpdateAsync(ShipmentStatusUpdate shipmentStatusUpdate)
        {
            if (!_shipments.TryGetValue(shipmentStatusUpdate.TrackingNumber, out var foundShipmentInformation))
            {
                throw new ShipmentNotFoundException(shipmentStatusUpdate.TrackingNumber);
            }

            foundShipmentInformation.Status = shipmentStatusUpdate.Status;
            _shipments[shipmentStatusUpdate.TrackingNumber] = foundShipmentInformation;

            return Task.CompletedTask;
        }
    }
}