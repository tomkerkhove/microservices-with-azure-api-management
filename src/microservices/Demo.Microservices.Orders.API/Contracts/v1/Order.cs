using System.Collections.Generic;

namespace Demo.Microservices.Orders.API.Contracts.v1
{
    public class Order
    {
        public Customer Customer { get; set; }

        public List<OrderLine> Basket { get; set; } = new List<OrderLine>();
    }
}
