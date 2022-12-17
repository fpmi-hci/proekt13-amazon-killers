using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonKillers.News.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImage",
                table: "News",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "News");
        }
    }
}
