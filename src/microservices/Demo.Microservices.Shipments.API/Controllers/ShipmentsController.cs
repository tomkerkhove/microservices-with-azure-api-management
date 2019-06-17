using System.Net;
using System.Threading.Tasks;
using Demo.Microservices.Shipments.API.Contracts.v1;
using Demo.Microservices.Shipments.API.Exceptions;
using Demo.Microservices.Shipments.API.OpenAPI;
using Demo.Microservices.Shipments.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.Microservices.Shipments.API.Controllers
{
    [Route("api/v1/shipments")]
    [ApiController]
    [ApiExplorerSettings(GroupName = OpenApiCategories.Shipments)]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly ILogger _logger;

        public ShipmentsController(IShipmentRepository shipmentRepository, ILogger<ShipmentsController> logger)
        {
            _logger = logger;
            _shipmentRepository = shipmentRepository;
        }

        /// <summary>
        ///     Get Shipment Information
        /// </summary>
        /// <remarks>Provides information about a shipment</remarks>
        /// <response code="200">Information about a specific shipment</response>
        /// <response code="400">Request was invalid</response>
        /// <response code="404">Requested product was not found</response>
        /// <response code="503">Something went wrong, please contact support</response>
        [HttpGet("{trackingNumber}")]
        [ProducesResponseType(typeof(ShipmentInformation), (int)HttpStatusCode.OK)]
        [SwaggerOperation(OperationId = "Shipment_Get")]
        public async Task<IActionResult> Get(string trackingNumber)
        {
            var shipmentInformation = await _shipmentRepository.GetAsync(trackingNumber);
            if (shipmentInformation == null)
            {
                _logger.LogInformation("No information found for shipment {TrackingNumber} ", trackingNumber);

                return NotFound();
            }

            _logger.LogInformation("Information found for shipment {TrackingNumber} ", trackingNumber);

            return Ok(shipmentInformation);
        }

        /// <summary>
        ///     Create New Shipment
        /// </summary>
        /// <remarks>Creates a new request to ship to an address</remarks>
        /// <response code="201">Shipment was initiated</response>
        /// <response code="400">Request was invalid</response>
        /// <response code="404">Requested product was not found</response>
        /// <response code="503">Something went wrong, please contact support</response>
        [HttpPost]
        [SwaggerOperation(OperationId = "Shipment_Create")]
        [ProducesResponseType(typeof(ShipmentInformation), (int)HttpStatusCode.Created)]
        [ApiExplorerSettings(GroupName = OpenApiCategories.ShipmentManagement)]
        public async Task<IActionResult> Create([FromBody] Address address)
        {
            var shipmentInformation = await _shipmentRepository.CreateAsync(address);

            _logger.LogInformation("Shipment {TrackingNumber} was created to deliver at {DeliverAddress}", shipmentInformation.TrackingNumber, shipmentInformation.DeliveryAddress);

            return CreatedAtAction(nameof(Get), new { trackingNumber = shipmentInformation.TrackingNumber }, shipmentInformation);
        }

        /// <summary>
        ///     Update Shipment Status
        /// </summary>
        /// <remarks>Webhook for external shipment partners to provide updates about a shipment</remarks>
        /// <response code="200">Update about a specific shipment and its status</response>
        /// <response code="400">Request was invalid</response>
        /// <response code="404">Requested product was not found</response>
        /// <response code="503">Something went wrong, please contact support</response>
        [HttpPost("webhook")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(OperationId = "Shipment_UpdateStatus")]
        [ApiExplorerSettings(GroupName = OpenApiCategories.ShipmentWebhook)]
        public async Task<IActionResult> Shipment_UpdateStatus([FromBody] ShipmentStatusUpdate shipmentStatusUpdate)
        {
            try
            {
                await _shipmentRepository.UpdateAsync(shipmentStatusUpdate);

                _logger.LogInformation("Shipment {TrackingNumber} was updated to status", shipmentStatusUpdate.TrackingNumber, shipmentStatusUpdate.Status);

                return Ok();
            }
            catch (ShipmentNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
