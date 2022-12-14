using AmazonKillers.Cart.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonKillers.Cart.Api
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        { }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<FavouriteItem> FavouriteItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .ToTable("CartItems");

            modelBuilder.Entity<FavouriteItem>()
                .ToTable("FavouriteItems");

            modelBuilder.Entity<CartItem>()
                .HasIndex(c => new { c.UserId, c.BookId })
                .IsUnique(true);

            modelBuilder.Entity<FavouriteItem>()
                .HasIndex(f => new { f.UserId, f.BookId })
                .IsUnique(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}