namespace AmazonKillers.Catalogue.Api.Models
{
    public enum CoverStyle
    {
        HardCover,
        SoftCover
    }

    public enum AvailabilityStatus
    {
        AvailableForOrder,
        AvailableForPreoder,
        Unavailable
    }

    public class Book
    {
        public int Id { get; set; }
        public string Isbm { get; set; }
        public string Name { get; set; }
        public string Annotation { get; set; }
        public byte[] CoverImage { get; set; }
        public byte[] Preview { get; set; }
        public double Price { get; set; }
        public int Pages { get; set; }
        public DateTime PublishingYear { get; set; }
        public AvailabilityStatus Availability { get; set; }
        public CoverStyle CoverStyle { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
        public int PublisherId { get; set; }

        public Book() { }

    }
}
