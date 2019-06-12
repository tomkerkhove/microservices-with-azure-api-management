namespace Demo.Microservices.Orders.API.Contracts.v1
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
