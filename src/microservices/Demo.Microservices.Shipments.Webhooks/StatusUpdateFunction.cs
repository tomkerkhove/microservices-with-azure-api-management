using System.IO;
using System.Threading.Tasks;
using Demo.Microservices.Shipments.Contracts.v1;
using Demo.Microservices.Shipments.Data.Exceptions;
using Demo.Microservices.Shipments.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Demo.Microservices.Shipments.Webhooks
{
    public class StatusUpdateFunction
    {
        private readonly IShipmentRepository _shipmentsRepository;

        public StatusUpdateFunction(IShipmentRepository shipmentsRepository)
        {
            _shipmentsRepository = shipmentsRepository;
        }

        [FunctionName("StatusUpdateFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "update-status")] HttpRequest request, ILogger logger)
        {
            logger.LogInformation("Shipment webhook arrived with an update");

            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            var shipmentStatusUpdate = JsonConvert.DeserializeObject<ShipmentStatusUpdate>(requestBody);
            if (shipmentStatusUpdate == null)
            {
                return new BadRequestResult();
            }

            logger.LogInformation("Shipment {TrackingNumber} has changed to {Status}", shipmentStatusUpdate.TrackingNumber, shipmentStatusUpdate.Status);

            try
            {
                await _shipmentsRepository.UpdateAsync(shipmentStatusUpdate);

                logger.LogInformation("Shipment {TrackingNumber} was updated to status {Status}", shipmentStatusUpdate.TrackingNumber, shipmentStatusUpdate.Status);

                return new OkResult();
            }
            catch (ShipmentNotFoundException)
            {
                logger.LogInformation("Shipment {TrackingNumber} was not found");
                return new NotFoundResult();
            }
        }
    }
}
