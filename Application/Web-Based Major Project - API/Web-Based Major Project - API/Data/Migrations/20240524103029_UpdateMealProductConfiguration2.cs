using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Major_Project___API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMealProductConfiguration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens");

            migrationBuilder.AlterColumn<int>(
                name: "MealAllergensId",
                table: "Allergens",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens",
                column: "MealAllergensId",
                principalTable: "MealAllergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens");

            migrationBuilder.AlterColumn<int>(
                name: "MealAllergensId",
                table: "Allergens",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens",
                column: "MealAllergensId",
                principalTable: "MealAllergens",
                principalColumn: "Id");
        }
    }
}
