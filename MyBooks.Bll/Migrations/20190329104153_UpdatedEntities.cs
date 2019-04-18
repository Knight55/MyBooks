using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBooks.Bll.Migrations
{
    public partial class UpdatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoodreadsId",
                table: "BOOK",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodreadsId",
                table: "BOOK");
        }
    }
}
