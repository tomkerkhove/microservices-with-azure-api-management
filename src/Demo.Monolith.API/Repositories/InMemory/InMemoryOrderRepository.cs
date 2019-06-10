using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Monolith.API.Contracts.v1;
using Demo.Monolith.API.Repositories.Interfaces;

namespace Demo.Monolith.API.Repositories.InMemory
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<string, Order> _orders = new Dictionary<string, Order>();

        public Task<OrderConfirmation> CreateAsync(Order createdOrder)
        {
            var confirmationId = Guid.NewGuid().ToString();

            _orders.Add(confirmationId, createdOrder);

            var orderConfirmation= new OrderConfirmation
            {
                ConfirmationId = confirmationId,
                Customer = createdOrder.Customer
            };

            return Task.FromResult(orderConfirmation);
        }

        public Task<Order> GetAsync(string confirmationId)
        {
            if(_orders.TryGetValue(confirmationId, out Order createdOrder))
            {
                return Task.FromResult(createdOrder);
            }

            return Task.FromResult<Order>(null);
        }
    }
}