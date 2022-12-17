namespace AmazonKillers.Orders.Api.Models
{
    public abstract class AddressBase
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int Building { get; set; }
        public int Flat { get; set; }
        public string PostCode { get; set; }
    }
}