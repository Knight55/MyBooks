﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBooks.Bll.Migrations
{
    public partial class Removed_BookAuthor_DbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Authors_AuthorId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Books_BookId",
                table: "BookAuthors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors");

            migrationBuilder.RenameTable(
                name: "BookAuthors",
                newName: "BookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                table: "BookAuthor",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                table: "BookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "BookAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthors",
                newName: "IX_BookAuthors_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BookAuthors",
                newName: "IX_BookAuthors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Authors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Books_BookId",
                table: "BookAuthors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
