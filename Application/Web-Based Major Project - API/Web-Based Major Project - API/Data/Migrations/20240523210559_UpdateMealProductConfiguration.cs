using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Based_Major_Project___API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMealProductConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAllergens_Allergens_AllergensId",
                table: "MealAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_MealAllergens_Meals_MealId",
                table: "MealAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_MealCosts_Meals_MealId",
                table: "MealCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_MealProducts_Meals_MealId",
                table: "MealProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealAllergens",
                table: "MealAllergens");

            migrationBuilder.DropIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens");

            migrationBuilder.DropColumn(
                name: "CostOfAllIngredients",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CostOfMakeIt",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "AllergensId",
                table: "MealAllergens");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "MealProducts",
                newName: "MealIngredientsId");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "MealCosts",
                newName: "MealPricingId");

            migrationBuilder.RenameIndex(
                name: "IX_MealCosts_MealId",
                table: "MealCosts",
                newName: "IX_MealCosts_MealPricingId");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "MealAllergens",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "MealAllergensId",
                table: "Allergens",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealAllergens",
                table: "MealAllergens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MealIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealIngredients_Meals_Id",
                        column: x => x.Id,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPricings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ProposedPrice = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CostOfAllIngredients = table.Column<float>(type: "real", nullable: false),
                    CostOfMakeIt = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealPricings_Meals_Id",
                        column: x => x.Id,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergens_MealAllergensId",
                table: "Allergens",
                column: "MealAllergensId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens",
                column: "MealAllergensId",
                principalTable: "MealAllergens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAllergens_Meals_Id",
                table: "MealAllergens",
                column: "Id",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealCosts_MealPricings_MealPricingId",
                table: "MealCosts",
                column: "MealPricingId",
                principalTable: "MealPricings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealProducts_MealIngredients_MealIngredientsId",
                table: "MealProducts",
                column: "MealIngredientsId",
                principalTable: "MealIngredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_MealAllergens_MealAllergensId",
                table: "Allergens");

            migrationBuilder.DropForeignKey(
                name: "FK_MealAllergens_Meals_Id",
                table: "MealAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_MealCosts_MealPricings_MealPricingId",
                table: "MealCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_MealProducts_MealIngredients_MealIngredientsId",
                table: "MealProducts");

            migrationBuilder.DropTable(
                name: "MealIngredients");

            migrationBuilder.DropTable(
                name: "MealPricings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealAllergens",
                table: "MealAllergens");

            migrationBuilder.DropIndex(
                name: "IX_Allergens_MealAllergensId",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "MealAllergensId",
                table: "Allergens");

            migrationBuilder.RenameColumn(
                name: "MealIngredientsId",
                table: "MealProducts",
                newName: "MealId");

            migrationBuilder.RenameColumn(
                name: "MealPricingId",
                table: "MealCosts",
                newName: "MealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealCosts_MealPricingId",
                table: "MealCosts",
                newName: "IX_MealCosts_MealId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MealAllergens",
                newName: "MealId");

            migrationBuilder.AddColumn<float>(
                name: "CostOfAllIngredients",
                table: "Meals",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CostOfMakeIt",
                table: "Meals",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Meals",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "AllergensId",
                table: "MealAllergens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealAllergens",
                table: "MealAllergens",
                columns: new[] { "AllergensId", "MealId" });

            migrationBuilder.CreateIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAllergens_Allergens_AllergensId",
                table: "MealAllergens",
                column: "AllergensId",
                principalTable: "Allergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealAllergens_Meals_MealId",
                table: "MealAllergens",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealCosts_Meals_MealId",
                table: "MealCosts",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealProducts_Meals_MealId",
                table: "MealProducts",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
