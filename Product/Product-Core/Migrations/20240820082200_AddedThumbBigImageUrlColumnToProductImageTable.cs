using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Core.Migrations
{
    /// <inheritdoc />
    public partial class AddedThumbBigImageUrlColumnToProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbBigImageUrl",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbBigImageUrl",
                table: "ProductImage");
        }
    }
}
