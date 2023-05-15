using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Demo.Microservices.Orders.API.Contracts.v1;
using Demo.Microservices.Orders.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Demo.Microservices.Orders.API.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;

        public ShipmentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ShipmentInformation> CreateAsync(Address address)
        {
            var shipmentBaseUri = _configuration["SHIPMENTS_API_URI"];

            var rawBody = JsonConvert.SerializeObject(address);
            var postBody = new StringContent(rawBody,Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(shipmentBaseUri, postBody);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception("Unable to initiate a shipment");
            }

            var rawResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ShipmentInformation>(rawResponse);
        }
    }
}
