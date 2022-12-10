namespace AmazonKillers.Orders.Api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Order Order  { get; set; }

        public OrderItem() { }
    }
}
