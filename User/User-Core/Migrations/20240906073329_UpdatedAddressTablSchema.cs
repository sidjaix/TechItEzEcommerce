using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAddressTablSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "IsShippingAddress",
                table: "Address",
                newName: "IsDefaultAddress");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Address",
                newName: "TownOrCity");

            migrationBuilder.RenameColumn(
                name: "Address1",
                table: "Address",
                newName: "AreaOrStreet");

            migrationBuilder.AlterColumn<string>(
                name: "UnitNumber",
                table: "Address",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Landmark",
                table: "Address",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pincode",
                table: "Address",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Landmark",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Pincode",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "TownOrCity",
                table: "Address",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "IsDefaultAddress",
                table: "Address",
                newName: "IsShippingAddress");

            migrationBuilder.RenameColumn(
                name: "AreaOrStreet",
                table: "Address",
                newName: "Address1");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "UserAddress",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "UnitNumber",
                table: "Address",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Address",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Address",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
