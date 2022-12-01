using AmazonKillers.News.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonKillers.News.Api
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        { }
        public DbSet<Models.News> News { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.News>()
                .ToTable("News");

            modelBuilder.Entity<Publisher>()
                .ToTable("Publishers");

            modelBuilder.Entity<Subscription>()
                .ToTable("Subscriptions");

            modelBuilder.Entity<Models.News>()
                .HasOne(n => n.Publisher)
                .WithMany(p => p.News)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}