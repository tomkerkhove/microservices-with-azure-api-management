using System.Threading.Tasks;
using Demo.Microservices.Orders.API.Contracts.v1;
using Demo.Microservices.Orders.API.Repositories.Interfaces;
using Demo.Microservices.Orders.API.Services.Interfaces;

namespace Demo.Microservices.Orders.API.Managers
{
    public class OrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShipmentService _shipmentService;

        public OrderManager(IOrderRepository orderRepository, IShipmentService shipmentService)
        {
            _orderRepository = orderRepository;
            _shipmentService = shipmentService;
        }

        public async Task<OrderConfirmation> CreateAsync(Order createdOrder)
        {
            var confirmationId = await _orderRepository.CreateAsync(createdOrder);
            var shipmentInformation = await _shipmentService.CreateAsync(createdOrder.Customer.Address);

            var orderConfirmation = new OrderConfirmation
            {
                ConfirmationId = confirmationId,
                ShipmentInformation = shipmentInformation,
                Customer = createdOrder.Customer
            };

            return orderConfirmation;
        }
    }
}
