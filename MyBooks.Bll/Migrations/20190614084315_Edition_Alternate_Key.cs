using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBooks.Bll.Migrations
{
    public partial class Edition_Alternate_Key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsbnNumber",
                table: "Editions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Editions_IsbnNumber",
                table: "Editions",
                column: "IsbnNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Editions_IsbnNumber",
                table: "Editions");

            migrationBuilder.AlterColumn<string>(
                name: "IsbnNumber",
                table: "Editions",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
