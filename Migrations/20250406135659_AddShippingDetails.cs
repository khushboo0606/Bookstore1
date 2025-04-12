using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCity",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCountry",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCounty",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingName",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingPostalCode",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5,
                column: "Name",
                value: "Jane Austen");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 6,
                column: "Name",
                value: "Mark Twain");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 7,
                column: "Name",
                value: "William Shakespeare");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 9,
                column: "Name",
                value: "Agatha Christie");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 10,
                column: "Name",
                value: "Leo Tolstoy");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "Description",
                value: "A novel set in the Jazz Age.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Description", "Genre" },
                values: new object[] { "A story about racial injustice in the 1930s.", "Fiction" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "Description",
                value: "A dystopian novel about a totalitarian regime.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                columns: new[] { "Description", "PublicationDate" },
                values: new object[] { "A young wizard's journey begins at Hogwarts.", new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A romantic novel exploring manners and marriage.", "Classic Fiction", "978-0141040349", 8.99m, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Pride and Prejudice" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "The journey of Huck Finn along the Mississippi River.", "Adventure", "978-0486280615", 7.99m, new DateTime(1884, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "The Adventures of Huckleberry Finn" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A tragedy about ambition and guilt.", "Drama", "978-0743477109", 5.99m, new DateTime(1606, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Macbeth" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8,
                columns: new[] { "Description", "Genre", "ISBN" },
                values: new object[] { "A story of a fisherman battling a giant marlin.", "Literature", "978-0684830490" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A famous detective solves a murder on a train.", "Mystery", "978-0062693662", 10.99m, new DateTime(1934, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Murder on the Orient Express" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10,
                columns: new[] { "Description", "Genre", "ISBN", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A sweeping epic about the Napoleonic Wars.", "Historical Fiction", "978-1400079988", new DateTime(1869, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "War and Peace" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCountry",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCounty",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingPostalCode",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 5,
                column: "Name",
                value: "Mark Twain");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 6,
                column: "Name",
                value: "Jane Austen");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 7,
                column: "Name",
                value: "J.R.R. Tolkien");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 9,
                column: "Name",
                value: "Charles Dickens");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 10,
                column: "Name",
                value: "Stephen King");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                column: "Description",
                value: "A novel about the American Dream.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "Description", "Genre" },
                values: new object[] { "A novel about racial injustice in the Deep South.", "Classic Fiction" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3,
                column: "Description",
                value: "A dystopian novel about totalitarianism.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4,
                columns: new[] { "Description", "PublicationDate" },
                values: new object[] { "A young wizard's journey begins.", new DateTime(1997, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "The adventures of a mischievous boy.", "Adventure", "978-0486400777", 7.99m, new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "The Adventures of Tom Sawyer" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A story of love and social class.", "Romance", "978-1503290563", 8.99m, new DateTime(1813, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, "Pride and Prejudice" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A hobbit's adventure with dwarves and a dragon.", "Fantasy", "978-0618968633", 10.99m, new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, "The Hobbit" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8,
                columns: new[] { "Description", "Genre", "ISBN" },
                values: new object[] { "A man's battle with a giant fish.", "Literary Fiction", "978-0684801223" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9,
                columns: new[] { "Description", "Genre", "ISBN", "Price", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A story of love and revolution during the French Revolution.", "Historical Fiction", "978-1503219705", 11.99m, new DateTime(1859, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "A Tale of Two Cities" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10,
                columns: new[] { "Description", "Genre", "ISBN", "PublicationDate", "Quantity", "Title" },
                values: new object[] { "A psychological horror novel set in a haunted hotel.", "Horror", "978-0307743657", new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "The Shining" });
        }
    }
}
