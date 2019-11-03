using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBooks.Dal.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoodreadsId",
                table: "Editions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAcquisition",
                table: "BookOwnership",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GoodreadsId",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodreadsId",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "DateOfAcquisition",
                table: "BookOwnership");

            migrationBuilder.DropColumn(
                name: "GoodreadsId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Authors");
        }
    }
}
