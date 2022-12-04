using System.Text.Json.Serialization;

namespace AmazonKillers.News.Api.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public DateTime PublishingDate { get; set; }
        public Publisher Publisher { get; set; }
        [JsonConverter(typeof(JsonToByteArrayConverter))]
        public byte[] CoverImage { get; set; }

        public News() { }
    }
}
