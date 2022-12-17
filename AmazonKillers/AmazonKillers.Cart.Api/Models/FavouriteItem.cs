namespace AmazonKillers.Cart.Api.Models
{
    public class FavouriteItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }

        public FavouriteItem() { }
    }
}
