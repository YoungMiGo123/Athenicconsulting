using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthenicConsult.Migrations
{
    public partial class Office : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deactivatated",
                table: "Industries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedDate",
                table: "Industries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deactivatated",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedDate",
                table: "Campaigns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deactivatated",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivatedDate",
                table: "Brands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deactivatated",
                table: "Industries");

            migrationBuilder.DropColumn(
                name: "DeactivatedDate",
                table: "Industries");

            migrationBuilder.DropColumn(
                name: "Deactivatated",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "DeactivatedDate",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Deactivatated",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "DeactivatedDate",
                table: "Brands");
        }
    }
}
