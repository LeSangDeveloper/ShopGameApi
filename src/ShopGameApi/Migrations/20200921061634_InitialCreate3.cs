using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopGameApi.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Companies_CompanyId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Ratings_RatingId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_RatingId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameRef",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_GameRef",
                table: "Ratings",
                column: "GameRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Companies_CompanyId",
                table: "Games",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Games_GameRef",
                table: "Ratings",
                column: "GameRef",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Companies_CompanyId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Games_GameRef",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_GameRef",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "GameRef",
                table: "Ratings");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_RatingId",
                table: "Games",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Companies_CompanyId",
                table: "Games",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Ratings_RatingId",
                table: "Games",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "RatingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
