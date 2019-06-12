using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Microservices.Shipments.API.Contracts.v1;
using Demo.Microservices.Shipments.API.Data.Contracts.v1;
using Demo.Microservices.Shipments.API.Data.Providers;
using Demo.Microservices.Shipments.API.Exceptions;
using Demo.Microservices.Shipments.API.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Demo.Microservices.Shipments.API.Repositories.InMemory
{
    public class ShipmentsTableRepository : IShipmentRepository
    {
        private readonly TableStorageAccessor _tableStorageAccessor;
        private const string TableName = "shipments";

        public ShipmentsTableRepository(TableStorageAccessor tableStorageAccessor)
        {
            _tableStorageAccessor = tableStorageAccessor;
        }

        private readonly Dictionary<string, ShipmentInformation> _shipments = new Dictionary<string, ShipmentInformation>();

        public async Task<ShipmentInformation> CreateAsync(Address address)
        {
            var trackingNumber = Guid.NewGuid().ToString();

            var shipmentInformation = new ShipmentInformation
            {
                TrackingNumber = trackingNumber,
                DeliveryAddress = address,
                Status = ShipmentStatus.AwaitingPickup
            };

            var shipmentInformationTableEntry = MapToTableEntry(shipmentInformation);
            await _tableStorageAccessor.PersistAsync(TableName, shipmentInformationTableEntry);

            return shipmentInformation;
        }

        public async Task<ShipmentInformation> GetAsync(string trackingNumber)
        {
            var shipmentTableEntry = await GetShipmentTableEntryAsync(trackingNumber);
            if (shipmentTableEntry == null)
            {
                return null;
            }

            var shipmentInformation = MapToContract(shipmentTableEntry);
            return shipmentInformation;
        }

        public async Task UpdateAsync(ShipmentStatusUpdate shipmentStatusUpdate)
        {
            var shipmentTableEntry = await GetShipmentTableEntryAsync(shipmentStatusUpdate.TrackingNumber);

            shipmentTableEntry.RowKey = DateTimeOffset.UtcNow.ToString("s");
            shipmentTableEntry.Status = shipmentStatusUpdate.Status.ToString();

            await _tableStorageAccessor.PersistAsync(TableName, shipmentTableEntry);
        }

        private async Task<ShipmentTableEntity> GetShipmentTableEntryAsync(string trackingNumber)
        {
            var shipmentUpdates = await _tableStorageAccessor.GetAsync<ShipmentTableEntity>(TableName, trackingNumber);
            return shipmentUpdates.OrderByDescending(shipmentUpdate => shipmentUpdate.RowKey).FirstOrDefault();
        }

        private ShipmentInformation MapToContract(ShipmentTableEntity shipmentTableEntry)
        {
            return new ShipmentInformation
            {
                TrackingNumber = shipmentTableEntry.TrackingNumber,
                DeliveryAddress = JsonConvert.DeserializeObject<Address>(shipmentTableEntry.DeliveryAddress),
                Status = Enum.Parse<ShipmentStatus>(shipmentTableEntry.Status),
            };
        }

        private ShipmentTableEntity MapToTableEntry(ShipmentInformation shipmentInformation)
        {
            return new ShipmentTableEntity
            {
                PartitionKey = shipmentInformation.TrackingNumber,
                RowKey = DateTimeOffset.UtcNow.ToString("s"),
                TrackingNumber = shipmentInformation.TrackingNumber,
                DeliveryAddress = JsonConvert.SerializeObject(shipmentInformation.DeliveryAddress),
                Status = shipmentInformation.Status.ToString(),
            };
        }
    }
}