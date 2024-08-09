using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Major_Project___API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMealProductConfiguration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens");

            migrationBuilder.DropIndex(
                name: "IX_Allergens_MealAllergensId",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "MealAllergensId",
                table: "Allergens");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "MealAllergens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MealAllergenMap",
                columns: table => new
                {
                    AllergensId = table.Column<int>(type: "integer", nullable: false),
                    MealAllergensId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealAllergenMap", x => new { x.AllergensId, x.MealAllergensId });
                    table.ForeignKey(
                        name: "FK_MealAllergenMap_Allergens_AllergensId",
                        column: x => x.AllergensId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealAllergenMap_MealAllergens_MealAllergensId",
                        column: x => x.MealAllergensId,
                        principalTable: "MealAllergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealAllergenMap_MealAllergensId",
                table: "MealAllergenMap",
                column: "MealAllergensId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAllergens_Meals_MealId",
                table: "MealAllergens",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAllergens_Meals_MealId",
                table: "MealAllergens");

            migrationBuilder.DropTable(
                name: "MealAllergenMap");

            migrationBuilder.DropIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "MealAllergens");

            migrationBuilder.AddColumn<int>(
                name: "MealAllergensId",
                table: "Allergens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Allergens_MealAllergensId",
                table: "Allergens",
                column: "MealAllergensId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens",
                column: "MealAllergensId",
                principalTable: "MealAllergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
