using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBooks.Bll.Migrations
{
    public partial class SeedWithHasData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOOKAUTHOR_AUTHOR_AuthorId",
                table: "BOOKAUTHOR");

            migrationBuilder.DropForeignKey(
                name: "FK_BOOKAUTHOR_BOOK_BookId",
                table: "BOOKAUTHOR");

            migrationBuilder.DropForeignKey(
                name: "FK_BOOKOWNERSHIP_BOOK_BookId",
                table: "BOOKOWNERSHIP");

            migrationBuilder.DropForeignKey(
                name: "FK_BOOKOWNERSHIP_AspNetUsers_UserId1",
                table: "BOOKOWNERSHIP");

            migrationBuilder.DropForeignKey(
                name: "FK_EDITION_BOOK_BookId",
                table: "EDITION");

            migrationBuilder.DropForeignKey(
                name: "FK_EDITION_PUBLISHER_PublisherId",
                table: "EDITION");

            migrationBuilder.DropForeignKey(
                name: "FK_RATING_BOOK_BookId",
                table: "RATING");

            migrationBuilder.DropForeignKey(
                name: "FK_RATING_AspNetUsers_UserId1",
                table: "RATING");

            migrationBuilder.DropForeignKey(
                name: "FK_READINGSTATUS_EDITION_EditionId",
                table: "READINGSTATUS");

            migrationBuilder.DropForeignKey(
                name: "FK_READINGSTATUS_AspNetUsers_UserId1",
                table: "READINGSTATUS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BOOKOWNERSHIP",
                table: "BOOKOWNERSHIP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BOOKAUTHOR",
                table: "BOOKAUTHOR");

            migrationBuilder.DropPrimaryKey(
                name: "PK_READINGSTATUS",
                table: "READINGSTATUS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RATING",
                table: "RATING");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PUBLISHER",
                table: "PUBLISHER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EDITION",
                table: "EDITION");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BOOK",
                table: "BOOK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AUTHOR",
                table: "AUTHOR");

            migrationBuilder.RenameTable(
                name: "BOOKOWNERSHIP",
                newName: "BookOwnership");

            migrationBuilder.RenameTable(
                name: "BOOKAUTHOR",
                newName: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "READINGSTATUS",
                newName: "ReadingStatuses");

            migrationBuilder.RenameTable(
                name: "RATING",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "PUBLISHER",
                newName: "Publishers");

            migrationBuilder.RenameTable(
                name: "EDITION",
                newName: "Editions");

            migrationBuilder.RenameTable(
                name: "BOOK",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "AUTHOR",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_BOOKOWNERSHIP_UserId1",
                table: "BookOwnership",
                newName: "IX_BookOwnership_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_BOOKOWNERSHIP_BookId",
                table: "BookOwnership",
                newName: "IX_BookOwnership_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BOOKAUTHOR_BookId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BOOKAUTHOR_AuthorId",
                table: "BookAuthor",
                newName: "IX_BookAuthor_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_READINGSTATUS_UserId1",
                table: "ReadingStatuses",
                newName: "IX_ReadingStatuses_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_READINGSTATUS_EditionId",
                table: "ReadingStatuses",
                newName: "IX_ReadingStatuses_EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_RATING_UserId1",
                table: "Ratings",
                newName: "IX_Ratings_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_RATING_BookId",
                table: "Ratings",
                newName: "IX_Ratings_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_EDITION_PublisherId",
                table: "Editions",
                newName: "IX_Editions_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_EDITION_BookId",
                table: "Editions",
                newName: "IX_Editions_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookOwnership",
                table: "BookOwnership",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingStatuses",
                table: "ReadingStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Editions",
                table: "Editions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1975, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brandon Sanderson" },
                    { 2, new DateTime(1977, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brent Weeks" },
                    { 3, new DateTime(1948, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Jordan" },
                    { 4, new DateTime(1988, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pierce Brown" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CoverImagePath", "Genre", "GoodreadsId", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, "wayofkings.jpg", "Fantasy", "7235533-the-way-of-kings", null, "The Way of Kings" },
                    { 2, "blackprism.jpg", "Fantasy", "7165300-the-black-prism", null, "The Black Prism" },
                    { 3, "theeyeoftheworld.jpg", "Fantasy", "228665.The_Eye_of_the_World", null, "The Eye of the World" },
                    { 4, "wayofkings.jpg", "Scifi", "7235533-the-way-of-kings", null, "Red Rising" },
                    { 5, "brokeneye.jpg", "Fantasy", "12652457-the-broken-eye", null, "The Broken Eye" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tor Books" },
                    { 2, "Tom Doherty" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "Id", "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "Editions",
                columns: new[] { "Id", "BookId", "DateOfPublish", "IsbnNumber", "NumberOfPages", "PublisherId" },
                values: new object[] { 1, 1, new DateTime(2010, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "0765326353", 1007, 1 });

            migrationBuilder.InsertData(
                table: "Editions",
                columns: new[] { "Id", "BookId", "DateOfPublish", "Format", "IsbnNumber", "NumberOfPages", "PublisherId" },
                values: new object[] { 2, 1, new DateTime(2011, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paperback", "0765365278", 1258, 2 });

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

            migrationBuilder.AddForeignKey(
                name: "FK_BookOwnership_Books_BookId",
                table: "BookOwnership",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOwnership_AspNetUsers_UserId1",
                table: "BookOwnership",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Books_BookId",
                table: "Editions",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Editions_Publishers_PublisherId",
                table: "Editions",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId1",
                table: "Ratings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingStatuses_Editions_EditionId",
                table: "ReadingStatuses",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingStatuses_AspNetUsers_UserId1",
                table: "ReadingStatuses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Authors_AuthorId",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_Books_BookId",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOwnership_Books_BookId",
                table: "BookOwnership");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOwnership_AspNetUsers_UserId1",
                table: "BookOwnership");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Books_BookId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Editions_Publishers_PublisherId",
                table: "Editions");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId1",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingStatuses_Editions_EditionId",
                table: "ReadingStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingStatuses_AspNetUsers_UserId1",
                table: "ReadingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookOwnership",
                table: "BookOwnership");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingStatuses",
                table: "ReadingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Editions",
                table: "Editions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "BookOwnership",
                newName: "BOOKOWNERSHIP");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "BOOKAUTHOR");

            migrationBuilder.RenameTable(
                name: "ReadingStatuses",
                newName: "READINGSTATUS");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "RATING");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "PUBLISHER");

            migrationBuilder.RenameTable(
                name: "Editions",
                newName: "EDITION");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "BOOK");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "AUTHOR");

            migrationBuilder.RenameIndex(
                name: "IX_BookOwnership_UserId1",
                table: "BOOKOWNERSHIP",
                newName: "IX_BOOKOWNERSHIP_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_BookOwnership_BookId",
                table: "BOOKOWNERSHIP",
                newName: "IX_BOOKOWNERSHIP_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_BookId",
                table: "BOOKAUTHOR",
                newName: "IX_BOOKAUTHOR_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_AuthorId",
                table: "BOOKAUTHOR",
                newName: "IX_BOOKAUTHOR_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingStatuses_UserId1",
                table: "READINGSTATUS",
                newName: "IX_READINGSTATUS_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingStatuses_EditionId",
                table: "READINGSTATUS",
                newName: "IX_READINGSTATUS_EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_UserId1",
                table: "RATING",
                newName: "IX_RATING_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_BookId",
                table: "RATING",
                newName: "IX_RATING_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Editions_PublisherId",
                table: "EDITION",
                newName: "IX_EDITION_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Editions_BookId",
                table: "EDITION",
                newName: "IX_EDITION_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BOOKOWNERSHIP",
                table: "BOOKOWNERSHIP",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BOOKAUTHOR",
                table: "BOOKAUTHOR",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_READINGSTATUS",
                table: "READINGSTATUS",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RATING",
                table: "RATING",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PUBLISHER",
                table: "PUBLISHER",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EDITION",
                table: "EDITION",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BOOK",
                table: "BOOK",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AUTHOR",
                table: "AUTHOR",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BOOKAUTHOR_AUTHOR_AuthorId",
                table: "BOOKAUTHOR",
                column: "AuthorId",
                principalTable: "AUTHOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BOOKAUTHOR_BOOK_BookId",
                table: "BOOKAUTHOR",
                column: "BookId",
                principalTable: "BOOK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BOOKOWNERSHIP_BOOK_BookId",
                table: "BOOKOWNERSHIP",
                column: "BookId",
                principalTable: "BOOK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BOOKOWNERSHIP_AspNetUsers_UserId1",
                table: "BOOKOWNERSHIP",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDITION_BOOK_BookId",
                table: "EDITION",
                column: "BookId",
                principalTable: "BOOK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EDITION_PUBLISHER_PublisherId",
                table: "EDITION",
                column: "PublisherId",
                principalTable: "PUBLISHER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RATING_BOOK_BookId",
                table: "RATING",
                column: "BookId",
                principalTable: "BOOK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RATING_AspNetUsers_UserId1",
                table: "RATING",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_READINGSTATUS_EDITION_EditionId",
                table: "READINGSTATUS",
                column: "EditionId",
                principalTable: "EDITION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_READINGSTATUS_AspNetUsers_UserId1",
                table: "READINGSTATUS",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
