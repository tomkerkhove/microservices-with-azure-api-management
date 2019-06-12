using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.Microservices.Shipments.API.Controllers
{
    [Route("api/v1/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        ///     Get Health
        /// </summary>
        /// <remarks>Provides an indication about the health of the scraper</remarks>
        /// <response code="200">API is healthy</response>
        /// <response code="503">API is not healthy</response>
        [HttpGet]
        [SwaggerOperation(OperationId = "Health_Get")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
