namespace AmazonKillers.News.Api.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string SubscriberId { get; set; }
        public int PublisherId { get; set; }

        public Subscription() { }
    }
}
