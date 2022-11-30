namespace AmazonKillers.Catalogue.Api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }

        public Comment() { }
    }
}
