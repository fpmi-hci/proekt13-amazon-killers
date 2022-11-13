namespace AmazonKillers.Catalogue.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public Category() { }
    }
}
