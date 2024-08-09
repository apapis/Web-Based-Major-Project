using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Major_Project___API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "PricePerGram",
                table: "Products",
                newName: "PricePerUnit");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Products",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "PricePerUnit",
                table: "Products",
                newName: "PricePerGram");
        }
    }
}
