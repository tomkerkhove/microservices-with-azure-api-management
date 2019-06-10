using System.Net;
using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;
using Demo.Monolith.API.Exceptions;
using Demo.Monolith.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.Monolith.API.Controllers
{
    [Route("api/v1/shipments")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentsController(IShipmentRepository shipmentRepository)
        {
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
                return NotFound();
            }

            return Ok(shipmentInformation);
        }

        /// <summary>
        ///     Update Shipment Status
        /// </summary>
        /// <remarks>Webhook for external shipment partners to provide updates about a shipment</remarks>
        /// <response code="200">Update about a specific shipment and its status</response>
        /// <response code="400">Request was invalid</response>
        /// <response code="404">Requested product was not found</response>
        /// <response code="503">Something went wrong, please contact support</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [SwaggerOperation(OperationId = "Shipment_UpdateStatus")]
        public async Task<IActionResult> Shipment_UpdateStatus([FromBody] ShipmentStatusUpdate shipmentStatusUpdate)
        {
            try
            {
                await _shipmentRepository.UpdateAsync(shipmentStatusUpdate);
                return Ok();
            }
            catch (ShipmentNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
