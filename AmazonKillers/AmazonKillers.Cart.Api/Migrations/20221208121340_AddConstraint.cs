using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonKillers.Cart.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FavouriteItems_UserId_BookId",
                table: "FavouriteItems",
                columns: new[] { "UserId", "BookId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId_BookId",
                table: "CartItems",
                columns: new[] { "UserId", "BookId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavouriteItems_UserId_BookId",
                table: "FavouriteItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserId_BookId",
                table: "CartItems");
        }
    }
}
