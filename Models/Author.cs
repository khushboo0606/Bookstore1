using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        public string ?Name { get; set; }

        // Navigation property
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
