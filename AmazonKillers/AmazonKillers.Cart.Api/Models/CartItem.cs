namespace AmazonKillers.Cart.Api.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }

        public CartItem() { }
    }
}
