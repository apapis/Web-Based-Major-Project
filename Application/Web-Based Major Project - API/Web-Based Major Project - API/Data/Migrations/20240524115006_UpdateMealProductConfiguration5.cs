using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web_Based_Major_Project___API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMealProductConfiguration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAllergens_Meals_Id",
                table: "MealAllergens");

            migrationBuilder.DropIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MealAllergens",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens",
                column: "MealId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MealAllergens",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_MealAllergens_MealId",
                table: "MealAllergens",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAllergens_Meals_Id",
                table: "MealAllergens",
                column: "Id",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
