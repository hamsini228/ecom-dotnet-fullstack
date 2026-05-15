using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bajaj.eCommerce.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddZipcodeInCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Zipcode",
                table: "Customers",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zipcode",
                table: "Customers");
        }
    }
}
