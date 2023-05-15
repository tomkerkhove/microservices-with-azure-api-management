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
            var request = new HttpRequestMessage(HttpMethod.Post, shipmentBaseUri);
            
            var rawBody = JsonConvert.SerializeObject(address);
            request.Content = new StringContent(rawBody,Encoding.UTF8, "application/json");

            var shipmentApiKey = _configuration["SHIPMENTS_API_KEY"];
            if (!string.IsNullOrWhiteSpace(shipmentApiKey))
            {
                request.Headers.Add("API-Key", shipmentApiKey);
            }

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception("Unable to initiate a shipment");
            }

            var rawResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ShipmentInformation>(rawResponse);
        }
    }
}
