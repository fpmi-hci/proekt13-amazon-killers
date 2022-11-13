namespace AmazonKillers.Catalogue.Api.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public ICollection<Book> Books { get; set; }

        public Author() { }
    }
}
