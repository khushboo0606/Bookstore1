using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string ?Title { get; set; }

        public string ?Genre { get; set; }
        public string ?ISBN { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public bool InStock { get; set; }
        public int Quantity { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ?Description { get; set; } 

        // Foreign Key for Author
        public int AuthorId { get; set; }  

        // Navigation property for Author
        public Author ?Author { get; set; }

        public string ?ImageUrl { get; set; }

    }

}
