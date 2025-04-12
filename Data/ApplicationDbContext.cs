using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Bookstore.Models;

namespace Bookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // 

            // Seeding Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "F. Scott Fitzgerald" },
            new Author { AuthorId = 2, Name = "Harper Lee" },
            new Author { AuthorId = 3, Name = "George Orwell" },
            new Author { AuthorId = 4, Name = "J.K. Rowling" },
            new Author { AuthorId = 5, Name = "Jane Austen" },
            new Author { AuthorId = 6, Name = "Mark Twain" },
            new Author { AuthorId = 7, Name = "William Shakespeare" },
            new Author { AuthorId = 8, Name = "Ernest Hemingway" },
            new Author { AuthorId = 9, Name = "Agatha Christie" },
            new Author { AuthorId = 10, Name = "Leo Tolstoy" }
            );

            // Seeding Books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "The Great Gatsby", AuthorId = 1, ISBN = "978-0743273565", Price = 12.99m, Description = "A novel set in the Jazz Age.", PublicationDate = new DateTime(1925, 4, 10), InStock = true, Quantity = 25, Genre = "Classic Fiction", ImageUrl = "/images/Book1.jpeg" },
        new Book { BookId = 2, Title = "To Kill a Mockingbird", AuthorId = 2, ISBN = "978-0061120084", Price = 14.99m, Description = "A story about racial injustice in the 1930s.", PublicationDate = new DateTime(1960, 7, 11), InStock = true, Quantity = 18, Genre = "Fiction", ImageUrl = "/images/Book2.jpeg" },
        new Book { BookId = 3, Title = "1984", AuthorId = 3, ISBN = "978-0451524935", Price = 11.99m, Description = "A dystopian novel about a totalitarian regime.", PublicationDate = new DateTime(1949, 6, 8), InStock = true, Quantity = 15, Genre = "Dystopian Fiction", ImageUrl = "/images/Book3.jpeg" },
        new Book { BookId = 4, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 4, ISBN = "978-0590353427", Price = 9.99m, Description = "A young wizard's journey begins at Hogwarts.", PublicationDate = new DateTime(1997, 6, 26), InStock = true, Quantity = 30, Genre = "Fantasy", ImageUrl = "/images/Book4.jpeg" },
        new Book { BookId = 5, Title = "Pride and Prejudice", AuthorId = 5, ISBN = "978-0141040349", Price = 8.99m, Description = "A romantic novel exploring manners and marriage.", PublicationDate = new DateTime(1813, 1, 28), InStock = true, Quantity = 10, Genre = "Classic Fiction", ImageUrl = "/images/Book5.jpeg" },
        new Book { BookId = 6, Title = "The Adventures of Huckleberry Finn", AuthorId = 6, ISBN = "978-0486280615", Price = 7.99m, Description = "The journey of Huck Finn along the Mississippi River.", PublicationDate = new DateTime(1884, 12, 10), InStock = true, Quantity = 20, Genre = "Adventure", ImageUrl = "/images/Book6.jpeg" },
        new Book { BookId = 7, Title = "Macbeth", AuthorId = 7, ISBN = "978-0743477109", Price = 5.99m, Description = "A tragedy about ambition and guilt.", PublicationDate = new DateTime(1606, 1, 1), InStock = true, Quantity = 12, Genre = "Drama", ImageUrl = "/images/Book7.jpeg" },
        new Book { BookId = 8, Title = "The Old Man and the Sea", AuthorId = 8, ISBN = "978-0684830490", Price = 6.99m, Description = "A story of a fisherman battling a giant marlin.", PublicationDate = new DateTime(1952, 9, 1), InStock = true, Quantity = 22, Genre = "Literature", ImageUrl = "/images/Book8.jpeg" },
        new Book { BookId = 9, Title = "Murder on the Orient Express", AuthorId = 9, ISBN = "978-0062693662", Price = 10.99m, Description = "A famous detective solves a murder on a train.", PublicationDate = new DateTime(1934, 1, 1), InStock = true, Quantity = 17, Genre = "Mystery", ImageUrl = "/images/Book9.jpeg" },
        new Book { BookId = 10, Title = "War and Peace", AuthorId = 10, ISBN = "978-1400079988", Price = 15.99m, Description = "A sweeping epic about the Napoleonic Wars.", PublicationDate = new DateTime(1869, 1, 1), InStock = true, Quantity = 8, Genre = "Historical Fiction", ImageUrl = "/images/Book10.jpeg" }
            );
        }
    }
}
