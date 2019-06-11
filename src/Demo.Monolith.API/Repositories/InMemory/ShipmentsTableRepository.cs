using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;
using Demo.Monolith.API.Data.Contracts.v1;
using Demo.Monolith.API.Data.Providers;
using Demo.Monolith.API.Exceptions;
using Demo.Monolith.API.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Demo.Monolith.API.Repositories.InMemory
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

        public async Task<ShipmentInformation> CreateAsync(Order createdOrder)
        {
            var trackingNumber = Guid.NewGuid().ToString();

            var shipmentInformation = new ShipmentInformation
            {
                TrackingNumber = trackingNumber,
                DeliveryAddress = createdOrder.Customer.Address,
                Status = ShipmentStatus.AwaitingPickup
            };

            var shipmentInformationTableEntry = MapToTableEntry(shipmentInformation);
            await _tableStorageAccessor.PersistAsync(TableName, shipmentInformationTableEntry);

            return shipmentInformation;
        }

        public async Task<ShipmentInformation> GetAsync(string trackingNumber)
        {
            var shipmentTableEntry = await _tableStorageAccessor.GetAsync<ShipmentTableEntity>(TableName, trackingNumber, trackingNumber);

            var shipmentInformation = MapToContract(shipmentTableEntry);
            return shipmentInformation;
        }

        public async Task UpdateAsync(ShipmentStatusUpdate shipmentStatusUpdate)
        {
            var shipmentTableEntry = await GetShipmentTableEntryAsync(shipmentStatusUpdate.TrackingNumber);
            shipmentTableEntry.Status = shipmentStatusUpdate.Status.ToString();

            await _tableStorageAccessor.UpdateAsync(TableName, shipmentTableEntry);
        }

        private async Task<ShipmentTableEntity> GetShipmentTableEntryAsync(string trackingNumber)
        {
            return await _tableStorageAccessor.GetAsync<ShipmentTableEntity>(TableName, trackingNumber, trackingNumber);
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
                RowKey = shipmentInformation.TrackingNumber,
                TrackingNumber = shipmentInformation.TrackingNumber,
                DeliveryAddress = JsonConvert.SerializeObject(shipmentInformation.DeliveryAddress),
                Status = shipmentInformation.Status.ToString(),
            };
        }
    }
}