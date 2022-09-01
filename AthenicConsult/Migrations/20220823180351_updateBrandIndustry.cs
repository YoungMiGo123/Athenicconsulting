using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthenicConsult.Migrations
{
    public partial class updateBrandIndustry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Brands");

            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_IndustryId",
                table: "Brands",
                column: "IndustryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Industries_IndustryId",
                table: "Brands",
                column: "IndustryId",
                principalTable: "Industries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Industries_IndustryId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_IndustryId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Brands");

            migrationBuilder.AddColumn<int>(
                name: "Industry",
                table: "Brands",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
