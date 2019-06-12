using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Demo.Monolith.API.Contracts.v1;
using Demo.Monolith.API.OpenAPI;
using Demo.Monolith.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Demo.Monolith.API.Controllers
{
    [Route("api/v1/products")]
    [ApiExplorerSettings(GroupName = OpenApiCategories.Products)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
       
        /// <summary>
        ///     Get Products
        /// </summary>
        /// <remarks>Provides information about product catalog</remarks>
        /// <response code="200">Overview of our product catalog</response>
        /// <response code="503">Something went wrong, please contact support</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        [SwaggerOperation(OperationId = "Product_GetAll")]
        public async Task<ActionResult> Get()
        {
            var products = await _productRepository.GetAsync();
            return Ok(products);
        }

        /// <summary>
        ///     Get Product
        /// </summary>
        /// <remarks>Provides information about a specific product in our catalog</remarks>
        /// <response code="200">Information about a specific product</response>
        /// <response code="400">Request was invalid</response>
        /// <response code="404">Requested product was not found</response>
        /// <response code="503">Something went wrong, please contact support</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [SwaggerOperation(OperationId = "Product_Get")]
        public async Task<ActionResult> Get(int id)
        {
            var foundProduct = await _productRepository.GetAsync(id);
            if (foundProduct == null)
            {
                return NotFound();
            }

            return Ok(foundProduct);
        }
    }
}