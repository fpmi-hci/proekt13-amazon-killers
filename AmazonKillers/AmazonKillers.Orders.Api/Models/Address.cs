namespace AmazonKillers.Orders.Api.Models
{
    public class Address : AddressBase
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public Address() { }
    }
}