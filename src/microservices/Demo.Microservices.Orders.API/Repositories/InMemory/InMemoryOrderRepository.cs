using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Microservices.Orders.API.Contracts.v1;
using Demo.Microservices.Orders.API.Repositories.Interfaces;

namespace Demo.Microservices.Orders.API.Repositories.InMemory
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<string, Order> _orders = new Dictionary<string, Order>();

        public Task<string> CreateAsync(Order createdOrder)
        {
            var confirmationId = Guid.NewGuid().ToString();

            _orders.Add(confirmationId, createdOrder);

            return Task.FromResult(confirmationId);
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