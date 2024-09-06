using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserAddressConstraintName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfiguration_Address",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductConfiguration_User",
                table: "UserAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Address",
                table: "UserAddress",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_User",
                table: "UserAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Address",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_User",
                table: "UserAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfiguration_Address",
                table: "UserAddress",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConfiguration_User",
                table: "UserAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
