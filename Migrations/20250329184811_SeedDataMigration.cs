using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 4, "J.K. Rowling" },
                    { 5, "Mark Twain" },
                    { 6, "Jane Austen" },
                    { 7, "J.R.R. Tolkien" },
                    { 8, "Ernest Hemingway" },
                    { 9, "Charles Dickens" },
                    { 10, "Stephen King" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "A novel about the American Dream.", "/images/Book1.jpeg" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "A novel about racial injustice in the Deep South.", "/images/Book2.jpeg" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "A dystopian novel about totalitarianism.", "/images/Book3.jpeg" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Description", "Genre", "ISBN", "ImageUrl", "InStock", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[,]
                {
                    { 4, 4, "A young wizard's journey begins.", "Fantasy", "978-0590353427", "/images/Book4.jpeg", true, 9.99m, new DateTime(1997, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, "Harry Potter and the Sorcerer's Stone" },
                    { 5, 5, "The adventures of a mischievous boy.", "Adventure", "978-0486400777", "/images/Book5.jpeg", true, 7.99m, new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "The Adventures of Tom Sawyer" },
                    { 6, 6, "A story of love and social class.", "Romance", "978-1503290563", "/images/Book6.jpeg", true, 8.99m, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, "Pride and Prejudice" },
                    { 7, 7, "A hobbit's adventure with dwarves and a dragon.", "Fantasy", "978-0618968633", "/images/Book7.jpeg", true, 10.99m, new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "The Hobbit" },
                    { 8, 8, "A man's battle with a giant fish.", "Literary Fiction", "978-0684801223", "/images/Book8.jpeg", true, 6.99m, new DateTime(1952, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, "The Old Man and the Sea" },
                    { 9, 9, "A story of love and revolution during the French Revolution.", "Historical Fiction", "978-1503219705", "/images/Book9.jpeg", true, 11.99m, new DateTime(1859, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "A Tale of Two Cities" },
                    { 10, 10, "A psychological horror novel set in a haunted hotel.", "Horror", "978-0307743657", "/images/Book10.jpeg", true, 15.99m, new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "The Shining" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Sample Book 1", null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Sample Book 2", null });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Sample Book 3", null });
        }
    }
}
