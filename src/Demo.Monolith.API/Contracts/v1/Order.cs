using System.Collections.Generic;

namespace Demo.Monolith.API.Contracts.v1
{
    public class Order
    {
        public Customer Customer { get; set; }

        public List<OrderLine> Basket { get; set; } = new List<OrderLine>();
    }
}
