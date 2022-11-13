using AmazonKillers.Catalogue.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonKillers.Catalogue.Api
{
    public class CatalogueContext : DbContext
    {
        public CatalogueContext(DbContextOptions<CatalogueContext> options) : base(options)
        { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .ToTable("Books");

            modelBuilder.Entity<Book>()
                .Property(b => b.CoverStyle)
                .HasConversion<string>();

            modelBuilder.Entity<Book>()
                .Property(b => b.Availability)
                .HasConversion<string>();

            modelBuilder.Entity<Author>()
                .ToTable("Authors");

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books)
                .UsingEntity(j => j.ToTable("BookCategories"));

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(j => j.ToTable("BookAuthors"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
